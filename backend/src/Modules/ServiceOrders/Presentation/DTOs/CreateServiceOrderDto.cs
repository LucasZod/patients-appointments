using Backend.Modules.ServiceOrders.Domain;

namespace Backend.Modules.ServiceOrders.Presentation.DTOs;

public record CreateServiceOrderItemDto(string ExamCode, string ExamName, string TubeType);

public record CreateServiceOrderDto(
    Guid PatientId,
    Priority Priority,
    IReadOnlyCollection<CreateServiceOrderItemDto> Items);
