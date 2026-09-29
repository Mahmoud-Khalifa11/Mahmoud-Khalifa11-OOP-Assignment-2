namespace SrpLab;

public sealed class TicketPriorityCalculator
{
    public string Calculate(string subject, string body)
    {
        var blob = (subject + " " + body).ToLowerInvariant();

        if (blob.Contains("down") ||
            blob.Contains("outage") ||
            blob.Contains("cannot login"))
        {
            return "P1";
        }

        if (blob.Contains("urgent") ||
            blob.Contains("asap") ||
            blob.Contains("blocked"))
        {
            return "P2";
        }

        return "P3";
    }
}