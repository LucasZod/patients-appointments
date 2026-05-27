using Backend.Modules.ServiceOrders.Domain;

namespace Backend.Modules.ServiceOrders.Presentation.DTOs;

public record OrderItemResponseDto(
    Guid Id,
    string ExamCode,
    string ExamName,
    string TubeType,
    OrderItemStatus Status)
{
    public static OrderItemResponseDto FromDomain(OrderItem item) => new(
        item.Id,
        item.ExamCode,
        item.ExamName,
        item.TubeType,
        item.Status);
}
