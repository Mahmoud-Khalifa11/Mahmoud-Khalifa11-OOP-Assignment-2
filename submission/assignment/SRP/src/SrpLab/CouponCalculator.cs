namespace SrpLab;

public sealed class CouponCalculator
{
    public decimal Calculate(string? couponRaw, decimal subtotal)
    {
        if (string.IsNullOrWhiteSpace(couponRaw))
            return 0m;

        var t = couponRaw.Trim().ToUpperInvariant();

        if (t.StartsWith("SAVE") &&
            int.TryParse(t[4..], out var pct) &&
            pct is > 0 and <= 50)
        {
            return Math.Round(subtotal * pct / 100m, 2);
        }

        if (t.Contains("FREESHIP"))
            return 0m;

        if (t == "WELCOME10")
            return Math.Min(10m, subtotal);

        return 0m;
    }
}