namespace Backend.Modules.Patients.Presentation.DTOs;

public record RegisterPatientDto(string Name, string Cpf, DateOnly BirthDate, string? Phone);
