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
	[Activity (Label = "View Items")]
	public class DisplayItemsActivity : Activity
	{
		private ItemListAdapter ListAdapter { get; set; }
		private Property Property { get; set; }
		private int selectedItem { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.DisplayItemsView);

			ListView listView = FindViewById<ListView> (Resource.Id.itemsList);
			ListAdapter = new ItemListAdapter (this,Property);
			listView.Adapter = ListAdapter;


			Button additemButton = FindViewById<Button> (Resource.Id.addItemButton);
			additemButton.Click += (sender, e) => 
			{
				StartActivityForResult (typeof(AddItemActivity),0);
			};
			Button backButton3 = FindViewById<Button> (Resource.Id.backButton3);
			backButton3.Click += (sender, e) => 
			{
				SetResult (Result.Canceled);
				Finish ();
			};

//			listView.ItemClick += (sender, e) => 
//			{
//				var ItemRequest = new Intent (this,typeof(ItemsDetailActivity));
//				ItemRequest.PutExtra (Item.ItemIDKey,ListAdapter[e.Position].ID);
//				StartActivity (ItemRequest);
//			};

			listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
			{
				selectedItem = e.Position;

				var transaction = FragmentManager.BeginTransaction ();
				OptionDialogFragment optionDialog = new OptionDialogFragment ();
				optionDialog.Show (transaction, "optionDialog");
				optionDialog.OnItemSelected += (DialogClickEventArgs a) =>
				{
					switch (a.Which) {
					case 0:
					{
						//var ItemRequest = new Intent (this, typeof(ItemsDetailActivity));
						var ItemRequest = new Intent (this,typeof(AddItemActivity));
						ItemRequest.PutExtra (Item.ItemIDKey, ListAdapter [e.Position].ID);
						StartActivityForResult (ItemRequest,0);
						break;
					}
					case 1:
					{
						var ItemRequest = new Intent (this, typeof(AddItemActivity));
						ItemRequest.PutExtra (Item.ItemIDKey, ListAdapter [e.Position].ID);
						StartActivityForResult (ItemRequest,0);
						break;
					}

					case 2:
					{
						Property.ItemList.Remove (ListAdapter [e.Position]);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
					};



				};




			};}

		public void showItem(AdapterView.ItemClickEventArgs e)
		{
			var ItemRequest = new Intent (this,typeof(ItemsDetailActivity));
			ItemRequest.PutExtra (Item.ItemIDKey,ListAdapter[e.Position].ID);
			StartActivity (ItemRequest);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			ListAdapter.NotifyDataSetChanged ();

		}

	}
}


