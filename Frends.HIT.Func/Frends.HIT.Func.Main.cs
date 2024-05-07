using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Frends.HIT.Func.Tele2JsonFormatters;
using Newtonsoft.Json.Linq;

namespace Frends.HIT.Func;

public static class Main {

    public static FormatedTele2Data FormatJsonDataTele2([PropertyTab] FormatTele2Data tele2Data)
    {
        var data = OrganizationStructureFormatter.FormatOrganizationStructure(tele2Data.InputData);
        CostCenterManager.GetCostCenter(data);
        if (tele2Data.Type == FormatDataForType.Orginzation)
        {
            NameStructureFormatter.FormatNameAndIdentifiers(data);
        }
        return new FormatedTele2Data
        {
            Data = data
        };
    }
    
    /// <summary>
    /// Returns the given input string as output
    /// </summary>
    /// <param name="input">Input String</param>
    /// <returns>Output String</returns>
    public static OutputData ReturnInput([PropertyTab] InputData input) {
        var output = new OutputData();
        output.OutputString = input.InputString;
        Console.WriteLine(@"Hello World");

        return output;
    }


}