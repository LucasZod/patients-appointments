using Backend.Modules.Patients.Domain.Repositories;
using Backend.Modules.ServiceOrders.Domain;
using Backend.Modules.ServiceOrders.Domain.Repositories;
using Backend.Shared.Domain;

namespace Backend.Modules.ServiceOrders.Application.UseCases;

public record CreateServiceOrderItem(string ExamCode, string ExamName, string TubeType);

public class CreateServiceOrderUseCase(
    IServiceOrderRepository serviceOrderRepository,
    IPatientRepository patientRepository)
{
    public async Task<ServiceOrder> ExecuteAsync(Guid patientId, Priority priority, IEnumerable<CreateServiceOrderItem> items)
    {
        var patient = await patientRepository.FindByIdAsync(patientId);
        if (patient is null)
            throw new NotFoundException($"Paciente não encontrado");

        var serviceOrder = ServiceOrder.Create(
            patientId,
            priority,
            items.Select(i => (i.ExamCode, i.ExamName, i.TubeType)));

        await serviceOrderRepository.SaveAsync(serviceOrder);
        return serviceOrder;
    }
}
