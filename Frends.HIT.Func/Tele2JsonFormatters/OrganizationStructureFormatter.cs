using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Frends.HIT.Func.Tele2JsonFormatters;

public class OrganizationStructureFormatter
{
    private const string Concern = "Koncern";
    private const string Field = "field";
    private const string Id = "ID";
    
    public static JArray FormatOrganizationStructure(JArray data)
    {
        var filteredData = new List<JObject>();
        foreach (var item in data)
        {
            var obj = (JObject)item;
            if (string.IsNullOrEmpty(obj.GetValue(Field + "01", StringComparison.OrdinalIgnoreCase)?.ToString()))
            {
                continue; 
            }
            if (obj.GetValue(Field + "01", StringComparison.OrdinalIgnoreCase)?.ToString() == Concern)
            {
                obj = SortOrganizationTree(obj);
            }
            var properties = obj.Properties().ToList();
            var previousValues = new Dictionary<string, string?>();
            foreach (var property in properties)
            {
                if (!property.Name.StartsWith(Field, StringComparison.OrdinalIgnoreCase)) continue;
                var propertyValue = GetValue(property.Value);
                if (string.IsNullOrEmpty(propertyValue)) continue;
                var value = TrimValue(propertyValue);
                if (property.Name != Field + "01")
                {
                    if (previousValues.TryGetValue(GetPreviousFieldName(property.Name), out var previousFieldValue))
                    {
                        if (value == previousFieldValue)
                        {
                            value = null;
                        }
                    }
                }

                if (string.IsNullOrEmpty(value))
                {
                    property.Value = null!;  
                    previousValues[property.Name] = null; 
                }
                else
                {
                    property.Value = value;  
                    previousValues[property.Name] = value; 
                }
            }
            filteredData.Add(obj);
        }
        return JArray.FromObject(filteredData);;
    }


    private static string? GetValue(JToken token)
    {
        var value = Convert.ToString(token);
        return string.IsNullOrEmpty(value) ? null : value;
    }
    private static string TrimValue(string str)
    {
        return Regex.Replace(str, @"^\d+\s*", "")
            .Replace(@"BU ", "")
            .Replace(@"SF ", "")
            .Trim();
    }
    private static JObject SortOrganizationTree(JObject obj)
    {
        for (var i = 1; i <= 6; i++)
        {
            obj[Field + i.ToString("D2")] = obj[Field + (i + 1).ToString("D2")];
        }
        for (var i = 1; i <= 6; i++)
        {
            obj[Id + Field + i.ToString("D2")] = obj[Id + Field + (i + 1).ToString("D2")];
        }
        obj[Field + "07"] = null;
        obj[Id + Field + "07"] = null;
        return obj;
    }

    private static string? GetPreviousFieldName(string fieldName)
    {
        if (int.TryParse(fieldName.AsSpan(5), out var number) && number > 1)
        {
            return Field + (number - 1).ToString("D2");
        }
        return null;
    }
}
