using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Frends.HIT.Func.Tele2JsonFormatters;
using Newtonsoft.Json.Linq;

namespace Frends.HIT.Func;

public static class Main {

    public static FormatedTele2Data FormatJsonDataTele2([PropertyTab] FormatTele2Data tele2Data)
    {
        var formatedData = OrganizationStructureFormatter.FormatOrganizationStructure(tele2Data.InputData);
        if (tele2Data.Type == FormatDataForType.Orginzation)
        {
            NameStructureFormatter.FormatNameAndIdentifiers(formatedData);
        }
        return new FormatedTele2Data
        {
            Data = formatedData
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
        
        return output;
    }


}