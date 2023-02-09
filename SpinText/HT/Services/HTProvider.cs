using DobriyCoder.Core.Events;
using SpiningText.Models;
using SpinText.Blocks.Services;
using SpinText.Coins.Services;
using SpinText.Generator.Services;
using SpinText.HT.DB;
using SpinText.HT.Models;
using SpinText.Models;
using SpinText.Types;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

namespace SpinText.HT.Services;

public class HTProvider
{
    HTGeneratingStatus _status;
    HTGeneratedLogData _log;
    BlocksManager _blocks;
    HTManager _ht;
    IGenerator _generator;
    bool _working = false;
    DBFactory _dbFactory;
    EventsBus _events;
    Queue<Task> _tasks;
    public HTProvider(EventsBus events, DBFactory dbFactory, BlocksManager blocks, HTManager ht, IGenerator generator)
    {
        this._blocks = blocks;
        this._ht = ht;
        this._generator = generator;
        this._log = new HTGeneratedLogData();
        this._dbFactory = dbFactory;
        this._events = events;
        _tasks = new Queue<Task>();

        _events.Add(OnCoinsAdded, EEvent.CoinsAdded);
    }
    public void Stop()
    {
        _working = false;
    }
    public HTGeneratingStatus Add(IEnumerable<string> urls)
    {
        CreateStatus(urls.Count());

        Task.Run(async () =>
        {
            var urls_data = GetUrlsData(urls);
            await AddAsync(urls_data);
        });

        return GetStatus();
    }
    public HTGeneratingStatus Add(int count, EType type)
    {
        Task t = new Task(async () =>
        {
            CreateStatus(count);
            await AddAsync(count, type);

            if (_tasks.Count > 0)
            {
                _tasks.Dequeue().Start();
            }
        });

        _tasks.Enqueue(t);

        if (!_working)
        {
            CreateStatus(count);
            _tasks.Dequeue().Start();
            Thread.Sleep(300);
        }

        return GetStatus();
    }
    public void Add(string key, Dictionary<string, string> vars)
    {
        CreateStatus(1);
        var url_data = new UrlData(key, vars);
        AddAsync(new UrlData[] { url_data }).Wait();
    }
    public async Task AddAsync(int count, EType type)
    {
        _working = true;
        var blocks = _blocks.GetBlocks(type);
        List<HTData> group = new List<HTData>();
        int i = 1;

        try
        {
            for (int num = 0; num < count; num++)
            {
                if (!_working)
                {
                    CreateStatus(0);
                    break;
                }

                var templates = _generator.GenerateHT(type, null, blocks, AddToLog);

                group.AddRange(templates);
                ChangeStatus(1);

                if (i++ >= 227)
                {
                    _ht.AddHTs(group);
                    
                    foreach (var g in group)
                    {
                        g.Dispose();
                    }
                    
                    group.Clear();
                    GC.SuppressFinalize(templates);
                    GC.Collect();
                    GC.WaitForFullGCComplete();
                    i = 0;
                }
            }

            _ht.AddHTs(group);
            group.Clear();
            _ht.RefreshDb();
            _dbFactory.Remove();
            _working = false;
        }
        catch (Exception ex)
        {
            CreateStatus(0);
        }
    }
    public async Task AddAsync(IEnumerable<UrlData> urls)
    {
        _working = true;
        var blocks = _blocks.GetBlocks();
        List<HTData> group = new List<HTData>();
        int i = 1;

        try
        {
            foreach (var url in urls)
            {
                if (url.PageKey is null) continue;

                var vars = new STVarsDictionary(url.Data);
                var templates = _generator.GenerateHT(null, vars, blocks, AddToLog);

                group.AddRange(templates);
                ChangeStatus(1);

                if (i++ >= 227)
                {
                    _ht.AddHTs(group);
                    group.Clear();
                    i = 0;
                }

                if (!_working)
                {
                    CreateStatus(0);
                    break;
                }
            }

            _ht.AddHTs(group);
            group.Clear();
            _dbFactory.Remove();
        }
        catch (Exception ex)
        {
            CreateStatus(0);
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
    IEnumerable<UrlData> GetUrlsData(IEnumerable<string> urls)
    {
        return urls.Select(i => new UrlData(i));
    }
    void ChangeStatus(int count = 1)
    {
        _status.Progress.Position += count;
    }
    void AddToLog(HTData data)
    {
        _log.Items.Add(new HTGeneratedLogItem()
        {
            Type = data.TemplateType,
            Language = data.Language,
            Status = EHTGeneratedLogStatus.Ok,
        });
    }

    async void OnCoinsAdded (object sender, EventArgs e)
    {
        await Task.Run(() => { });

        var coins = (CoinsManager)sender;
        int coins_count = coins.GetCoinsCount() + 1;
        int pairs_count = coins.GetPairsCount(coins_count);
        int coins_ht_count = _ht.GetCount(EType.Coin);
        int pairs_ht_count = _ht.GetCount(EType.Pair);
        int need_coins_count = coins_count - coins_ht_count;
        int need_pairs_count = pairs_count - pairs_ht_count;

        Add(need_coins_count, EType.Coin);
        Add(need_pairs_count, EType.Pair);
    }
}
