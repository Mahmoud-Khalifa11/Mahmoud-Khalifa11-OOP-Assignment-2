namespace SrpLab;

public sealed class StockAllocator
{
    public IReadOnlyList<(string Sku, int Allocated)> Allocate(
        List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        var result = new List<(string, int)>();

        foreach (var line in lines)
        {
            var allocated = Math.Min(
                line.QtyNeeded,
                line.QtyOnHand);

            result.Add((line.Sku, allocated));
        }

        return result;
    }
}