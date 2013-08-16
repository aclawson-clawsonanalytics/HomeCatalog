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
			int itemID = Intent.GetIntExtra (Item.ItemIDKey, 0);

			if (itemID > 0) {
				Item = PropertyStore.CurrentStore.Property.ItemList.ItemWithID (itemID);
			}
			//Item = PropertyCollection.SharedCollection.FindItemById (itemID);


			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.AddItemView);

			// Load EditText views by ID
			itemNameField = FindViewById<EditText> (Resource.Id.itemNameField);
			purchaseDateField = FindViewById<EditText> (Resource.Id.purchaseDateField);
			purchaseValueField = FindViewById<EditText> (Resource.Id.purchaseValueField);
			appraisalDateField = FindViewById<EditText> (Resource.Id.appraisalDateField);
			appraisalValueField = FindViewById<EditText> (Resource.Id.appraisalValueField);
			modelNumberField = FindViewById<EditText> (Resource.Id.modelNumberField);
			serialNumberField = FindViewById<EditText> (Resource.Id.serialNumberField);
			roomLabelSpinner = FindViewById<Spinner> (Resource.Id.roomLabelSpinner);
			categoryLabelSpinner = FindViewById<Spinner> (Resource.Id.categoryLabelSpinner);

			DisplayItemInfo ();

			Button receiptButton = FindViewById<Button> (Resource.Id.receiptButton);
			receiptButton.Click += (sender, e) => 
			{

			};

			Button goToPhotosButton = FindViewById<Button> (Resource.Id.goToPhotosButton);
			goToPhotosButton.Click += (sender, e) => 
			{

			};

			Button cancelAddItemButton = FindViewById<Button> (Resource.Id.cancelAddItemButton);
			cancelAddItemButton.Click += (sender, e) =>
			{
				SetResult (Result.Canceled);
				Finish ();
			};



			Button saveAddItemButton = FindViewById<Button> (Resource.Id.saveAddItemButton);
			saveAddItemButton.Click += (sender, e) => 
			{

			};

			Button deleteItemButton = FindViewById<Button> (Resource.Id.deleteItemButton);

			RoomSpinnerAdapter roomAdapter = new RoomSpinnerAdapter (this,Property);

			roomLabelSpinner.Adapter = roomAdapter;

			CategorySpinnerAdapter categoryAdapter = new CategorySpinnerAdapter (this, Property);
			categoryLabelSpinner.Adapter = categoryAdapter;

		}

		private void DisplayItemInfo()
		{
			if (Item == null) {
				return;
			}

			itemNameField.Text = Item.ItemName;
			purchaseDateField.Text = Item.PurchaseDate.ToString ();
			purchaseValueField.Text = Item.PurchaseValue.ToString ();
			appraisalDateField.Text = Item.AppraisalDate.ToString ();
			appraisalValueField.Text = Item.AppraisalValue.ToString ();
			modelNumberField.Text = Item.ModelNumber;
			serialNumberField.Text = Item.SerialNumber;
		}
	}
}

