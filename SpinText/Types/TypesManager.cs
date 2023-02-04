namespace SpinText.Types;

public class TypesManager
{
    public Dictionary<string, string> Types { get; set; }

    public TypesManager()
    {
        Types = new Dictionary<string, string>();

        foreach (var type in Enum.GetValues(typeof(EType)))
        {
            Types.Add(((int)type).ToString(), type.ToString());
        }
    }
}
