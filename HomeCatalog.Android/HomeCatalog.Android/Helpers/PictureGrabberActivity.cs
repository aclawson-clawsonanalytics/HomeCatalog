using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Net;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using HomeCatalog.Core;

// This activity returns a success code if a photo was saved to the destination path
namespace HomeCatalog.Android
{
	[Activity (Label = "PhotoBrowserActivity", Theme = "@android:style/Theme.Translucent.NoTitleBar")]
	public class PictureGrabberActivity : Activity
	{
		string _DestinationPath;
		Java.IO.File _TempFile;

		public static string DestinationPathKey = "DestinationPathKey";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			_DestinationPath = Intent.GetStringExtra (DestinationPathKey);

			var photoDialog = new PhotoDialogFragment ();
			photoDialog.OnPhotoOptionSelected = delegate(object addSender, DialogClickEventArgs addEvent) {
				if (IsThereAnAppToTakePictures ()) {
					switch (addEvent.Which) {
					case 0:
						TakePhoto ();
						break;
					case 1:
						GrabFromGallery ();
						break;
					}
				} else {
					GrabFromGallery ();
				}
			};
			var transaction = FragmentManager.BeginTransaction ();
			photoDialog.Show (transaction, "photoDialog");
		}

		public static bool IsThereAnAppToTakePictures (Activity context)
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = context.PackageManager.QueryIntentActivities (intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		public bool IsThereAnAppToTakePictures ()
		{
			return PictureGrabberActivity.IsThereAnAppToTakePictures (this);
		}

		public Java.IO.File File { get; private set; } 

		public void TakePhoto ()
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			_TempFile = GetTempFile ();
			intent.PutExtra (MediaStore.ExtraOutput, global::Android.Net.Uri.FromFile (_TempFile));
			StartActivityForResult (intent, 1);
		}

		public void GrabFromGallery ()
		{
			var imageIntent = new Intent ();
			imageIntent.SetType ("image/*");
			imageIntent.SetAction (Intent.ActionGetContent);
			StartActivityForResult (Intent.CreateChooser (imageIntent, "Select Photo"), 0);
		}

		private Java.IO.File GetTempFile ()
		{
			Java.IO.File path = new Java.IO.File (Environment.GetExternalStoragePublicDirectory (Environment.DirectoryPictures), PackageName);
			if (!path.Exists ()) {
				path.Mkdirs ();
			}
			return new Java.IO.File (path, "image.tmp");
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			// result code ok 
			// result code cancelled
			if (resultCode == Result.Ok) {
				var input = new FileStream (_TempFile.ToString (), FileMode.Open, FileAccess.Read);
				var output = new FileStream (_DestinationPath, FileMode.CreateNew, FileAccess.Write);
				CopyStream (input, output);
				input.Close ();
				output.Close ();
			}

			base.OnActivityResult (requestCode, resultCode, data);
			SetResult (resultCode);
			Finish ();
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

