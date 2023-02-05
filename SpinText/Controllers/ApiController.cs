using DobriyCoder.Core.Common;
using DobriyCoder.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpiningText.Models;
using SpinText.Blocks.Services;
using SpinText.Generator.Services;
using SpinText.HT.Services;
using SpinText.Languages.Models;
using SpinText.Types;

namespace SpinText.Controllers
{
    [Route("/api/")]
    public class ApiController : Controller
    {
        [Route("generate/{key}/{lang}/")]
        public JsonResult Generate(
            string key,
            ELanguage lang,
            [FromServices] IGenerator generator,
            [FromServices] HTManager templates)
        {
            var tmp = templates.GetHT(1, EType.Coin, lang);
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
                Content = content
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
    }
}
