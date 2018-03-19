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
	public class cHistory
	{
		string Stocksymbol { get; set; }
		public List<string> prices;
		public List<string> date;
		public cHistory ()
		{
			Stocksymbol = "";
			prices = new List<string> ();
			date = new List<string> ();
		}
		public void setSym(string sym)
		{
			Stocksymbol = sym;
		}
		public bool getStockInformation()
		{
			string symbol = Stocksymbol;
			var request = HttpWebRequest.Create (string.Format (@"http://jazdzewskij.cs.spu.edu/requestHistory.php?sym={0}", symbol));//http://jazdzewskij.cs.spu.edu/requestQuote.php?sym=msft
			request.ContentType = "application/json";
			request.Method = "GET";
			using (HttpWebResponse response = request.GetResponse () as HttpWebResponse) {
				if (response.StatusCode != HttpStatusCode.OK)
					Console.Out.WriteLine ("error {0}", response.StatusCode);
				using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
					var content = reader.ReadToEnd ();
					if (string.IsNullOrWhiteSpace (content)) {
						Console.Out.WriteLine ("Response contained empty body...");
					} else {
						Console.Out.WriteLine ("Response Body: \r\n {0}", content);
					}
					Assert.NotNull (content);
					try
                    {
						string Result = (content.ToString ());
						var arr = JsonArray.Parse (Result);
						if (arr.Count > 0) {
							foreach (JsonObject obj in arr)
							{
								prices.Add(obj["qLastSalePrice"]);
								date.Add(obj["qQuoteDateTime"]);
							}

						} else {
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

