using Backend.Modules.Patients.Domain;
using Backend.Modules.Patients.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.Patients.Application.UseCases;

public class FindPatientByCpfUseCase(IPatientRepository repository)
{
    public async Task<Patient> ExecuteAsync(string cpf)
    {
        var patient = await repository.FindByCpfAsync(cpf);
        if (patient is null)
            throw new NotFoundException($"Patient with CPF '{cpf}' not found");

        return patient;
    }
}
