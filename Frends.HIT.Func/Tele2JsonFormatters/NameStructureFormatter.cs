using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Frends.HIT.Func.Tele2JsonFormatters;

public class NameStructureFormatter
{
    private const string OrganizationDomainSuffix = "@org.hoglandsforbundet.se";
    private const string Organisation = "Organisation";
    private const string Field = "field";

    public static void FormatNameAndIdentifiers(JArray data)
    {
        foreach (var jToken in data)
        {
            var obj = (JObject)jToken;
            var name = GetName(obj);
            var id = GetId(name);

            obj["id"] = id + OrganizationDomainSuffix;
            obj["reference"] = id;
            obj["firstName"] = Organisation;
            obj["lastName"] = obj.GetValue(Field + "01").ToString();
        }
    }

    private static string GetName(JObject obj)
    {
        var id = string.Empty;
        for (var i = 1; i < 7; i++)
        {
            var value = obj.GetValue(Field + "0" + i.ToString())?.ToString();
            if (string.IsNullOrEmpty(value)) continue;
            if (i is > 1 and < 7)
            {
                id += " - ";
            }
            id += value;
        }
        return id;
    }

    private static string GetId(string str)
    {
        var parts = str.Split(" - ");
        for (var i = 0; i < parts.Length; i++)
        {
            parts[i] = parts[i].Replace(" ", "");
            parts[i] = parts[i].Replace("å", "a").Replace("ä", "a").Replace("ö", "o");
            parts[i] = parts[i].ToLower();
        }
        var result = string.Join("-", parts);
        return HashId(result);
    }

    private static string HashId(string id)
    {
        using var md5 = MD5.Create();
        var bytes = Encoding.UTF8.GetBytes(id);
        var hashBytes = md5.ComputeHash(bytes);
        var builder = new StringBuilder();
        foreach (var t in hashBytes)
        {
            builder.Append(t.ToString("x2"));
        }
        return builder.ToString();
    }
}
