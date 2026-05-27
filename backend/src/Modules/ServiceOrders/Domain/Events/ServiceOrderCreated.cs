using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Domain.Events;

public sealed record ServiceOrderCreated(Guid ServiceOrderId, Guid PatientId, Priority Priority) : DomainEvent;
