using System;
using System.Collections.Generic;
using System.Collections;
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


	[Activity (Label = "EditRoomsActivity")]			
	public class EditRoomsActivity : Activity
	{
		private Property Property { get;set;}

		private EditText BathField { get;set; }
		private EditText BedField { get; set; }
		private EditText CustomField {get;set;}

		private CheckBox KitchenCheckBox { get; set; }
		private CheckBox LivingCheckBox { get; set; }
		private CheckBox StorageCheckBox { get; set; }
		private CheckBox BasementCheckBox { get; set; }
		private CheckBox OfficeCheckBox { get; set; }
		
		private Room Kitchen { get; set; }
		private Room LivingRoom {get;set;}
		private Room Storage { get; set; }
		private Room Basement { get; set; }
		private Room Office { get; set; }

		private string CustomRoomLabel;



		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			String propertyID = Intent.GetStringExtra (Property.PropertyIDKey);

			Property = PropertyCollection.SharedCollection.FindPropertyWithId (propertyID);

			SetContentView (Resource.Layout.SetUpRoomView);

			//Set Views of EditTexts
			BathField = FindViewById<EditText> (Resource.Id.BathField);
			BedField = FindViewById<EditText> (Resource.Id.BedField);
			CustomField = FindViewById<EditText> (Resource.Id.CustomField);

			//Load Buttons and respective views
			Button AddCustomButton = FindViewById<Button> (Resource.Id.AddCustomButton);
			Button CancelRoomEditButton = FindViewById<Button> (Resource.Id.CancelRoomEditButton);
			Button SaveRoomsButton = FindViewById<Button> (Resource.Id.SaveRoomsButton);

			// Load CheckBox views
			CheckBox KitchenCheckBox = FindViewById<CheckBox> (Resource.Id.KitchenCheckBox);
			CheckBox LivingCheckBox = FindViewById<CheckBox> (Resource.Id.LivingCheckBox);
			CheckBox StorageCheckBox = FindViewById<CheckBox> (Resource.Id.StorageCheckBox);
			CheckBox BasementCheckBox = FindViewById<CheckBox> (Resource.Id.BasementCheckBox);
			CheckBox OfficeCheckBox = FindViewById<CheckBox> (Resource.Id.OfficeCheckBox);

			//Set CheckBox checks for existing rooms in RoomList
			KitchenCheckBox.Checked = SetCheckBoxByRoom ("Kitchen");
			LivingCheckBox.Checked = SetCheckBoxByRoom ("Living Room");
			StorageCheckBox.Checked = SetCheckBoxByRoom ("Storage");
			BasementCheckBox.Checked = SetCheckBoxByRoom ("Basement");
			OfficeCheckBox.Checked = SetCheckBoxByRoom ("Office");

			//Load EditTexts
			EditText BathField = FindViewById<EditText> (Resource.Id.BathField);
			EditText BedField = FindViewById<EditText> (Resource.Id.BedField);







			//Testing
			Room Kitchen = new Room ();
			Kitchen = Property.CreateRoom (Kitchen, "Kitchen");
			Property.RoomList.Add (Kitchen);








		
		}

		private bool CheckForRoomByLabel(Property prop, string label)
		{
			int count = 0;
			foreach (Room room in Property.RoomList)
			{
				if (room.Label == label)
				{
					count = count + 1;
				}
			}
			if (count == 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		private void SaveRooms()
		{
			//Save Kitchen Room
			if (KitchenCheckBox.Checked == true)
			{
				//If not found, add to Property.RoomList
				//If found, don't do anything
				if (CheckForRoomByLabel (Property,"Kitchen") == false)
				{
					Room Kitchen = new Room ();
					Kitchen = Property.CreateRoom (Kitchen, "Kitchen");
					Property.RoomList.Add (Kitchen);
				}
			}
			//If box.checked == false, check for room and delete if existing
			else
			{
				Property.RoomList.Remove (Kitchen);
			}

			//Save Living Room
			if (LivingCheckBox.Checked == true)
			{
				//If not found, add to Property.RoomList
				//If found, don't do anything
				if (CheckForRoomByLabel (Property,"Living Room") == false)
				{
					Room LivingRoom = new Room ();
					LivingRoom = Property.CreateRoom (LivingRoom,"Living Room");
					Property.RoomList.Add (LivingRoom);
				}
				else
				{
					Property.RoomList.Remove (LivingRoom);
				}
			}

			// Save storage
			if (StorageCheckBox.Checked == true)
			{
				//If not found, add to Property.RoomList
				//If found, don't do anything
				if (CheckForRoomByLabel (Property,"Storage") == false)
				{
					Room Storage = new Room ();
					Storage = Property.CreateRoom (Storage,"Storage");
					Property.RoomList.Add (Storage);
				}
				else
				{
					Property.RoomList.Remove (Storage);
				}
			}

			// Basement
			if (BasementCheckBox.Checked == true)
			{
				//If not found, add to Property.RoomList
				//If found, don't do anything
				if (CheckForRoomByLabel (Property,"Basement") == false)
				{
					Room Basment = new Room ();
					Basement = Property.CreateRoom (Basement, "Basement");
					Property.RoomList.Add (Basement);
				}
				else
				{
					Property.RoomList.Remove (Basement);
				}
			}

			if (OfficeCheckBox.Checked == true)
			{
				//If not found, add to Property.RoomList
				//If found, don't do anything
				if (CheckForRoomByLabel (Property,"Office") == false)
				{
					Room Office = new Room ();
					Office = Property.CreateRoom (Office, "Office");
					Property.RoomList.Add (Office);
				}
				else
				{
					Property.RoomList.Remove (Office);
				}
			}

			//ADD MORE CODE FOR CUSTOM ROOMS

		}




		// Find Rooms in the property list and set
		// Checkboxes to display those that are in the list.
		private bool SetCheckBoxByRoom (string label)
		{
			bool returnString;
			returnString = false;
			if (Property.RoomList == null)
			{
				return returnString;
			}
			foreach (Room room in Property.RoomList)
			{


				if (room.Label == label)
				{
					returnString = true;
					break;
				}
				else
				{
					returnString = false;
				}
			}
			return returnString;

		}

		private Property CreateBathrooms (Property prop, string num)
		{
			int numberBaths = (int)num;
			for (int i =1;i<= numberBaths;i++)
			{
				string BathString = "Bathroom" + i;
				Room Bathroom = new Room();
				Property.CreateRoom (Bathroom,
			}
		}
		private void RemoveRoomByCheckBox (CheckBox checkbox)
		{





		}		
	}
}

