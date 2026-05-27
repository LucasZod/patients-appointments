using Backend.Modules.Queue.Domain;
using Backend.Modules.Queue.Domain.Repositories;

namespace Backend.Modules.Queue.Application.UseCases;

public class GetQueueUseCase(IQueueRepository repository)
{
    public Task<IReadOnlyList<QueueItem>> ExecuteAsync() => repository.GetQueueAsync();
}
