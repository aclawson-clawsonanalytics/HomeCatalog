using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HomeCatalog.Core;


namespace HomeCatalog.Android
{
	[Activity (Label = "HomeCatalog.Android", MainLauncher = true)]
	public class BrowsePropertyActivity : Activity
	{
		private enum PropertyRequest
		{
			ADD_PROPERTY
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.MainView);

			Button addPropertyButton = FindViewById<Button> (Resource.Id.AddPropertyButton);
			
			addPropertyButton.Click += (sender,e) => {

				Property property = new Property();
				PropertyCollection.SharedCollection.AddProperty(property);
				Intent PassPropertyID = new Intent(this,typeof(AddEditPropertyActivity));
				PassPropertyID.PutExtra ("propertyID",property.PropertyID);
				StartActivityForResult (PassPropertyID, (int)PropertyRequest.ADD_PROPERTY);
			};
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (requestCode == (int)PropertyRequest.ADD_PROPERTY) {

			}
		}
	}
}


