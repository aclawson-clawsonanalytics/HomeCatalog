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
		public static void DisplayDialogForObject (IValidatable anObject, Activity context){
			var errorList = anObject.GetValidationErrors ();
			if (errorList != null) {
				var dialog = new ValidationDialogFragment (errorList[0]);
				var transaction = context.FragmentManager.BeginTransaction ();
				dialog.Show (transaction, "validationDialog");
			}
		}
		private string message { get; set; }
		//public delegate void OnItemSelectedDelegate (DialogClickEventArgs e);
		//public OnItemSelectedDelegate OnItemSelected {get; set;}

		public ValidationDialogFragment (string aMessage)
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

