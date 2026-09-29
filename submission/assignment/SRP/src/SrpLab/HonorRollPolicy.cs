namespace SrpLab;

public sealed class HonorRollPolicy
{
    public bool MeetsHonorRoll(decimal average, string letter)
    {
        return average >= 85 &&
               (letter is "A" or "B");
    }
}