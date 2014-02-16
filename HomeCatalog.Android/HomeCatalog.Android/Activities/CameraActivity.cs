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
using Android.Provider;
using Java.IO;

namespace HomeCatalog.Android
{
	[Activity (Label = "Camera")]			
	public class CameraActivity : StandardActivity
	{
		Java.IO.File _file;
		Java.IO.File _dir;
		ImageView _imageView;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//SetContentView (Resource.Layout.CameraView);


		}

//		private bool IsThereAnAppToTakePictures()
//		{
//			//Intent intent = new Intent (MediaStore.ActionImageCapture);
//			//IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities (intent, PackageInfoFlags.MatchDefaultOnly);
//			//return availableActivities != null && availableActivities.Count > 0;
//		}

		private void TakeAPicture(object sender,EventArgs eventArgs)
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			_file = new File (_dir, String.Format ("myPhoto_{0}.jpg", Guid.NewGuid ()));
			//intent.PutExtra (MediaStore.ExtraOutput, Uri.FromFile (_file));
			StartActivityForResult (intent, 0);
		}
	}
}

