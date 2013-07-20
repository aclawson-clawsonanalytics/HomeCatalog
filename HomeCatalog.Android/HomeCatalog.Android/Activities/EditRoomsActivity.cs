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


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			String propertyID = Intent.GetStringExtra ("propertyID");

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

			// Set CheckBox checks for existing rooms in RoomList
			KitchenCheckBox.Checked = SetCheckBoxByRoom (Kitchen);
			LivingCheckBox.Checked = SetCheckBoxByRoom (Living);
			StorageCheckBox.Checked = SetCheckBoxByRoom (Storage);
			BasementCheckBox.Checked = SetCheckBoxByRoom (Basement);
			OfficeCheckBox.Checked = SetCheckBoxByRoom (Office);

			// Set Click events for CheckBoxes
			KitchenCheckBox.Click += (o,e) => 
			{
				CheckBoxClicker (KitchenCheckBox, "Kitchen");
			};

			LivingCheckBox.Click += (o,e) =>
			{
				CheckBoxClicker (LivingCheckBox,"Living Room");
			};

			StorageCheckBox.Click += (o,e) =>
			{
				CheckBoxClicker (StorageCheckBox,"Storage");
			};

			BasementCheckBox.Click += (o,e) =>
			{
				CheckBoxClicker (BasementCheckBox, "Living Room");
			};

			OfficeCheckBox.Click += (o,e) =>
			{
				CheckBoxClicker (OfficeCheckBox,"Office");
			};


		
		}

		private void CheckBoxClicker (CheckBox cb,string label)
		{
			if (cb.Checked == false)
			{
				foreach (Room rm in Property.RoomList)
				{
					if (rm.Label == label)
					{
						Property.RoomList.Remove (rm);
					}
				}
			}
			else
			{
				int count = 0;
				foreach (Room rm in Property.RoomList)
				{
					if (rm.Label == label)
					{
						count = count + 1;
					}
				}

				if (count == 0) 
				{
					Room room = new Room ();
					switch (label)
					{
					case "Kitchen":
							
						room.RoomID = 1;
						room.Label = "Kitchen";
						Property.RoomList.Add (room);
					case "LivingRoom":
						room.RoomID = 2;
						Room.Label = "Living Room";
						Property.RoomList.Add (room);
					case "Storage":
						room.RoomID = 3;
						room.Label = "Storage";
						Property.RoomList.Add (room);
					case "Basement":
						room.RoomID = 4;
						room.Label = "Basement";
						Property.RoomList.Add (room);
					case "Office":
						room.RoomID = 5;
						room.Label = "Office";
						Property.RoomList.Add (room);
					}
				}
			}
		}


		private bool SetCheckBoxByRoom (Room room)
		{
			if (Property.RoomList.Contains(Room)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void RemoveRoomByCheckBox (CheckBox checkbox)
		{





	}
}

