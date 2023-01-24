using SpinText.HTProvider.Models;
using System.Security.Cryptography.X509Certificates;

namespace SpinText.HTProvider.Services;

public class HTProvider
{
    public void Add(string[] urls)
    {

    }
    public HTGeneratingStatus GetStatus()
    {
        return default;
    }
    public HTGeneratedLogData GetLastLog()
    {
        return default;
    }
    IEnumerable<UrlData> GetUrlsData(string[] urls)
    {
        return default;
    }
    void ChangeStatus()
    {

    }
}
