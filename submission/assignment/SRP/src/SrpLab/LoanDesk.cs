namespace SrpLab;

public sealed class LoanDesk
{
    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }

    private readonly LoanRiskCalculator _riskCalculator;
    private readonly LoanDocumentPolicy _documentPolicy;
    private readonly LoanDecisionLetterBuilder _decisionLetterBuilder;
    private readonly UnderwriterCsvExporter _csvExporter;

    public LoanDesk(
        decimal requestedAmount,
        int creditScore,
        int employmentMonths,
        bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;

        _riskCalculator = new LoanRiskCalculator();
        _documentPolicy = new LoanDocumentPolicy();
        _decisionLetterBuilder = new LoanDecisionLetterBuilder();
        _csvExporter = new UnderwriterCsvExporter();
    }

    public decimal RiskScore()
    {
        return _riskCalculator.Calculate(
            RequestedAmount,
            CreditScore,
            EmploymentMonths,
            HasCollateral);
    }

    public bool IsEligible()
    {
        return _riskCalculator.IsEligible(
            RiskScore(),
            CreditScore);
    }

    public IReadOnlyList<string> RequiredDocuments()
    {
        return _documentPolicy.GetRequiredDocuments(
            RequestedAmount,
            EmploymentMonths,
            HasCollateral,
            IsEligible());
    }

    public string DecisionLetter(string applicantName)
    {
        return _decisionLetterBuilder.Build(
            applicantName,
            RequestedAmount,
            RiskScore(),
            IsEligible(),
            RequiredDocuments());
    }

    public string UnderwriterCsvRow(string applicationId)
    {
        return _csvExporter.Export(
            applicationId,
            CreditScore,
            EmploymentMonths,
            HasCollateral,
            RiskScore(),
            IsEligible());
    }
}