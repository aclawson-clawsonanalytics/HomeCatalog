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
		public Button AddPropertyButton { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.MainView);

			// Get our button from the layout resource,
			// and attach an event to it
			Button AddPropertyButton = FindViewById<Button> (Resource.Id.AddPropertyButton);
			
			AddPropertyButton.Click += (sender,e) => {
				StartActivity (typeof(AddEditPropertyActivity));
			};
		}
	}
}


