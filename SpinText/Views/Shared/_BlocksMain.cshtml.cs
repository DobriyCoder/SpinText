using DobriyCoder.Asp.ViewModels;
using SpinText.Blocks.DB;
using System.Linq;

namespace SpinText.ViewModels;

public class BlocksMain : PartialModel
{
    public IEnumerable<DropDownBlock> Blocks { get; set; }

    public BlocksMain(IEnumerable<BlockData> blocks)
    {
        Blocks = blocks
            .GroupBy(i => i.BlockIndex)
            .Select(i => new DropDownBlock(
                i.Select(j => new BlockTextInput(j.Template))
                    .ToList()))
            .ToList();
    }
}
