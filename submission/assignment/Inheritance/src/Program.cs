using LibrarySystem;

Console.WriteLine("=== Library System Demo ===");

// =========================
// Members
// =========================

StudentMember student = new StudentMember(
    "S001",
    "Ahmed Ali",
    "01000000000");

PremiumMember premium = new PremiumMember(
    "P001",
    "Mohamed Khaled",
    "01111111111",
    20);

Console.WriteLine();
Console.WriteLine($"Student: {student.FullName}");
Console.WriteLine($"Premium: {premium.FullName}");

// =========================
// Library Items
// =========================

Book book = new Book(
    "B001",
    "Clean Code",
    10);

DVD dvd = new DVD(
    "D001",
    "Inception",
    10);

Magazine magazine = new Magazine(
    "M001",
    "Science Weekly",
    10);

Console.WriteLine();
Console.WriteLine("=== Items ===");

Console.WriteLine(
    $"Book: {book.Title}, Period={book.LoanPeriodDays}, DailyFee={book.DailyLateFee}");

Console.WriteLine(
    $"DVD: {dvd.Title}, Period={dvd.LoanPeriodDays}, DailyFee={dvd.DailyLateFee}");

Console.WriteLine(
    $"Magazine: {magazine.Title}, Period={magazine.LoanPeriodDays}, DailyFee={magazine.DailyLateFee}");

// =========================
// Staff
// =========================

Librarian librarian = new Librarian(
    "L001",
    "Omar Hassan",
    "01222222222",
    new DateOnly(2024, 1, 1),
    8000);

Shelver shelver = new Shelver(
    "S002",
    "Ali Samir",
    "01233333333",
    new DateOnly(2024, 2, 1),
    6000,
    "Fiction");

HeadLibrarian headLibrarian = new HeadLibrarian(
    "H001",
    "Mona Ahmed",
    "01244444444",
    new DateOnly(2023, 1, 1),
    12000);

List<Staff> staffMembers = new List<Staff>();

staffMembers.Add(librarian);
staffMembers.Add(shelver);
staffMembers.Add(headLibrarian);

Console.WriteLine();
Console.WriteLine("=== Staff ===");

foreach (Staff staff in staffMembers)
{
    Console.WriteLine(
        $"{staff.FullName} - Monthly Pay: {staff.MonthlyPay}");
}

// =========================
// Borrowing
// =========================

Console.WriteLine();
Console.WriteLine("=== Borrowing ===");

Loan studentLoan = student.Borrow(
    book,
    new DateOnly(2026, 9, 29));

Console.WriteLine($"Borrowed: {studentLoan.Item.Title}");
Console.WriteLine($"Due Date: {studentLoan.DueDate}");
Console.WriteLine($"Status: {studentLoan.Status}");

// =========================
// Already On Loan
// =========================

Console.WriteLine();
Console.WriteLine("=== Business Rule: Already On Loan ===");

try
{
    premium.Borrow(
        book,
        new DateOnly(2026, 9, 29));
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}

// =========================
// Withdrawn Item
// =========================

Console.WriteLine();
Console.WriteLine("=== Business Rule: Withdrawn Item ===");

magazine.Withdraw();

try
{
    premium.Borrow(
        magazine,
        new DateOnly(2026, 9, 29));
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}

// =========================
// Student Loan Limit
// =========================

Console.WriteLine();
Console.WriteLine("=== Business Rule: Student Loan Limit ===");

Book book2 = new Book(
    "B002",
    "Design Patterns",
    10);

Book book3 = new Book(
    "B003",
    "Refactoring",
    10);

Book book4 = new Book(
    "B004",
    "Clean Architecture",
    10);

student.Borrow(
    book2,
    new DateOnly(2026, 9, 29));

student.Borrow(
    book3,
    new DateOnly(2026, 9, 29));

try
{
    student.Borrow(
        book4,
        new DateOnly(2026, 9, 29));
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}

