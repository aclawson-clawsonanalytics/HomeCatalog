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

		string _DestinationAsset;

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
				var property = PropertyStore.CurrentStore.Property;
				_DestinationAsset = property.Store.Assets.NewEmptyAsset () + ".jpg";
				var destinationPath = property.Store.Assets.PathForEmptyAsset (_DestinationAsset);
				var grabIntent = new Intent (this, typeof(PictureGrabberActivity));
				grabIntent.PutExtra (PictureGrabberActivity.DestinationPathKey, destinationPath);
				StartActivityForResult (grabIntent, 0);
			};

			// Item Click
			photoBrowserGridView.ItemClick += (sender, e) => {
				var ItemRequest = new Intent (this, typeof(PhotoPagerActivity));
				ItemRequest.PutExtra (PhotoPagerActivity.ItemTitleKey, this.Item.ItemName);
				ItemRequest.PutExtra (PhotoPagerActivity.PositionKey, e.Position);
				ItemRequest.PutStringArrayListExtra (
					PhotoPagerActivity.PhotoAssetIDListKey,
					GridViewAdapter.Photos.Select (photo => photo.AssetID).ToArray ());
				StartActivity (ItemRequest);
			};
		}

		private PhotoDialogFragment _photoDialog { get; set; }

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (resultCode == Result.Ok) {
				var photo = new Photo ();
				photo.AssetID = _DestinationAsset;
				photo.DateAdded = System.DateTime.Now;
				Item.PhotoList.Add (photo);
				Item.PhotoList.Save (photo);
				GridViewAdapter.NotifyDataSetChanged ();
				base.OnActivityResult (requestCode, resultCode, data);
			}
		}
	}
}

	



