using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Provider;
using Android.Widget;
using Android.Content.PM;


namespace HomeCatalog.Android
{
	public class PhotoDialogFragment : DialogFragment
	{
		private string[] optionList;

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
							

					}
				}

				else
				{

				}
			});

			return builder.Create ();
		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities (intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void TakePhoto()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);

			//_file = new File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
			//
			//intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(_file));

			StartActivityForResult(intent, 0);
		}
		}
}

