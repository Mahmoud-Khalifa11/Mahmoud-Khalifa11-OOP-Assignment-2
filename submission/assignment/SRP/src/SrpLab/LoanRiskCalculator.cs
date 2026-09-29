namespace SrpLab;

public sealed class LoanRiskCalculator
{
    public decimal Calculate(
        decimal requestedAmount,
        int creditScore,
        int employmentMonths,
        bool hasCollateral)
    {
        decimal score = 100m;

        score -= Math.Max(0, 700 - creditScore) * 0.15m;

        if (employmentMonths < 6)
            score -= 20m;

        if (requestedAmount > 50_000m && !hasCollateral)
            score -= 25m;

        if (requestedAmount > 150_000m)
            score -= 10m;

        return Math.Clamp(score, 0m, 100m);
    }

    public bool IsEligible(decimal riskScore, int creditScore)
    {
        return riskScore >= 55m && creditScore >= 580;
    }
}

