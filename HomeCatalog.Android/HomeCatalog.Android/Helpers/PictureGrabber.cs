using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Net;
using Android.Content.PM;
using Java.IO;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	public class PictureGrabber
	{
		public static bool IsThereAnAppToTakePictures ()
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = Activity.PackageManager.QueryIntentActivities (intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		public Context _Context { get; set; }
		public Java.IO.File File { get; private set; } 

		public PictureGrabber (Context context) {
			_Context = context;
		}

		public void TakePhoto ()
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			File = GetTempFile ();
			intent.PutExtra (MediaStore.ExtraOutput, Uri.FromFile (File));
			Activity.StartActivityForResult (intent, 1);
		}

		private void GrabFromGallery ()
		{
			var imageIntent = new Intent ();
			imageIntent.SetType ("image/*");
			imageIntent.SetAction (Intent.ActionGetContent);
			Activity.StartActivityForResult (Intent.CreateChooser (imageIntent, "Select Photo"), 0);
		}

		private Java.IO.File GetTempFile ()
		{
			Java.IO.File path = new Java.IO.File (Environment.GetExternalStoragePublicDirectory (Environment.DirectoryPictures), Activity.PackageName);
			if (!path.Exists ()) {
				path.Mkdirs ();
			}
			return new File (path, "image.tmp");
		}
	}
}

