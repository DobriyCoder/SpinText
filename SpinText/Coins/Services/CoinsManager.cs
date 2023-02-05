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
        Add(names.Select(i => new Coin() { Name = i }));
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
