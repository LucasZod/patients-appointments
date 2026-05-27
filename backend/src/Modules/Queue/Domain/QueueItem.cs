using Backend.Modules.ServiceOrders.Domain;

namespace Backend.Modules.Queue.Domain;

public sealed record QueueItem(
    Guid ServiceOrderId,
    Guid PatientId,
    Priority Priority,
    int Position,
    DateTime CreatedAt,
    TimeSpan WaitTime);
