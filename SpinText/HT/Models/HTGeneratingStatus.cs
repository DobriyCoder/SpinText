using SpinText.Models;

namespace SpinText.HT.Models;

public class HTGeneratingStatus
{
    int _count = 0;
    public int Count => _count + Progress.Position;
    public HTGeneratingStatus(int max, int count)
    {
        Progress = new ProgressBar();
        GeneratingCount = max;
        _count = count;
    }
    public int GeneratingCount
    {
        get => Progress.Max;
        set => Progress.Max = value;
    }
    public bool IsCompleted => Progress.Position >= Progress.Max;
    public ProgressBar Progress { get; set; }
}
