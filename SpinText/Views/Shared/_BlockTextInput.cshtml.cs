using DobriyCoder.Asp.ViewModels;
namespace SpinText.ViewModels;

public class BlockTextInput : PartialModel
{
    public string Text { get; set; }
    public BlockTextInput() { }
    public BlockTextInput(string text)
    {
        Text = text;
    }
}
