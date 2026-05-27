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
        return rows
            .Select((r, index) => new QueueItem(r.Id, r.PatientId, r.Priority, index + 1, r.CreatedAt, now - r.CreatedAt))
            .ToList();
    }

    public async Task<QueueItem?> GetPositionAsync(Guid serviceOrderId)
    {
        var rows = await FetchOrderedWaitingAsync();
        var index = rows.FindIndex(r => r.Id == serviceOrderId);
        if (index == -1)
            return null;

        var match = rows[index];
        return new QueueItem(match.Id, match.PatientId, match.Priority, index + 1, match.CreatedAt, DateTime.UtcNow - match.CreatedAt);
    }

    private Task<List<WaitingRow>> FetchOrderedWaitingAsync()
    {
        return context.ServiceOrders
            .Where(so => so.Status == ServiceOrderStatus.Waiting)
            .OrderBy(so => so.Priority == Priority.Urgent ? 0
                         : so.Priority == Priority.Preferred ? 1
                         : 2)
            .ThenBy(so => so.CreatedAt)
            .Select(so => new WaitingRow(so.Id, so.PatientId, so.Priority, so.CreatedAt))
            .ToListAsync();
    }

    private sealed record WaitingRow(Guid Id, Guid PatientId, Priority Priority, DateTime CreatedAt);
}
