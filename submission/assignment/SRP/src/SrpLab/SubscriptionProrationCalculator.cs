namespace SrpLab;

public sealed class SubscriptionProrationCalculator
{
    public decimal Calculate(
        decimal monthlyPrice,
        DateOnly periodStart,
        DateOnly periodEnd,
        DateOnly activeFrom)
    {
        if (activeFrom <= periodStart)
            return monthlyPrice;

        if (activeFrom >= periodEnd)
            return 0m;

        var totalDays =
            periodEnd.DayNumber - periodStart.DayNumber;

        if (totalDays <= 0)
            return monthlyPrice;

        var used =
            periodEnd.DayNumber - activeFrom.DayNumber;

        return Math.Round(
            monthlyPrice * used / totalDays,
            2);
    }
}