namespace LibrarySystem;

public class Member : Person
{
    private readonly List<Loan> _loans = new();

    public IReadOnlyList<Loan> Loans => _loans;

    public int MaxLoans { get; }
    public decimal DiscountPercentage { get; }

    protected Member(
        string personId,
        string fullName,
        string phone,
        int maxLoans,
        decimal discountPercentage)
        : base(personId, fullName, phone)
    {
        if (maxLoans <= 0)
            throw new ArgumentException("Maximum loans must be positive.");

        if (discountPercentage < 0)
            throw new ArgumentException(
                "Discount percentage cannot be negative.");

        MaxLoans = maxLoans;
        DiscountPercentage = discountPercentage;
    }

    public Loan Borrow(
        LibraryItem item,
        DateOnly borrowDate)
    {
        int activeLoans = 0;

        foreach (Loan loan in _loans)
        {
            if (loan.Status == LoanStatus.Borrowed)
            {
                activeLoans++;
            }
        }

        if (activeLoans >= MaxLoans)
            throw new InvalidOperationException(
                "Member has reached the maximum number of active loans.");

        if (item.IsWithdrawn)
            throw new InvalidOperationException(
                "A withdrawn item cannot be borrowed.");

        if (item.IsOnLoan)
            throw new InvalidOperationException(
                "This item is already on loan.");

        Loan newLoan = new Loan(
            this,
            item,
            borrowDate);

        _loans.Add(newLoan);

        item.MarkAsLoaned();

        return newLoan;
    }
}