using Backend.Modules.Samples.Domain;
using Backend.Modules.Samples.Domain.Repositories;
using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.Samples.Application.UseCases;

public class ApproveSampleUseCase(
    ISampleRepository sampleRepository,
    IServiceOrderRepository serviceOrderRepository)
{
    public async Task<Sample> ExecuteAsync(Guid sampleId)
    {
        var sample = await sampleRepository.FindByIdAsync(sampleId)
            ?? throw new NotFoundException($"Amostra não encontrada");

        sample.Approve();
        await sampleRepository.SaveAsync(sample);

        var allSamples = await sampleRepository.FindByServiceOrderIdAsync(sample.ServiceOrderId);
        if (allSamples.All(s => s.Status == SampleStatus.Approved))
        {
            var serviceOrder = await serviceOrderRepository.FindByIdAsync(sample.ServiceOrderId)
                ?? throw new NotFoundException($"Ordem de serviço não encontrada");

            if (serviceOrder.Status == ServiceOrderStatus.Collected)
            {
                serviceOrder.MarkAsCompleted();
                await serviceOrderRepository.SaveAsync(serviceOrder);
            }
        }

        return sample;
    }
}
