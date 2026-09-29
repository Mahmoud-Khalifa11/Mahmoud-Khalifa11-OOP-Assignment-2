namespace SrpLab;

public sealed class GradeAverageCalculator
{
    public decimal Calculate(List<decimal> scores)
    {
        if (scores.Count == 0)
            return 0m;

        return Math.Round(scores.Average(), 2);
    }
}