using SpiningText.Models;
using SpinText.Blocks.Services;
using SpinText.Generator.Services;
using SpinText.HT.DB;
using SpinText.HT.Models;
using System.Security.Cryptography.X509Certificates;

namespace SpinText.HT.Services;

public class HTProvider
{
    HTGeneratingStatus _status;
    HTGeneratedLogData _log;
    BlocksManager _blocks;
    HTManager _ht;
    IGenerator _generator;

    public HTProvider(BlocksManager blocks, HTManager ht, IGenerator generator)
    {
        this._blocks = blocks;
        this._ht = ht;
        this._generator = generator;
        this._log = new HTGeneratedLogData();
    }

    public HTGeneratingStatus Add(string[] urls)
    {
        var urls_data = GetUrlsData(urls);
        CreateStatus(urls_data.Count());
        Task.Run(() => AddAsync(urls_data));

        return GetStatus();
    }
    public void Add(string key, Dictionary<string, string> vars)
    {
        CreateStatus(1);
        var url_data = new UrlData(key, vars);
        AddAsync(new UrlData[] { url_data }).Wait();
    }
    public async Task AddAsync(IEnumerable<UrlData> urls)
    {
        var blocks = _blocks.GetBlocks();

        foreach (var url in urls)
        {
            if (url.PageKey is null) continue;

            var vars = new STVarsDictionary(url.Data);

            var templates = _generator.GenerateHT(url.PageKey, vars, blocks, AddToLog);

            _ht.AddHTs(templates.ToArray());
            ChangeStatus();
        }
    }
    
    public HTGeneratingStatus CreateStatus(int max)
    {
        _status = new HTGeneratingStatus(max);
        return GetStatus();
    }
    public HTGeneratingStatus GetStatus()
    {
        return _status;
    }
    public HTGeneratedLogData GetLastLog()
    {
        return _log;
    }
    IEnumerable<UrlData> GetUrlsData(string[] urls)
    {
        return urls.Select(i => new UrlData(i));
    }
    void ChangeStatus()
    {
        _status.Progress.Position++;
    }
    void AddToLog(HTData data)
    {
        _log.Items.Add(new HTGeneratedLogItem()
        {
            Key = data.PageKey,
            Language = data.Language,
            Status = EHTGeneratedLogStatus.Ok,
        });
    }
}
