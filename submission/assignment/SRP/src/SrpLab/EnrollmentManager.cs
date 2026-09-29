namespace SrpLab;

public sealed class EnrollmentManager
{
    public string Register(
        string studentEmail,
        int capacity,
        HashSet<string> seated,
        List<string> waitlist)
    {
        if (string.IsNullOrWhiteSpace(studentEmail))
            throw new ArgumentException("email");

        var email = studentEmail.Trim();

        if (seated.Contains(email) || waitlist.Contains(email))
            return "ALREADY_REGISTERED";

        if (seated.Count < capacity)
        {
            seated.Add(email);
            return "SEATED";
        }

        waitlist.Add(email);
        return $"WAITLIST:{waitlist.Count}";
    }

    public int WaitlistPosition(
        string studentEmail,
        List<string> waitlist)
    {
        var idx = waitlist.FindIndex(
            x => x.Equals(
                studentEmail,
                StringComparison.OrdinalIgnoreCase));

        return idx < 0 ? -1 : idx + 1;
    }

    public void PromoteFromWaitlist(
        int seats,
        int capacity,
        HashSet<string> seated,
        List<string> waitlist)
    {
        while (seats > 0 &&
               waitlist.Count > 0 &&
               seated.Count < capacity)
        {
            var next = waitlist[0];

            waitlist.RemoveAt(0);
            seated.Add(next);

            seats--;
        }
    }
}