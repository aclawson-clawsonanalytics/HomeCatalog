using System;
using System.IO;
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
using Android.Util;
using HomeCatalog.Core;
 

namespace HomeCatalog.Android
{


	[Activity (Label = "EditRoomsActivity")]			
	public class EditRoomsActivity : Activity
	{
		private Property Property { get;set;}

		private int numberBath {get;set;}
		private int numberBeds { get; set; }
		private string num { get; set; }

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



		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.SetUpRoomView);

			//Set Views of EditTexts
			BathField = FindViewById<EditText> (Resource.Id.BathField);
			BedField = FindViewById<EditText> (Resource.Id.BedField);
			CustomField = FindViewById<EditText> (Resource.Id.CustomField);

			//Load Buttons and respective views
			Button addCustomButton = FindViewById<Button> (Resource.Id.AddCustomButton);
			Button cancelRoomEditButton = FindViewById<Button> (Resource.Id.CancelRoomEditButton);
			Button saveRoomsButton = FindViewById<Button> (Resource.Id.SaveRoomsButton);
			Button viewRoomListButton = FindViewById<Button> (Resource.Id.ViewRoomListButton);


			// Load CheckBox views
			KitchenCheckBox = FindViewById<CheckBox> (Resource.Id.KitchenCheckBox);
			KitchenCheckBox.Clickable = true;
			LivingCheckBox = FindViewById<CheckBox> (Resource.Id.LivingCheckBox);
			LivingCheckBox.Clickable = true;
			StorageCheckBox = FindViewById<CheckBox> (Resource.Id.StorageCheckBox);
			StorageCheckBox.Clickable = true;
			BasementCheckBox = FindViewById<CheckBox> (Resource.Id.BasementCheckBox);
			BasementCheckBox.Clickable = true;
			OfficeCheckBox = FindViewById<CheckBox> (Resource.Id.OfficeCheckBox);
			OfficeCheckBox.Clickable = true;

			//Set CheckBox checks for existing rooms in RoomList
			KitchenCheckBox.Checked = SetCheckBoxByRoom ("Kitchen");
			LivingCheckBox.Checked = SetCheckBoxByRoom ("Living Room");
			StorageCheckBox.Checked = SetCheckBoxByRoom ("Storage");
			BasementCheckBox.Checked = SetCheckBoxByRoom ("Basement");
			OfficeCheckBox.Checked = SetCheckBoxByRoom ("Office");

			viewRoomListButton.Click += (sender, e) => 
			{
				SaveRooms ();
				Intent PassPropertyID = new Intent (this,typeof(ViewRoomListActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivityForResult (PassPropertyID,0);
			};

			addCustomButton.Click += (sender, e) => 
			{
				SaveCustomRoom (CustomField.Text);
				CustomField.Text = "";
			};

			cancelRoomEditButton.Click += (sender, e) => 
			{
				SetResult (Result.Canceled);
				Finish ();
			};

			saveRoomsButton.Click += (sender,e) =>
			{
				SaveRooms ();

				foreach (Room room in Property.RoomList.AllItems ())
				{
					Console.WriteLine (room.Label);
					string tag = "RoomCheck";
					string message = room.Label+" is in RoomList";
					Log.Info (tag,message);
				}
				Intent returnIntent = new Intent ();
				SetResult (Result.Ok, returnIntent);     
				Finish ();
			};


		}


		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			SetAllCheckBoxes ();
		}

		private void SetAllCheckBoxes ()
		{
			KitchenCheckBox.Checked = SetCheckBoxByRoom ("Kitchen");
			LivingCheckBox.Checked = SetCheckBoxByRoom ("Living Room");
			StorageCheckBox.Checked = SetCheckBoxByRoom ("Storage");
			BasementCheckBox.Checked = SetCheckBoxByRoom ("Basement");
			OfficeCheckBox.Checked = SetCheckBoxByRoom ("Office");
		}

		private bool CheckForRoomByLabel(string label)
		{
			int count = 0;
			foreach (Room room in Property.RoomList.AllItems ())
			{
				if (room.Label == label)
				{
					count = count + 1;
				}
			}
			if (count == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}


		private void SaveRooms()
		{
			SaveRoomFromCheckBox ("Kitchen", KitchenCheckBox);
			SaveRoomFromCheckBox ("Living Room", LivingCheckBox);
			SaveRoomFromCheckBox ("Storage", StorageCheckBox);
			SaveRoomFromCheckBox ("Basement", BasementCheckBox);
			SaveRoomFromCheckBox ("Office", OfficeCheckBox);

			//ADD MORE CODE FOR CUSTOM ROOMS
			SaveBathrooms (BathField.Text);
			SaveBedrooms (BedField.Text);
			SaveCustomRoom (CustomField.Text);
			DisplayRoomsInConsole ();
		}

		private void SaveRoomFromCheckBox(string label,CheckBox check)
		{
			if (check.Checked == true && CheckForRoomByLabel(label) == false)
			{
				Room newRoom = new Room ();
				newRoom.Label = label;
				Property.RoomList.Add (newRoom);
			}
			else if (check.Checked == false && CheckForRoomByLabel(label) == true)
			{
				Room room = Property.RoomList.RoomWithName (label);
				Property.RoomList.Remove (room);
			}
		}




		// Find Rooms in the property list and set
		// Checkboxes to display those that are in the list.
		private bool SetCheckBoxByRoom (string label)
		{
			if (CheckForRoomByLabel (label) == true)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		private void SaveBathrooms (string num)
		{
			if (num != "")
			{
				Property.NumberBaths = int.Parse (num);
				//int numberBath = Int32.Parse (num);
				for (int i =1; i<= Property.NumberBaths; i++)
				{

					string BathString = "Bathroom" + i;
					Room Bathroom = new Room();
					Property.CreateRoom (Bathroom, BathString);
					Property.RoomList.Add (Bathroom);
				}
			}
		}

		private void SaveBedrooms(string num)
		{
			if (num != "")
			{
				Property.NumberBeds = int.Parse (num);
				//int numberBeds = Int32.Parse (num);
				for (int i=1; i <= Property.NumberBeds; i++)
				{
					string BedString = "Bedroom" + i;
					Room Bedroom = new Room ();
					Property.CreateRoom (Bedroom, BedString);
					Property.RoomList.Add (Bedroom);
				}
			}
		}


		private void SaveCustomRoom (string label)
		{
			if (CustomField.Text != "")
			{
				Room CustomRoom = new Room ();
				Property.CreateRoom (CustomRoom, label);
				Property.RoomList.Add (CustomRoom);
			}
		}

		private void DisplayRoomsInConsole ()
		{
			foreach (Room room in Property.RoomList.AllItems ())
			{
				Console.WriteLine (room.Label);
			}
		}

		private void WriteRoomsToFile(string position)
		{
			string FileName = "C:\\Users\\Andrew\\Documents\\GitHub\\HomeCatalog\\RoomTestResults_"+position+".txt";
			StreamWriter file = new StreamWriter (FileName);
			file.WriteLine (Property.PropertyName);
			file.WriteLine (Property.Address);
			file.WriteLine (Property.City);
			file.WriteLine (Property.ZipCode);
			file.WriteLine ("Room List: ");
			file.WriteLine ();
			foreach (Room room in Property.RoomList.AllItems ())
			{
				file.WriteLine (room.Label);
			}

			file.Close ();

		}
		
	}
}

