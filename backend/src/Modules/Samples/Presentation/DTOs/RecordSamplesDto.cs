namespace Backend.Modules.Samples.Presentation.DTOs;

public record RecordSamplesDto(Guid ServiceOrderId, IReadOnlyCollection<string> TubeTypes);
