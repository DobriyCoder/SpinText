using Microsoft.EntityFrameworkCore;
using SpinText.HT.DB;
using SpinText.Languages.Models;
using SpinText.Models;
using SpinText.Types;

namespace SpinText.HT.Services
{
    public class HTCommonManager
    {
        Db _db;
        DBFactory _factory;
        DbSet<HTData> _htTable => _db.Templates;

        public HTCommonManager(DBFactory factory)
        {
            _db = factory.Create();
            _factory = factory;
        }

        public IEnumerable<HTData> GetHTs()
        {
            RefreshDb();
            return _htTable;
        }
        public HTData? GetHT(int index, EType type, ELanguage language)
        {
            RefreshDb();
            return _htTable
                .Where(i => i.Language == language && i.TemplateType == type)
                .Skip(index)
                .FirstOrDefault();
        }
        public void AddHT(HTData data)
        {
            RefreshDb();
            _htTable.Add(data);
            _db.SaveChanges();
            _db.Entry<HTData>(data).State = EntityState.Detached;
        }
        public void AddHTs(IEnumerable<HTData> data)
        {
            RefreshDb();
            _htTable.AddRange(data);
            _db.SaveChanges();
            Detach(data);
        }

        public void ClearHTs()
        {
            RefreshDb();
            _htTable.RemoveRange(_htTable);
            _db.SaveChanges();
        }
        public void RefreshDb()
        {
            _db.Dispose();
            _db = _factory.Create();
        }
        public int GetCount(EType? type)
        {
            RefreshDb();
            return type is null
                ? _htTable.Count()
                : _htTable.Count(i => i.TemplateType == type);
        }
        void Detach(IEnumerable<HTData> data)
        {
            foreach (var item in data)
            {
                _db.Entry<HTData>(item).State = EntityState.Detached;
            }
        }
    }
}
