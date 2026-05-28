using Backend.Modules.Patients.Domain;
using Backend.Modules.Queue.Domain;
using Backend.Modules.Queue.Domain.Repositories;
using Backend.Modules.ServiceOrders.Domain;
using Backend.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Queue.Infrastructure.Repositories;

public class EfQueueRepository(AppDbContext context) : IQueueRepository
{
    public async Task<IReadOnlyList<QueueItem>> GetQueueAsync()
    {
        var rows = await FetchOrderedWaitingAsync();
        var now = DateTime.UtcNow;
        return rows.Select((r, index) => ToQueueItem(r, index + 1, now)).ToList();
    }

    public async Task<QueueItem?> GetPositionAsync(Guid serviceOrderId)
    {
        var rows = await FetchOrderedWaitingAsync();
        var index = rows.FindIndex(r => r.Id == serviceOrderId);
        if (index == -1)
            return null;

        return ToQueueItem(rows[index], index + 1, DateTime.UtcNow);
    }

    private static QueueItem ToQueueItem(WaitingRow row, int position, DateTime now) =>
        new(
            row.Id,
            row.PatientId,
            row.PatientName,
            row.Priority,
            position,
            row.CreatedAt,
            now - row.CreatedAt,
            row.TubeTypes.Distinct().ToList());

    private async Task<List<WaitingRow>> FetchOrderedWaitingAsync()
    {
        var rows = await context.ServiceOrders
            .Where(so => so.Status == ServiceOrderStatus.Waiting)
            .OrderBy(so => so.Priority == Priority.Urgent ? 0
                         : so.Priority == Priority.Preferred ? 1
                         : 2)
            .ThenBy(so => so.CreatedAt)
            .Select(so => new RawRow(
                so.Id,
                so.PatientId,
                so.Priority,
                so.CreatedAt,
                so.Items.Select(i => i.TubeType).ToList()))
            .ToListAsync();

        var names = await GetPatientNamesAsync(rows.Select(r => r.PatientId));

        return rows
            .Select(r => new WaitingRow(
                r.Id,
                r.PatientId,
                names.GetValueOrDefault(r.PatientId, string.Empty),
                r.Priority,
                r.CreatedAt,
                r.TubeTypes))
            .ToList();
    }

    private async Task<Dictionary<Guid, string>> GetPatientNamesAsync(IEnumerable<Guid> patientIds)
    {
        var ids = patientIds.Distinct().ToList();
        return await context.Set<Patient>()
            .Where(p => ids.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, p => p.Name);
    }

    private sealed record RawRow(
        Guid Id,
        Guid PatientId,
        Priority Priority,
        DateTime CreatedAt,
        List<string> TubeTypes);

    private sealed record WaitingRow(
        Guid Id,
        Guid PatientId,
        string PatientName,
        Priority Priority,
        DateTime CreatedAt,
        List<string> TubeTypes);
}
