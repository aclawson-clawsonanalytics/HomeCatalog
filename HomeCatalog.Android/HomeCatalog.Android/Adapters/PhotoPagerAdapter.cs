using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Graphics;
using Android.Widget;
using Android.Support.V4.View;
using Android.Webkit;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	class PhotoPagerAdapter : PagerAdapter
	{
		IList<string> _AssetIDs;
		Activity _Activity;

		public PhotoPagerAdapter (Activity activity, IList<string> assets) {
			_Activity = activity;
			_AssetIDs = assets;
		}

		public override int Count {
			get {
				return _AssetIDs.Count;
			}
		}

		public override bool IsViewFromObject (View view, Java.Lang.Object @object)
		{
			return view == ((RelativeLayout) @object);
		}

		// TODO
		// Create an image view reuse pool
		// Create an image view in use pool that stores the image view and its position
		// Create a public method void SetCurrentVisiblePosition(int position)
		// Create a "worker" class that sets a higher res image on the current view in the background
		// The worker should accept a cancellation token that prevents calling back to the main thread
		//     (in the event the activity is suddenly closed)
		// If the cancellation doesn't seem to work when closing activity:
		//   The worker should return a manualresetevent that is stored in a property list
		//   Create public void WaitForProcessing() that the activity can call when it is finishing
		// Implement ontrimmemory to release when ui is hidden

		public override Java.Lang.Object InstantiateItem (ViewGroup container, int position)
		{
			LayoutInflater inflatorservice = (LayoutInflater)_Activity.GetSystemService(Context.LayoutInflaterService);
			var layout = inflatorservice.Inflate(Resource.Layout.PhotoFullImageView, container, false);
			var textView = layout.FindViewById<TextView> (Resource.Id.assetID);
			textView.Text = _AssetIDs [position];
			PhotoFullImageView imgDisplay = layout.FindViewById<PhotoFullImageView>(Resource.Id.imgDisplay); 

			BitmapFactory.Options options = new BitmapFactory.Options();
			options.InSampleSize = 4;
			options.InPreferredConfig = Bitmap.Config.Argb8888;
			Bitmap bitmap = BitmapFactory.DecodeFile(PropertyStore.CurrentStore.Assets.PathForAsset(_AssetIDs [position]), options);
			imgDisplay.SetImageBitmap(bitmap);

			container.AddView(layout);
			Console.WriteLine ("Add View:" + position);
			return layout;
		}

		public override void DestroyItem (ViewGroup container, int position, Java.Lang.Object @object)
		{
			Console.WriteLine ("Remove View" + position);
			container.RemoveView((View) @object);
		}
	}
}

