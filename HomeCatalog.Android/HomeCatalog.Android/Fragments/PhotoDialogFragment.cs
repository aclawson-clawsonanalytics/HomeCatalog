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

		public System.EventHandler<DialogClickEventArgs> OnPhotoOptionSelected {get; set;}

		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			if (PictureGrabberActivity.IsThereAnAppToTakePictures (Activity)) {
				optionList = new string[] { "From Camera", "From Gallery" };
			} else {
				optionList = new string[] { "From Gallery" };
			}

			var builder = new AlertDialog.Builder (Activity);
			builder.SetTitle ("How to add the photo: ");
			builder.SetItems (optionList, OnPhotoOptionSelected);

			return builder.Create ();
		}
	}
}

