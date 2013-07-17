using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;



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
				StartActivityForResult (typeof(AddEditPropertyActivity), (int)PropertyRequest.ADD_PROPERTY);
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


