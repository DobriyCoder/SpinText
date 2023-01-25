namespace SpinText.HT.Models;

public class UrlData
{
    public string? PageKey => Data.ContainsKey("page_key")
        ? Data["page_key"]
        : null;

    public Dictionary<string, string> Data { get; set; }
    public UrlData(string url)
    {
        Data = new Dictionary<string, string>();
        ParseUrl(url);
    }
    void ParseUrl(string url)
    {
        var url_parts = url.Split('?');
        if (url_parts.Length < 2) return;

        var get_params = url_parts[1].Split('&');
        if (get_params.Length == 0) return;

        foreach ( var param in get_params )
        {
            var data = param.Split('=');
            if (Data.ContainsKey(data[0]))
            {
                Data[data[0]] = data[1];
                continue;
            }

            Data.Add(data[0], data[1]);
        }
    }
}
