namespace SrpLab;

public sealed class AppointmentDesk
{
    private readonly HashSet<DateTimeOffset> _booked = new();

    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    private readonly BusinessHoursPolicy _businessHoursPolicy;
    private readonly AppointmentScheduler _scheduler;
    private readonly IcsCalendarBuilder _icsCalendarBuilder;
    private readonly SmsReminderBuilder _smsReminderBuilder;

    public AppointmentDesk(
        TimeOnly open,
        TimeOnly close,
        int slotMinutes)
    {
        Open = open;
        Close = close;
        SlotMinutes = slotMinutes;

        _businessHoursPolicy = new BusinessHoursPolicy();
        _scheduler = new AppointmentScheduler();
        _icsCalendarBuilder = new IcsCalendarBuilder();
        _smsReminderBuilder = new SmsReminderBuilder();
    }

    public bool IsWithinBusinessHours(
        DateTimeOffset when)
    {
        return _businessHoursPolicy.IsWithinBusinessHours(
            when,
            Open,
            Close,
            SlotMinutes);
    }

    public DateTimeOffset? FindNextSlot(
        DateTimeOffset from,
        int searchHours)
    {
        return _scheduler.FindNextSlot(
            from,
            searchHours,
            Open,
            Close,
            SlotMinutes,
            _booked,
            _businessHoursPolicy);
    }

    public bool TryBook(DateTimeOffset slot)
    {
        return _scheduler.TryBook(
            slot,
            Open,
            Close,
            SlotMinutes,
            _booked,
            _businessHoursPolicy);
    }

    public string ToIcs(
        DateTimeOffset slot,
        string patientName,
        string clinician)
    {
        return _icsCalendarBuilder.Build(
            slot,
            SlotMinutes,
            patientName,
            clinician);
    }

    public string SmsReminder(
        DateTimeOffset slot,
        string clinicPhone)
    {
        return _smsReminderBuilder.Build(
            slot,
            clinicPhone);
    }
}