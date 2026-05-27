using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Application.UseCases;

public class CallNextPatientUseCase(IServiceOrderRepository repository)
{
    public async Task<ServiceOrder> ExecuteAsync()
    {
        var next = await repository.FindNextInQueueAsync()
            ?? throw new NotFoundException("No service orders waiting in the queue");

        next.Call();
        await repository.SaveAsync(next);
        return next;
    }
}
