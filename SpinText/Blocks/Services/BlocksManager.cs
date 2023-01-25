using DobriyCoder.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using SpinText.Blocks.DB;
using SpinText.Languages.Models;
using SpinText.Models;

namespace SpinText.Blocks.Services;

public class BlocksManager
{
    Db _db;
    DbSet<BlockData> _blocks => _db.Blocks;

    public BlocksManager(DBFactory db_factory)
    {
        this._db = db_factory.Create();
    }

    public IEnumerable<BlockData> GetBlocks(ELanguage lang)
    {
        return _blocks.Where(i => i.Language == lang).ToArray();
    }
    public IEnumerable<BlockData> GetBlocks()
    {
        return _blocks.ToArray();
    }
    public void Clear()
    {
        _blocks.RemoveRange(_blocks);
        _db.SaveChanges();
    }
    public void Clear(ELanguage lang)
    {
        _blocks.RemoveRange(_blocks.Where(i => i.Language == lang));
        _db.SaveChanges();
    }
    public void SaveBlocks(ELanguage lang, IEnumerable<BlockData> blocks)
    {
        Clear(lang);
        _blocks.AddRange(blocks);
        _db.SaveChanges();
    }
}
