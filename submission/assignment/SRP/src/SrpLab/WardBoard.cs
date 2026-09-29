namespace SrpLab;

public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();

    private readonly AcuityScorer _acuityScorer;
    private readonly PagerAlertLog _pagerAlertLog;
    private readonly HandoffNoteBuilder _handoffNoteBuilder;
    private readonly CensusCsvExporter _censusCsvExporter;

    public WardBoard()
    {
        _acuityScorer = new AcuityScorer();
        _pagerAlertLog = new PagerAlertLog();
        _handoffNoteBuilder = new HandoffNoteBuilder();
        _censusCsvExporter = new CensusCsvExporter();
    }

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0)
            throw new ArgumentOutOfRangeException(nameof(bed));

        if (string.IsNullOrWhiteSpace(patientId))
            throw new ArgumentException("patient required");

        var normalizedPatientId = patientId.Trim().ToUpperInvariant();
        var acuity = _acuityScorer.Score(heartRate, spo2);

        _bedPatient[bed] = normalizedPatientId;
        _vitalsScore[bed] = acuity;

        if (acuity >= 8)
            _pagerAlertLog.Add($"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}");
    }

    public int ScoreAcuity(int heartRate, int spo2)
    {
        return _acuityScorer.Score(heartRate, spo2);
    }

    public string BuildHandoffNote(int bed)
    {
        return _handoffNoteBuilder.Build(bed, _bedPatient, _vitalsScore);
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        return _pagerAlertLog.Drain();
    }

    public string ExportCensusCsv()
    {
        return _censusCsvExporter.Export(_bedPatient, _vitalsScore);
    }
}