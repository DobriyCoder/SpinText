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
    public List<string> Data => _data.Items.Select(i => ItemToString(i)).ToList();
    
    public string ItemToString(HTGeneratedLogItem item)
    {
        var values = item
            .GetType()
            .GetProperties()
            .Select(i => i.GetValue(item)?.ToString() ?? "");

        return String.Join("\t| ", values);
    }
}
