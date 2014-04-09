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
using System.Linq;
using Java.IO;

namespace HomeCatalog.Android
{
	[Activity (Label = "PhotoBrowserActivity")]
	public class PhotoBrowserActivity : StandardActivity
	{
		private PhotoBrowserAdapter GridViewAdapter { get; set; }

		private Property Property { get; set; }

		private Item Item { get; set; }

		PhotoFileHolder Photo;

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
			GridViewAdapter = new PhotoBrowserAdapter (this, Item, Property);

			photoBrowserGridView.Adapter = GridViewAdapter;

			// Add Photo Button
			Button addPhotoButton = FindViewById<Button> (Resource.Id.addPhotoButton);
			addPhotoButton.Click += (sender, e) => {
				var transaction = FragmentManager.BeginTransaction ();
				_photoDialog = new PhotoDialogFragment ();
				_photoDialog.ItemID = Item.ID;
				_photoDialog.Show (transaction, "photoDialog");
				Photo = new PhotoFileHolder ();
				_photoDialog.Photo = Photo;
			};

			// Item Click
			photoBrowserGridView.ItemClick += (sender, e) => {
				var ItemRequest = new Intent (this, typeof(PhotoPagerActivity));
				ItemRequest.PutExtra (PhotoPagerActivity.ItemTitleKey, this.Item.ItemName);
				ItemRequest.PutExtra (PhotoPagerActivity.PositionKey, e.Position);
				ItemRequest.PutStringArrayListExtra (
					PhotoPagerActivity.PhotoAssetIDListKey,
					GridViewAdapter.Photos.Select (photo => photo.AssetID).ToArray());
				StartActivity (ItemRequest);
			};
		}

		private PhotoDialogFragment _photoDialog { get; set; }

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			// result code ok 
			// result code cancelled

			if (resultCode == Result.Ok) {
				var destinationAsset = Property.Store.Assets.NewEmptyAsset () + ".jpg";
				var destinationPath = Property.Store.Assets.PathForEmptyAsset (destinationAsset);
				// TODO: On the device the Photo Path is null occasionally. Possibly garbage collected with the fragment?
				// Probably best to move the picture taking code out of the fragment and make the fragment just a dialog
				var input = new FileStream (Photo.Path, FileMode.Open, FileAccess.Read);
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

		private static void CopyStream (System.IO.Stream input, System.IO.Stream output)
		{
			byte[] buffer = new byte[32768];
			int read;
			while ((read = input.Read (buffer, 0, buffer.Length)) > 0) {
				output.Write (buffer, 0, read);
			}
		}
	}
}

	



