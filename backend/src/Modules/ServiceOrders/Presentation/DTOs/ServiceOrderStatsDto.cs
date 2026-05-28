using Backend.Modules.ServiceOrders.Domain;

namespace Backend.Modules.ServiceOrders.Presentation.DTOs;

public record ServiceOrderStatsDto(int Waiting, int InProgress, int CompletedToday, int RejectedToday)
{
    public static ServiceOrderStatsDto FromDomain(ServiceOrderStats stats) =>
        new(stats.Waiting, stats.InProgress, stats.CompletedToday, stats.RejectedToday);
}
