using SpinText.Languages.Models;
using SpinText.Types;

namespace SpinText.HT.Models;

public class HTGeneratedLogItem
{
    public string Key { get; set; }
    public EType Type { get; set; }
    public ELanguage Language { get; set; }
    public EHTGeneratedLogStatus Status { get; set; }
    public string Error { get; set; }
}
