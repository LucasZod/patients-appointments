using Backend.Modules.Samples.Domain;
using Backend.Modules.Samples.Domain.Repositories;
using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.Samples.Application.UseCases;

public class RecordSamplesUseCase(
    ISampleRepository sampleRepository,
    IServiceOrderRepository serviceOrderRepository)
{
    public async Task<IReadOnlyList<Sample>> ExecuteAsync(Guid serviceOrderId, IEnumerable<string> tubeTypes)
    {
        var serviceOrder = await serviceOrderRepository.FindByIdAsync(serviceOrderId)
            ?? throw new NotFoundException($"Service order '{serviceOrderId}' not found");

        if (serviceOrder.Status != ServiceOrderStatus.Collected)
            throw new DomainException($"Samples can only be recorded when service order is 'Collected', current status is '{serviceOrder.Status}'");

        var samples = tubeTypes.Select(t => Sample.Create(serviceOrderId, t)).ToList();
        if (samples.Count == 0)
            throw new DomainException("At least one tube is required");

        await sampleRepository.SaveManyAsync(samples);
        return samples;
    }
}
