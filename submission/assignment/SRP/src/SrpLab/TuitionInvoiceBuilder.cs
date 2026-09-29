namespace SrpLab;

public sealed class TuitionInvoiceBuilder
{
    public string Build(
        string courseCode,
        string studentEmail,
        decimal tuition,
        HashSet<string> seated)
    {
        if (!seated.Contains(studentEmail))
            return $"{courseCode},WAITLIST,0.00";

        var vat = Math.Round(tuition * 0.14m, 2);

        return $"{courseCode},TUITION,{tuition:0.00}," +
               $"VAT,{vat:0.00}," +
               $"TOTAL,{(tuition + vat):0.00}";
    }
}