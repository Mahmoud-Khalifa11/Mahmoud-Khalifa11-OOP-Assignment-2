namespace LibrarySystem;

public class LibraryItem
{
    public string CatalogNumber { get; }
    public string Title { get; }

    public int LoanPeriodDays { get; }

    public decimal BaseLateFee { get; private set; }

    public bool IsWithdrawn { get; private set; }

    public bool IsOnLoan { get; internal set; }

    protected decimal LateFeeMultiplier { get; }

    protected LibraryItem(
        string catalogNumber,
        string title,
        int loanPeriodDays,
        decimal baseLateFee,
        decimal lateFeeMultiplier)
    {
        if (string.IsNullOrWhiteSpace(catalogNumber))
            throw new ArgumentException(
                "Catalog number cannot be empty.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Title cannot be empty.");

        if (loanPeriodDays <= 0)
            throw new ArgumentException(
                "Loan period must be positive.");

        if (baseLateFee <= 0)
            throw new ArgumentException(
                "Base late fee must be positive.");

        if (lateFeeMultiplier <= 0)
            throw new ArgumentException(
                "Late fee multiplier must be positive.");

        CatalogNumber = catalogNumber;
        Title = title;
        LoanPeriodDays = loanPeriodDays;
        BaseLateFee = baseLateFee;
        LateFeeMultiplier = lateFeeMultiplier;
    }

    public decimal DailyLateFee
    {
        get
        {
            return BaseLateFee * LateFeeMultiplier;
        }
    }

    public void ChangePrice(decimal newFee)
    {
        if (newFee <= 0)
            throw new ArgumentException(
                "New late fee must be positive.");

        BaseLateFee = newFee;
    }

    public void Withdraw()
    {
        IsWithdrawn = true;
    }

    public void Restore()
    {
        IsWithdrawn = false;
    }

    internal void MarkAsLoaned()
    {
        if (IsWithdrawn)
            throw new InvalidOperationException(
                "A withdrawn item cannot be borrowed.");

        if (IsOnLoan)
            throw new InvalidOperationException(
                "This item is already on loan.");

        IsOnLoan = true;
    }

    internal void MarkAsReturned()
    {
        IsOnLoan = false;
    }
}