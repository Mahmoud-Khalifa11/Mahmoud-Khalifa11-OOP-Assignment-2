namespace SrpLab;

public sealed class GradeReportExporter
{
    public string BuildTranscript(
        string studentId,
        string fullName,
        decimal average,
        string letter,
        bool honorRoll)
    {
        return $"TRANSCRIPT\n" +
               $"Student: {fullName} ({studentId})\n" +
               $"Average: {average}\n" +
               $"Letter: {letter}\n" +
               $"Honor: {honorRoll}\n";
    }

    public string ExportCsv(
        Dictionary<string, List<decimal>> scores,
        GradeAverageCalculator averageCalculator,
        GradeLetterPolicy letterPolicy,
        HonorRollPolicy honorRollPolicy)
    {
        var rows = new List<string>
        {
            "studentId,average,letter,honor"
        };

        foreach (var id in scores.Keys.OrderBy(x => x))
        {
            var average = averageCalculator.Calculate(scores[id]);
            var letter = letterPolicy.GetLetter(average);
            var honor = honorRollPolicy.MeetsHonorRoll(
                average,
                letter);

            rows.Add(
                $"{id},{average},{letter},{(honor ? 1 : 0)}");
        }

        return string.Join('\n', rows);
    }
}