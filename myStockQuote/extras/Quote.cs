using System;
using System.Json;
using System.IO;
using System.Net;
using NUnit.Framework;
using System.Web.Services;

namespace myStockQuote
{
	public class Quote
	{
		private string Stocksymbol { get; set; }
		public string companyName { get; set; }
		public string price { get; set; }
		public string date { get; set; }
		public string earningsPerShare { get; set; }
		public string NetChange { get; set; }
		public string Dividend { get; set; }
		public string OutstandingShares { get; set; }
		public string MarketCap{ get; set;}

		public Quote ()
		{
			Stocksymbol = "";
			companyName = "";
			price = "0.00";
			date = "01/01/0001";
			earningsPerShare = "0.00";
			NetChange = "0.00";
			Dividend = "0.00";
			OutstandingShares = "0";
			MarketCap = "0";

		}
		public void setSym(string Symbol)
		{
			Stocksymbol = Symbol;
		}
		public bool getStockInformation()
		{
			string symbol = Stocksymbol;
			var request = HttpWebRequest.Create(string.Format(@"http://jazdzewskij.cs.spu.edu/requestQuote.php?sym={0}", symbol));//http://jazdzewskij.cs.spu.edu/requestQuote.php?sym=msft
			request.ContentType = "application/json";
			request.Method = "GET";
			using (HttpWebResponse response = request.GetResponse () as HttpWebResponse) {
				if(response.StatusCode != HttpStatusCode.OK)
					Console.Out.WriteLine("error {0}", response.StatusCode);
				using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
					var content = reader.ReadToEnd ();
					if (string.IsNullOrWhiteSpace (content)) {
						Console.Out.WriteLine ("Response contained empty body...");
					} 
					else {
						Console.Out.WriteLine ("Response Body: \r\n {0}", content);
					}
					Assert.NotNull (content);
					try
					{
						//Result 
						string Result = (content.ToString());
						var obj = JsonObject.Parse(Result);
						if (obj.Count > 0)
						{
							companyName = obj["symName"];
							Stocksymbol = obj["symSymbol"];
							price = obj["qLastSalePrice"];
							date = obj["qQuoteDateTime"];
							earningsPerShare = obj["qNetChangePrice"];
							NetChange = obj["qNetChangePrice"];
							Dividend = obj["qCashDividendAmount"];
							OutstandingShares = obj["qTotalOutstandingSharesQty"];
							MarketCap = obj["symMarketCap"];

						}
						else
						{
							return false;
						}
						return true;
						//output.Text = obj["symName"] + "'s stock price today is $" + obj["qLastSalePrice"];
					}
					catch(Exception)
					{
						return false;
						//output.Text = "Stock was not found";
					}
				}
			}
		}
	}
}

