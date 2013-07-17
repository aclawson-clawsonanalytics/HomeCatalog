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
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Property = new Property ();
			SetContentView (Resource.Layout.PropertyAddEditView);
			EditText propNameField = FindViewById<EditText> (Resource.Id.propNameField);
			propNameField.Text = Property.PropertyID;
			Button saveButton = FindViewById<Button> (Resource.Id.SaveButton);
			saveButton.Click += (sender,e) => {
				Intent returnIntent = new Intent();
				SetResult(Result.Ok, returnIntent);     
				Finish();
			}; 
		}
	}
}

