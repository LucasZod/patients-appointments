using Backend.Modules.Patients.Application.UseCases;
using Backend.Modules.Patients.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.Patients.Presentation.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController(
    RegisterPatientUseCase registerPatient,
    FindPatientByCpfUseCase findPatientByCpf) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PatientResponseDto>> Register([FromBody] RegisterPatientDto dto)
    {
        var patient = await registerPatient.ExecuteAsync(dto.Name, dto.Cpf, dto.BirthDate, dto.Phone);
        return Ok(PatientResponseDto.FromDomain(patient));
    }

    [HttpGet]
    public async Task<ActionResult<PatientResponseDto>> FindByCpf([FromQuery] string cpf)
    {
        var patient = await findPatientByCpf.ExecuteAsync(cpf);
        return Ok(PatientResponseDto.FromDomain(patient));
    }
}
