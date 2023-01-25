using DobriyCoder.Core.Common;
using SpiningText.Models;
using SpiningText.Parser;
using SpinText.Blocks.DB;
using SpinText.HT.DB;

namespace SpinText.Generator.Services;

public interface IGenerator
{
    IEnumerable<HTData> GenerateHT(string page_key, ISTVars data, IEnumerable<BlockData> blocks);
}

public class Generator : IGenerator
{
    ISTParser _parser;

    public Generator(ISTParser parser)
    {
        _parser = parser;
    }
    public IEnumerable<HTData> GenerateHT(string page_key, ISTVars data, IEnumerable<BlockData> blocks)
    {
        var result = blocks.GroupBy(i => i.Language).Select(i =>
        {
            var tpls = i.OrderBy(j => j.BlockIndex).GroupBy(j => j.BlockIndex).Select(j =>
            {
                int count = j.Count();
                int num = new Random().Next(count - 1);
                return j.ToList()[num].Template;
            });

            var tpl = String.Join('\n', tpls);

            string? text = _parser.Parse(tpl, data, null, out IErrors errors);
            if (errors.IsErrors) return null;

            return new HTData()
            {
                PageKey = page_key,
                Language = i.Key,
                Template = text!,
            };
        }).Where(i => i is not null);

        return result!;
    }
}
