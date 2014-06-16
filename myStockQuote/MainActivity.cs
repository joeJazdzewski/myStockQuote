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
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.btnGetQuote);
			Button btnHistory = FindViewById<Button> (Resource.Id.btnGetHistory);
			Button btnSearch = FindViewById<Button> (Resource.Id.btnSearchStocks);
			TextView output = FindViewById<TextView> (Resource.Id.sQuote);
			EditText input = FindViewById<EditText> (Resource.Id.search);

			button.Click += (sender, e) => {
				if ( input.Text != "")
				{
					var tentQuote = new Intent(this, typeof(QuotePage));
					tentQuote.PutExtra("symbol", input.Text);
					StartActivity(tentQuote);
				}
			};

			btnHistory.Click += (sender, e) => {
				if(input.Text != "")
				{
					var tentHistory = new Intent(this, typeof(History));
					tentHistory.PutExtra("symbol", input.Text);
					StartActivity(tentHistory);
				}
			};

			btnSearch.Click += (sender, e) => {
				if(input.Text != "")
				{
					var tentSearch = new Intent(this, typeof(SearchResults));
					tentSearch.PutExtra("symbol", input.Text);
					StartActivity(tentSearch);
				}
			};

		}
	}
}