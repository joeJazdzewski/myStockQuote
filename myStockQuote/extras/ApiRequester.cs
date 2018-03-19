using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace myStockQuote.extras
{
    public class ApiRequester
    {
        public void Request(ParseRequest parseRequest, string functionName, string parameter)
        {
            
            var request = HttpWebRequest.Create(string.Format("http://jazdzewskij.cs.spu.edu/{0}.php?sym={1}", functionName, parameter));
            request.ContentType = "application/json";
            request.Method = "GET";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.Out.WriteLine("error {0}", response.StatusCode);
                }

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);
                    }

                    try
                    {
                        string Result = (content.ToString());
                        var jsonArray = JsonArray.Parse(Result);
                        parseRequest(jsonArray);
                    }
                    catch (Exception e)
                    {
                        Console.Out.WriteLine(e.Message);
                    }
                }
            }
        }
    }

    public delegate void ParseRequest(JsonValue jsonValue);
}