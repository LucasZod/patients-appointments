using Backend.Modules.ServiceOrders.Domain.Events;
using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Domain;

public class ServiceOrder : Entity
{
    private readonly List<OrderItem> _items = [];

    public Guid PatientId { get; private set; }
    public ServiceOrderStatus Status { get; private set; }
    public Priority Priority { get; private set; }
    public DateTime? CalledAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private ServiceOrder() { }

    public static ServiceOrder Create(Guid patientId, Priority priority, IEnumerable<(string examCode, string examName, string tubeType)> items)
    {
        if (patientId == Guid.Empty)
            throw new DomainException("ID do paciente é obrigatório");

        var orderItems = items.Select(i => OrderItem.Create(i.examCode, i.examName, i.tubeType)).ToList();
        if (orderItems.Count == 0)
            throw new DomainException("A ordem deve ter pelo menos um exame");

        var order = new ServiceOrder
        {
            PatientId = patientId,
            Priority = priority,
            Status = ServiceOrderStatus.Waiting,
        };
        order._items.AddRange(orderItems);

        order.RaiseEvent(new ServiceOrderCreated(order.Id, patientId, priority));
        return order;
    }

    public void Call()
    {
        EnsureCanBeCalledForCollection();
        Status = ServiceOrderStatus.InProgress;
        CalledAt = DateTime.UtcNow;
    }

    public void CompleteCollection()
    {
        EnsureCanCompleteCollection();
        Status = ServiceOrderStatus.Collected;
        FinishedAt = DateTime.UtcNow;

        foreach (var item in _items)
            item.MarkAsCollected();
    }

    public void MarkAsCompleted()
    {
        if (Status != ServiceOrderStatus.Collected)
            throw new DomainException($"Ordem não pode ser concluída com status '{Status}'");

        Status = ServiceOrderStatus.Completed;
        RaiseEvent(new ServiceOrderCompleted(Id));
    }

    public void MarkAsRejected()
    {
        if (Status != ServiceOrderStatus.Collected)
            throw new DomainException($"Ordem não pode ser rejeitada com status '{Status}'");

        Status = ServiceOrderStatus.Rejected;
    }

    public void EnsureCanBeCalledForCollection()
    {
        if (Status != ServiceOrderStatus.Waiting)
            throw new DomainException($"Ordem não pode ser chamada com status '{Status}'");
    }

    public void EnsureCanCompleteCollection()
    {
        if (Status != ServiceOrderStatus.InProgress)
            throw new DomainException($"Ordem não pode finalizar coleta com status '{Status}'");
    }
}
