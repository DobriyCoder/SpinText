using SpinText.Exporter.Models;
using SpinText.HT.Models;

namespace SpinText.Models;

public class HTGeneratedLogToILogData : ILogData
{
    HTGeneratedLogData _data;
    public HTGeneratedLogToILogData (HTGeneratedLogData data)
    {
        _data = data;
    }
    public List<string> Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
