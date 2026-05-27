using Backend.Shared.Domain;

namespace Backend.Modules.Patients.Domain;

public class Patient : Entity
{
    public string Name { get; private set; } = null!;
    public Cpf Cpf { get; private set; } = null!;
    public DateOnly BirthDate { get; private set; }
    public string? Phone { get; private set; }

    private Patient() { }

    public static Patient Create(string name, string cpf, DateOnly birthDate, string? phone)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name is required");

        if (birthDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new DomainException("Birth date cannot be in the future");

        return new Patient
        {
            Name = name.Trim(),
            Cpf = new Cpf(cpf),
            BirthDate = birthDate,
            Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim(),
        };
    }
}
