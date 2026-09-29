namespace SrpLab;

public sealed class AppointmentScheduler
{
    public DateTimeOffset? FindNextSlot(
        DateTimeOffset from,
        int searchHours,
        TimeOnly open,
        TimeOnly close,
        int slotMinutes,
        HashSet<DateTimeOffset> booked,
        BusinessHoursPolicy businessHoursPolicy)
    {
        var cursor = Align(from, slotMinutes);
        var end = from.AddHours(searchHours);

        while (cursor < end)
        {
            if (businessHoursPolicy.IsWithinBusinessHours(
                    cursor,
                    open,
                    close,
                    slotMinutes) &&
                !booked.Contains(cursor))
            {
                return cursor;
            }

            cursor = cursor.AddMinutes(slotMinutes);
        }

        return null;
    }

    public bool TryBook(
        DateTimeOffset slot,
        TimeOnly open,
        TimeOnly close,
        int slotMinutes,
        HashSet<DateTimeOffset> booked,
        BusinessHoursPolicy businessHoursPolicy)
    {
        if (!businessHoursPolicy.IsWithinBusinessHours(
                slot,
                open,
                close,
                slotMinutes) ||
            booked.Contains(slot))
        {
            return false;
        }

        booked.Add(slot);
        return true;
    }

    private DateTimeOffset Align(
        DateTimeOffset from,
        int slotMinutes)
    {
        var minutes =
            from.Minute - (from.Minute % slotMinutes);

        return new DateTimeOffset(
            from.Year,
            from.Month,
            from.Day,
            from.Hour,
            minutes,
            0,
            from.Offset);
    }
}