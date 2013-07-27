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
	public class ViewRoomListActivity : Activity
	{
		private RoomListAdapter ListAdapter { get; set; }
		private Property Property { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			String propertyID = Intent.GetStringExtra (Property.PropertyIDKey);

			Property = PropertyCollection.SharedCollection.FindPropertyWithId (propertyID);

			SetContentView (Resource.Layout.RoomsView);

			ListView listView = FindViewById<ListView> (Resource.Id.roomList);
			ListAdapter = new RoomListAdapter (this,Property);
			listView.Adapter = ListAdapter;

			Button EditRoomsButton = FindViewById<Button> (Resource.Id.EditRoomsButton2);
			EditRoomsButton.Click += (sender, e) => 
			{
				Intent PassPropertyID = new Intent (this,typeof(EditRoomsActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);
			};

			Button BackButton = FindViewById<Button> (Resource.Id.BackButton);
			BackButton.Click += (sender,e) =>
			{
				SetResult (Result.Canceled);
			};

			listView.ItemClick += (sender, e) => 
			{
				Property.RoomList.Remove (ListAdapter[e.Position]);
				ListAdapter.NotifyDataSetChanged ();
			};




		}

	}
}


