namespace Backend.Modules.ServiceOrders.Domain;

public sealed record ServiceOrderStats(int Waiting, int InProgress, int CompletedToday, int RejectedToday);
