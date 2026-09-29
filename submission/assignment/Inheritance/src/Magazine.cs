namespace LibrarySystem;

public class Magazine : LibraryItem
{
    public Magazine(
        string catalogNumber,
        string title,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            3,
            baseLateFee,
            0.5m)
    {
    }
}