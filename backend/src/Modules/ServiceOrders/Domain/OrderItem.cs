using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Domain;

public class OrderItem : Entity
{
    public Guid ServiceOrderId { get; private set; }
    public string ExamCode { get; private set; } = null!;
    public string ExamName { get; private set; } = null!;
    public string TubeType { get; private set; } = null!;
    public OrderItemStatus Status { get; private set; }

    private OrderItem() { }

    internal static OrderItem Create(string examCode, string examName, string tubeType)
    {
        if (string.IsNullOrWhiteSpace(examCode))
            throw new DomainException("Exam code is required");
        if (string.IsNullOrWhiteSpace(examName))
            throw new DomainException("Exam name is required");
        if (string.IsNullOrWhiteSpace(tubeType))
            throw new DomainException("Tube type is required");

        return new OrderItem
        {
            ExamCode = examCode.Trim(),
            ExamName = examName.Trim(),
            TubeType = tubeType.Trim(),
            Status = OrderItemStatus.Pending,
        };
    }

    internal void MarkAsCollected() => Status = OrderItemStatus.Collected;
}
