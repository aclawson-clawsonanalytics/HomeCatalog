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

		private EditText itemNameField { get; set; }
		private EditText purchaseDateField { get; set; }
		private EditText purchaseValueField { get; set; }
		private EditText appraisalDateField { get; set; }
		private EditText modelNumberField { get; set; }
		private EditText serialNumberField { get; set; }


		private Spinner roomLabelSpinner { get; set; }
		private Spinner categoryLabelSpinner { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Grab intent from sending Activity
			String propertyID = Intent.GetStringExtra (Property.PropertyIDKey);
			Property = PropertyCollection.SharedCollection.FindPropertyWithId (propertyID);

			SetContentView (Resource.Layout.AddItemView);

			// Load EditText views by ID
			itemNameField = FindViewById<EditText> (Resource.Id.itemNameField);
			purchaseDateField = FindViewById<EditText> (Resource.Id.purchaseDateField);
			purchaseValueField = FindViewById<EditText> (Resource.Id.purchaseValueField);
			appraisalDateField = FindViewById<EditText> (Resource.Id.appraisalDateField);
			modelNumberField = FindViewById<EditText> (Resource.Id.modelNumberField);
			serialNumberField = FindViewById<EditText> (Resource.Id.serialNumberField);

			Button receiptButton = FindViewById<Button> (Resource.Id.receiptButton);
			Button goToPhotosButton = FindViewById<Button> (Resource.Id.goToPhotosButton);
			Button cancelAddItemButton = FindViewById<Button> (Resource.Id.cancelAddItemButton);
			Button saveAddItemButton = FindViewById<Button> (Resource.Id.saveAddItemButton);

		}
	}
}

