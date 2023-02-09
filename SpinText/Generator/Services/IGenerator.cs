using DobriyCoder.Core.Common;
using SpiningText.Models;
using SpiningText.Parser;
using SpinText.Blocks.DB;
using SpinText.HT.DB;
using SpinText.Types;
using static System.Net.Mime.MediaTypeNames;

namespace SpinText.Generator.Services;

public interface IGenerator
{
    IEnumerable<HTData> GenerateHT(EType? type, ISTVars? data, IEnumerable<BlockData> blocks, Action<HTData> on_ht);
    string? GenerateContent(string content, ISTVars data, out IErrors errors);
}

public class Generator : IGenerator
{
    ISTParser _parser;

    public Generator(ISTParser parser)
    {
        _parser = parser;
    }

    public string? GenerateContent(string content, ISTVars data, out IErrors errors)
    {
        return _parser.Parse(content, data, data, out errors);
    }

    public IEnumerable<HTData> GenerateHT(EType? type, ISTVars? data, IEnumerable<BlockData> blocks, Action<HTData> on_ht)
    {
        var result = blocks.GroupBy(i => i.Language).Select(i =>
        {
            var tpls = i.OrderBy(j => j.BlockIndex).GroupBy(j => j.BlockIndex).Select(j =>
            {
                int count = j.Count();
                int num = new Random().Next(0, count);
                if (num >= count) num--;
                return j.ToList()[num].Template;
            });

            var tpl = String.Join('\n', tpls);

            string? text = _parser.Parse(tpl, data, null, out IErrors errors);
            if (errors.IsErrors) return null;

            var result = new HTData()
            {
                TemplateType = type ?? EType.Coin,
                Language = i.Key,
                Template = text!,
            };

            on_ht(result);

            return result;
        }).Where(i => i is not null);

        return result!;
    }
}
