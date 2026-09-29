namespace SrpLab;

public sealed class CourseEnrollmentDesk
{
    private readonly HashSet<string> _seated =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly List<string> _waitlist = new();

    public int Capacity { get; }
    public decimal Tuition { get; }
    public string CourseCode { get; }

    private readonly EnrollmentManager _enrollmentManager;
    private readonly WelcomePacketBuilder _welcomePacketBuilder;
    private readonly TuitionInvoiceBuilder _tuitionInvoiceBuilder;

    public CourseEnrollmentDesk(
        string courseCode,
        int capacity,
        decimal tuition)
    {
        CourseCode = courseCode;
        Capacity = capacity;
        Tuition = tuition;

        _enrollmentManager = new EnrollmentManager();
        _welcomePacketBuilder = new WelcomePacketBuilder();
        _tuitionInvoiceBuilder = new TuitionInvoiceBuilder();
    }

    public string Register(string studentEmail)
    {
        return _enrollmentManager.Register(
            studentEmail,
            Capacity,
            _seated,
            _waitlist);
    }

    public int WaitlistPosition(string studentEmail)
    {
        return _enrollmentManager.WaitlistPosition(
            studentEmail,
            _waitlist);
    }

    public string WelcomePacketMarkdown(
        string studentEmail,
        string studentName)
    {
        return _welcomePacketBuilder.Build(
            CourseCode,
            studentEmail,
            studentName,
            _seated,
            _waitlist);
    }

    public string TuitionInvoiceLine(string studentEmail)
    {
        return _tuitionInvoiceBuilder.Build(
            CourseCode,
            studentEmail,
            Tuition,
            _seated);
    }

    public void PromoteFromWaitlist(int seats)
    {
        _enrollmentManager.PromoteFromWaitlist(
            seats,
            Capacity,
            _seated,
            _waitlist);
    }
}