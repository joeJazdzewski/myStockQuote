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
	[Activity (Label = "SearchResults")]			
	public class SearchResults : Activity
	{
		ListView Results;
		TextView Message;
		SearchStocks con = new SearchStocks (); 
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.SearchResultsLayout);
			string sym = Intent.GetStringExtra ("symbol") ?? "";
			con.setSearchString (sym);
			con.getStockInformation ();
			Message = FindViewById<TextView> (Resource.Id.SearchMessage);
			if (con.CompanySym.Count != 0) {
				Results = (ListView)FindViewById (Resource.Id.SearchList);
				Message.SetHeight (0);
				Results.Adapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, con.CompanyName);
				Results.ItemClick += (sender, e) => {
					var tentQuote = new Intent (this, typeof(QuotePage));
					tentQuote.PutExtra ("symbol", con.CompanySym.ToArray () [e.Position]);
					StartActivity (tentQuote);
				};
			} else {
				Message.Text = "No Results Found";
			}

		}
	}
}

