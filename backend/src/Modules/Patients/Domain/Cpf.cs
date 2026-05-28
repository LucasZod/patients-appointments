using Backend.Shared.Domain;

namespace Backend.Modules.Patients.Domain;

public sealed class Cpf
{
    public string Value { get; }

    public Cpf(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            throw new DomainException("CPF é obrigatório");

        var digits = new string(raw.Where(char.IsDigit).ToArray());

        if (digits.Length != 11)
            throw new DomainException("CPF deve ter 11 dígitos");

        if (!IsValid(digits))
            throw new DomainException("CPF inválido");

        Value = digits;
    }

    private static bool IsValid(string digits)
    {
        if (digits.Distinct().Count() == 1) return false;
        return CheckDigit(digits, 10) == digits[9] - '0'
            && CheckDigit(digits, 11) == digits[10] - '0';
    }

    private static int CheckDigit(string digits, int position)
    {
        var sum = 0;
        for (var i = 0; i < position - 1; i++)
            sum += (digits[i] - '0') * (position - i);

        var remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }

    public override bool Equals(object? obj) => obj is Cpf other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Cpf? left, Cpf? right) => Equals(left, right);

    public static bool operator !=(Cpf? left, Cpf? right) => !Equals(left, right);

    public override string ToString() => Value;
}
