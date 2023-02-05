using Microsoft.EntityFrameworkCore;
using SpinText.Languages.Models;
using SpinText.Types;
using System.ComponentModel.DataAnnotations;

namespace SpinText.HT.DB;

public class HTBaseData
{
    public uint Id { get; set; }
    public ELanguage Language { get; set; }
    public EType TemplateType { get; set; }
    public string Template { get; set; }
}
public class HTData : HTBaseData { }

public class HTPairsData : HTBaseData { }