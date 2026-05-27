namespace Backend.Modules.Patients.Domain.Repositories;

public interface IPatientRepository
{
    Task<Patient?> FindByIdAsync(Guid id);
    Task<Patient?> FindByCpfAsync(string cpf);
    Task SaveAsync(Patient patient);
}
