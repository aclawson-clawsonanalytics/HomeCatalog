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
	public class PropertyDialogFragment : DialogFragment
	{
		private string[] optionList;
		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			optionList = new string[] {"Rename","Delete"};
			var builder = new AlertDialog.Builder (Activity,object obj);
			builder.SetTitle ("DELETE ITEM");
			builder.SetMessage ("Are you sure?");

			// Set buttons
			builder.SetPositiveButton ("Ok", (sender,EventArgs) =>
			{

			});
			                          

			return builder.Create ();
		}


	}
}

