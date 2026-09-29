namespace SrpLab;

public sealed class TicketSlaPolicy
{
    public DateTimeOffset GetDeadline(
        string priority,
        DateTimeOffset openedAt)
    {
        var hours = priority switch
        {
            "P1" => 4,
            "P2" => 24,
            _ => 72
        };

        return openedAt.AddHours(hours);
    }

    public bool IsBreached(
        string priority,
        DateTimeOffset openedAt,
        DateTimeOffset now)
    {
        return now > GetDeadline(priority, openedAt);
    }
}