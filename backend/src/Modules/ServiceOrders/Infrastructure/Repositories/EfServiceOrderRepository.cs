using Backend.Modules.Patients.Domain;
using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.ServiceOrders.Infrastructure.Repositories;

public class EfServiceOrderRepository(AppDbContext context) : IServiceOrderRepository
{
    public Task<ServiceOrder?> FindByIdAsync(Guid id)
    {
        return context.ServiceOrders
            .Include(so => so.Items)
            .FirstOrDefaultAsync(so => so.Id == id);
    }

    public Task<ServiceOrder?> FindNextInQueueAsync()
    {
        return context.ServiceOrders
            .Include(so => so.Items)
            .Where(so => so.Status == ServiceOrderStatus.Waiting)
            .OrderBy(so => so.Priority == Priority.Urgent ? 0
                         : so.Priority == Priority.Preferred ? 1
                         : 2)
            .ThenBy(so => so.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<ServiceOrderWithPatient>> ListAsync(
        ServiceOrderStatus? status,
        DateTime? createdFrom)
    {
        var query = context.ServiceOrders.Include(so => so.Items).AsQueryable();

        if (status.HasValue)
            query = query.Where(so => so.Status == status.Value);

        if (createdFrom.HasValue)
            query = query.Where(so => so.CreatedAt >= createdFrom.Value);

        var orders = await query
            .OrderBy(so => so.Priority == Priority.Urgent ? 0
                         : so.Priority == Priority.Preferred ? 1
                         : 2)
            .ThenByDescending(so => so.CreatedAt)
            .ToListAsync();

        var names = await GetPatientNamesAsync(orders.Select(o => o.PatientId));

        return orders
            .Select(o => new ServiceOrderWithPatient(o, names.GetValueOrDefault(o.PatientId, string.Empty)))
            .ToList();
    }

    public async Task<ServiceOrderStats> GetStatsAsync(DateTime completedFrom)
    {
        var waiting = await context.ServiceOrders.CountAsync(so => so.Status == ServiceOrderStatus.Waiting);
        var inProgress = await context.ServiceOrders.CountAsync(so => so.Status == ServiceOrderStatus.InProgress);
        var completedToday = await context.ServiceOrders.CountAsync(
            so => so.Status == ServiceOrderStatus.Completed && so.CreatedAt >= completedFrom);
        var rejectedToday = await context.ServiceOrders.CountAsync(
            so => so.Status == ServiceOrderStatus.Rejected && so.CreatedAt >= completedFrom);

        return new ServiceOrderStats(waiting, inProgress, completedToday, rejectedToday);
    }

    public async Task SaveAsync(ServiceOrder serviceOrder)
    {
        var exists = await context.ServiceOrders.AnyAsync(so => so.Id == serviceOrder.Id);
        if (exists)
            context.ServiceOrders.Update(serviceOrder);
        else
            context.ServiceOrders.Add(serviceOrder);

        await context.SaveChangesAsync();
    }

    private async Task<Dictionary<Guid, string>> GetPatientNamesAsync(IEnumerable<Guid> patientIds)
    {
        var ids = patientIds.Distinct().ToList();
        return await context.Set<Patient>()
            .Where(p => ids.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, p => p.Name);
    }
}
