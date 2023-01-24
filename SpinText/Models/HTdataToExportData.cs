using SpinText.Exporter.Models;
using SpinText.HTProvider.DB;

namespace SpinText.Models;

public class HTdataToExportData : IExportData
{
    HTData[] _data;
    public HTdataToExportData(HTData[] data)
    {
        _data = data;
    }

    public List<List<string>> Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
