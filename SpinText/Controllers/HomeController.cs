using DobriyCoder.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using SpinText.Blocks.DB;
using SpinText.Blocks.Services;
using SpinText.Languages.Models;
using SpinText.Languages.Services;
using SpinText.Models;
using SpinText.ViewModels;
using System.Diagnostics;

namespace SpinText.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(ELanguage? lang
        , [FromServices] LanguagesManager lang_manager
        , [FromServices] BlocksManager blocks_manager)
    {
        lang = lang ?? lang_manager.GetDefaultLanguage();

        var model = new HomeModel(lang.Value)
        {
            Blocks = new BlocksMain(blocks_manager.GetBlocks(lang.Value))
        };

        ViewBag.HomeModel = model;

        return View(model);
    }
    public IActionResult ExportHT()
    {
        return View();
    }
    public IActionResult GetBlocks(ELanguage lang)
    {
        return View();
    }

    [HttpPost]
    public IActionResult SaveBlocks(
        FormBlocksData data,
        [FromServices] BlocksManager blocks)
    {
        List<BlockData> db_data = new List<BlockData>();

        for (int i = 0; i < data.Blocks.Count; i++)
        {
            foreach(var tpl in data.Blocks[i])
            {
                db_data.Add(new BlockData()
                {
                    Language = data.Language,
                    BlockIndex = (byte)i,
                    Template = tpl,
                });
            }
        }

        blocks.SaveBlocks(data.Language, db_data);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public JsonResult AddHT(
        FormAddHTData data)
    {
        data.PrintAsJson();
        return new JsonResult(new { a = 234 });
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