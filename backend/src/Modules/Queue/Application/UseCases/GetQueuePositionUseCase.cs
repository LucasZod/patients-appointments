using Backend.Modules.Queue.Domain;
using Backend.Modules.Queue.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.Queue.Application.UseCases;

public class GetQueuePositionUseCase(IQueueRepository repository)
{
    public async Task<QueueItem> ExecuteAsync(Guid serviceOrderId)
    {
        var item = await repository.GetPositionAsync(serviceOrderId);
        if (item is null)
            throw new NotFoundException($"Service order '{serviceOrderId}' is not in the queue");

        return item;
    }
}
