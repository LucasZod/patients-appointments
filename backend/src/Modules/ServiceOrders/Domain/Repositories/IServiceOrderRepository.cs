namespace Backend.Modules.ServiceOrders.Domain.Repositories;

public interface IServiceOrderRepository
{
    Task<ServiceOrder?> FindByIdAsync(Guid id);
    Task<ServiceOrderWithPatient?> FindByIdWithPatientAsync(Guid id);
    Task<ServiceOrder?> FindNextInQueueAsync();
    Task<IReadOnlyList<ServiceOrderWithPatient>> ListAsync(ServiceOrderStatus? status, DateTime? createdFrom);
    Task<ServiceOrderStats> GetStatsAsync(DateTime completedFrom);
    Task SaveAsync(ServiceOrder serviceOrder);
}
