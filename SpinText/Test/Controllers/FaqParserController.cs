using Microsoft.AspNetCore.Mvc;

namespace SpinText.Test.Controllers;

public class FaqParserController : Controller
{
    [Route("/")]
    public string Index()
    {
        return "Result";
    }
}
