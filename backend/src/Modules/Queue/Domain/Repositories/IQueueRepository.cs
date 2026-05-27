namespace Backend.Modules.Queue.Domain.Repositories;

public interface IQueueRepository
{
    Task<IReadOnlyList<QueueItem>> GetQueueAsync();
    Task<QueueItem?> GetPositionAsync(Guid serviceOrderId);
}
