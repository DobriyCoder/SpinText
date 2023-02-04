using DobriyCoder.Asp.ViewModels;
namespace SpinText.ViewModels;

public class TypesMenu : PartialModel
{
    public Dictionary<string, string> Types { get; set; }
    public string? CurrentType { get; set; }
}
