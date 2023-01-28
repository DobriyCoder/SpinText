using Microsoft.EntityFrameworkCore;
using SpinText.Languages.Models;
using System.ComponentModel.DataAnnotations;

namespace SpinText.HT.DB;

[PrimaryKey("PageKey", "Language")]
public class HTData
{
    //public int Id { get; set; }
    [MaxLength(50)]
    public string PageKey { get; set; }
    public ELanguage Language { get; set; }
    public string Template { get; set; }
}
