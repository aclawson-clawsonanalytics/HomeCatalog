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
	[Activity (Label = "PropertyDetailActivity")]			
	public class PropertyDetailActivity : Activity
	{
		private Property Property {get;set;}

		// Declare TextViews
		private TextView NameText {get;set;}
		private TextView AddressText {get;set;}
		private TextView CityText {get;set;}
		private TextView StateText { get; set; }
		private TextView ZipText { get; set; }




		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Grab propertyID from intent being sent by previous activity
			String propertyID = Intent.GetStringExtra (Property.PropertyIDKey);
			Property = PropertyCollection.SharedCollection.FindPropertyWithId (propertyID);

			//Load view
			SetContentView (Resource.Layout.PropertyDetailView);

			//Load TextView views and set values
			NameText = FindViewById<TextView> (Resource.Id.NameLabel);
			NameText.Text = Property.PropertyName;

			AddressText = FindViewById<TextView> (Resource.Id.AddressLabel);
			AddressText.Text = Property.Address;

			CityText = FindViewById<TextView> (Resource.Id.CityLabel);
			CityText.Text = Property.City;

			ZipText = FindViewById<TextView> (Resource.Id.ZipLabel);
			ZipText.Text = Property.ZipCode;

			//Load Buttons From View

			Button EditButton = FindViewById<Button> (Resource.Id.EditButton);
			Button ItemsButton = FindViewById<Button> (Resource.Id.GoToItemsButton);
			Button CollectionsItem = FindViewById<Button> (Resource.Id.GoToCollectionsButton);
			Button ContactsButton = FindViewById<Button> (Resource.Id.GoToContactsButton);
			Button ReportsButton = FindViewById<Button> (Resource.Id.ReportsButton);
			Button CancelDetailsButton = FindViewById<Button> (Resource.Id.CancelDetailsButton);


		}
	}
}

