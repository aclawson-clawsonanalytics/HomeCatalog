using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using Android.Graphics;
using Android.Media;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	class PhotoBrowserAdapter : BaseAdapter<Photo>
	{
		public IList<Photo> Photos { get; set; }

		Item _Item;
		Activity Context;
		AssetStore Assets;

		public PhotoBrowserAdapter (Activity context, Item item, Property property) : base ()
		{
			_Item = item;
			this.Context = context;
			this.Photos = item.PhotoList.AllItemsByDate ();
			Assets = property.Store.Assets;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Photo this [int position] {  
			get { return Photos [position]; }
		}

		public override int Count {
			get { return Photos.Count; }
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			ImageView imageView;
			if (convertView == null) {  // if it's not recycled, initialize some attributes
				imageView = new ImageView (Context);
				int imageSize = (int)(90.0 * Context.Resources.DisplayMetrics.Density);
				imageView.LayoutParameters = new GridView.LayoutParams (imageSize, imageSize);
				imageView.SetScaleType (ImageView.ScaleType.FitCenter);
//				int padding = (int)(10.0 * Context.Resources.DisplayMetrics.Density);
//				imageView.SetPadding (padding, 0, 0, 0);
			} else {
				imageView = (ImageView)convertView;
			}
			var assetPath = Assets.PathForAsset (Photos [position].AssetID);
			int THUMBSIZE = 80;
			BitmapFactory.Options options = new BitmapFactory.Options ();
			options.InSampleSize = 4;
			Bitmap thumbImage = ThumbnailUtils.ExtractThumbnail (BitmapFactory.DecodeFile (assetPath, options), THUMBSIZE, THUMBSIZE);
			imageView.SetImageBitmap (thumbImage);
			return imageView;
		}

		public override void NotifyDataSetChanged ()
		{
			this.Photos = _Item.PhotoList.AllItemsByDate ();

			base.NotifyDataSetChanged ();
		}
	}
}



