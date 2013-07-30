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
		private Item Item {get;set;}

		private EditText itemNameField { get; set; }
		private EditText purchaseDateField { get; set; }
		private EditText purchaseValueField { get; set; }
		private EditText appraisalDateField { get; set; }
		private EditText appraisalValueField { get; set; }
		private EditText modelNumberField { get; set; }
		private EditText serialNumberField { get; set; }


		private Spinner roomLabelSpinner { get; set; }
		private Spinner categoryLabelSpinner { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Grab intent from sending Activity
			string ID = Intent.GetStringExtra (Item.ItemIDKey);
			int itemID = Convert.ToInt32 (ID);
			Item = PropertyCollection.SharedCollection.FindItemById (itemID);
			Property = PropertyCollection.SharedCollection.FindParentProperty (itemID);

			SetContentView (Resource.Layout.AddItemView);

			// Load EditText views by ID
			itemNameField = FindViewById<EditText> (Resource.Id.itemNameField);
			purchaseDateField = FindViewById<EditText> (Resource.Id.purchaseDateField);
			purchaseValueField = FindViewById<EditText> (Resource.Id.purchaseValueField);
			appraisalDateField = FindViewById<EditText> (Resource.Id.appraisalDateField);
			appraisalValueField = FindViewById<EditText> (Resource.Id.appraisalValueField);
			modelNumberField = FindViewById<EditText> (Resource.Id.modelNumberField);
			serialNumberField = FindViewById<EditText> (Resource.Id.serialNumberField);

			DisplayItemInfo ();

			Button receiptButton = FindViewById<Button> (Resource.Id.receiptButton);
			Button goToPhotosButton = FindViewById<Button> (Resource.Id.goToPhotosButton);

			Button cancelAddItemButton = FindViewById<Button> (Resource.Id.cancelAddItemButton);
			cancelAddItemButton.Click += (sender, e) => 
			{
				SetResult(Result.Canceled);
			};

			Button saveAddItemButton = FindViewById<Button> (Resource.Id.saveAddItemButton);
			Button deleteItemButton = FindViewById<Button> (Resource.Id.deleteItemButton);
		}

		private void DisplayItemInfo()
		{
			itemNameField.Text = Item.ItemName;
			purchaseDateField.Text = Item.PurchaseDate;
			purchaseValueField.Text = Item.PurchaseValue;
			appraisalDateField.Text = Item.AppraisalDate;
			appraisalValueField.Text = Item.AppraisalValue;
			modelNumberField.Text = Item.ModelNumber;
			serialNumberField.Text = Item.SerialNumber;

		}
	}
}

