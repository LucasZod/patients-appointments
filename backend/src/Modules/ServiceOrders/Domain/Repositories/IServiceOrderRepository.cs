namespace Backend.Modules.ServiceOrders.Domain.Repositories;

public interface IServiceOrderRepository
{
    Task<ServiceOrder?> FindByIdAsync(Guid id);
    Task<ServiceOrder?> FindNextInQueueAsync();
    Task SaveAsync(ServiceOrder serviceOrder);
}
