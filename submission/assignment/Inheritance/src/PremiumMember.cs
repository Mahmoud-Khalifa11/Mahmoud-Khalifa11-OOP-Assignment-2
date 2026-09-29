namespace LibrarySystem;

public class PremiumMember : Member
{
    public int ReadingPoints
    {
        get
        {
            int points = 0;

            foreach (Loan loan in Loans)
            {
                if (loan.Status == LoanStatus.Returned)
                {
                    points += 5;
                }
            }

            return points;
        }
    }

    public PremiumMember(
        string personId,
        string fullName,
        string phone,
        decimal discountPercentage)
        : base(
            personId,
            fullName,
            phone,
            10,
            discountPercentage)
    {
    }
}