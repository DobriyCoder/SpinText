using DobriyCoder.Core.Common;
using DobriyCoder.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpiningText.Models;
using SpinText.Blocks.Services;
using SpinText.Coins.Services;
using SpinText.Generator.Services;
using SpinText.HT.Services;
using SpinText.Languages.Models;
using SpinText.Types;

namespace SpinText.Controllers;

[Route("/api/")]
public class ApiController : Controller
{
    [Route("generate/{key}/{lang}/{type}")]
    public JsonResult Generate(
        string key,
        ELanguage lang,
        EType type,
        [FromServices] IGenerator generator,
        [FromServices] CoinsManager coins,
        [FromServices] HTManager templates)
    {
        int? index = type == EType.Pair 
            ? coins.GetPairIndex(key) 
            : coins.GetCoinIndex(key);

        if (index == null) return new JsonResult(new
        {
            Status = "Failed",
            Error = $"The page {key} with language {lang.ToString()} is not existing!"
        });

        var tmp = templates.GetHT(index.Value, type, lang);

        if (tmp is null) return new JsonResult(new
        {
            Status = "Failed",
            Error = $"The page {key} with language {lang.ToString()} is not existing!"
        });

        var vars = new Dictionary<string, string>();

        Request.Query
            .ToList()
            .ForEach(i => vars.Add(i.Key, i.Value.FirstOrDefault() ?? ""));

        string? content = generator.GenerateContent(tmp.Template, new STVarsDictionary(vars), out IErrors errors);

        if (errors.IsErrors) return new JsonResult(new
        {
            Status = "Failed",
            Error = errors.Message
        });

        return new JsonResult(new
        {
            Status = "Ok",
            Content = content,
            LastModified = tmp.CreatedDate,
        });
    }
    
    [Route("add-page/{key}/")]
    public JsonResult AddPage(
        string key,
        [FromServices] HTProvider ht)
    {
        var vars = new Dictionary<string, string>();

        Request.Query
            .ToList()
            .ForEach(i => vars.Add(i.Key, i.Value.FirstOrDefault() ?? ""));

        ht.Add(key, vars);

        return new JsonResult(new
        {
            Result = "Ok",
        });
    }


    [Route("add-coin/{name}/")]
    public JsonResult AddCoin(
        string name,
        [FromServices] CoinsManager coins)
    {
        coins.Add(name);

        return new JsonResult(new
        {
            Result = "Ok",
        });
    }

    [Route("add-pair/{from}--{to}")]
    public JsonResult AddPair(
        string from,
        string to,
        [FromServices] CoinsManager coins)
    {
        coins.Add(new string[] { from, to });

        return new JsonResult(new
        {
            Result = "Ok",
        });
    }
}
