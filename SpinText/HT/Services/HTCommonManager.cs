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
        DbSet<HTData> _htTable => _db.Templates;

        public HTCommonManager(DBFactory factory)
        {
            _db = factory.Create();
        }

        public IEnumerable<HTData> GetHTs()
        {
            return _htTable;
        }
        public HTData? GetHT(int index, EType type, ELanguage language)
        {
            return _htTable
                .Where(i => i.Language == language && i.TemplateType == type)
                .Skip(index)
                .FirstOrDefault();
        }
        public void AddHT(HTData data)
        {
            _htTable.Add(data);
            _db.SaveChanges();
            _db.Entry<HTData>(data).State = EntityState.Detached;
        }
        public void AddHTs(IEnumerable<HTData> data)
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

        void Detach(IEnumerable<HTData> data)
        {
            foreach (var item in data)
            {
                _db.Entry<HTData>(item).State = EntityState.Detached;
            }
        }
    }
}
