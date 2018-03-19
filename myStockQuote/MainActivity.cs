using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using System.Json;
using NUnit.Framework;
using System.Web.Services;

namespace myStockQuote
{
	[Activity (Label = "Financial Stock Exchange", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            #region QuoteButton
            Button getQuotebutton = FindViewById<Button>(Resource.Id.btnGetQuote);
            getQuotebutton.Click += (sender, e) => 
            {
                EditText search = FindViewById<EditText>(Resource.Id.search);
                if (!string.IsNullOrWhiteSpace(search.Text))
				{
					var tentQuote = new Intent(this, typeof(QuotePage));
					tentQuote.PutExtra("symbol", search.Text);
					StartActivity(tentQuote);
				}
			};
            #endregion
            #region History Button
            Button getHistoryButton = FindViewById<Button>(Resource.Id.btnGetHistory);
            getHistoryButton.Click += (sender, e) => 
            {
                EditText search = FindViewById<EditText>(Resource.Id.search);
                if (!string.IsNullOrWhiteSpace(search.Text))
				{
					var tentHistory = new Intent(this, typeof(History));
					tentHistory.PutExtra("symbol", search.Text);
					StartActivity(tentHistory);
				}
			};
            #endregion
            #region Search Button
            Button searchButton = FindViewById<Button>(Resource.Id.btnSearchStocks);
            searchButton.Click += (sender, e) => 
            {
                EditText search = FindViewById<EditText>(Resource.Id.search);
                if (!string.IsNullOrWhiteSpace(search.Text))
				{
					var tentSearch = new Intent(this, typeof(SearchResults));
					tentSearch.PutExtra("symbol", search.Text);
					StartActivity(tentSearch);
				}
			};
            #endregion
        }
    }
}