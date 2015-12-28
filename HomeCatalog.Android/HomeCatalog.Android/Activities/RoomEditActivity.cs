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
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	[Activity (Label = "RoomEditActivity")]			
	public class RoomEditActivity : StandardActivity
	{
		private Property Property { get; set; }
		private int roomID { get; set; }
		private Room room { get; set; }

		private EditText roomLabelField { get; set; }

		private TextView labelTest { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			int roomID = Intent.GetIntExtra ("roomID",0);
			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.RoomEditLayout);

			roomLabelField = FindViewById<EditText> (Resource.Id.roomField);
			//room = Property.RoomList.RoomWithID (roomID);
			//roomLabelField.Text = Property.RoomList.RoomWithID (roomID).Label;
			roomLabelField.Text = Property.RoomList.RoomWithID (roomID).Label;

			Button saveButton = FindViewById<Button> (Resource.Id.saveRoomLabelButton);
			saveButton.Click += (sender, e) => 
			{
				//Property.RoomList.RoomWithID (roomID).Label = roomLabelField.Text;
				//Finish ();
				room = Property.RoomList.RoomWithID (roomID);
				room.Label = roomLabelField.Text;

				try {
					Property.RoomList.Save (room);
				}
				catch (InvalidObjectException) {
					// - CODE FOR DIALOG GOES HERE
					ValidationDialogFragment.DisplayDialogForObject (room, this);
				}
				//Intent returnIntent = new Intent ();
				SetResult (Result.Ok);
				Finish ();


			};

			Button deleteButton = FindViewById<Button> (Resource.Id.deleteRoomButton);
			deleteButton.Click += (sender, e) => 
			{
				var transaction = FragmentManager.BeginTransaction();
				DeleteDialogFragment deleteDialog = new DeleteDialogFragment();
				deleteDialog.Show(transaction,"deleteDialog");
				deleteDialog.OnItemSelected += (DialogClickEventArgs a) =>
				{
					switch (a.Which)
					{
					case 0:
						{
							Property.RoomList.Remove (Property.RoomList.RoomWithID (roomID));
							Finish ();
							break;
						}
					case 1:
						{
							
							break;
						}
					}
				};
				//Property.RoomList.Remove (Property.RoomList.RoomWithID (roomID));
				//Finish ();
			};


			Button cancelButton = FindViewById<Button> (Resource.Id.cancelRoomEditButton);
			cancelButton.Click += (sender, e) => 
			{                       
				Finish ();
			};



		}

		private void FillField ()
		{
			if (Property.RoomList.RoomWithID(roomID) == null)
			{
				return;
			}
			else
			{
				roomLabelField.Text = Property.RoomList.RoomWithID (roomID).Label;
			}
		}


	}
}

