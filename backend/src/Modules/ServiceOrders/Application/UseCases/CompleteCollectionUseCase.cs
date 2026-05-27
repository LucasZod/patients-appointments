using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Application.UseCases;

public class CompleteCollectionUseCase(IServiceOrderRepository repository)
{
    public async Task<ServiceOrder> ExecuteAsync(Guid serviceOrderId)
    {
        var serviceOrder = await repository.FindByIdAsync(serviceOrderId)
            ?? throw new NotFoundException($"Service order '{serviceOrderId}' not found");

        serviceOrder.CompleteCollection();
        await repository.SaveAsync(serviceOrder);
        return serviceOrder;
    }
}
