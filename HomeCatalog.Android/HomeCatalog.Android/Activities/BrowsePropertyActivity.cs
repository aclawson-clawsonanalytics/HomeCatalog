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
	[Activity (Label = "Select or add a property", MainLauncher = true)]
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
				var transaction = FragmentManager.BeginTransaction();
				OptionDialogFragment optionDialog = new OptionDialogFragment();
				optionDialog.Show(transaction,"optionDialog");

				optionDialog.OnItemSelected += (DialogClickEventArgs a) =>
				{
					switch (a.Which)
					{
					case 0:
					{
						var PropertyDetails = new Intent (this,typeof(PropertyDetailActivity));
						PropertyStore store = PropertyCollection.SharedCollection.FindPropertyStoreWithID (ListAdapter[e.Position].ID);
						PropertyStore.CurrentStore = store;
						StartActivity (PropertyDetails);
						break;
					}
					case 1:
					{
						PropertyStore store = PropertyCollection.SharedCollection.FindPropertyStoreWithID (ListAdapter[e.Position].ID);
						PropertyStore.CurrentStore = store;
						StartActivity (typeof(AddEditPropertyActivity));
						break;
					}
					case 2:
					{

						PropertyCollection.SharedCollection.RemovePropertyStoreWithID (ListAdapter[e.Position].ID);
						ListAdapter.NotifyDataSetChanged();
						break;
					}
					}
				};
//				var PropertyDetails = new Intent (this,typeof(PropertyDetailActivity));
//				PropertyStore store = PropertyCollection.SharedCollection.FindPropertyStoreWithID (ListAdapter[e.Position].ID);
//				PropertyStore.CurrentStore = store;
//				StartActivity (PropertyDetails);
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


