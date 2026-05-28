using Backend.Modules.Queue.Domain;
using Backend.Modules.ServiceOrders.Domain;

namespace Backend.Modules.Queue.Presentation.DTOs;

public record QueueItemResponseDto(
    Guid ServiceOrderId,
    Guid PatientId,
    string PatientName,
    Priority Priority,
    int Position,
    DateTime CreatedAt,
    int WaitSeconds,
    IReadOnlyList<string> TubeTypes)
{
    public static QueueItemResponseDto FromDomain(QueueItem item) => new(
        item.ServiceOrderId,
        item.PatientId,
        item.PatientName,
        item.Priority,
        item.Position,
        item.CreatedAt,
        (int)item.WaitTime.TotalSeconds,
        item.TubeTypes);
}
