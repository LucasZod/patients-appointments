using Backend.Shared.Domain;

namespace Backend.Modules.Samples.Domain;

public class Sample : Entity
{
    public Guid ServiceOrderId { get; private set; }
    public string TubeType { get; private set; } = null!;
    public SampleStatus Status { get; private set; }
    public RejectionReason? RejectionReason { get; private set; }
    public DateTime CollectedAt { get; private set; }
    public DateTime? ReviewedAt { get; private set; }

    private Sample() { }

    public static Sample Create(Guid serviceOrderId, string tubeType)
    {
        if (serviceOrderId == Guid.Empty)
            throw new DomainException("ServiceOrderId is required");
        if (string.IsNullOrWhiteSpace(tubeType))
            throw new DomainException("Tube type is required");

        return new Sample
        {
            ServiceOrderId = serviceOrderId,
            TubeType = tubeType.Trim(),
            Status = SampleStatus.Collected,
            CollectedAt = DateTime.UtcNow,
        };
    }

    public void Approve()
    {
        EnsureCanBeReviewed();
        Status = SampleStatus.Approved;
        ReviewedAt = DateTime.UtcNow;
    }

    public void Reject(RejectionReason reason)
    {
        ArgumentNullException.ThrowIfNull(reason);
        EnsureCanBeReviewed();
        Status = SampleStatus.Rejected;
        ReviewedAt = DateTime.UtcNow;
        RejectionReason = reason;
    }

    public void EnsureCanBeReviewed()
    {
        if (Status != SampleStatus.Collected)
            throw new DomainException($"Sample cannot be reviewed from status '{Status}'");
    }
}
