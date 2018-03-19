using System;
using System.Collections.Generic;
using System.Json;
using System.IO;
using System.Net;
using System.Xml;
using NUnit.Framework;
using System.Web.Services;

namespace myStockQuote
{
	public class SearchStocks
	{
		public string SearchString { get; private set;}
		public List<string> CompanyName { get; set; }
		public List<string> CompanySymbol { get; set; }

		public SearchStocks ()
		{
			CompanyName = new List<string>();
			CompanySymbol = new List<string>();
		}

		public SearchStocks(string search)
		{
			SearchString = search;
			CompanyName = new List<string>();
			CompanySymbol = new List<string>();
		}

		public void setSearchString(string search)
		{
			SearchString = search;
		}

		public bool getStockInformation()
		{
			string symbol = SearchString;
			var request = HttpWebRequest.Create (string.Format("http://jazdzewskij.cs.spu.edu/requestSymbol.php?sym={0}", symbol));//http://jazdzewskij.cs.spu.edu/requestQuote.php?sym=msft
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
                    Assert.NotNull(content);
                    try
                    { 
                        string Result = (content.ToString());
                        var jsonArray = JsonArray.Parse(Result);
                        if (jsonArray.Count > 0)
                        {
                            foreach (JsonObject obj in jsonArray)
                            {
                                CompanyName.Add(obj["symName"]);
                                CompanySymbol.Add(obj["symSymbol"]);
                            }
                        }
                        else
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
		}
	}
}

