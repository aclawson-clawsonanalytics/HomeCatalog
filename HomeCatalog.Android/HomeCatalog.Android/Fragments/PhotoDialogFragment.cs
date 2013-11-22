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
	public class PhotoDialogFragment : DialogFragment
	{
		private string[] optionList;

		public int ItemID { get; set; }

		public Java.IO.File File { get; set; }

		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			if (IsThereAnAppToTakePictures ()) {
				optionList = new string[] { "From Camera", "From Gallery" };
			} else {
				optionList = new string[] { "From Gallery" };
			}

			var builder = new AlertDialog.Builder (Activity);
			builder.SetTitle ("How to add the photo: ");
			builder.SetItems (optionList, delegate(object sender, DialogClickEventArgs e) {
				if (IsThereAnAppToTakePictures ()) {
					switch (e.Which) {
					case 0:
						TakePhoto ();
						break;
					case 1:
						OpenGallery ();
						break;
					}
				} else {
					OpenGallery ();
				}
			});

			return builder.Create ();
		}

		private bool IsThereAnAppToTakePictures ()
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = Activity.PackageManager.QueryIntentActivities (intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void TakePhoto ()
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			File = GetTempFile ();
			intent.PutExtra (MediaStore.ExtraOutput, Uri.FromFile (File));

			Activity.StartActivityForResult (intent, 1);
		}

		private void OpenGallery ()
		{
			var imageIntent = new Intent ();
			imageIntent.SetType ("image/*");
			imageIntent.SetAction (Intent.ActionGetContent);
			Activity.StartActivityForResult (Intent.CreateChooser (imageIntent, "Select Photo"), 0);
		}

		private Java.IO.File GetTempFile ()
		{
			//it will return /sdcard/image.tmp
			File path = new File (Environment.GetExternalStoragePublicDirectory (Environment.DirectoryPictures), Activity.PackageName);
			if (!path.Exists ()) {
				path.Mkdirs ();
			}
			return new File (path, "image.tmp");
		}
	}
}

