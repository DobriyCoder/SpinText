using SpinText.Languages.Models;
using SpinText.Types;

namespace SpinText.ViewModels;

public class FormBlocksData
{
    public ELanguage Language { get; set; }
    public EType TemplatesType { get; set; }
    //public string Blocks { get; set; }
    public List<List<string>> Blocks { get; set; }
}
