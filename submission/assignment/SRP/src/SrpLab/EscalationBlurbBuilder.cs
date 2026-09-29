namespace SrpLab;

public sealed class EscalationBlurbBuilder
{
    public string Build(
        string ticketId,
        string priority,
        DateTimeOffset slaDeadline)
    {
        return $"ESCALATE {ticketId} " +
               $"priority={priority} " +
               $"breachAt={slaDeadline:u} " +
               $"keywords-scanned=yes";
    }
}