using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HomeCatalog.Core;


namespace HomeCatalog.Android
{
	[Activity (Label = "ViewRoomListActivity")]
	public class ViewItemListActivity : Activity
	{
		private ItemListAdapter ListAdapter { get; set; }
		private Property Property { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			String propertyID = Intent.GetStringExtra (Property.PropertyIDKey);

			Property = PropertyCollection.SharedCollection.FindPropertyWithId (propertyID);

			SetContentView (Resource.Layout.ItemsView);

			ListView listView = FindViewById<ListView> (Resource.Id.itemsList);
			ListAdapter = new ItemListAdapter (this,Property);
			listView.Adapter = ListAdapter;



			Button backButton3 = FindViewById<Button> (Resource.Id.backButton3);
			backButton3.Click += (sender, e) => 
			{
				SetResult (Result.Ok);
				Finish ();
			};

			listView.ItemClick += (sender, e) => 
			{
				Property.RoomList.Remove (ListAdapter[e.Position]);
				ListAdapter.NotifyDataSetChanged ();
			};




		}

	}
}


