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
		private string _stocksymbol { get; set; }

		public string CompanyName { get; set; }
		public string Price { get; set; }
		public string Date { get; set; }
		public string EarningsPerShare { get; set; }
		public string NetChange { get; set; }
		public string Dividend { get; set; }
		public string OutstandingShares { get; set; }
		public string MarketCap{ get; set;}

        public Quote(string symbol = "")
        {
            _stocksymbol = symbol;
            CompanyName = "";
            Price = "0.00";
            Date = "01/01/0001";
            EarningsPerShare = "0.00";
            NetChange = "0.00";
            Dividend = "0.00";
            OutstandingShares = "0";
            MarketCap = "0";
        }

        public void setSymbol(string symbol)
		{
			_stocksymbol = symbol;
		}

		public bool getStockInformation()
		{
			string symbol = _stocksymbol;
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
						string Result = (content.ToString());
						var obj = JsonObject.Parse(Result);
						if (obj.Count > 0)
						{
							CompanyName = obj["symName"];
							_stocksymbol = obj["symSymbol"];
							Price = obj["qLastSalePrice"];
							Date = obj["qQuoteDateTime"];
							EarningsPerShare = obj["qNetChangePrice"];
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
					}
					catch(Exception)
					{
						return false;
					}
				}
			}
		}
	}
}

