using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Content.PM;
using Java.IO;

using HomeCatalog.Core;


namespace HomeCatalog.Android
{
	public class PhotoDialogFragment : DialogFragment
	{
		private string[] optionList;
		public int itemID { get; set; }
		Java.IO.File _file;

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			if (IsThereAnAppToTakePictures())
			{
				optionList = new string[] {"From Camera","From Gallery"};
			}
			else
			{
				optionList = new string[] { "From Gallery" };
			}

			var builder = new AlertDialog.Builder (Activity);
			builder.SetTitle ("How to add the photo: ");
			builder.SetItems (optionList, delegate(object sender, DialogClickEventArgs e)
			{
				if(IsThereAnAppToTakePictures())
				{
					switch (e.Which)
					{
					case 0:
						TakePhoto ();
						break;

					case 1:
						//OpenGallery();
						break;
							
					}
				}

				else
				{
					//OpenGallery();
				}
			});

			return builder.Create ();
		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = Activity.PackageManager.QueryIntentActivities (intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void TakePhoto()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			string asset = AssetStore.CurrentStore.NewEmptyAsset ();
			string path = AssetStore.CurrentStore.PathForEmptyAsset (asset);
			_file = new File(path);
			
			intent.PutExtra(MediaStore.ExtraOutput, global::Android.Net.Uri.FromFile(_file));

			Activity.StartActivityForResult(intent, 0);
		}


		}
}

