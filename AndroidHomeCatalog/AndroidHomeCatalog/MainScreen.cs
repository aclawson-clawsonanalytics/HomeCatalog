using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;


namespace AndroidHomeCatalog
{
	[Activity (Label = "AndroidHomeCatalog", MainLauncher = true)]
	public class Activity1 : Activity
	{




		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Dictionary<int,ItemContainer> propertyList = new Dictionary<int,ItemContainer> ();
			ItemContainer property1 = new ItemContainer ();



			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.MainView);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}
	}


}


