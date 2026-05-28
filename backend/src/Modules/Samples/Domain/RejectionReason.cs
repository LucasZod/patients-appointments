using Backend.Shared.Domain;

namespace Backend.Modules.Samples.Domain;

public sealed class RejectionReason
{
    public RejectionReasonCode Code { get; private set; }
    public string? Notes { get; private set; }

    private RejectionReason() { }

    public RejectionReason(RejectionReasonCode code, string? notes)
    {
        if (code == RejectionReasonCode.Other && string.IsNullOrWhiteSpace(notes))
            throw new DomainException("Descrição é obrigatória quando o motivo é 'Outro'");

        Code = code;
        Notes = string.IsNullOrWhiteSpace(notes) ? null : notes.Trim();
    }
}
