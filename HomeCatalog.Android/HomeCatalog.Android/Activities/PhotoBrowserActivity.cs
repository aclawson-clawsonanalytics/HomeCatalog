using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.OS;
using Android.Net;
using Android.Graphics;
using Android.Media;
using HomeCatalog.Core;
using Android.Content.PM;
using System.IO;
using Java.IO;

namespace HomeCatalog.Android
{
	[Activity (Label = "PhotoBrowserActivity")]
	public class PhotoBrowserActivity : StandardActivity
	{
		private PhotoBrowserAdapter GridViewAdapter { get; set; }

		private Property Property { get; set; }

		private Item Item { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.PhotoBrowserView);

			Property = PropertyStore.CurrentStore.Property;
			// Get Item from intent
			int itemID = Intent.GetIntExtra (Item.ItemIDKey, 0);

			if (itemID > 0) {
				Item = PropertyStore.CurrentStore.Property.ItemList.ItemWithID (itemID);
			}	

			GridView photoBrowserGridView = FindViewById<GridView> (Resource.Id.photoBrowserGridView);
			GridViewAdapter = new PhotoBrowserAdapter (this, Item);

			photoBrowserGridView.Adapter = GridViewAdapter;

			Button addPhotoButton = FindViewById<Button> (Resource.Id.addPhotoButton);
			addPhotoButton.Click += (sender, e) => {
				var transaction = FragmentManager.BeginTransaction ();
				_photoDialog = new PhotoDialogFragment ();
				_photoDialog.ItemID = Item.ID;
				_photoDialog.Show (transaction, "photoDialog");
			};

//			photoBrowserGridView.ItemClick += (sender, e) => 
//			{
//				// This is not correct.  We are not requesting an item or moving
//				// to the ItemsDetailActivity
//				var ItemRequest = new Intent (this,typeof(FullImageActivity));
//				ItemRequest.PutExtra (Item.ItemIDKey,GridViewAdapter[e.Position].ID);
//				StartActivity (ItemRequest);
//			};
		}

		private PhotoDialogFragment _photoDialog { get; set; }

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			// result code ok 
			// result code cancelled

			if (resultCode == Result.Ok) {
				var destinationAsset = AssetStore.CurrentStore.NewEmptyAsset ();
				var destinationPath = AssetStore.CurrentStore.PathForEmptyAsset (destinationAsset);
				var input = new FileStream (_photoDialog.File.ToString (), FileMode.Open, FileAccess.Read);
				var output = new FileStream (destinationPath, FileMode.CreateNew, FileAccess.Write);
				CopyStream (input, output);
				input.Close ();
				output.Close ();
				var photo = new Photo ();
				photo.AssetID = destinationAsset;
				photo.DateAdded = System.DateTime.Now;
				Item.PhotoList.Add (photo);
				Item.PhotoList.Save (photo);
				GridViewAdapter.NotifyDataSetChanged ();
			}

			base.OnActivityResult (requestCode, resultCode, data);
		}

		private static void CopyStream(System.IO.Stream input, System.IO.Stream output)
		{
			byte[] buffer = new byte[32768];
			int read;
			while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
			{
				output.Write (buffer, 0, read);
			}
		}
	}
}

	



