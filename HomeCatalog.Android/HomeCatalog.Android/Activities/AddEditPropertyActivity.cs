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
	[Activity (Label = "AddEditProperty")]			
	public class AddEditPropertyActivity : Activity
	{
		private Property Property { get; set; }


		private EditText PropNameField { get; set; }
		private EditText PropAddressField { get; set; }
		private EditText PropCityField { get; set; }
		private EditText PropStateField { get; set; }
		private EditText PropZipField { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			String propertyID = Intent.GetStringExtra (Property.PropertyIDKey);

			Property = PropertyCollection.SharedCollection.FindPropertyWithId (propertyID);

			SetContentView (Resource.Layout.PropertyAddEditView);


			PropNameField = FindViewById<EditText> (Resource.Id.propNameField);
			PropAddressField = FindViewById<EditText> (Resource.Id.PropAddressField);
			PropCityField = FindViewById<EditText> (Resource.Id.PropCityField);
			PropStateField = FindViewById<EditText> (Resource.Id.PropCityField);
			PropZipField = FindViewById<EditText> (Resource.Id.PropZipField);

			DisplayPropertyInField ();

			Button SaveButton = FindViewById<Button> (Resource.Id.SaveButton);
			Button CancelButton = FindViewById<Button> (Resource.Id.CancelButton);
			Button EditRoomsButton = FindViewById<Button> (Resource.Id.EditRoomsButton);
			Button EditCategoriesButton = FindViewById<Button> (Resource.Id.EditCategoriesButton);

			SaveButton.Click += (sender,e) => {
				SaveProperty ();
				Intent returnIntent = new Intent ();
				SetResult (Result.Ok, returnIntent);     
				Finish ();
			};

			EditRoomsButton.Click += (sender,e) => {
				SaveProperty ();

				Intent PassPropertyID = new Intent (this,typeof(EditRoomsActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);

			};

			EditCategoriesButton.Click += (sender, e) => 
			{
				SaveProperty ();

				Intent PassPropertyID = new Intent (this,typeof(EditCategoriesActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);
			};


		}

		private void SaveProperty() 
		{
			// Set Property Attributes by the text in each EditText field
			Property.PropertyName = PropNameField.Text;
			Property.Address = PropAddressField.Text;
			Property.City = PropCityField.Text;
			Property.State = PropStateField.Text;
			Property.ZipCode = PropZipField.Text;
		}

		private void DisplayPropertyInField ()
		{
			PropNameField.Text = Property.PropertyName;
			PropAddressField.Text = Property.Address;
			PropCityField.Text = Property.City;
			PropStateField.Text = Property.State;
			PropZipField.Text = Property.ZipCode;
		}
	}

}

