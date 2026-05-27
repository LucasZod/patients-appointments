using Backend.Modules.Samples.Domain;
using Backend.Modules.Samples.Domain.Repositories;

namespace Backend.Modules.Samples.Application.UseCases;

public class ListSamplesByServiceOrderUseCase(ISampleRepository repository)
{
    public Task<IReadOnlyList<Sample>> ExecuteAsync(Guid serviceOrderId)
        => repository.FindByServiceOrderIdAsync(serviceOrderId);
}
