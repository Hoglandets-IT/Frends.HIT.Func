using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Frends.HIT.Func.Tele2JsonFormatters;

public class NameStructureFormatter
{
    private const string OrganizationDomainSuffix = "@org.hoglandsforbundet.se";
    private const string Organisation = "Organisation";
    private const string Field = "field";
    private const string Id = "ID";

    public static void FormatNameAndIdentifiers(JArray data)
    {
        foreach (var jToken in data)
        {
            var obj = (JObject)jToken;
            var id = GetId(GetIdString(obj));
            var names = GetFirstLastName(obj);
            DeleteIdFields(obj);
            obj["id"] = id + OrganizationDomainSuffix;
            obj["reference"] = id;
            obj["firstName"] = names["firstName"];
            obj["lastName"] = names["lastName"];
            obj["title"] = Organisation;
        }
    }
    private static string GetIdString(JObject obj)
    {
        var id = string.Empty;
        for (var i = 1; i < 7; i++)
        {
            var value = obj.GetValue(Id + Field + i.ToString("D2"))?.ToString();
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

    private static Dictionary<string, string> GetFirstLastName(JObject jObject)
    {
        var dict = new Dictionary<string, string>();
        var firstNameFound = false;
        var firstName = string.Empty;
        var lastName = string.Empty;
        
        for (var i = 7; i > 0; i--)
        {
            var value = jObject.GetValue(Field + "0" + i)?.ToString();
            if (string.IsNullOrEmpty(value)) continue;
            if (!firstNameFound)
            {
                firstName = value;
                firstNameFound = true;
            }
            else
            {
                lastName = value;
                break; 
            }
        }
        dict["firstName"] = firstName;
        dict["lastName"] = lastName;
        return dict;
    }

    private static void DeleteIdFields(JObject jObject)
    {
        for (var i = 7; i > 0; i--)
        {
            jObject.Remove(Id + Field + i.ToString("D2"));
        }
    }

}
