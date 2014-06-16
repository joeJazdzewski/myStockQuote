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
	[Activity (Label = "History")]			
	public class History : Activity
	{
		ListView myListView;
		cHistory myhistory = new cHistory();
		Quote myQuote = new Quote();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.HistoryPage);
			LinearLayout MyLayout = new LinearLayout (this);
			MyLayout.Orientation = Orientation.Vertical;
			string sym = Intent.GetStringExtra ("symbol") ?? "";
			myhistory.setSym (sym);
			myhistory.getStockInformation ();
			TextView title = (TextView)FindViewById (Resource.Id.HisTitle);
			title.SetAllCaps (true);
			if (sym != "") {
				myQuote.setSym (sym);
				myQuote.getStockInformation ();
				if (myQuote.companyName != "") {
					title.Text = myQuote.companyName;
					myListView = (ListView)FindViewById (Resource.Id.HisList);
					myListView.Adapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, myhistory.date);
					myListView.TextAlignment = TextAlignment.Center;
					myListView.ItemClick += (sender, e) => {

						var PopUp = new AlertDialog.Builder (this);
						string t = "Price: $" + myhistory.prices.ToArray () [e.Position];
						PopUp.SetTitle (myhistory.date.ToArray () [e.Position]);
						PopUp.SetMessage (t);
						PopUp.Create ().Show ();
					}; 
				} else {
					var tentSearch = new Intent(this, typeof(SearchResults));
					tentSearch.PutExtra("symbol", sym);
					StartActivity(tentSearch);
				}
			}
		}
	}
}

