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
	[Activity (Label = "PhotoBrowserActivity")]
	public class PhotoBrowserActivity : Activity
	{
		private ImageAdapter GridViewAdapter { get; set; }
		private Property Property { get; set; }
		private Item Item {get;set;}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property = PropertyStore.CurrentStore.Property;
			// Get Item from intent
			int itemID = Intent.GetIntExtra (Item.ItemIDKey, 0);
			if (itemID > 0)
			{
				Item = PropertyStore.CurrentStore.Property.ItemList.ItemWithID (itemID);
			}
			SetContentView (Resource.Layout.DisplayItemsView);

			GridView photoBrowserGridView = FindViewById<GridView> (Resource.Id.photoBrowserGridView);
			GridViewAdapter = new ImageAdapter (this,Item);

			photoBrowserGridView.Adapter = GridViewAdapter;


			Button addPhotoButton = FindViewById<Button> (Resource.Id.addPhotoButton);
			addPhotoButton.Click += (sender, e) => 
			{
				StartActivity (typeof(AddPhotoActivity));
			};



			photoBrowserGridView.ItemClick += (sender, e) => 
			{
				// This is not correct.  We are not requesting an item or moving
				// to the ItemsDetailActivity
				var ItemRequest = new Intent (this,typeof(FullImageActivity));
				ItemRequest.PutExtra (Item.ItemIDKey,GridViewAdapter[e.Position].ID);
				StartActivity (ItemRequest);
			};




		}

	}
}




