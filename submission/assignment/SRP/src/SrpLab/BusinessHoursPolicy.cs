namespace SrpLab;

public sealed class BusinessHoursPolicy
{
    public bool IsWithinBusinessHours(
        DateTimeOffset when,
        TimeOnly open,
        TimeOnly close,
        int slotMinutes)
    {
        if (when.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday)
            return false;

        var time = TimeOnly.FromDateTime(when.DateTime);

        return time >= open &&
               time.AddMinutes(slotMinutes) <= close;
    }
}