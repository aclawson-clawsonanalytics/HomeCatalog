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


namespace HomeCatalog.Android
{
	public class PhotoDialogFragment : DialogFragment
	{
		private string[] optionList;

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			optionList = new string[] {"From Camera","From Gallery"};
			var builder = new AlertDialog.Builder (Activity);
			builder.SetTitle ("How to add the photo: ");
			builder.SetItems (optionList, delegate(object sender, DialogClickEventArgs e) {
				});

			return builder.Create ();
		}
		}
}

