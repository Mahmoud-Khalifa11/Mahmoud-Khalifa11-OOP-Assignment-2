namespace SrpLab;

public sealed class WelcomePacketBuilder
{
    public string Build(
        string courseCode,
        string studentEmail,
        string studentName,
        HashSet<string> seated,
        List<string> waitlist)
    {
        var status = seated.Contains(studentEmail)
            ? "confirmed seat"
            : $"waitlist #{GetWaitlistPosition(studentEmail, waitlist)}";

        return $"# Welcome to {courseCode}\n" +
               $"Hi {studentName},\n" +
               $"Your status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: " +
               $"https://example.invalid/{courseCode.ToLowerInvariant()}\n";
    }

    private int GetWaitlistPosition(
        string studentEmail,
        List<string> waitlist)
    {
        var idx = waitlist.FindIndex(
            x => x.Equals(
                studentEmail,
                StringComparison.OrdinalIgnoreCase));

        return idx < 0 ? -1 : idx + 1;
    }
}