using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;

namespace Backend.Modules.ServiceOrders.Application.UseCases;

public class ListServiceOrdersUseCase(IServiceOrderRepository repository)
{
    public Task<IReadOnlyList<ServiceOrderWithPatient>> ExecuteAsync(
        ServiceOrderStatus? status,
        DateTime? createdFrom)
        => repository.ListAsync(status, createdFrom);
}
