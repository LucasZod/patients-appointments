using Backend.Modules.Samples.Domain;
using Backend.Modules.Samples.Domain.Repositories;
using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.Samples.Application.UseCases;

public class RejectSampleUseCase(
    ISampleRepository sampleRepository,
    IServiceOrderRepository serviceOrderRepository)
{
    public async Task<Sample> ExecuteAsync(Guid sampleId, RejectionReasonCode reasonCode, string? notes)
    {
        var sample = await sampleRepository.FindByIdAsync(sampleId)
            ?? throw new NotFoundException($"Amostra não encontrada");

        sample.Reject(new RejectionReason(reasonCode, notes));
        await sampleRepository.SaveAsync(sample);

        var serviceOrder = await serviceOrderRepository.FindByIdAsync(sample.ServiceOrderId)
            ?? throw new NotFoundException($"Ordem de serviço não encontrada");

        if (serviceOrder.Status == ServiceOrderStatus.Collected)
        {
            serviceOrder.MarkAsRejected();
            await serviceOrderRepository.SaveAsync(serviceOrder);
        }

        return sample;
    }
}
