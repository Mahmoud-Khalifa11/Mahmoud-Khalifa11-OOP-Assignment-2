namespace SrpLab;

public sealed class PickerScriptBuilder
{
    public string Build(
        List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines,
        IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> walkingOrder,
        IReadOnlyList<(string Sku, int Allocated)> allocations)
    {
        var steps = walkingOrder
            .Select((s, i) =>
                $"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} × {s.Sku}");

        var shortfalls = allocations.Where(a =>
        {
            var need = lines.First(l => l.Sku == a.Sku).QtyNeeded;
            return a.Allocated < need;
        });

        var warn = shortfalls.Any()
            ? "SHORTAGES: " +
              string.Join(", ", shortfalls.Select(s => s.Sku))
            : "SHORTAGES: none";

        return string.Join('\n', steps) + "\n" + warn;
    }
}