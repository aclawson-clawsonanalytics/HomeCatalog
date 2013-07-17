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
	public class AddEditProperty : Activity
	{
		Property Property = new Property ();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property.PropertyName = "Home";

			SetContentView (Resource.Layout.PropertyAddEditView);
			EditText propNameField = FindViewById<EditText> (Resource.Id.propNameField);
			propNameField.Text = Property.PropertyName;




			// Create your application here
		}
	}
}

