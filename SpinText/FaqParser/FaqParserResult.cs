using DobriyCoder.Core.Common;

namespace SpinText.FaqParser;

public class FaqParserResult
{
    public string Content { get; set; }
    public string ContentWithoutFaq { get; set; }
    public List<Dictionary<string, string>> Faq { get; set; }
    public IErrors Errors { get; set; }
}
