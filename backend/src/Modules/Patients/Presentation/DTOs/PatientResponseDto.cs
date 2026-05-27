using Backend.Modules.Patients.Domain;

namespace Backend.Modules.Patients.Presentation.DTOs;

public record PatientResponseDto(
    Guid Id,
    string Name,
    string Cpf,
    DateOnly BirthDate,
    string? Phone,
    DateTime CreatedAt)
{
    public static PatientResponseDto FromDomain(Patient patient) => new(
        patient.Id,
        patient.Name,
        patient.Cpf.Value,
        patient.BirthDate,
        patient.Phone,
        patient.CreatedAt);
}
