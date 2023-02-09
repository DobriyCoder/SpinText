using Microsoft.EntityFrameworkCore;
using SpinText.HT.DB;
using SpinText.Languages.Models;
using SpinText.Models;
using SpinText.Types;

namespace SpinText.HT.Services
{
    public class HTPairsManager
    {
        Db _db;
        DBFactory _factory;
        DbSet<HTPairsData> _htTable => _db.PairsTemplates;

        public HTPairsManager(DBFactory factory)
        {
            _db = factory.Create();
            _factory = factory;
        }

        public IEnumerable<HTPairsData> GetHTs()
        {
            return _htTable;
        }
        public HTPairsData? GetHT(int index, ELanguage language)
        {
            RefreshDb();
            return _htTable
                .Where(i => i.Language == language)
                .Skip(index)
                .FirstOrDefault();
        }
        public void AddHT(HTPairsData data)
        {
            RefreshDb();
            _htTable.Add(data);
            _db.SaveChanges();
            _db.Entry<HTPairsData>(data).State = EntityState.Detached;
        }
        public void AddHTs(IEnumerable<HTPairsData> data)
        {
            RefreshDb();
            _htTable.AddRange(data);
            _db.SaveChanges();
            Detach(data);
        }

        public void ClearHTs()
        {
            RefreshDb();
            _db.Database.ExecuteSqlRaw("DELETE FROM PairsTemplates");
            _db.SaveChanges();
        }
        public void RefreshDb ()
        {
            _db.Dispose();
            _db = _factory.Create();
        }
        public int GetCount()
        {
            RefreshDb();
            return _htTable.Count();
        }
        void Detach(IEnumerable<HTPairsData> data)
        {
            foreach (var item in data)
            {
                _db.Entry<HTPairsData>(item).State = EntityState.Detached;
            }
        }
    }
}
