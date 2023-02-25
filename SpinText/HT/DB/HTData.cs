using Microsoft.EntityFrameworkCore;
using SpinText.Languages.Models;
using SpinText.Types;
using System.ComponentModel.DataAnnotations;

namespace SpinText.HT.DB;

public class HTBaseData : IDisposable
{
    public uint Id { get; set; }
    public ELanguage Language { get; set; }
    public EType TemplateType { get; set; }
    public string Template { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public void Dispose()
    {
        Template = "";
        Template = null;
        GC.SuppressFinalize(this);
    }
}
public class HTData : HTBaseData { }

public class HTPairsData : HTBaseData { }