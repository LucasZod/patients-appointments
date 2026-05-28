using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Application.UseCases;

public class CallNextPatientUseCase(IServiceOrderRepository repository)
{
    public async Task<ServiceOrder> ExecuteAsync()
    {
        var next = await repository.FindNextInQueueAsync()
            ?? throw new NotFoundException("Nenhuma ordem de serviço aguardando na fila");

        next.Call();
        await repository.SaveAsync(next);
        return next;
    }
}
