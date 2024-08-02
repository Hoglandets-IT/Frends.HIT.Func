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


    public class StringValuePair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class ByteValuePair
    {
        public string Key { get; set; }
        public byte[] Value { get; set; }
    }

    public class FileValuePair
    {
        public string Key { get; set; }
        public string FileName { get; set; }
        public byte[] Value { get; set; }
    }

    public class MultipartFormInput {
        public string Url { get; set; }

        public string BasicUsername { get; set; }

        public string BasicPassword  { get; set; }

        public bool ReturnsJson { get; set; }


        public StringValuePair[] Headers { get; set; }

        public StringValuePair[] QueryParameters { get; set; }

        public StringValuePair[] StringFormParameters { get; set; }

        public ByteValuePair[] ByteFormParameters { get; set; }

        public FileValuePair[] FileFormParameters { get; set; }
    }

    public class MultipartFormResponse {
        public int StatusCode { get; set; }

        public List<StringValuePair> Headers { get; set; }

        public byte[] ByteContent { get; set; }

        public JObject ParsedContent { get; set; }
    }

}
