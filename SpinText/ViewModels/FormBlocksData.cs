using SpinText.Languages.Models;

namespace SpinText.ViewModels;

public class FormBlocksData
{
    public ELanguage Language { get; set; }
    //public string Blocks { get; set; }
    public List<List<string>> Blocks { get; set; }
}
