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
			String propertyID = Intent.GetStringExtra ("propertyID");
			Property = PropertyCollection.SharedCollection.FindPropertyWithId (propertyID);

			SetContentView (Resource.Layout.PropertyAddEditView);


			PropNameField = FindViewById<EditText> (Resource.Id.propNameField);
			PropAddressField = FindViewById<EditText> (Resource.Id.PropAddressField);
			PropCityField = FindViewById<EditText> (Resource.Id.PropCityField);
			PropStateField = FindViewById<EditText> (Resource.Id.PropCityField);

			Button saveButton = FindViewById<Button> (Resource.Id.SaveButton);
			saveButton.Click += (sender,e) => {
				SaveProperty();
				Intent returnIntent = new Intent();
				SetResult(Result.Ok, returnIntent);     
				Finish();
			}; 
		}
		private void SaveProperty() 
		{
			Property.PropertyName = PropNameField.Text
		}
	}

}

