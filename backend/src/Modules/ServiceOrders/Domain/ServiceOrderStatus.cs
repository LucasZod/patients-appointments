namespace Backend.Modules.ServiceOrders.Domain;

public enum ServiceOrderStatus
{
    Waiting,
    InProgress,
    Collected,
    Completed,
    Rejected,
}