// =========================
// Premium Late Return
// =========================

Console.WriteLine();
Console.WriteLine("=== Premium Member Late Return ===");

Loan premiumLoan = premium.Borrow(
    dvd,
    new DateOnly(2026, 9, 1));

premiumLoan.Return(
    premiumLoan.DueDate.AddDays(5));

Console.WriteLine($"Due Date: {premiumLoan.DueDate}");
Console.WriteLine($"Late Fee: {premiumLoan.LateFee}");
Console.WriteLine($"Reading Points: {premium.ReadingPoints}");

// =========================
// Invalid Loan Status Changes
// =========================

Console.WriteLine();
Console.WriteLine("=== Business Rule: Invalid Loan Status Changes ===");

try
{
    premiumLoan.Return(
        new DateOnly(2026, 9, 15));
}
catch (Exception ex)
{
    Console.WriteLine(
        $"Return Again Rejected: {ex.Message}");
}

try
{
    premiumLoan.MarkAsLost();
}
catch (Exception ex)
{
    Console.WriteLine(
        $"Mark Lost Rejected: {ex.Message}");
}

// =========================
// Give Raise
// =========================

Console.WriteLine();
Console.WriteLine("=== Give Raise ===");

Console.WriteLine(
    $"Before Raise: {librarian.MonthlySalary}");

librarian.GiveRaise(10);

Console.WriteLine(
    $"After Raise: {librarian.MonthlySalary}");

Console.WriteLine(
    $"Monthly Pay: {librarian.MonthlyPay}");

// =========================
// Shelver Reassign
// =========================

Console.WriteLine();
Console.WriteLine("=== Shelver Reassign ===");

Console.WriteLine(
    $"Before: {shelver.Section}");

shelver.Reassign("Technology");

Console.WriteLine(
    $"After: {shelver.Section}");

// =========================
// Withdraw / Restore
// =========================

Console.WriteLine();
Console.WriteLine("=== Withdraw / Restore ===");

Console.WriteLine(
    $"Magazine Withdrawn: {magazine.IsWithdrawn}");

headLibrarian.Restore(magazine);

Console.WriteLine(
    $"Magazine Withdrawn After Restore: {magazine.IsWithdrawn}");

headLibrarian.Withdraw(magazine);

Console.WriteLine(
    $"Magazine Withdrawn Again: {magazine.IsWithdrawn}");

// =========================
// Change Late Fee
// =========================

Console.WriteLine();
Console.WriteLine("=== Change Late Fee ===");

Console.WriteLine(
    $"Old DVD Daily Fee: {dvd.DailyLateFee}");

headLibrarian.ChangeLateFee(
    dvd,
    15);

Console.WriteLine(
    $"New DVD Daily Fee: {dvd.DailyLateFee}");

// =========================
// Invalid Raise
// =========================

Console.WriteLine();
Console.WriteLine("=== Invalid Raise ===");

try
{
    librarian.GiveRaise(-5);
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}

// =========================
// Invalid Late Fee
// =========================

Console.WriteLine();
Console.WriteLine("=== Invalid Late Fee ===");

try
{
    headLibrarian.ChangeLateFee(
        dvd,
        0);
}
catch (Exception ex)
{
    Console.WriteLine($"Rejected: {ex.Message}");
}

// =========================
// Compile-Time Restrictions
// =========================

/*
These lines must NOT compile:

Person person = new Person(
    "P001",
    "Test",
    "01000000000");

Member member = new StudentMember(
    "S002",
    "Test",
    "01000000000");

Staff staff = new Librarian(
    "L002",
    "Test",
    "01000000000",
    new DateOnly(2026, 1, 1),
    5000);

student.FullName = "New Name";

student.Loans.Add(studentLoan);

book.IsOnLoan = false;
*/

Console.WriteLine();
Console.WriteLine("=== Demo Finished ===");