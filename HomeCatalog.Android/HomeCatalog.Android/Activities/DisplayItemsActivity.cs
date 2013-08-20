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
	[Activity (Label = "ViewItemsListActivity")]
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
				StartActivity (typeof(AddItemActivity));
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
				ItemDialogFragment itemDialog = new ItemDialogFragment ();
				itemDialog.Show (transaction, "itemDialog");
				itemDialog.OnItemSelected += (DialogClickEventArgs a) =>
				{
					switch (a.Which) {
					case 0:
						var ItemRequest = new Intent (this, typeof(ItemsDetailActivity));
						ItemRequest.PutExtra (Item.ItemIDKey, ListAdapter [e.Position].ID);
						StartActivity (ItemRequest);
						break;
					case 1:
						break;

					case 2:
						Property.ItemList.Remove (ListAdapter [e.Position]);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
					;



				};




			};}

		public void showItem(AdapterView.ItemClickEventArgs e)
		{
			var ItemRequest = new Intent (this,typeof(ItemsDetailActivity));
			ItemRequest.PutExtra (Item.ItemIDKey,ListAdapter[e.Position].ID);
			StartActivity (ItemRequest);
		}

	}
}


