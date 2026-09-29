namespace SrpLab;

public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();

    private readonly AllergenDetector _allergenDetector;
    private readonly KitchenTimingCalculator _timingCalculator;
    private readonly ThermalTicketRenderer _thermalTicketRenderer;

    public KitchenTicket()
    {
        _allergenDetector = new AllergenDetector();
        _timingCalculator = new KitchenTimingCalculator();
        _thermalTicketRenderer = new ThermalTicketRenderer();
    }

    public void AddItem(
        string item,
        IEnumerable<string> ingredients,
        int prepMinutes)
    {
        _items.Add((
            item,
            ingredients
                .Select(i => i.Trim().ToLowerInvariant())
                .ToList(),
            prepMinutes));
    }

    public IReadOnlyList<string> DetectAllergens()
    {
        return _allergenDetector.Detect(_items);
    }

    public int EstimatedReadyMinutes(int openStations)
    {
        var allergens = DetectAllergens();

        return _timingCalculator.EstimatedReadyMinutes(
            _items,
            openStations,
            allergens);
    }

    public string RenderThermalTicket(int orderNumber)
    {
        var allergens = DetectAllergens();
        var estimatedMinutes = EstimatedReadyMinutes(2);

        return _thermalTicketRenderer.Render(
            orderNumber,
            _items,
            estimatedMinutes,
            allergens);
    }

    public string ExpoLaneHint()
    {
        var allergens = DetectAllergens();
        var estimatedMinutes = EstimatedReadyMinutes(2);

        return _timingCalculator.GetExpoLane(
            estimatedMinutes,
            allergens.Count > 0);
    }
}