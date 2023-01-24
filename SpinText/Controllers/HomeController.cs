using Microsoft.AspNetCore.Mvc;
using SpinText.Languages.Models;
using SpinText.Models;
using System.Diagnostics;

namespace SpinText.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(ELanguage? lang)
        {
            return View();
        }
        public IActionResult ExportHT()
        {
            return View();
        }
        public IActionResult GetBlocks(ELanguage lang)
        {
            return View();
        }
        public IActionResult AddHT(string urls)
        {
            return View();
        }
        public IActionResult GetHTGeneratingStatus()
        {
            return View();
        }
        public IActionResult GetHTGeneratedLog()
        {
            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}