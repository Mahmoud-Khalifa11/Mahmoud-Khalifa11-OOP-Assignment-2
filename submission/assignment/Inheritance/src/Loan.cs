namespace LibrarySystem;

public class Loan
{
    public string LoanId { get; }
    public DateOnly BorrowDate { get; }
    public DateOnly? ReturnDate { get; private set; }

    public Member Member { get; }
    public LibraryItem Item { get; }

    public LoanStatus Status { get; private set; }

    public DateOnly DueDate
    {
        get
        {
            return BorrowDate.AddDays(Item.LoanPeriodDays);
        }
    }

    public decimal LateFee
    {
        get
        {
            if (!ReturnDate.HasValue)
                return 0;

            if (ReturnDate.Value <= DueDate)
                return 0;

            int lateDays = ReturnDate.Value.DayNumber - DueDate.DayNumber;

            decimal fee = lateDays * Item.DailyLateFee;

            decimal discount = fee * Member.DiscountPercentage / 100;

            fee -= discount;

            if (fee < 0)
                return 0;

            return fee;
        }
    }

    public Loan(
        Member member,
        LibraryItem item,
        DateOnly borrowDate)
    {
        if (member == null)
            throw new ArgumentNullException(nameof(member));

        if (item == null)
            throw new ArgumentNullException(nameof(item));

        LoanId = Guid.NewGuid().ToString("N");
        Member = member;
        Item = item;
        BorrowDate = borrowDate;
        Status = LoanStatus.Borrowed;
    }

    public void Return(DateOnly returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Only a borrowed loan can be returned.");

        if (returnDate < BorrowDate)
            throw new ArgumentException(
                "Return date cannot be earlier than borrow date.");

        ReturnDate = returnDate;
        Status = LoanStatus.Returned;

        Item.MarkAsReturned();
    }

    public void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Only a borrowed loan can be marked as lost.");

        Status = LoanStatus.Lost;

        Item.MarkAsReturned();
    }
}