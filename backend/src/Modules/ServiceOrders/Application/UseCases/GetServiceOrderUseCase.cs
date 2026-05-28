using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Application.UseCases;

public class GetServiceOrderUseCase(IServiceOrderRepository repository)
{
    public async Task<ServiceOrderWithPatient> ExecuteAsync(Guid id)
    {
        var result = await repository.FindByIdWithPatientAsync(id);
        if (result is null)
            throw new NotFoundException($"Ordem de serviço não encontrada");

        return result;
    }
}
