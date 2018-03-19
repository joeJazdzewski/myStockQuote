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
		SearchStocks searchStocks = new SearchStocks (); 
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.SearchResultsLayout);

			string symbol = Intent.GetStringExtra ("symbol") ?? "";
			searchStocks.setSearchString (symbol);
			searchStocks.getStockInformation ();

            TextView message = FindViewById<TextView> (Resource.Id.SearchMessage);

            if (searchStocks.CompanySymbol.Count != 0)
            {
                message.SetHeight(0);
                ListView results = FindViewById<ListView>(Resource.Id.SearchList);
				results.Adapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, searchStocks.CompanyName);

                results.ItemClick += (sender, e) => 
                {
					var tentQuote = new Intent (this, typeof(QuotePage));
					tentQuote.PutExtra ("symbol", searchStocks.CompanySymbol.ToArray()[e.Position]);
					StartActivity (tentQuote);
				};
			}
            else
            {
				message.Text = "No Results Found";
			}
		}
	}
}