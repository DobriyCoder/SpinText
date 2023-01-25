using Microsoft.EntityFrameworkCore;
using SpinText.HT.DB;
using SpinText.Models;

namespace SpinText.HT.Services;

public class HTManager
{
    Db _db;
    DbSet<HTData> _htTable => _db.Templates;

    public HTManager(DBFactory db_factory)
    {
        _db = db_factory.Create();
    }
    public IEnumerable<HTData> GetHTs()
    {
        return _htTable;
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
        foreach(var ht in data)
        {
            AddHT(ht);
        }
    }
}
