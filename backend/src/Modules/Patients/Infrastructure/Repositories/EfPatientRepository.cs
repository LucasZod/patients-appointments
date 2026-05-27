using Backend.Modules.Patients.Domain;
using Backend.Modules.Patients.Domain.Repositories;
using Backend.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Patients.Infrastructure.Repositories;

public class EfPatientRepository(AppDbContext context) : IPatientRepository
{
    public Task<Patient?> FindByIdAsync(Guid id)
    {
        return context.Patients.FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<Patient?> FindByCpfAsync(string cpf)
    {
        var normalized = new Cpf(cpf);
        return context.Patients.FirstOrDefaultAsync(p => p.Cpf == normalized);
    }

    public async Task SaveAsync(Patient patient)
    {
        var exists = await context.Patients.AnyAsync(p => p.Id == patient.Id);
        if (exists)
            context.Patients.Update(patient);
        else
            context.Patients.Add(patient);

        await context.SaveChangesAsync();
    }
}
