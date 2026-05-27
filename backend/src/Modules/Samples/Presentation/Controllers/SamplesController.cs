using Backend.Modules.Samples.Application.UseCases;
using Backend.Modules.Samples.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.Samples.Presentation.Controllers;

[ApiController]
[Route("api/samples")]
public class SamplesController(
    RecordSamplesUseCase recordSamples,
    ApproveSampleUseCase approveSample,
    RejectSampleUseCase rejectSample,
    ListSamplesByServiceOrderUseCase listSamples) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<IReadOnlyCollection<SampleResponseDto>>> Record([FromBody] RecordSamplesDto dto)
    {
        var samples = await recordSamples.ExecuteAsync(dto.ServiceOrderId, dto.TubeTypes);
        return Ok(samples.Select(SampleResponseDto.FromDomain).ToList());
    }

    [HttpPatch("{id:guid}/approve")]
    public async Task<ActionResult<SampleResponseDto>> Approve(Guid id)
    {
        var sample = await approveSample.ExecuteAsync(id);
        return Ok(SampleResponseDto.FromDomain(sample));
    }

    [HttpPatch("{id:guid}/reject")]
    public async Task<ActionResult<SampleResponseDto>> Reject(Guid id, [FromBody] RejectSampleDto dto)
    {
        var sample = await rejectSample.ExecuteAsync(id, dto.Reason, dto.Notes);
        return Ok(SampleResponseDto.FromDomain(sample));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<SampleResponseDto>>> ListByServiceOrder([FromQuery] Guid serviceOrderId)
    {
        var samples = await listSamples.ExecuteAsync(serviceOrderId);
        return Ok(samples.Select(SampleResponseDto.FromDomain).ToList());
    }
}
