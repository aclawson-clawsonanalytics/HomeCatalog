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
		private string roomLabel { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.RoomsView);

			ListView listView = FindViewById<ListView> (Resource.Id.roomList);
			ListAdapter = new RoomListAdapter (this,Property);
			listView.Adapter = ListAdapter;
			ListAdapter.NotifyDataSetChanged ();
			listView.ItemClick += (Object sender, AdapterView.ItemClickEventArgs e) =>
			{
				//roomLabel = ListAdapter[e.Position].Label;
				Intent RoomEdit = new Intent(this,typeof(RoomEditActivity));
				RoomEdit.PutExtra ("roomID",ListAdapter[e.Position].ID);
				StartActivityForResult (RoomEdit,0);
			};
			Button AddBathroomButton = FindViewById<Button> (Resource.Id.addBathroomButton);
			AddBathroomButton.Click += (sender, e) => 
			{
				AddBathroom ();
				ListAdapter.NotifyDataSetChanged ();
//				Intent PassPropertyID = new Intent (this,typeof(EditRoomsActivity));
//				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
//				StartActivity (PassPropertyID);
			};

			Button AddBedroomButton = FindViewById<Button> (Resource.Id.addBedroomButton);
			AddBedroomButton.Click += (sender,e) =>
			{
				AddBedroom ();
				ListAdapter.NotifyDataSetChanged();
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
						Room newRoom = new Room ();
						newRoom.Label = "Custom";

						Property.RoomList.Add (newRoom);
						Property.RoomList.Save (newRoom);
						Intent createCustomRoom = new Intent(this,typeof(RoomEditActivity));
						createCustomRoom.PutExtra ("roomID",newRoom.ID);
						StartActivityForResult (createCustomRoom,0);
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
//			listView.ItemClick += (Object sender, AdapterView.ItemClickEventArgs e) =>
//			{
//
//
//				//				var PropertyDetails = new Intent (this,typeof(PropertyDetailActivity));
//				//				PropertyStore store = PropertyCollection.SharedCollection.FindPropertyStoreWithID (ListAdapter[e.Position].ID);
//				//				PropertyStore.CurrentStore = store;
//				//				StartActivity (PropertyDetails);
//			};
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			if (requestCode == 0)
			{
				ListAdapter.NotifyDataSetChanged ();
			}
		}


		private void AddBathroom()
		{

			if (RoomLabelExists("Bathroom") == false)
			{
				Room newRoom = new Room ();
				newRoom.Label = "Bathroom";
				Property.RoomList.Add (newRoom);
				SetResult (Result.Ok);
			}
			else
			{
				int bathCount = 2;
				string bathString = "Bathroom" + bathCount.ToString ();
				while (RoomLabelExists(bathString) == true)
				{
					bathCount = bathCount + 1;
					bathString = "Bathroom" + bathCount.ToString ();
				}

				Room newRoom = new Room ();
				newRoom.Label = bathString;
				Property.RoomList.Add (newRoom);
			}
		}

		private void AddBedroom()
		{

			if (RoomLabelExists("Bedroom") == false)
			{
				Room newRoom = new Room ();
				newRoom.Label = "Bedroom";
				Property.RoomList.Add (newRoom);
			}
			else
			{
				int bedCount = 2;
				string bedString = "Bedroom" + bedCount.ToString ();
				while (RoomLabelExists(bedString) == true)
				{
					bedCount = bedCount + 1;
					bedString = "Bedroom" + bedCount.ToString ();
				}

				Room newRoom = new Room ();
				newRoom.Label = bedString;
				Property.RoomList.Add (newRoom);
			}
		}

		private bool RoomLabelExists(string label)
		{
			foreach (Room rm in Property.RoomList.AllItems ())
			{
				if (rm.Label == label)
				{
					return true;
				}
			}
			return false;
		}
	}
}


