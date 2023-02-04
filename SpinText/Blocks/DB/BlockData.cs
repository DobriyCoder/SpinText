using SpinText.Languages.Models;
using SpinText.Types;

namespace SpinText.Blocks.DB;

public class BlockData
{
    public int Id { get; set; }
    public ELanguage Language { get; set; }
    public EType TemplatesType { get; set; }
    public byte BlockIndex { get; set; }
    public string Template { get; set; }
}
