using Backend.Modules.Queue.Application.UseCases;
using Backend.Modules.Queue.Domain.Repositories;
using Backend.Modules.Queue.Infrastructure.Repositories;

namespace Backend.Modules.Queue;

public static class QueueModule
{
    public static IServiceCollection AddQueueModule(this IServiceCollection services)
    {
        services.AddScoped<IQueueRepository, EfQueueRepository>();
        services.AddScoped<GetQueueUseCase>();
        services.AddScoped<GetQueuePositionUseCase>();
        return services;
    }
}
