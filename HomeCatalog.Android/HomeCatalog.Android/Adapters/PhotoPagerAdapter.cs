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

		public override Java.Lang.Object InstantiateItem (ViewGroup container, int position)
		{
			LayoutInflater inflatorservice = (LayoutInflater)_Activity.GetSystemService(Context.LayoutInflaterService);
			var layout = inflatorservice.Inflate(Resource.Layout.PhotoFullImageView, container, false);
			var textView = layout.FindViewById<TextView> (Resource.Id.assetID);
			textView.Text = _AssetIDs [position];
			WebView view = layout.FindViewById<WebView>(Resource.Id.webView); 
			view.Settings.DefaultZoom = WebSettings.ZoomDensity.Far;
			view.Settings.SetSupportZoom(true); 
			view.Settings.BuiltInZoomControls = true; 

			view.LoadUrl("file://" + PropertyStore.CurrentStore.Assets.PathForAsset(_AssetIDs [position]));
			container.AddView(layout);
			return layout;
		}

		public override void DestroyItem (ViewGroup container, int position, Java.Lang.Object @object)
		{
			container.RemoveView((View) @object);
		}
	}
}

