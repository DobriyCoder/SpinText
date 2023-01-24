using SpinText.Languages.Models;

namespace SpinText.Blocks.DB;

public class BlockData
{
    public int Id { get; set; }
    public ELanguage Language { get; set; }
    public byte BlockIndex { get; set; }
    public string Template { get; set; }
}
