using Backend.Modules.ServiceOrders.Application.UseCases;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Modules.ServiceOrders.Infrastructure.Repositories;

namespace Backend.Modules.ServiceOrders;

public static class ServiceOrdersModule
{
    public static IServiceCollection AddServiceOrdersModule(this IServiceCollection services)
    {
        services.AddScoped<IServiceOrderRepository, EfServiceOrderRepository>();
        services.AddScoped<CreateServiceOrderUseCase>();
        services.AddScoped<GetServiceOrderUseCase>();
        services.AddScoped<CallNextPatientUseCase>();
        services.AddScoped<CompleteCollectionUseCase>();
        return services;
    }
}
