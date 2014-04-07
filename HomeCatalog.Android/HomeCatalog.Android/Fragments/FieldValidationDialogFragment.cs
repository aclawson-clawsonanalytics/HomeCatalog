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
	public class ValidationDialogFragment : DialogFragment
	{
		private string[] optionList;
		private Property Property { get; set; }
		private string field { get; set; }
		private string message { get; set; }
		private bool unique { get; set; }
		public delegate void OnItemSelectedDelegate (DialogClickEventArgs e);
		public OnItemSelectedDelegate OnItemSelected {get; set;}

		public override Dialog OnCreateDialog(Bundle savedInstanceState, string _aFieldTitle, bool UNIQUE_FLAG)
		{
			optionList = new string[] {"Delete"};
			Property = PropertyStore.CurrentStore.Property;
			field = _aFieldTitle;

			var builder = new AlertDialog.Builder (Activity);
			builder.SetTitle ("Delete");

			if (unique) {
				message = "Please enter a unique " + field + ".";
			} else {
				message = "Please enter a " + field + ".";
			}

			builder.SetMessage (message);

			return builder.Create ();
		}


	}
}

