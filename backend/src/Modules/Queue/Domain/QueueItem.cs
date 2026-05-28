using Backend.Modules.ServiceOrders.Domain;

namespace Backend.Modules.Queue.Domain;

public sealed record QueueItem(
    Guid ServiceOrderId,
    Guid PatientId,
    string PatientName,
    Priority Priority,
    int Position,
    DateTime CreatedAt,
    TimeSpan WaitTime,
    IReadOnlyList<string> TubeTypes);
