using Backend.Modules.Queue.Application.UseCases;
using Backend.Modules.Queue.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.Queue.Presentation.Controllers;

[ApiController]
[Route("api/queue")]
public class QueueController(
    GetQueueUseCase getQueue,
    GetQueuePositionUseCase getQueuePosition) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<QueueItemResponseDto>>> List()
    {
        var queue = await getQueue.ExecuteAsync();
        return Ok(queue.Select(QueueItemResponseDto.FromDomain).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<QueueItemResponseDto>> GetPosition(Guid id)
    {
        var item = await getQueuePosition.ExecuteAsync(id);
        return Ok(QueueItemResponseDto.FromDomain(item));
    }
}
