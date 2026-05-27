using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Domain.Events;

public sealed record ServiceOrderCompleted(Guid ServiceOrderId) : DomainEvent;
