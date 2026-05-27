using Backend.Modules.ServiceOrders.Application.UseCases;
using Backend.Modules.ServiceOrders.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.ServiceOrders.Presentation.Controllers;

[ApiController]
[Route("api/service-orders")]
public class ServiceOrdersController(
    CreateServiceOrderUseCase createServiceOrder,
    GetServiceOrderUseCase getServiceOrder,
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ServiceOrderResponseDto>> GetById(Guid id)
    {
        var serviceOrder = await getServiceOrder.ExecuteAsync(id);
        return Ok(ServiceOrderResponseDto.FromDomain(serviceOrder));
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
