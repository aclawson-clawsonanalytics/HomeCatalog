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

			KitchenCheckBox.Checked = true;
//			if (Property.RoomList.Contains (Kitchen))
//			{
//				KitchenCheckBox.Checked;
//				//Toast.MakeText (this, "Selected", ToastLength.Short).Show ();
//			}
////			else
////			{
////				Toast.MakeText (this, "Not Selected", ToastLength.Short).Show ();
////			}
//
//			if (Property.RoomList.Contains (LivingRoom))
//			{
//				LivingCheckBox.Checked;
//			}
		
		}




	}
}

