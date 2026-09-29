namespace SrpLab;

public sealed class LedgerJournalExporter
{
    public string Export(
        string customerId,
        string invoiceNumber,
        decimal amount)
    {
        return $"{customerId}," +
               $"{invoiceNumber}," +
               $"{amount:0.00}," +
               $"AR-SUB";
    }
}