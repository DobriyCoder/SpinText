using SpinText.Languages.Models;

namespace SpinText.Languages.Services;

public class LanguagesManager
{
    public ELanguage GetDefaultLanguage()
    {
        return ELanguage.English;
    }
}
