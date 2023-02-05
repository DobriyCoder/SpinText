using SpinText.HT.DB;
using SpinText.HT.Models;
using SpinText.Models;
using System.IO;

namespace SpinText.Exporter.Services;

public class ExporterProvider
{
    IExportFile _exporter;
    ILogFile _logger;
    public ExporterProvider (IExportFile exporter, ILogFile logger)
    {
        this._exporter = exporter;
        this._logger = logger;
    }
    public byte[] CreateExportHTFile(IEnumerable<HTBaseData> data)
    {
        return _exporter.CreateFile(new HTdataToExportData(data));
    }

    public byte[] CreateLogFile(HTGeneratedLogData data)
    {
        return _logger.CreateFile(new HTGeneratedLogToILogData(data));
    }
}
