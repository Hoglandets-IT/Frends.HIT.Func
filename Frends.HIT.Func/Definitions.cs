using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Reflection;

namespace Frends.HIT.Func {   
    /// <summary>
    /// Input data for the return input function
    /// </summary>
    public class InputData {

        /// <summary>
        /// The input string
        /// </summary>
        [Display(Name = "Input Data")]
        [DisplayFormat(DataFormatString = "Text")]
        public string InputString { get; set; }
    }

    /// <summary>
    /// Output data for the return input function
    /// </summary>
    public class OutputData {

        /// <summary>
        /// The output string
        /// </summary>
        [Display(Name = "Output Data")]
        public string OutputString { get; set; }
    }
}