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
			TextView Title = (TextView) FindViewById (Resource.Id.QuoteTitle);
			TextView Price = (TextView) FindViewById (Resource.Id.QuotePrice);
			TextView Date = (TextView)FindViewById (Resource.Id.QuoteDate);
			TextView EarningsPerShare = (TextView)FindViewById (Resource.Id.QuoteEarnings);
			TextView NetChange = (TextView)FindViewById (Resource.Id.QuoteNet);
			TextView Dividends = (TextView)FindViewById (Resource.Id.QuoteDividend);
			TextView MrktCap = FindViewById<TextView> (Resource.Id.QuoteMktCap);
			TextView OutstandingShares = FindViewById<TextView> (Resource.Id.QuoteOutstandingShares);
			Button getHistory = FindViewById<Button> (Resource.Id.btnQpageGetHistory);

			Quote Con = new Quote();
			string sym = Intent.GetStringExtra ("symbol") ?? "";
			Con.setSym(sym);
			Con.getStockInformation();

			getHistory.Click += (object sender, EventArgs e) => {
				var tentSearch = new Intent(this, typeof(History));
				tentSearch.PutExtra("symbol", sym);
				StartActivity(tentSearch);
			};

			if (Con.companyName != "" && Con.price != "")
			{
				Title.Text = Con.companyName; 
				Price.Text ="$" + Con.price;
				Date.Text =  Con.date;
				EarningsPerShare.Text = Con.earningsPerShare;
				NetChange.Text =  Con.NetChange;
				Dividends.Text = "$" + Con.Dividend;
				MrktCap.Text = Con.MarketCap;
				OutstandingShares.Text = Con.OutstandingShares;
			}
			else 
			{
				var tentSearch = new Intent(this, typeof(SearchResults));
				tentSearch.PutExtra("symbol", sym);
				StartActivity(tentSearch);
			}
		}
	}
}

