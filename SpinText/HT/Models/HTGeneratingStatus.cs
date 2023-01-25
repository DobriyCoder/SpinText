using SpinText.Models;

namespace SpinText.HT.Models;

public class HTGeneratingStatus
{
    public HTGeneratingStatus(int max)
    {
        Progress = new ProgressBar();
        GeneratingCount = max;
    }
    public int GeneratingCount
    {
        get => Progress.Max;
        set => Progress.Max = value;
    }
    public bool IsCompleted => Progress.Position >= Progress.Max;
    public ProgressBar Progress { get; set; }
}
