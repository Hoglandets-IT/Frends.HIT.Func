using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Frends.HIT.Func.Tele2JsonFormatters;

public class OrganizationStructureFormatter
{
    private const string Concern = "Koncern";
    private const string Field = "field";
    
    public static JArray OrganizationStructure(JArray data)
    {
        foreach (var jToken in data)
        {
            var obj = (JObject)jToken;
            if ((string)obj.GetValue(Field + "01", StringComparison.OrdinalIgnoreCase)?.ToString() == Concern)
            {
                obj = SortOrganizationTree(obj);
            }
            var properties = obj.Properties().ToList();
            var previousValues = new Dictionary<string, string?>();
            foreach (var property in properties)
            {
                if (!property.Name.StartsWith(Field, StringComparison.OrdinalIgnoreCase)) continue;
                var propertyValue = (string)property.Value;
                if (string.IsNullOrEmpty(propertyValue)) continue;
                var value = TrimValue(propertyValue);
                if (previousValues.TryGetValue(GetPreviousFieldName(property.Name), out var previousFieldValue))
                {
                    if (value == previousFieldValue)
                    {
                        value = null;
                    }
                }
                property.Value = value;  
                previousValues[property.Name] = value; 
            }
        }
        return data;
    }

    private static string TrimValue(string str)
    {
        return Regex.Replace(str, @"^\d+\s*", "").Trim();
    }

    private static JObject SortOrganizationTree(JObject obj)
    {
        for (var i = 1; i <= 6; i++)
        {
            obj[Field + i.ToString("D2")] = obj[Field + (i + 1).ToString("D2")];
        }
        obj[Field + "07"] = null;
        return obj;
    }

    private static string GetPreviousFieldName(string fieldName)
    {
        if (int.TryParse(fieldName.AsSpan(5), out var number) && number > 1)
        {
            return Field + (number - 1).ToString("D2");
        }
        return null;
    }
}
