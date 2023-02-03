using Microsoft.EntityFrameworkCore;
using SpinText.HT.DB;
using SpinText.Languages.Models;
using SpinText.Models;

namespace SpinText.HT.Services;

public class HTManager
{
    Db _db;
    DbSet<HTData> _htTable => _db.Templates;
    Task bisy;

    public HTManager(DBFactory db_factory)
    {
        _db = db_factory.Create();
    }
    public IEnumerable<HTData> GetHTs()
    {
        return _htTable;
    }
    public HTData? GetHT(string page_key, ELanguage language)
    {
        return _htTable
            .FirstOrDefault(
                i => i.PageKey == page_key 
                && i.Language == language);
    }
    public void AddHT(HTData data)
    {
        var ht = _htTable.FirstOrDefault(i => i.PageKey == data.PageKey && i.Language == data.Language);
        
        if (ht == null)
        {
            _htTable.Add(data);
        }
        else
        {
            ht.Template = data.Template;
        }

        _db.SaveChanges();
    }
    public void AddHTs(IEnumerable<HTData> data)
    {
        var list = data.ToList();
        var where_list = list.Select(i => $"(PageKey=\"{i.PageKey}\" AND Language={(int)i.Language})");
        string where = String.Join(" OR ", where_list);
        if (where.Length == 0) return;
        string query = $"DELETE FROM Templates WHERE {where};";
        
        try
        {
            _db.Database.ExecuteSqlRaw(query);

            _htTable.AddRange(data);
            _db.SaveChanges();
            Detach(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public void ClearHTs ()
    {
        _htTable.RemoveRange(_htTable);
        _db.SaveChanges();
    }

    void Detach (IEnumerable<HTData> data)
    {
        foreach (var item in data)
        {
            _db.Entry<HTData>(item).State = EntityState.Detached;
        }
    }
}
