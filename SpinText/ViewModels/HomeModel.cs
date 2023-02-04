using SpinText.Languages.Models;

namespace SpinText.ViewModels;

public class HomeModel
{
    public CtrlLanguage Language { get; set; }
    public BlocksMain Blocks { get; set; }
    public TypesMenu TypesMenu { get; set; }

    public HomeModel(ELanguage lang)
    {
        Language = new CtrlLanguage()
        {
            Current = lang
        };
    }
}
