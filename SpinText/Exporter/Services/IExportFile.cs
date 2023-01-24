using SpinText.Exporter.Models;
using System.IO;

namespace SpinText.Exporter.Services;

public interface IExportFile
{
    /*TODO: IFileStream*/ object CreateFile(IExportData export_data);
    /*TODO: IFileStream*/ object CreateFile(ILogData log);
}
