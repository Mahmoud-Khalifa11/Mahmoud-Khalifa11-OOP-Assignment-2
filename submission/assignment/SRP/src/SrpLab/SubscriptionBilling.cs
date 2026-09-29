namespace SrpLab;

public sealed class SubscriptionBilling
{
    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public int FailedPayments { get; private set; }

    private readonly SubscriptionProrationCalculator _prorationCalculator;
    private readonly InvoiceNumberGenerator _invoiceNumberGenerator;
    private readonly DunningEmailBuilder _dunningEmailBuilder;
    private readonly LedgerJournalExporter _ledgerExporter;

    public SubscriptionBilling(
        string customerId,
        decimal monthlyPrice,
        DateOnly periodStart,
        DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;

        _prorationCalculator = new SubscriptionProrationCalculator();
        _invoiceNumberGenerator = new InvoiceNumberGenerator();
        _dunningEmailBuilder = new DunningEmailBuilder();
        _ledgerExporter = new LedgerJournalExporter();
    }

    public decimal Prorate(DateOnly activeFrom)
    {
        return _prorationCalculator.Calculate(
            MonthlyPrice,
            PeriodStart,
            PeriodEnd,
            activeFrom);
    }

    public string NextInvoiceNumber()
    {
        return _invoiceNumberGenerator.Generate(
            PeriodStart);
    }

    public void RegisterFailedPayment()
    {
        FailedPayments++;
    }

    public string DunningEmail(
        string customerName,
        DateOnly asOf)
    {
        var amount = Prorate(PeriodStart);
        var invoice = NextInvoiceNumber();

        return _dunningEmailBuilder.Build(
            customerName,
            asOf,
            amount,
            invoice,
            FailedPayments);
    }

    public string LedgerJournalLine(DateOnly activeFrom)
    {
        var invoice = NextInvoiceNumber();
        var amount = Prorate(activeFrom);

        return _ledgerExporter.Export(
            CustomerId,
            invoice,
            amount);
    }
}