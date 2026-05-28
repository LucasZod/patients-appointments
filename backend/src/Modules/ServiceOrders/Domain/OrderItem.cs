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
            throw new DomainException("Código do exame é obrigatório");
        if (string.IsNullOrWhiteSpace(examName))
            throw new DomainException("Nome do exame é obrigatório");
        if (string.IsNullOrWhiteSpace(tubeType))
            throw new DomainException("Tipo de tubo é obrigatório");

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
