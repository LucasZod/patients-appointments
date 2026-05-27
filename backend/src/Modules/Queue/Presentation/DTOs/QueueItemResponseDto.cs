using Backend.Modules.Queue.Domain;
using Backend.Modules.ServiceOrders.Domain;

namespace Backend.Modules.Queue.Presentation.DTOs;

public record QueueItemResponseDto(
    Guid ServiceOrderId,
    Guid PatientId,
    Priority Priority,
    int Position,
    DateTime CreatedAt,
    int WaitSeconds)
{
    public static QueueItemResponseDto FromDomain(QueueItem item) => new(
        item.ServiceOrderId,
        item.PatientId,
        item.Priority,
        item.Position,
        item.CreatedAt,
        (int)item.WaitTime.TotalSeconds);
}
