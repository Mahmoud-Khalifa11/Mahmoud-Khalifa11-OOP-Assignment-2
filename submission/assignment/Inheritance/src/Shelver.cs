namespace LibrarySystem;

public class Shelver : Staff
{
    public string Section { get; private set; }

    public Shelver(
        string personId,
        string fullName,
        string phone,
        DateOnly hireDate,
        decimal monthlySalary,
        string section)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            0)
    {
        if (string.IsNullOrWhiteSpace(section))
            throw new ArgumentException("Section cannot be empty.");

        Section = section;
    }

    public void Reassign(string section)
    {
        if (string.IsNullOrWhiteSpace(section))
            throw new ArgumentException("Section cannot be empty.");

        Section = section;
    }
}