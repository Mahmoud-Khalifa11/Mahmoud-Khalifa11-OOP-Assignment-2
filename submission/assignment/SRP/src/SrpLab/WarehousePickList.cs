namespace SrpLab;

public sealed class WarehousePickList
{
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();

    private readonly StockAllocator _stockAllocator;
    private readonly WarehousePathPlanner _pathPlanner;
    private readonly PickerScriptBuilder _pickerScriptBuilder;
    private readonly WmsXmlExporter _wmsXmlExporter;

    public WarehousePickList()
    {
        _stockAllocator = new StockAllocator();
        _pathPlanner = new WarehousePathPlanner();
        _pickerScriptBuilder = new PickerScriptBuilder();
        _wmsXmlExporter = new WmsXmlExporter();
    }

    public void AddNeed(
        string sku,
        string aisle,
        int bin,
        int qtyNeeded,
        int qtyOnHand)
    {
        _lines.Add((
            sku,
            aisle,
            bin,
            qtyNeeded,
            qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        return _stockAllocator.Allocate(_lines);
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
    {
        return _pathPlanner.BuildWalkingOrder(_lines);
    }

    public string PickerScript()
    {
        var walkingOrder = WalkingOrder();
        var allocations = Allocate();

        return _pickerScriptBuilder.Build(
            _lines,
            walkingOrder,
            allocations);
    }

    public string WmsXmlBatch(string batchId)
    {
        var allocations = Allocate();

        return _wmsXmlExporter.Export(
            batchId,
            allocations);
    }
}