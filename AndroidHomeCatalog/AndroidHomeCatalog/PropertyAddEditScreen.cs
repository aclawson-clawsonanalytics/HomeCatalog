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

namespace AndroidHomeCatalog
{
	[Activity (Label = "PropertyAddEditScreen")]			
	public class PropertyAddEditScreen : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Declare newProperty as Item Container

			// Fill Item Container with intent
			ItemContainer NewProperty = (ItemContainer) Intent.GetSerializableExtra ("NewProperty");

			SetContentView (Resource.Layout.PropertyAddEditView);
			EditText propNameField = FindViewById<EditText> (Resource.Layout.PropertyAddEditView);



			// Create your application here
		}

		protected override void OnResume()
		{

		}
	}
}

