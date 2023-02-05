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
        DbSet<HTPairsData> _htTable => _db.PairsTemplates;

        public HTPairsManager(DBFactory factory)
        {
            _db = factory.Create();
        }

        public IEnumerable<HTPairsData> GetHTs()
        {
            return _htTable;
        }
        public HTPairsData? GetHT(int index, ELanguage language)
        {
            return _htTable
                .Where(i => i.Language == language)
                .Skip(index)
                .FirstOrDefault();
        }
        public void AddHT(HTPairsData data)
        {
            _htTable.Add(data);
            _db.SaveChanges();
            _db.Entry<HTPairsData>(data).State = EntityState.Detached;
        }
        public void AddHTs(IEnumerable<HTPairsData> data)
        {
            _htTable.AddRange(data);
            _db.SaveChanges();
            Detach(data);
        }

        public void ClearHTs()
        {
            _htTable.RemoveRange(_htTable);
            _db.SaveChanges();
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
