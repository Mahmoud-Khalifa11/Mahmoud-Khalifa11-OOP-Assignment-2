namespace SrpLab;

public sealed class GiftMessageBuilder
{
    public string Build(
        string fromName,
        List<(string Sku, decimal Price, int Qty)> lines,
        decimal grandTotal)
    {
        var items = string.Join(", ", lines.Select(l => l.Sku));

        return $"Dear friend,\n" +
               $"A gift from {fromName} awaits ({items}).\n" +
               $"Total surprise value: {grandTotal:C}\n";
    }
}