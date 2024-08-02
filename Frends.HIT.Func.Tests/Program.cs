using Frends.HIT.Func;


public class Program {
    public static void Main() {
        var test = "199101011234";

        Console.WriteLine(test.Substring(0, 8) + "-" + test.Substring(8));




        var mpc = new MultipartFormInput{
            Url = "https://httpbin.org/anything",
            ReturnsJson = true,
            Headers = new StringValuePair[] {
                new StringValuePair{
                    Key="Authorization",
                    Value="Test 123"
                },
                new StringValuePair{
                    Key="Content-Type",
                    Value="application/json"
                }
            },
            StringFormParameters = new StringValuePair[] {
                new StringValuePair{
                    Key="key1",
                    Value="value1"
                },
                new StringValuePair{
                    Key="key2",
                    Value="value2"
                }
            }, 
            ByteFormParameters = new ByteValuePair[] {
                new ByteValuePair{
                    Key="key3",
                    Value=System.Text.Encoding.UTF8.GetBytes("value3")
                },
                new ByteValuePair{
                    Key="key4",
                    Value=System.Text.Encoding.UTF8.GetBytes("value4")
                }
            },
            FileFormParameters = new FileValuePair[] {
                new FileValuePair{
                    Key="key5",
                    FileName="file1.txt",
                    Value=System.Text.Encoding.UTF8.GetBytes("file1 content")
                },
                new FileValuePair{
                    Key="key6",
                    FileName="file2.txt",
                    Value=System.Text.Encoding.UTF8.GetBytes("file2 content")
                }
            },
        };

        var response = Multipart.MultipartRequest(mpc);

        Console.WriteLine(response.StatusCode);
        Console.WriteLine(System.Text.Encoding.UTF8.GetString(response.ByteContent));

    }
}