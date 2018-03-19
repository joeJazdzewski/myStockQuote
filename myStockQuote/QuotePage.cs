using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace myStockQuote
{
	[Activity (Label = "Quote")]			
	public class QuotePage : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.QuotePopUp);
			
			Button getHistory = FindViewById<Button>(Resource.Id.btnQpageGetHistory);

			Quote quote = new Quote();
			string symbol = Intent.GetStringExtra("symbol") ?? "";
            quote.setSymbol(symbol);
            quote.getStockInformation();

			getHistory.Click += (object sender, EventArgs e) => {
				var tentSearch = new Intent(this, typeof(History));
				tentSearch.PutExtra("symbol", symbol);
				StartActivity(tentSearch);
			};

			if (!string.IsNullOrWhiteSpace(quote.CompanyName) && !string.IsNullOrWhiteSpace(quote.Price))
			{
                FindViewById<TextView>(Resource.Id.QuoteTitle).Text = quote.CompanyName; 
				FindViewById<TextView>(Resource.Id.QuotePrice).Text = "$" + quote.Price;
                FindViewById<TextView>(Resource.Id.QuoteDate).Text = quote.Date;
                FindViewById<TextView>(Resource.Id.QuoteEarnings).Text = quote.EarningsPerShare;
                FindViewById<TextView>(Resource.Id.QuoteNet).Text = quote.NetChange;
                FindViewById<TextView>(Resource.Id.QuoteDividend).Text = "$" + quote.Dividend;
                FindViewById<TextView>(Resource.Id.QuoteMktCap).Text = quote.MarketCap;
                FindViewById<TextView>(Resource.Id.QuoteOutstandingShares).Text = quote.OutstandingShares;
			}
			else 
			{
				var tentSearch = new Intent(this, typeof(SearchResults));
				tentSearch.PutExtra("symbol", symbol);
				StartActivity(tentSearch);
			}
		}
	}
}

