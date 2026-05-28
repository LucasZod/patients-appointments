using Backend.Modules.ServiceOrders.Application.UseCases;
using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.ServiceOrders.Presentation.Controllers;

[ApiController]
[Route("api/service-orders")]
public class ServiceOrdersController(
    CreateServiceOrderUseCase createServiceOrder,
    GetServiceOrderUseCase getServiceOrder,
    ListServiceOrdersUseCase listServiceOrders,
    GetServiceOrdersStatsUseCase getStats,
    CallNextPatientUseCase callNextPatient,
    CompleteCollectionUseCase completeCollection) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ServiceOrderResponseDto>> Create([FromBody] CreateServiceOrderDto dto)
    {
        var items = dto.Items.Select(i => new CreateServiceOrderItem(i.ExamCode, i.ExamName, i.TubeType));
        var serviceOrder = await createServiceOrder.ExecuteAsync(dto.PatientId, dto.Priority, items);
        return CreatedAtAction(nameof(GetById), new { id = serviceOrder.Id }, ServiceOrderResponseDto.FromDomain(serviceOrder));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ServiceOrderResponseDto>>> List(
        [FromQuery] ServiceOrderStatus? status,
        [FromQuery] string? date)
    {
        DateTime? createdFrom = date == "today" ? DateTime.UtcNow.Date : null;
        var orders = await listServiceOrders.ExecuteAsync(status, createdFrom);
        return Ok(orders.Select(o => ServiceOrderResponseDto.FromDomain(o.Order, o.PatientName)).ToList());
    }

    [HttpGet("stats")]
    public async Task<ActionResult<ServiceOrderStatsDto>> Stats([FromQuery] string? date)
    {
        var completedFrom = date == "today" ? DateTime.UtcNow.Date : DateTime.UtcNow.Date;
        var stats = await getStats.ExecuteAsync(completedFrom);
        return Ok(ServiceOrderStatsDto.FromDomain(stats));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ServiceOrderResponseDto>> GetById(Guid id)
    {
        var result = await getServiceOrder.ExecuteAsync(id);
        return Ok(ServiceOrderResponseDto.FromDomain(result.Order, result.PatientName, result.PatientCpf));
    }

    [HttpPost("call-next")]
    public async Task<ActionResult<ServiceOrderResponseDto>> CallNext()
    {
        var serviceOrder = await callNextPatient.ExecuteAsync();
        return Ok(ServiceOrderResponseDto.FromDomain(serviceOrder));
    }

    [HttpPatch("{id:guid}/complete-collection")]
    public async Task<ActionResult<ServiceOrderResponseDto>> CompleteCollection(Guid id)
    {
        var serviceOrder = await completeCollection.ExecuteAsync(id);
        return Ok(ServiceOrderResponseDto.FromDomain(serviceOrder));
    }
}
