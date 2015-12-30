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
	[Activity (Label = "Edit Property Information")]			
	public class AddEditPropertyActivity : StandardActivity
	{
		private Property Property { get; set; }

		private EditText PropNameField { get; set; }
		private EditText PropAddressField { get; set; }
		private EditText PropCityField { get; set; }
		private EditText PropStateField { get; set; }
		private EditText PropZipField { get; set; }
		private EditText PropCountryField { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			SetContentView (Resource.Layout.PropertyAddEditView);

			PropNameField = FindViewById<EditText> (Resource.Id.propNameField);
			PropAddressField = FindViewById<EditText> (Resource.Id.PropAddressField);
			PropCityField = FindViewById<EditText> (Resource.Id.PropCityField);
			PropStateField = FindViewById<EditText> (Resource.Id.PropStateField);
			PropZipField = FindViewById<EditText> (Resource.Id.PropZipField);
			PropCountryField = FindViewById<EditText> (Resource.Id.PropCountryField);

			DisplayPropertyInField ();

			Button SaveButton = FindViewById<Button> (Resource.Id.SaveButton);
			Button CancelButton = FindViewById<Button> (Resource.Id.CancelButton);
			Button EditRoomsButton = FindViewById<Button> (Resource.Id.EditRoomsButton);
			Button EditCategoriesButton = FindViewById<Button> (Resource.Id.EditCategoriesButton);
			Button AddPropertyPhotoButton = FindViewById<Button> (Resource.Id.AddPropertyPhotoButton);

			CancelButton.Click += (sender, e) => 
			{
				SetResult (Result.Canceled);
				Finish ();
			};
			SaveButton.Click += (sender,e) => {
				SaveProperty ();
				Intent returnIntent = new Intent ();
				SetResult (Result.Ok, returnIntent);     
				Finish ();
			};

			EditRoomsButton.Click += (sender,e) => {
				SaveProperty ();

				Intent PassPropertyID = new Intent (this,typeof(ViewRoomListActivity)); //Was EditRoomsActivity
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);

			};

			EditCategoriesButton.Click += (sender, e) => 
			{
				SaveProperty ();

				Intent PassPropertyID = new Intent (this,typeof(ViewCategoryListActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);
			};

			AddPropertyPhotoButton.Click += (sender, e) => 
			{
				var transaction = FragmentManager.BeginTransaction();
				PhotoDialogFragment photoDialog = new PhotoDialogFragment();
				photoDialog.Show(transaction,"photoDialog");
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
			Property.Country = PropCountryField.Text;
			PropertyStore.CurrentStore.SaveProperty ();
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

