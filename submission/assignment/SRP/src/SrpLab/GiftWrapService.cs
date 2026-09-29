namespace SrpLab;

public sealed class GiftWrapService
{
    public decimal GetFee(bool giftWrap)
    {
        return giftWrap ? 4.99m : 0m;
    }
}