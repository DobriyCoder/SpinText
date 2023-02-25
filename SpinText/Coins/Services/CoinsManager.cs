using DobriyCoder.Core.Events;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    EventsBus _events;

    public CoinsManager(DBFactory db_factory, EventsBus events)
    {
        _db = db_factory.Create();
        _events = events;
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

        string query = @"
            SET @row_number=0;
            SELECT `s`.`Id`, `s`.`Name`
            FROM (
                SELECT (@row_number:=@row_number + 1) as Id, Name from Coins as c
            ) AS `s`
            WHERE (LOWER(`s`.`Name`) = """ + name + @""")
            LIMIT 1;";

        int? index = _coins
            .FromSqlRaw(query)
            .ToArray()
            .FirstOrDefault()
            ?.Id;

        return index is null ? null : index - 1;
    }
    public int[] GetCoinsIndexes(string from, string to)
    {
        from = from.ToLower();
        to = to.ToLower();

        string query = @"
            SET @row_number=0;
            SELECT `s`.`Id`, `s`.`Name`
            FROM (
                SELECT (@row_number:=@row_number + 1) as Id, Name from Coins as c
            ) AS `s`
            WHERE (LOWER(`s`.`Name`) = """ + from + @""" OR LOWER(`s`.`Name`) = """ + to + @""")
            LIMIT 2;";

        return _coins
            .FromSqlRaw(query)
            .ToArray()
            .Select(i => i.Id - 1)
            .ToArray();
    }
    public int? GetPairIndex(string from, string to)
    {
        if (from == to) return null;

        int i1 = GetCoinIndex(from) ?? -1; if (i1 == -1) return null;
        int i2 = GetCoinIndex(to) ?? -1; if (i2 == -1) return null;

        int count = _coins.Count();

        /*
         * eth 0
         * btc 1
         * zoc 2
         * usd 3
         * eur 4
         * 
         * eth-btc 0-1 = 0
         * eth-zoc 0-2 = 1
         * eth-usd 0-3 = 2
         * eth-eur 0-4 = 3
         * btc-eth 1-0 = 4
         * btc-zoc 1-2 = 5
         * btc-usd 1-3 = 6
         * btc-eur 1-4 = 7
         * zoc-eth 2-0 = 8
         * zoc-btc 2-1 = 9
         * zoc-usd 2-3 = 10
         * zoc-eur 2-4 = 11
         * usd-eth 3-0 = 12
         * usd-btc 3-1 = 13
         * usd-zoc 3-2 = 14
         * usd-eur 3-4 = 15
         * eur-eth 4-0 = 16
         * eur-btc 4-1 = 17
         * eur-zoc 4-2 = 18
         * eur-usd 4-3 = 19
         * 
         * zoc-usd 2-3 = 10
         * i1 = 2; i2 = 1; count = 4;
         * index = i1*(count - 1) + (i1 < i2 ? i2 - 1 : i2)
         * pairs_count = count*(count - 1)
         */

        int index = i1 * (count - 1) + (i1 < i2 ? i2 - 1 : i2);

        /*int n = i1;
        int pre_count = 0;

        for (int i = 1; i <= n; i++)
        {
            pre_count += count - 1;
        }

        int index = pre_count + Math.Abs(i2 - i1) - 1;*/

        return index;
    }
    public int? GetPairIndex(string name)
    {
        string[] pair = name.Split("--").Select(i => i.Trim()).ToArray();
        if (pair.Length < 2) return null;
        return GetPairIndex(pair[0], pair[1]);
    }

    public void Add(Coin coin)
    {
        if (_coins.Any(i => i.Name == coin.Name)) return;

        _coins.Add(coin);
        _db.SaveChanges();
        _db.Entry<Coin>(coin).State = EntityState.Detached;

        _events.Trigger(this, EEvent.CoinsAdded);
    }

    public void Add(IEnumerable<Coin> coins)
    {
        int added = 0;
        foreach (Coin coin in coins)
        {
            if (_coins.Any(i => i.Name == coin.Name)) continue;
            _coins.Add(coin);
            added++;
        }

        _db.SaveChanges();
        Detach(coins);

        if (added > 0)
            _events.Trigger(this, EEvent.CoinsAdded);
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
    public int GetCoinsCount()
    {
        return _coins.Count();
    }
    public int GetPairsCount(int? coins_count)
    {
        int count = coins_count ?? _coins.Count();
        /*int i2 = count - 1;
        int i1 = i2 - 1;
        int n = i1;
        int pre_count = 0;

        for (int i = 1; i <= n; i++)
        {
            pre_count += count - 1;
        }

        int pairs_count = pre_count + (i2 - i1);*/

        int pairs_count = count * (count - 1);

        return pairs_count;
    }

    void Detach(IEnumerable<Coin> coins)
    {
        foreach(Coin coin in coins)
        {
            _db.Entry<Coin>(coin).State = EntityState.Detached;
        }
    }

}
