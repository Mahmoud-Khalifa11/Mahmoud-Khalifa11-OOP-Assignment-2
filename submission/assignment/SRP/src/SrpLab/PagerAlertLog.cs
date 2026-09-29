namespace SrpLab;

public sealed class PagerAlertLog
{
    private readonly List<string> _messages = new();

    public void Add(string message)
    {
        _messages.Add(message);
    }

    public IReadOnlyList<string> Drain()
    {
        var copy = _messages.ToList();
        _messages.Clear();
        return copy;
    }
}