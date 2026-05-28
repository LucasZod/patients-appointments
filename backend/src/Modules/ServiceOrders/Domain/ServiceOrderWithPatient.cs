namespace Backend.Modules.ServiceOrders.Domain;

public sealed record ServiceOrderWithPatient(ServiceOrder Order, string PatientName, string? PatientCpf = null);
