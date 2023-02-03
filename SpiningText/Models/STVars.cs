using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiningText.Models;

public interface ISTVars
{
    string? GetVar(string name);
    IEnumerable<string> GetVarNames();
}
public class STVars<T> : ISTVars
{
    T _data;

    public STVars(T data) => this._data = data;

    public string? GetVar(string name) => throw new NotImplementedException();
    public IEnumerable<string> GetVarNames() => throw new NotImplementedException();
}

public class STVarsDictionary : Dictionary<string, string>, ISTVars
{
    public STVarsDictionary(Dictionary<string, string> data)
    {
        foreach (var pair in data)
        {
            Add(pair.Key, pair.Value);
        }
    }
    public string? GetVar(string name) => ContainsKey(name)
        ? this[name]
        : null;
    public IEnumerable<string> GetVarNames() => Keys.ToList();
}
