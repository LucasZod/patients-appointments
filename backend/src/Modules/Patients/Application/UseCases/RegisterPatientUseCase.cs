using Backend.Modules.Patients.Domain;
using Backend.Modules.Patients.Domain.Repositories;

namespace Backend.Modules.Patients.Application.UseCases;

public class RegisterPatientUseCase(IPatientRepository repository)
{
    public async Task<Patient> ExecuteAsync(string name, string cpf, DateOnly birthDate, string? phone)
    {
        var existing = await repository.FindByCpfAsync(cpf);
        if (existing is not null)
            return existing;

        var patient = Patient.Create(name, cpf, birthDate, phone);
        await repository.SaveAsync(patient);
        return patient;
    }
}
