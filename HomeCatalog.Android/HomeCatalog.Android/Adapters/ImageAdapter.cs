using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	public class ImageAdapter : BaseAdapter<Photo>
	{
		Context Context;

		public ImageAdapter (Context c)
		{
			Context = c;
		}

		public override int Count {
			//get { return thumbIds.Length;}
			get { return 0;}
		}

		public override long GetItemId (int position)
		{
			return 0;
		}

		public override Photo this [int position] {  
			get { return null; }
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			ImageView imageView;

			if (convertView == null) {
				imageView = new ImageView (Context);
				imageView.LayoutParameters = new GridView.LayoutParams (85, 85);
				imageView.SetScaleType (ImageView.ScaleType.CenterCrop);
				imageView.SetPadding (8, 8, 8, 8);
			} else {
				imageView = (ImageView)convertView;
			}

			//imageView.SetImageResource (thumbIds[position]);
			return imageView;
		}

		// Include a reference to the images.
		//int[] thumbIds = Item.PhotoList;
	}
}


