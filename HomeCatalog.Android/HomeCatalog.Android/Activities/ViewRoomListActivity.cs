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
	[Activity (Label = "View Rooms")]
	public class ViewRoomListActivity : Activity
	{
		private RoomListAdapter ListAdapter { get; set; }
		private Property Property { get; set; }
		private string roomIDText { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.RoomsView);

			ListView listView = FindViewById<ListView> (Resource.Id.roomList);
			ListAdapter = new RoomListAdapter (this,Property);
			listView.Adapter = ListAdapter;

			Button AddBathroomButton = FindViewById<Button> (Resource.Id.addBathroomButton);
			AddBathroomButton.Click += (sender, e) => 
			{
//				Intent PassPropertyID = new Intent (this,typeof(EditRoomsActivity));
//				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
//				StartActivity (PassPropertyID);
			};

			Button AddBedroomButton = FindViewById<Button> (Resource.Id.addBedroomButton);
			AddBedroomButton.Click += (sender,e) =>
			{
//				SetResult (Result.Canceled);
//				Finish ();
			};

			Button AddRoomButton = FindViewById<Button> (Resource.Id.addRoomButton);
			AddRoomButton.Click += (sender,e) =>
			{
				var transaction = FragmentManager.BeginTransaction();
				RoomListDialogFragment roomDialog = new RoomListDialogFragment ();
				roomDialog.Show(transaction,"roomListDialog");

				roomDialog.OnItemSelected += (DialogClickEventArgs a) =>
				{
					switch (a.Which)
					{
						case 0:
					{
						Room newRoom = new Room();
						newRoom.Label = "Attic";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 1:
					{
						Room newRoom = new Room();
						newRoom.Label = "Basement";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 2:
					{
						Room newRoom = new Room ();
						newRoom.Label = "Garage";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged();
						break;
					}
						case 3:
					{
						Room newRoom = new Room ();
						newRoom.Label = "Kitchen";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 4:
					{
						Room newRoom = new Room ();
						newRoom.Label = "Living Room";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 5:
					{
						Room newRoom = new Room ();
						newRoom.Label = "Office";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 6:
					{
						Room newRoom = new Room ();
						newRoom.Label = "Storage";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 7:
					{
						// Add Code to go to the Edit Room View for custom room
						Intent createCustomRoom = new Intent(this,typeof(RoomEditActivity));
						createCustomRoom.PutExtra ("roomID",0);
						break;
					}
					}
			};

//			listView.ItemClick += (Object sender, AdapterView.ItemClickEventArgs e) =>
//			{
//				var transaction = FragmentManager.BeginTransaction ();
//				DeleteDialogFragment deleteDialog = new DeleteDialogFragment ();
//				deleteDialog.Show (transaction, "deleteDialog");
//
//				deleteDialog.OnItemSelected += (DialogClickEventArgs a) =>
//				{
//					Property.RoomList.Remove (ListAdapter [e.Position]);
//					ListAdapter.NotifyDataSetChanged ();
//				};
//			};

			};

			// listView ItemClick event
			// Clicking on an existing room takes the user to a roomEditActivity
			listView.ItemClick += (Object sender, AdapterView.ItemClickEventArgs e) =>
			{


				//				var PropertyDetails = new Intent (this,typeof(PropertyDetailActivity));
				//				PropertyStore store = PropertyCollection.SharedCollection.FindPropertyStoreWithID (ListAdapter[e.Position].ID);
				//				PropertyStore.CurrentStore = store;
				//				StartActivity (PropertyDetails);
			};
		}

		private void AddBathroom()
		{

		}

	}
}


