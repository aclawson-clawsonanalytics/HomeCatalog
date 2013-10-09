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
	[Activity (Label = "Property details")]			
	public class PropertyDetailActivity : Activity
	{
		private Property Property {get;set;}

		// Declare TextViews
		private TextView NameText {get;set;}
		private TextView AddressText {get;set;}
		private TextView CityText {get;set;}
		private TextView StateText { get; set; }
		private TextView ZipText { get; set; }
		private TextView CountryText { get; set; }
	
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Property = PropertyStore.CurrentStore.Property;

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

			CountryText = FindViewById<TextView> (Resource.Id.CountryLabel);
			CountryText.Text = Property.Country;

			//Load Buttons From View

			Button EditButton = FindViewById<Button> (Resource.Id.EditButton);
			EditButton.Click += (sender, e) =>
			{
				Intent PassPropertyID = new Intent(this,typeof(AddEditPropertyActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);
			};


			Button ItemsButton = FindViewById<Button> (Resource.Id.ItemsButton);
			ItemsButton.Click += (sender, e) =>
			{
				Intent PassPropertyID = new Intent(this,typeof(DisplayItemsActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);
			};

			Button CollectionsButton = FindViewById<Button> (Resource.Id.CollectionsButton);
			CollectionsButton.Click += (sender, e) => 
			{
				Intent PassPropertyID = new Intent(this,typeof(DisplayCollectionsActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);
			};

			Button ContactsButton = FindViewById<Button> (Resource.Id.ContactButton);
			ContactsButton.Click += (sender, e) => 
			{

			};
			Button ReportsButton = FindViewById<Button> (Resource.Id.ReportsButton);
			ReportsButton.Click += (sender, e) => 
			{
				Intent PassPropertyID = new Intent(this,typeof(DisplayContactsActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);
			};

			Button DeletePropertyButton = FindViewById<Button> (Resource.Id.DeletePropertyButton);
			DeletePropertyButton.Click += (sender, e) => 
			{
				PropertyCollection.SharedCollection.RemovePropertyStoreWithID (Property.PropertyID);
				Finish ();
			};

			Button CancelDetailsButton = FindViewById<Button> (Resource.Id.CancelDetailsButton);
			CancelDetailsButton.Click += (sender, e) => 
			{
				SetResult (Result.Canceled);
				Finish ();
			};




		}
	}
}

