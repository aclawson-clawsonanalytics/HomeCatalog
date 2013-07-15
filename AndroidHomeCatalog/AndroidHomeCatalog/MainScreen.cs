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




			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.MainView);

			// AddPropertyButton
			Button AddPropertyButton = FindViewById<Button> (Resource.Id.AddPropertyButton);
			
			AddPropertyButton.Click += (sender,e) =>
			{
				ItemContainer NewProperty = new ItemContainer();
				NewProperty = ItemContainer.InitializeProperty (NewProperty);
				Intent SendNewProperty = new Intent(this,typeof(PropertyAddEditScreen));
				SendNewProperty.PutExtras ("NewProperty",NewProperty);
			};
		}

		protected override void OnResume ()
		{

		}
	}

}


