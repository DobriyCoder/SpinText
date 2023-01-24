using SpinText.Blocks.DB;
using SpinText.Languages.Models;

namespace SpinText.Blocks.Services;

public class BlocksManager
{
    public IEnumerable<BlockData> GetBlocks(ELanguage lang)
    {
        return default;
    }
    public IEnumerable<BlockData> GetBlocks()
    {
        return default;
    }
    public void SaveBlocks(ELanguage lang, BlockData[] blocks)
    {

    }

}
