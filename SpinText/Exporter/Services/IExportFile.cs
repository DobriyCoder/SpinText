using DobriyCoder.Core.Exceptions;
using SpinText.Exporter.Models;
using System.IO;
using System.Text;

namespace SpinText.Exporter.Services;

public interface IExportFile
{
    byte[] CreateFile(IExportData export_data);
}

public class CsvExportFile : IExportFile
{
    public byte[] CreateFile(IExportData export_data)
    {
        string content = ToCsv(export_data.Data);
        content = content.Replace("‘", "'");
        content = content.Replace("’", "'");

        return Encoding.Convert(Encoding.UTF8, Encoding.ASCII, Encoding.UTF8.GetBytes(content));
    }
    public string ToCsv (List<List<string>> data)
    {
        var lines = data.Select(i => String.Join(";", i.Select(j => $"\"{j.Replace("\"", "\"\"")}\"")));
        return String.Join("\n", lines);
    }
}
