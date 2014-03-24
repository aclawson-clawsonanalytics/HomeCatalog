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

namespace HomeCatalog.Android
{
	[Activity]			
	public class PhotoPagerActivity : StandardActivity
	{
		// Required in intent
		public static string PhotoAssetIDListKey = "PhotoAssetIDArrayKey"; // List of photos to display
		public static string PositionKey = "PositionKey"; // Position Index of photo to display
		public static string ItemTitleKey = "ItemTitleKey"; // Title of item relating to photos

		ViewPager _Pager;
		PhotoPagerAdapter _PagerAdapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			this.ActionBar.Title = Intent.GetStringExtra (ItemTitleKey);

			SetContentView (Resource.Layout.PhotoPagerLayout);
			_Pager = FindViewById<ViewPager> (Resource.Id.pager);
			_PagerAdapter = new PhotoPagerAdapter (this, Intent.GetStringArrayListExtra (PhotoAssetIDListKey));
			_Pager.Adapter = _PagerAdapter;
		}
	}
}

