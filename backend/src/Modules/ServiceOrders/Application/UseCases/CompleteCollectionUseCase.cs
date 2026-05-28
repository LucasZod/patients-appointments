using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Application.UseCases;

public class CompleteCollectionUseCase(IServiceOrderRepository repository)
{
    public async Task<ServiceOrderWithPatient> ExecuteAsync(Guid serviceOrderId)
    {
        var result = await repository.FindByIdWithPatientAsync(serviceOrderId)
            ?? throw new NotFoundException($"Ordem de serviço não encontrada");

        result.Order.CompleteCollection();
        await repository.SaveAsync(result.Order);
        return result;
    }
}
