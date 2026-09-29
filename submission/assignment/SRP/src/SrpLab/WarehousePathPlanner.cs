namespace SrpLab;

public sealed class WarehousePathPlanner
{
    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> BuildWalkingOrder(
        List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        return lines
            .OrderBy(l => l.Aisle)
            .ThenBy(l => l.Bin)
            .Select(l => (
                l.Aisle,
                l.Bin,
                l.Sku,
                Math.Min(l.QtyNeeded, l.QtyOnHand)))
            .Where(x => x.Item4 > 0)
            .ToList();
    }
}