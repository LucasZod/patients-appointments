using Backend.Modules.Samples.Application.UseCases;
using Backend.Modules.Samples.Domain.Repositories;
using Backend.Modules.Samples.Infrastructure.Repositories;

namespace Backend.Modules.Samples;

public static class SamplesModule
{
    public static IServiceCollection AddSamplesModule(this IServiceCollection services)
    {
        services.AddScoped<ISampleRepository, EfSampleRepository>();
        services.AddScoped<RecordSamplesUseCase>();
        services.AddScoped<ApproveSampleUseCase>();
        services.AddScoped<RejectSampleUseCase>();
        services.AddScoped<ListSamplesByServiceOrderUseCase>();
        return services;
    }
}
