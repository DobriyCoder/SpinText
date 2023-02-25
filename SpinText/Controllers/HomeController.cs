using DobriyCoder.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using SpinText.Blocks.DB;
using SpinText.Blocks.Services;
using SpinText.Coins.Services;
using SpinText.Exporter.Services;
using SpinText.HT.Models;
using SpinText.HT.Services;
using SpinText.Languages.Models;
using SpinText.Languages.Services;
using SpinText.Models;
using SpinText.Types;
using SpinText.ViewModels;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace SpinText.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Route("/{type:int?}")]
    public IActionResult Index(EType type, ELanguage? lang
        , [FromServices] LanguagesManager lang_manager
        , [FromServices] BlocksManager blocks_manager
        , [FromServices] TypesManager types_manager)
    {
        lang = lang ?? lang_manager.GetDefaultLanguage();

        var model = new HomeModel(lang.Value)
        {
            Blocks = new BlocksMain(blocks_manager.GetBlocks(lang.Value, type)),
            TypesMenu = new TypesMenu()
            {
                Types = new TypesManager().Types,
                CurrentType = ((int)type).ToString(),
            }
        };

        ViewBag.HomeModel = model;

        return View(model);
    }
    public FileResult ExportHT(
        [FromServices] HTManager ht,
        [FromServices] ExporterProvider exporter)
    {
        var hts = ht.GetHTs();
        byte[] data = exporter.CreateExportHTFile(hts);
        return File(data, "text/csv", "export.csv");
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
            foreach (var tpl in data.Blocks[i])
            {
                if (tpl == null) continue;

                db_data.Add(new BlockData()
                {
                    Language = data.Language,
                    TemplatesType = data.TemplatesType,
                    BlockIndex = (byte)i,
                    Template = tpl,
                });
            }
        }

        blocks.SaveBlocks(data.Language, data.TemplatesType, db_data);

        return RedirectToAction("Index");
    }

    [RequestSizeLimit(300000000)]
    [HttpPost]
    public JsonResult AddCoins(
        IFormFile data,
        [FromServices] CoinsManager coins)
    {
        if (data is null) return null;
        TextReader tr = new StreamReader(data.OpenReadStream());
        string content = tr.ReadToEnd();
        coins.Add(content);
        return new JsonResult(new { Status = "ok" });
    }

    [HttpPost]
    public JsonResult AddHT(
        FormAddHTData data,
        [FromServices] HTProvider ht)
    {
        HTGeneratingStatus status = ht.Add(data.Count, data.Type);
        return new JsonResult(status);
    }
    public void StopGenerating(
        [FromServices] HTProvider ht)
    {
        ht.Stop();
    }
    public void ClearHTs(
        [FromServices] HTManager ht)
    {
        ht.ClearHTs();
    }

    public JsonResult GetHTGeneratingStatus([FromServices] HTProvider ht)
    {
        return new JsonResult(ht.GetStatus());
    }
    public JsonResult GetHTCount([FromServices] HTManager ht)
    {
        return new JsonResult(new
        {
            Count = ht.GetCount()
        });
    }
    public FileResult GetHTGeneratedLog(
        [FromServices] HTProvider ht,
        [FromServices] ExporterProvider exporter)
    {
        var log = ht.GetLastLog();
        byte[] data = exporter.CreateLogFile(log);
        return File(data, "text/txt", "log.txt");
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