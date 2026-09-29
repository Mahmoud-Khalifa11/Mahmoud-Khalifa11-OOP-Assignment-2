namespace SrpLab;

public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly GradeAverageCalculator _averageCalculator;
    private readonly GradeLetterPolicy _letterPolicy;
    private readonly HonorRollPolicy _honorRollPolicy;
    private readonly GradeReportExporter _reportExporter;

    public GradeBook()
    {
        _averageCalculator = new GradeAverageCalculator();
        _letterPolicy = new GradeLetterPolicy();
        _honorRollPolicy = new HonorRollPolicy();
        _reportExporter = new GradeReportExporter();
    }

    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(score));

        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }

        list.Add(score);
    }

    public decimal Average(string studentId)
    {
        if (!_scores.TryGetValue(studentId, out var list))
            return 0m;

        return _averageCalculator.Calculate(list);
    }

    public string Letter(string studentId)
    {
        return _letterPolicy.GetLetter(
            Average(studentId));
    }

    public bool MeetsHonorRoll(string studentId)
    {
        var average = Average(studentId);
        var letter = Letter(studentId);

        return _honorRollPolicy.MeetsHonorRoll(
            average,
            letter);
    }

    public string TranscriptPlain(
        string studentId,
        string fullName)
    {
        return _reportExporter.BuildTranscript(
            studentId,
            fullName,
            Average(studentId),
            Letter(studentId),
            MeetsHonorRoll(studentId));
    }

    public string ExportCsv()
    {
        return _reportExporter.ExportCsv(
            _scores,
            _averageCalculator,
            _letterPolicy,
            _honorRollPolicy);
    }
}