using Frends.HIT;
using System.Net.Http;
using Newtonsoft.Json;

namespace Frends.HIT.Func;

public static class Multipart {
    public static MultipartFormResponse MultipartRequest(MultipartFormInput input) {
        var output = new MultipartFormResponse();
      
        HttpClient client = new();
        HttpRequestMessage message = new(HttpMethod.Post, input.Url);

        if (input.Headers != null) {
            foreach (var pair in input.Headers) {
                if (pair.Key.ToLower() != "content-type") {
                    message.Headers.Add(pair.Key, pair.Value);
                } 
            }
        }

        MultipartFormDataContent content = new();

        if (input.StringFormParameters != null) {
            foreach (var pair in input.StringFormParameters) {
                content.Add(new StringContent(pair.Value), pair.Key);
            }
        }

        if (input.ByteFormParameters != null) {
            foreach (var pair in input.ByteFormParameters) {
                content.Add(new ByteArrayContent(pair.Value), pair.Key);
            }
        }
        
        if (input.FileFormParameters != null) {
            foreach (var pair in input.FileFormParameters) {
                content.Add(new ByteArrayContent(pair.Value), pair.Key, pair.FileName);
            }
        }

        message.Content = content;

        HttpResponseMessage response = client.Send(message);

        output.StatusCode = (int)response.StatusCode;
        output.ByteContent = response.Content.ReadAsByteArrayAsync().Result;
        output.Headers = new List<StringValuePair>();

        foreach (var header in response.Headers) {
            output.Headers.Add(new StringValuePair { Key = header.Key, Value = string.Join(", ", header.Value) });
        }

        if (input.ReturnsJson) {
            try {
                 output.ParsedContent = Newtonsoft.Json.Linq.JObject.Parse(System.Text.Encoding.UTF8.GetString(output.ByteContent));
            }
            catch {
                output.ParsedContent = Newtonsoft.Json.Linq.JObject.Parse(System.Text.Encoding.Latin1.GetString(output.ByteContent));
            }
        }

        return output;
        }
}