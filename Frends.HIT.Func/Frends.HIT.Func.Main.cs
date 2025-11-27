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

    /// <summary>
    /// Runs a command line process and returns the output
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static OutputData RunCommand([PropertyTab] RunCommandInputData input) {
        var output = new OutputData();
        var processInfo = new System.Diagnostics.ProcessStartInfo(input.Command, String.Join(" ", input.Arguments));
        if (!string.IsNullOrEmpty(input.WorkingDirectory)) processInfo.WorkingDirectory = input.WorkingDirectory;
        processInfo.RedirectStandardOutput = true;
        processInfo.RedirectStandardError = true;
        processInfo.UseShellExecute = false;
        processInfo.CreateNoWindow = true;
        var process = new System.Diagnostics.Process();
        process.StartInfo = processInfo;
        process.Start();

        string outputString = process.StandardOutput.ReadToEnd();
        string errorString = process.StandardError.ReadToEnd();

        process.WaitForExit();

        output.OutputString = string.IsNullOrEmpty(errorString) ? outputString : errorString;

        return output;
    }
}