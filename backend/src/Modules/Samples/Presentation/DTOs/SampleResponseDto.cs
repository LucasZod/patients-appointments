using Backend.Modules.Samples.Domain;

namespace Backend.Modules.Samples.Presentation.DTOs;

public record RejectionReasonResponseDto(RejectionReasonCode Code, string? Notes)
{
    public static RejectionReasonResponseDto FromDomain(RejectionReason reason)
        => new(reason.Code, reason.Notes);
}

public record SampleResponseDto(
    Guid Id,
    Guid ServiceOrderId,
    string TubeType,
    SampleStatus Status,
    RejectionReasonResponseDto? RejectionReason,
    DateTime CollectedAt,
    DateTime? ReviewedAt,
    DateTime CreatedAt)
{
    public static SampleResponseDto FromDomain(Sample s) => new(
        s.Id,
        s.ServiceOrderId,
        s.TubeType,
        s.Status,
        s.RejectionReason is null ? null : RejectionReasonResponseDto.FromDomain(s.RejectionReason),
        s.CollectedAt,
        s.ReviewedAt,
        s.CreatedAt);
}
