namespace LibrarySystem;

public class HeadLibrarian : Staff
{
    public HeadLibrarian(
        string personId,
        string fullName,
        string phone,
        DateOnly hireDate,
        decimal monthlySalary)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            400)
    {
    }

    public void ChangeLateFee(
        LibraryItem item,
        decimal newFee)
    {
        item.ChangePrice(newFee);
    }

    public void Withdraw(LibraryItem item)
    {
        item.Withdraw();
    }

    public void Restore(LibraryItem item)
    {
        item.Restore();
    }
}