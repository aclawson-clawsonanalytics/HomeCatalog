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
	public class RoomListDialogFragment : DialogFragment
	{
		private string[] optionList;
		private Property Property { get; set; }
		public delegate void OnItemSelectedDelegate (DialogClickEventArgs e);
		public OnItemSelectedDelegate OnItemSelected {get; set;}

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			optionList = new string[] {"Attic","Basement","Garage","Kitchen","Living Room","Office","Storage","Custom"};
			Property = PropertyStore.CurrentStore.Property;

			var builder = new AlertDialog.Builder (Activity);
			builder.SetTitle ("Select a room to add:");
			builder.SetItems (optionList,delegate(object sender, DialogClickEventArgs e)
			{
				OnItemSelected(e);
			});

			return builder.Create ();
		}


	}
}

