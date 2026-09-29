namespace SrpLab;

public sealed class SupportTicket
{
    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    private readonly TicketPriorityCalculator _priorityCalculator;
    private readonly TicketSlaPolicy _slaPolicy;
    private readonly SupportReplyBuilder _replyBuilder;
    private readonly EscalationBlurbBuilder _escalationBuilder;

    public SupportTicket(
        string id,
        string subject,
        string body,
        DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;

        _priorityCalculator = new TicketPriorityCalculator();
        _slaPolicy = new TicketSlaPolicy();
        _replyBuilder = new SupportReplyBuilder();
        _escalationBuilder = new EscalationBlurbBuilder();

        RecalculatePriorityFromText();
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        RecalculatePriorityFromText();
    }

    public void RecalculatePriorityFromText()
    {
        Priority = _priorityCalculator.Calculate(Subject, Body);
    }

    public DateTimeOffset SlaDeadline()
    {
        return _slaPolicy.GetDeadline(Priority, OpenedAt);
    }

    public bool IsBreached(DateTimeOffset now)
    {
        return _slaPolicy.IsBreached(Priority, OpenedAt, now);
    }

    public string DraftPublicReply(string agentName)
    {
        return _replyBuilder.Build(
            Id,
            Priority,
            agentName,
            SlaDeadline());
    }

    public string InternalEscalationBlurb()
    {
        return _escalationBuilder.Build(
            Id,
            Priority,
            SlaDeadline());
    }
}