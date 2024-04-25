using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Frends.HIT.Func.Tele2JsonFormatters;
using Newtonsoft.Json.Linq;

namespace Frends.HIT.Func;

public static class Main {

    public static JArray FormatJsonDataTele2([PropertyTab] FormatTele2Data tele2Data)
    {
        var formatedData = OrganizationStructureFormatter.OrganizationStructure(tele2Data.InputData);
        return new JArray();
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