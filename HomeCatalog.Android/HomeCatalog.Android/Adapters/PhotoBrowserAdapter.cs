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
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	class PhotoBrowserAdapter : BaseAdapter<Photo>
	{
		IList<Photo> Photos;
		Item _Item;
		Activity Context;

		public PhotoBrowserAdapter(Activity context, Item item) : base() {
			_Item = item;
			this.Context = context;
			this.Photos = item.PhotoList.AllItemsByDate ();
		}

		public override long GetItemId(int position)
		{
			return position;
		}
		public override Photo this[int position] {  
			get { return Photos[position]; }
		}
		public override int Count {
			get { return Photos.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			ImageView imageView;
			if (convertView == null) {  // if it's not recycled, initialize some attributes
				imageView = new ImageView(Context);
				imageView.LayoutParameters = new GridView.LayoutParams (85, 85);
				imageView.SetScaleType (ImageView.ScaleType.CenterCrop);
				imageView.SetPadding (8, 8, 8, 8);
			} else {
				imageView = (ImageView) convertView;
			}
			var assetPath = AssetStore.CurrentStore.PathForAsset (Photos[position].AssetID);
			imageView.SetImageURI (Uri.Parse(assetPath));
			return imageView;
		}
		public override void NotifyDataSetChanged ()
		{
			this.Photos = _Item.PhotoList.AllItemsByDate ();

			base.NotifyDataSetChanged ();
		}

	}
}



