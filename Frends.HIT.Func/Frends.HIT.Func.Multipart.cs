using Frends.HIT;
using System.Net.Http;
using Newtonsoft.Json;

namespace Frends.HIT.Func;

public static class Multipart {
    public async static Task<MultipartFormResponse> MultipartRequest(MultipartFormInput input) {
        var output = new MultipartFormResponse();
      
        HttpClient client = new();
        HttpRequestMessage message = new(HttpMethod.Post, input.Url);

        try {
            if (input.Headers != null) {
                foreach (var pair in input.Headers) {
                    if (pair.Key.ToLower() != "content-type") {
                        message.Headers.Add(pair.Key, pair.Value);
                    } 
                }
            }
        } catch (Exception e) {
            throw new Exception($"Error adding headers: {e.Message}");
        }


        MultipartFormDataContent content = new();

        try {
            if (input.StringFormParameters != null) {
                foreach (var pair in input.StringFormParameters) {
                    content.Add(new StringContent(pair.Value), pair.Key);
                }
            }
        } catch (Exception e) {
            throw new Exception($"Error adding string form parameters: {e.Message}");
        }

        try {
            if (input.ByteFormParameters != null) {
                foreach (var pair in input.ByteFormParameters) {
                    content.Add(new ByteArrayContent(pair.Value), pair.Key);
                }
            }
        } catch (Exception e) {
            throw new Exception($"Error adding byte form parameters: {e.Message}");
        }
        
        try {
            if (input.FileFormParameters != null) {
                foreach (var pair in input.FileFormParameters) {
                    content.Add(new ByteArrayContent(pair.Value), pair.Key, pair.FileName);
                }
            }
        } catch (Exception e) {
            throw new Exception($"Error adding file form parameters: {e.Message}");
        }

        message.Content = content;

        HttpResponseMessage response = client.Send(message);
        try {
            output.StatusCode = (int)response.StatusCode;
            output.ByteContent = await response.Content.ReadAsByteArrayAsync();
        } catch (Exception e) {
            throw new Exception($"Error reading response: {e.Message}");
        }
        
        output.Headers = new List<StringValuePair>();

        try {
            if (response.Headers != null) {
                foreach (var header in response.Headers) {
                    output.Headers.Add(new StringValuePair { Key = header.Key, Value = string.Join(", ", header.Value) });
                }
            }
        } catch {
            throw new Exception("Error reading headers");
        }

        if (input.ReturnsJson) {
            try {
                 output.ParsedContent = Newtonsoft.Json.Linq.JObject.Parse(System.Text.Encoding.UTF8.GetString(output.ByteContent));
            }
            catch {
                try {
                    output.ParsedContent = Newtonsoft.Json.Linq.JObject.Parse(System.Text.Encoding.Latin1.GetString(output.ByteContent));
                } catch {
                    output.ParsedContent = Newtonsoft.Json.Linq.JObject.Parse("{}");
                }
            }
        }

        return output;
    }
}