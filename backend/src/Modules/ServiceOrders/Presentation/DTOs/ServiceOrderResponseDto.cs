using Backend.Modules.ServiceOrders.Domain;

namespace Backend.Modules.ServiceOrders.Presentation.DTOs;

public record ServiceOrderResponseDto(
    Guid Id,
    Guid PatientId,
    ServiceOrderStatus Status,
    Priority Priority,
    DateTime? CalledAt,
    DateTime? FinishedAt,
    DateTime CreatedAt,
    IReadOnlyCollection<OrderItemResponseDto> Items)
{
    public static ServiceOrderResponseDto FromDomain(ServiceOrder so) => new(
        so.Id,
        so.PatientId,
        so.Status,
        so.Priority,
        so.CalledAt,
        so.FinishedAt,
        so.CreatedAt,
        so.Items.Select(OrderItemResponseDto.FromDomain).ToList());
}
