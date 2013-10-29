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
	[Activity (Label = "RoomEditActivity")]			
	public class RoomEditActivity : Activity
	{
		private Property Property { get; set; }
		private EditText roomLabelField { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.RoomEditLayout);


		}
	}
}

