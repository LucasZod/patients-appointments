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
            ?? throw new NotFoundException($"Ordem de serviço não encontrada");

        if (serviceOrder.Status != ServiceOrderStatus.Collected)
            throw new DomainException($"Amostras só podem ser registradas quando a ordem está com status 'Coletado'. Status atual: '{serviceOrder.Status}'");

        var samples = tubeTypes.Select(t => Sample.Create(serviceOrderId, t)).ToList();
        if (samples.Count == 0)
            throw new DomainException("Pelo menos um tubo é necessário");

        await sampleRepository.SaveManyAsync(samples);
        return samples;
    }
}
