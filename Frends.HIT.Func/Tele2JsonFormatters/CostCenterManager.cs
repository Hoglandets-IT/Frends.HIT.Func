using Newtonsoft.Json.Linq;

namespace Frends.HIT.Func.Tele2JsonFormatters;

public class CostCenterManager
{
    private const string CostCenterKey = "costCenter";
    
    public static void GetCostCenter(JArray data)
    {
        foreach (var jToken in data)
        {
            var obj = (JObject)jToken;
            var value = obj.GetValue(CostCenterKey)?.ToString();
            if (string.IsNullOrEmpty(value)) continue;
            var costCenterNumber = ExtractValidCostCenterNumber(value);
            obj[CostCenterKey] = costCenterNumber != 0 ? costCenterNumber.ToString() : null;
        }
    }

    private static int ExtractValidCostCenterNumber(string str)
    {
        var parts = str.Split(',');
        foreach (var part in parts)
        {
            var trimmedPart = part.Trim();
            if (string.IsNullOrEmpty(trimmedPart)) continue;
            if (int.TryParse(trimmedPart, out var number))
            {
                return number;
            }
        }
        return 0;
    }
}