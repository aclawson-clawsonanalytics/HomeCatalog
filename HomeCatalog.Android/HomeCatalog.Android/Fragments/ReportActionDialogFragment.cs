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
using HomeCatalog.Core;


namespace HomeCatalog.Android
{
	public class ReportActionDialogFragment : DialogFragment
	{
		private string[] optionList;
		public delegate void OnItemSelectedDelegate (DialogClickEventArgs e);
		public OnItemSelectedDelegate OnItemSelected {get; set;}

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			optionList = new string[] {"View","Email"};

			var builder = new AlertDialog.Builder (Activity);
			builder.SetTitle ("Options");
			builder.SetItems (optionList,delegate(object sender, DialogClickEventArgs e)
			{
				OnItemSelected(e);
			});

			return builder.Create ();
		}


	}
}

