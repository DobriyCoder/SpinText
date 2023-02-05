using Microsoft.EntityFrameworkCore;
using SpinText.HT.DB;
using SpinText.Languages.Models;
using SpinText.Models;
using SpinText.Types;

namespace SpinText.HT.Services;

public class HTManager
{
    HTPairsManager _pairs;
    HTCommonManager _common;

    public HTManager(HTPairsManager pairs, HTCommonManager common)
    {
        _pairs = pairs;
        _common = common;
    }
    public IEnumerable<HTBaseData> GetHTs()
    {
        foreach (var pair in _pairs.GetHTs())
        {
            yield return pair;
        }

        foreach (var item in _common.GetHTs())
        {
            yield return item;
        }
    }
    
    public HTBaseData? GetHT(int index, EType type, ELanguage language) =>
        type == EType.Pair
            ? _pairs.GetHT(index, language)
            : _common.GetHT(index, type, language);

    public void AddHT(HTBaseData data)
    {
        if (data.TemplateType == EType.Pair)
            _pairs.AddHT(BaseToPairs(data));
        else
            _common.AddHT(BaseToCommon(data));
    }
        
    public void AddHTs(IEnumerable<HTBaseData> data)
    {
        var common_data = data
            .Where(i => i.TemplateType != EType.Pair)
            .Select(i => BaseToCommon(i));

        var pairs_data = data
            .Where(i => i.TemplateType == EType.Pair)
            .Select(i => BaseToPairs(i));

        _common.AddHTs(common_data);
        _pairs.AddHTs(pairs_data);
    }

    public void ClearHTs ()
    {
        _pairs.ClearHTs();
        _common.ClearHTs();
    }

    HTData BaseToCommon(HTBaseData pairs) => new HTData()
    {
        TemplateType = pairs.TemplateType,
        Language = pairs.Language,
        Template = pairs.Template,
    };

    HTPairsData BaseToPairs(HTBaseData pairs) => new HTPairsData()
    {
        TemplateType = pairs.TemplateType,
        Language = pairs.Language,
        Template = pairs.Template,
    };
}
