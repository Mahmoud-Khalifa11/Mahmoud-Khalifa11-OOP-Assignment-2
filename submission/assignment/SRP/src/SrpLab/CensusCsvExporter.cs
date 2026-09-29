namespace SrpLab;

public sealed class CensusCsvExporter
{
    public string Export(
        Dictionary<int, string> bedPatient,
        Dictionary<int, int> vitalsScore)
    {
        var lines = new List<string>
        {
            "bed,patient,acuity"
        };

        foreach (var bed in bedPatient.Keys.OrderBy(x => x))
        {
            lines.Add(
                $"{bed},{bedPatient[bed]},{vitalsScore[bed]}"
            );
        }

        return string.Join('\n', lines);
    }
}