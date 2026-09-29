namespace LibrarySystem;

public class Book : LibraryItem
{
    public Book(
        string catalogNumber,
        string title,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            21,
            baseLateFee,
            1)
    {
    }
}