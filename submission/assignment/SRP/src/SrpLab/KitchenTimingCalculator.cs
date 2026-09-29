namespace SrpLab;

public sealed class KitchenTimingCalculator
{
    public int EstimatedReadyMinutes(
        List<(string Item, List<string> Ingredients, int PrepMinutes)> items,
        int openStations,
        IReadOnlyList<string> allergens)
    {
        if (openStations <= 0)
            openStations = 1;

        var sequential = items.Sum(i => i.PrepMinutes);

        var parallel = (int)Math.Ceiling(
            sequential / (double)openStations);

        if (allergens.Count > 0)
            parallel += 3;

        var longest = items.Count == 0
            ? 0
            : items.Max(i => i.PrepMinutes);

        return Math.Max(parallel, longest);
    }

    public string GetExpoLane(
        int estimatedMinutes,
        bool hasAllergens)
    {
        if (hasAllergens)
            return "LANE-ALLERGY";

        return estimatedMinutes > 20
            ? "LANE-SLOW"
            : "LANE-FAST";
    }
}