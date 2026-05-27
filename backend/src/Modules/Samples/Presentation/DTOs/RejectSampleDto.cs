using Backend.Modules.Samples.Domain;

namespace Backend.Modules.Samples.Presentation.DTOs;

public record RejectSampleDto(RejectionReasonCode Reason, string? Notes);
