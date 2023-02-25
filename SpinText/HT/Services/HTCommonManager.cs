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
        DbSet<HTData> _htTable
        {
            get
            {
                try
                {
                    return _db.Templates;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    RefreshDb();
                    
                    try
                    {
                        return _db.Templates;
                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine(ex1.Message);
                        return null;
                    }
                }
            }
        }

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

            uint id = _htTable.FirstOrDefault()?.Id ?? 0;
            uint shift = id + (uint)index;

            return _htTable
                ?.Where(i => i.Language == language && i.TemplateType == type && i.Id == shift)
                ?.FirstOrDefault();
        }
        public void AddHT(HTData data)
        {
            RefreshDb();
            _htTable?.Add(data);
            _db.SaveChanges();
            _db.Entry<HTData>(data).State = EntityState.Detached;
        }
        public void AddHTs(IEnumerable<HTData> data)
        {
            RefreshDb();
            _htTable?.AddRange(data);
            _db.SaveChanges();
            Detach(data);
        }

        public void ClearHTs()
        {
            RefreshDb();
            _htTable?.RemoveRange(_htTable);
            _db.SaveChanges();
        }
        public void RefreshDb()
        {
            try
            {
                _db.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }
            _db = _factory.Create();
        }
        public int GetCount(EType? type)
        {
            RefreshDb();
            int count = 0;
            try
            {
                count = type is null
                    ? _htTable?.Count() ?? 0
                    : _htTable?.Count(i => i.TemplateType == type) ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }

            return count;
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
