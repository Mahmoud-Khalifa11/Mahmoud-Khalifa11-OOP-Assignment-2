namespace SrpLab;

public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private string? _couponRaw;
    private bool _giftWrap;

    private readonly CouponCalculator _couponCalculator;
    private readonly GiftWrapService _giftWrapService;
    private readonly GiftMessageBuilder _giftMessageBuilder;
    private readonly PaymentAuthorizer _paymentAuthorizer;

    public CheckoutBasket()
    {
        _couponCalculator = new CouponCalculator();
        _giftWrapService = new GiftWrapService();
        _giftMessageBuilder = new GiftMessageBuilder();
        _paymentAuthorizer = new PaymentAuthorizer();
    }

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0)
            throw new ArgumentOutOfRangeException(nameof(qty));

        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText)
    {
        _couponRaw = couponText;
    }

    public void EnableGiftWrap()
    {
        _giftWrap = true;
    }

    public decimal SubTotal()
    {
        return _lines.Sum(l => l.Price * l.Qty);
    }

    public decimal DiscountAmount()
    {
        return _couponCalculator.Calculate(_couponRaw, SubTotal());
    }

    public decimal GrandTotal()
    {
        var total = SubTotal() - DiscountAmount();
        total += _giftWrapService.GetFee(_giftWrap);

        return Math.Max(0m, total);
    }

    public string GiftMessageCard(string fromName)
    {
        return _giftMessageBuilder.Build(
            fromName,
            _lines,
            GrandTotal());
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        return _paymentAuthorizer.Authorize(
            GrandTotal(),
            cardLast4,
            _lines.Count);
    }
}
