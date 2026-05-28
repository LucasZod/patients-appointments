using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;

namespace Backend.Modules.ServiceOrders.Application.UseCases;

public class GetServiceOrdersStatsUseCase(IServiceOrderRepository repository)
{
    public Task<ServiceOrderStats> ExecuteAsync(DateTime completedFrom)
        => repository.GetStatsAsync(completedFrom);
}
