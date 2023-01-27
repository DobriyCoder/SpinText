using SpinText.Exporter.Models;
using SpinText.HT.DB;

namespace SpinText.Models;

public class HTdataToExportData : IExportData
{
    IEnumerable<HTData> _data;
    public HTdataToExportData(IEnumerable<HTData> data)
    {
        _data = data;
    }

    public List<List<string>> Data
    {
        get 
        {
            List<List<string>> content = new List<List<string>>();
            if (_data is null || !_data.Any()) return content;
            
            content.Add(GetTitleLine());

            foreach (var item in _data)
            {
                content.Add(GetLine(item));
            }

            return content;
        }
    }

    public List<string> GetLine(HTData data)
    {
        return data
            .GetType()
            .GetProperties()
            .Select(i => i.GetValue(data)?.ToString() ?? "")
            .ToList();
    }
    public List<string> GetTitleLine()
    {
        return _data
            .First()
            .GetType()
            .GetProperties()
            .Select(i => i.Name)
            .ToList();
    }
}
