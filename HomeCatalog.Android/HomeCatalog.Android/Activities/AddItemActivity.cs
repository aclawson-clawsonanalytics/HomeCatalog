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
	[Activity (Label = "AddItemActivity")]			
	public class AddItemActivity : Activity
	{

		private Property Property { get; set; }


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Grab intent from sending Activity
			String propertyID = Intent.GetStringExtra (Property.PropertyIDKey);
			Property = PropertyCollection.SharedCollection.FindPropertyWithId (propertyID);


		}
	}
}

