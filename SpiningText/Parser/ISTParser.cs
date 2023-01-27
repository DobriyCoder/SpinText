using DobriyCoder.Core.Common;
using SpiningText.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpiningText.Parser;

public interface ISTParser
{
    string? Parse(string text, ISTVars? static_vars, ISTVars? dinamic_vars, out IErrors vars);
}

public class STParser : ISTParser
{
    public string? Parse(string text, ISTVars? static_vars, ISTVars? dinamic_vars, out IErrors errors)
    {
        IErrors result_errors;
        errors = new Errors();
        string? result = ParseStaticVars(text, static_vars, out result_errors);
        result = ParseDinemicVars(result, dinamic_vars, out result_errors);
        result = ParseSynonyms(result, out errors);
        
        return result;
    }
    public string? ParseStaticVars(string text, ISTVars? static_vars, out IErrors errors)
    {
        IErrors result_errors;
        errors = new Errors();
        var result = ParseVars(@"\[(.+?)\]", text, static_vars, out result_errors);
        return result;
    }
    public string? ParseDinemicVars(string text, ISTVars? dinamic_vars, out IErrors errors)
    {
        IErrors result_errors;
        errors = new Errors();
        var result = ParseVars(@"\%(.+?)\%", text, dinamic_vars, out result_errors);
        return result;
    }
    public string? ParseVars(string pattern, string text, ISTVars? vars, out IErrors errors)
    {
        errors = new Errors();
        if (vars is null) return text;

        Regex regex = new Regex(pattern);

        return regex.Replace(text, i =>
        {
            if (i.Groups.Count < 2) return i.Value;

            var math_line = i.Groups[1].Value;

            return Calc(math_line, vars!);
        });
    }
    public string Calc(string line, ISTVars vars)
    {
        string result = Power(line, vars);
        result = Mul(result, vars);
        result = Div(result, vars);
        result = Plus(result, vars);
        result = Minus(result, vars);

        return result;
    }
    public string Plus(string line, ISTVars vars)
    {
        Regex reg = new Regex(@"([a-zA-Z_0-9]*) *\+ *([a-zA-Z_0-9]*)");
        
        return reg.Replace(line, i =>
        {
            string name1 = i.Groups[1].Value;
            string name2 = i.Groups[2].Value;

            if (!float.TryParse(vars.GetVar(name1), out float var_1)) return i.Value;
            if (!float.TryParse(vars.GetVar(name2), out float var_2)) return i.Value;

            return (var_1 + var_2).ToString();
        });
    }
    public string Minus(string line, ISTVars vars)
    {
        Regex reg = new Regex(@"([a-zA-Z_0-9]*) *- *([a-zA-Z_0-9]*)");

        return reg.Replace(line, i =>
        {
            string name1 = i.Groups[1].Value;
            string name2 = i.Groups[2].Value;

            if (!float.TryParse(vars.GetVar(name1), out float var_1)) return i.Value;
            if (!float.TryParse(vars.GetVar(name2), out float var_2)) return i.Value;

            return (var_1 - var_2).ToString();
        });
    }
    public string Mul(string line, ISTVars vars)
    {
        Regex reg = new Regex(@"([a-zA-Z_0-9]*) *\* *([a-zA-Z_0-9]*)");

        return reg.Replace(line, i =>
        {
            string name1 = i.Groups[1].Value;
            string name2 = i.Groups[2].Value;

            if (!float.TryParse(vars.GetVar(name1), out float var_1)) return i.Value;
            if (!float.TryParse(vars.GetVar(name2), out float var_2)) return i.Value;

            return (var_1 * var_2).ToString();
        });
    }
    public string Div(string line, ISTVars vars)
    {
        Regex reg = new Regex(@"([a-zA-Z_0-9]*) *\/ *([a-zA-Z_0-9]*)");

        return reg.Replace(line, i =>
        {
            string name1 = i.Groups[1].Value;
            string name2 = i.Groups[2].Value;

            if (!float.TryParse(vars.GetVar(name1), out float var_1)) return i.Value;
            if (!float.TryParse(vars.GetVar(name2), out float var_2)) return i.Value;
            if (var_2 == 0) return i.Value;

            return (var_1 / var_2).ToString();
        });
    }
    public string Power(string line, ISTVars vars)
    {
        Regex reg = new Regex(@"([a-zA-Z_0-9]*) *\^ *([a-zA-Z_0-9]*)");

        return reg.Replace(line, i =>
        {
            string name1 = i.Groups[1].Value;
            string name2 = i.Groups[2].Value;

            if (!float.TryParse(vars.GetVar(name1), out float var_1)) return i.Value;
            if (!float.TryParse(vars.GetVar(name2), out float var_2)) return i.Value;

            return Math.Pow(var_1, var_2).ToString();
        });
    }
    public string? ParseSynonyms(string text, out IErrors errors)
    {
        errors = new Errors();

        Regex regex = new Regex(@"\{(.+?)\}");

        return regex.Replace(text, i =>
        {
            if (i.Groups.Count < 2) return i.Value;

            string[] synonyms = i.Groups[1].Value.Split('|');
            int index = new Random().Next(0, synonyms.Length);

            return synonyms[index];
        });
    }
}
