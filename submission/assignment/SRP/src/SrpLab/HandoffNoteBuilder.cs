namespace SrpLab;

public sealed class HandoffNoteBuilder
{
    public string Build(
        int bed,
        Dictionary<int, string> bedPatient,
        Dictionary<int, int> vitalsScore)
    {
        if (!bedPatient.TryGetValue(bed, out var patient))
            return $"Bed {bed}: empty";

        var acuity = vitalsScore[bed];

        var tone = acuity >= 8
            ? "ESCALATE"
            : acuity >= 4
                ? "WATCH"
                : "STABLE";

        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }
}