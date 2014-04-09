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
	public class FieldValidationDialogFragment : DialogFragment
	{

		private string message { get; set; }
		//public delegate void OnItemSelectedDelegate (DialogClickEventArgs e);
		//public OnItemSelectedDelegate OnItemSelected {get; set;}

		public FieldValidationDialogFragment (string aMessage)
		{
			message = aMessage;
		}


		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{

			var builder = new AlertDialog.Builder (Activity);
			builder.SetTitle ("Form Attention!!");
			builder.SetMessage (message);

			builder.SetPositiveButton ("Ok", (sender, args) => {

			});
//			IDialogInterfaceOnClickListener listener = new IDialogInterfaceOnClickListener ();
//			;
//			builder.SetPositiveButton ("Ok", (sender, e) => {
//				this.Dismiss ();
//			});
			
			


			return builder.Create ();
		}


	}
}

