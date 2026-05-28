using Backend.Modules.Patients.Domain;
using Backend.Modules.Patients.Domain.Repositories;

namespace Backend.Modules.Patients.Application.UseCases;

public record RegisterPatientResult(Patient Patient, bool IsNew);

public class RegisterPatientUseCase(IPatientRepository repository)
{
    public async Task<RegisterPatientResult> ExecuteAsync(string name, string cpf, DateOnly birthDate, string? phone)
    {
        var existing = await repository.FindByCpfAsync(cpf);
        if (existing is not null)
            return new RegisterPatientResult(existing, IsNew: false);

        var patient = Patient.Create(name, cpf, birthDate, phone);
        await repository.SaveAsync(patient);
        return new RegisterPatientResult(patient, IsNew: true);
    }
}
