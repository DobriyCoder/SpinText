using DobriyCoder.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using SpinText.Blocks.DB;
using SpinText.Languages.Models;
using SpinText.Models;
using SpinText.Types;

namespace SpinText.Blocks.Services;

public class BlocksManager
{
    Db _db;
    DbSet<BlockData> _blocks => _db.Blocks;

    public BlocksManager(DBFactory db_factory)
    {
        this._db = db_factory.Create();
    }

    public IEnumerable<BlockData> GetBlocks(ELanguage lang, EType type)
    {
        return _blocks
            .Where(i => i.Language == lang && i.TemplatesType == type)
            .ToArray();
    }
    public IEnumerable<BlockData> GetBlocks(EType type)
    {
        return _blocks
            .Where(i => i.TemplatesType == type)
            .ToArray();
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
    public void Clear(ELanguage lang, EType type)
    {
        _blocks.RemoveRange(_blocks.Where(i => i.Language == lang && i.TemplatesType == type));
        _db.SaveChanges();
    }
    public void SaveBlocks(ELanguage lang, EType type, IEnumerable<BlockData> blocks)
    {
        Clear(lang, type);
        _blocks.AddRange(blocks);
        _db.SaveChanges();
    }
}
