using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Application.UseCases;

public class CompleteCollectionUseCase(IServiceOrderRepository repository)
{
    public async Task<ServiceOrderWithPatient> ExecuteAsync(Guid serviceOrderId)
    {
        var result = await repository.FindByIdWithPatientAsync(serviceOrderId)
            ?? throw new NotFoundException($"Service order '{serviceOrderId}' not found");

        result.Order.CompleteCollection();
        await repository.SaveAsync(result.Order);
        return result;
    }
}
