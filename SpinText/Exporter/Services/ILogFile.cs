using SpinText.Exporter.Models;
using System.Text;

namespace SpinText.Exporter.Services;

public interface ILogFile
{
    byte[] CreateFile(ILogData log);
}

public class TxtLogFile : ILogFile
{
    public byte[] CreateFile(ILogData log)
    {
        string content = ToText(log.Data);
        //return Encoding.UTF8.GetBytes(content);
        return Encoding.ASCII.GetBytes(content);
    }

    public string ToText(List<string> log)
    {
        return String.Join(Environment.NewLine, log);
    }
}
