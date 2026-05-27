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

    public async Task SaveAsync(ServiceOrder serviceOrder)
    {
        var exists = await context.ServiceOrders.AnyAsync(so => so.Id == serviceOrder.Id);
        if (exists)
            context.ServiceOrders.Update(serviceOrder);
        else
            context.ServiceOrders.Add(serviceOrder);

        await context.SaveChangesAsync();
    }
}
