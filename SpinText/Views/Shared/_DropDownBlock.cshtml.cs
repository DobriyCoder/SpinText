using DobriyCoder.Asp.ViewModels;
using SpinText.Blocks.DB;

namespace SpinText.ViewModels;

public class DropDownBlock : PartialModel
{
    public IEnumerable<BlockTextInput> Templates { get; set; }

    public DropDownBlock()
    {
        Templates = new List<BlockTextInput>();
    }
    public DropDownBlock(IEnumerable<BlockTextInput> tmp)
    {
        Templates = tmp;
    }
}
