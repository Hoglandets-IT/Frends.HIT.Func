using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Frends.HIT.Func {
    public enum FormatDataForType { User, Orginzation }
    public class FormatTele2Data
    {
        public FormatDataForType Type { get; set; }
        
        [DisplayFormat(DataFormatString = "Expression")]
        public JArray InputData { get; set; }
    }

    public class FormatedTele2Data
    {
        public JArray Data { get; set; }
    }
    
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