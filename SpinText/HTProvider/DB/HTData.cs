using SpinText.Languages.Models;

namespace SpinText.HTProvider.DB;

public class HTData
{
    public int Id { get; set; }
    public string PageKey { get; set; }
    public ELanguage Language { get; set; }
    public string Template { get; set; }
}
