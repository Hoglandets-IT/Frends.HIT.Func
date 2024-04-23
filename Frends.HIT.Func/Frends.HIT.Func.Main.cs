using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Frends.HIT.Func;

public static class Main {
    
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