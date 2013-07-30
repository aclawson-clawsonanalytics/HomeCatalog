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
		private PropertyListAdapter ListAdapter { get; set; }
		private enum PropertyRequest
		{
			ADD_PROPERTY
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.MainView);

			ListView listView = FindViewById<ListView> (Resource.Id.propertyList);
			ListAdapter = new PropertyListAdapter (this);
			listView.Adapter = ListAdapter;


			listView.ItemClick += (Object sender, AdapterView.ItemClickEventArgs e) =>
			{
				var PropertyDetails = new Intent (this,typeof(PropertyDetailActivity));
				PropertyStore store = PropertyCollection.SharedCollection.FindPropertyStoreWithID (ListAdapter[e.Position].ID);
				PropertyStore.CurrentStore = store;
				StartActivity (PropertyDetails);
			};

			Button AddPropertyButton = FindViewById<Button> (Resource.Id.AddPropertyButton);

			AddPropertyButton.Click += (sender,e) => {

				PropertyStore store = PropertyCollection.SharedCollection.NewPropertyStore ();
				PropertyStore.CurrentStore = store;
				Intent AddIntent = new Intent(this,typeof(AddEditPropertyActivity));
				StartActivityForResult (AddIntent, (int)PropertyRequest.ADD_PROPERTY);
			};
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (requestCode == (int)PropertyRequest.ADD_PROPERTY) {
				ListAdapter.NotifyDataSetChanged ();
			}
		}
	}
}


