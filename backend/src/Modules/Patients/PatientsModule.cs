using Backend.Modules.Patients.Application.UseCases;
using Backend.Modules.Patients.Domain.Repositories;
using Backend.Modules.Patients.Infrastructure.Repositories;

namespace Backend.Modules.Patients;

public static class PatientsModule
{
    public static IServiceCollection AddPatientsModule(this IServiceCollection services)
    {
        services.AddScoped<IPatientRepository, EfPatientRepository>();
        services.AddScoped<RegisterPatientUseCase>();
        services.AddScoped<FindPatientByCpfUseCase>();
        return services;
    }
}
