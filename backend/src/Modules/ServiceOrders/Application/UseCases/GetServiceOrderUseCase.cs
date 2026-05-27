using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Application.UseCases;

public class GetServiceOrderUseCase(IServiceOrderRepository repository)
{
    public async Task<ServiceOrder> ExecuteAsync(Guid id)
    {
        var serviceOrder = await repository.FindByIdAsync(id);
        if (serviceOrder is null)
            throw new NotFoundException($"Service order '{id}' not found");

        return serviceOrder;
    }
}
