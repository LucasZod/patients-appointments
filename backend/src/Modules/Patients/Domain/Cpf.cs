using Backend.Shared.Domain;

namespace Backend.Modules.Patients.Domain;

public sealed class Cpf
{
    public string Value { get; }

    public Cpf(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            throw new DomainException("CPF is required");

        var digits = new string(raw.Where(char.IsDigit).ToArray());

        if (digits.Length != 11)
            throw new DomainException("CPF must have 11 digits");

        Value = digits;
    }

    public override bool Equals(object? obj) => obj is Cpf other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Cpf? left, Cpf? right) => Equals(left, right);

    public static bool operator !=(Cpf? left, Cpf? right) => !Equals(left, right);

    public override string ToString() => Value;
}
