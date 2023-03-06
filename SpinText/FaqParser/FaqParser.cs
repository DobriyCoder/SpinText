using AngleSharp;
using DobriyCoder.Core.Errors.Old;
using System.Security.Cryptography.X509Certificates;

namespace SpinText.FaqParser;

public class FaqParser : IFaqParser
{
    public const string FAQ_NAME = "faq";
    public const string ITEM_NAME = "item";
    public const string QUESTION_NAME = "question";
    public const string H3_NAME = "h3";
    public const string ANSWER_NAME = "answer";
    public const string P_NAME = "p";

    public FaqParserResult Parse(string text)
    {

        var result = new FaqParserResult();
        result.Faq = new List<Dictionary<string, string>>();
        result.Content = text;
        result.Errors = new Errors();

        try
        {
            var config = Configuration.Default;
            using var context = BrowsingContext.New(config);
            using var doc = context.OpenAsync(req => req.Content(text)).Result;

            Dictionary<string, string> res;
            var faqs = doc.GetElementsByTagName(FAQ_NAME);
            foreach (var faq in faqs)
            {
                res = new Dictionary<string?, string?>();
                foreach (var item in faq.GetElementsByTagName(ITEM_NAME))
                    res.Add(item?
                        .GetElementsByTagName(QUESTION_NAME)?
                        .FirstOrDefault()?
                        .GetElementsByTagName(H3_NAME)?
                        .FirstOrDefault()?
                        .InnerHtml
                        , item?
                        .GetElementsByTagName(ANSWER_NAME)?
                        .FirstOrDefault()?
                        .GetElementsByTagName(P_NAME)?
                        .FirstOrDefault()?
                        .InnerHtml);
                result.Faq.Add(res);
            }
            foreach (var element in doc.QuerySelectorAll(FAQ_NAME))
                element.Remove();
            result.ContentWithoutFaq = doc.ToHtml();
        }
        catch (Exception ex)
        {
            result.Errors.ErrorsList.Add( new Error(ex.Message) );
        }
        
        return result;
    }
}
