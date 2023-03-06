namespace SpinText.FaqParser;

public interface IFaqParser
{
    FaqParserResult Parse(string text);
}
