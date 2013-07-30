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
	[Activity (Label = "ViewitemsListActivity")]
	public class DisplayItemsActivity : Activity
	{
		private ItemListAdapter ListAdapter { get; set; }
		private Property Property { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.ItemsView);

			ListView listView = FindViewById<ListView> (Resource.Id.itemsList);
			ListAdapter = new ItemListAdapter (this,Property);
			listView.Adapter = ListAdapter;


			Button additemButton = FindViewById<Button> (Resource.Id.addItemButton);
			additemButton.Click += (sender, e) => 
			{

			};
			Button backButton3 = FindViewById<Button> (Resource.Id.backButton3);
			backButton3.Click += (sender, e) => 
			{
				SetResult (Result.Ok);
				Finish ();
			};

			listView.ItemClick += (sender, e) => 
			{
				var ItemRequest = new Intent (this,typeof(ItemsDetailActivity));
				ItemRequest.PutExtra (Item.ItemIDKey,ListAdapter[e.Position].ID);
				StartActivity (ItemRequest);
			};




		}

	}
}


