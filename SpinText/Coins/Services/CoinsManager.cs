using Microsoft.EntityFrameworkCore;
using SpinText.Coins.Models;
using SpinText.HT.DB;
using SpinText.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpinText.Coins.Services;

public class CoinsManager
{
    Db _db;
    DbSet<Coin> _coins => _db.Coins;

    public CoinsManager(DBFactory db_factory)
    {
        _db = db_factory.Create();
    }

    public IEnumerable<Coin> GetCoins() => _db.Coins;
    public Coin? GetCoin(string name) =>
        _coins
            .Where(i => i.Name == name)
            .FirstOrDefault();

    public IEnumerable<Coin> GetCoinPair(string name1, string name2) =>
        _coins
            .Where(i => i.Name == name1 || i.Name == name2)
            .Take(2);

    public int? GetCoinIndex(string name)
    {
        name = name.ToLower();

        int? index = _coins
            .FromSqlRaw("SELECT row_number() OVER (ORDER BY Id) as Id, Name from Coins as c")
            .Where(i => i.Name.ToLower() == name)
            .FirstOrDefault()
            ?.Id;

        return index is null ? null : index - 1;
    }
    public int[] GetCoinsIndexes(string from, string to)
    {
        from = from.ToLower();
        to = to.ToLower();

        return _coins
            .FromSqlRaw("SELECT row_number() OVER (ORDER BY Id) as Id, Name from Coins as c")
            .Where(i => i.Name.ToLower() == from || i.Name.ToLower() == to)
            .Select(i => i.Id - 1)
            .ToArray();
    }
    public int? GetPairIndex(string from, string to)
    {
        var indexes = GetCoinsIndexes(from, to);
        if (indexes.Length < 2 || indexes[0] == indexes[1]) return null;

        int i1 = indexes[0];
        int i2 = indexes[1];
        int count = _coins.Count();
        int n = i1;
        int pre_count = 0;

        for (int i = 1; i <= n; i++)
        {
            pre_count += count - 1;
        }

        int index = pre_count + (i2 - i1) - 1;

        return index;
    }
    public int? GetPairIndex(string name)
    {
        string[] pair = name.Split('-').Select(i => i.Trim()).ToArray();
        if (pair.Length < 2) return null;
        return GetPairIndex(pair[0], pair[1]);
    }

    public void Add(Coin coin)
    {
        if (_coins.Any(i => i.Name == coin.Name)) return;

        _coins.Add(coin);
        _db.SaveChanges();
        _db.Entry<Coin>(coin).State = EntityState.Detached;
    }

    public void Add(IEnumerable<Coin> coins)
    {
        foreach (Coin coin in coins)
        {
            if (_coins.Any(i => i.Name == coin.Name)) continue;
            _coins.Add(coin);
        }

        _db.SaveChanges();
        Detach(coins);
    }

    public void Add(IEnumerable<string> names)
    {
        Add(names
            .Select(i => i.Trim())
            .Where(i => !string.IsNullOrEmpty(i))
            .Select(i => new Coin() { Name = i }));
    }
    public void Add(string names)
    {
        Add(names.Split('\n'));
    }

    public void Clear() 
    {
        _coins.RemoveRange(_coins);
    }

    public void Remove(Coin coin)
    {
        _coins.Remove(coin);
        _db.SaveChanges();
    }

    void Detach(IEnumerable<Coin> coins)
    {
        foreach(Coin coin in coins)
        {
            _db.Entry<Coin>(coin).State = EntityState.Detached;
        }
    }
}
