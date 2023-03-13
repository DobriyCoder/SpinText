using AngleSharp;
using AngleSharp.Dom;
using DobriyCoder.Core.Errors.Old;
using System.Text.RegularExpressions;

namespace SpinText.FaqParser;

public class FaqParser : IFaqParser
{
    public const string FAQ_NAME = "faq";
    public const string ITEM_NAME = "item";
    public const string QUESTION_NAME = "question";
    public const string ANSWER_NAME = "answer";

    public const string WITHOUT_FAQ_PATTERN = @"<faq>\s*(.+?)\s*</faq>";
    public const string QUESTION_WITH_ANSWER_PATTERN = @"<item>\s*<question>\s*(.+?)\s*</question>.*?<answer>\s*(.+?)\s*</answer>\s*</item>";

    public FaqParserResult RegexParse(string text)
    {
        var res = new FaqParserResult();
        res.Faq = new List<Dictionary<string, string>>();
        res.Content = text;
        res.ContentWithoutFaq = text;
        res.Errors = new Errors();
        Dictionary<string, string> result;

        try
        {
            var withoutFaqMatches = Regex.Matches(text, WITHOUT_FAQ_PATTERN);
            if (withoutFaqMatches.Count == 0) return res;

            foreach (Match m in withoutFaqMatches)
            {
                result = new Dictionary<string, string>();
                res.ContentWithoutFaq += m.Groups[1].Value;

                var quesionWithAnswerMatches = Regex.Matches(m.Groups[1].Value, QUESTION_WITH_ANSWER_PATTERN);

                foreach (Match qm in quesionWithAnswerMatches)
                {
                    if (result.ContainsKey(qm.Groups[1].Value))
                        result[qm.Groups[1].Value] = qm.Groups[2].Value;
                    else result.Add(qm.Groups[1].Value, qm.Groups[2].Value);
                }
                res.Faq.Add(result);
            }
        }
        catch (Exception ex)
        {
            res.Errors.ErrorsList.Add(new Error(ex.Message));
        }
        return res;
    }

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
            if (faqs.Count() == 0) throw new Exception($"Тег {FAQ_NAME} отсутствует в документе");

            foreach (var faq in faqs)
            {
                res = new Dictionary<string, string>();

                var items = faq.GetElementsByTagName(ITEM_NAME);
                if (items.Count() == 0)
                    throw new Exception($"Тег {ITEM_NAME} отсутствует в документе");

                IHtmlCollection<IElement> questions, answers;
                foreach (var item in items)
                {
                    questions = item.GetElementsByTagName(QUESTION_NAME);
                    answers = item.GetElementsByTagName(ANSWER_NAME);

                    if (questions.Count() == 0)
                        throw new Exception($"Тег {QUESTION_NAME} отсутствует в документе");
                    else if (answers.Count() == 0)
                        throw new Exception($"Тег {ANSWER_NAME} отсутствует в документе");

                    if (res.ContainsKey(questions
                        .First()
                        .InnerHtml))
                        res[questions
                        .First()
                        .InnerHtml] = answers
                        .First()
                        .InnerHtml;
                    else res.Add(questions
                            .First()
                            .InnerHtml
                            , answers
                            .First()
                            .InnerHtml);
                }
                result.Faq.Add(res);
            }
            foreach (var element in doc.QuerySelectorAll(FAQ_NAME))
                element.Replace(element.ChildNodes.ToArray());
            result.ContentWithoutFaq = doc?.Body?.InnerHtml;
        }
        catch (Exception ex)
        {
            result.Errors.ErrorsList.Add(new Error(ex.Message));
        }
        return result;
    }
}
