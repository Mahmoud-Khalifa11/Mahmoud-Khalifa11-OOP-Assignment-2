namespace SrpLab;

public sealed class ThermalTicketRenderer
{
    public string Render(
        int orderNumber,
        List<(string Item, List<string> Ingredients, int PrepMinutes)> items,
        int estimatedReadyMinutes,
        IReadOnlyList<string> allergens)
    {
        var width = 32;
        var line = new string('=', width);

        var body = string.Join(
            '\n',
            items.Select(i =>
                $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));

        var allergyLine = allergens.Count == 0
            ? "ALLERGENS: none"
            : "ALLERGENS: " + string.Join(",", allergens);

        return $"{line}\n" +
               $"ORDER #{orderNumber}\n" +
               $"ETA {estimatedReadyMinutes} MIN\n" +
               $"{body}\n" +
               $"{allergyLine}\n" +
               $"{line}\n";
    }
}