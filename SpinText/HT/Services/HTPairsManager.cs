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
        DbSet<HTPairsData> _htTable
        {
            get
            {
                try
                {
                    return _db.PairsTemplates;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    RefreshDb();

                    try
                    {
                        return _db.PairsTemplates;
                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine(ex1.Message);
                        return null;
                    }
                }
            }
        }

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

            uint id = _htTable.FirstOrDefault()?.Id ?? 0;
            uint shift = id + (uint)index;

            return _htTable
                ?.Where(i => i.Language == language && i.Id == shift)
                ?.FirstOrDefault();
        }
        public void AddHT(HTPairsData data)
        {
            RefreshDb();
            _htTable?.Add(data);
            _db.SaveChanges();
            _db.Entry<HTPairsData>(data).State = EntityState.Detached;
        }
        public void AddHTs(IEnumerable<HTPairsData> data)
        {
            RefreshDb();
            _htTable?.AddRange(data);
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
            try
            {
                _db.Dispose();
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }
            _db = _factory.Create();
        }
        public int GetCount()
        {
            //RefreshDb();
            try
            {

                if (_htTable == null) return 0;
                int c = 0;
                using (var db = _factory.Create())
                {
                    var t = _htTable;
                    c = t?.Count() ?? 0;
                }
                return c;
            }
            catch (Exception ex)
            {
                return 0;
            }
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
