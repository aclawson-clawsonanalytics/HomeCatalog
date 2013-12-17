using System;
using System.Collections;
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
	[Activity (Label = "Enter item information")]			
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

		private TextView purchaseDateDisplay { get; set; }
		private TextView appraisalDateDisplay { get; set; }

		private DatePicker purchaseDate { get; set; }
		private DatePicker appraisalDate { get; set; }

		private Spinner roomLabelSpinner { get; set; }
		private Spinner categoryLabelSpinner { get; set; }

		private DateTime CurrentDate { get; set; }
		private DateTime itemPurchaseDate { get; set; }
		private DateTime itemAppraisalDate { get; set; }
		const int Date_Dialog_ID1 = 0;

		const int Date_Dialog_ID2 = 1;

		const int roomUpdateRequestCode = 1;
		const int categoryUpdateRequestCode = 2;

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
			//purchaseDateField = FindViewById<EditText> (Resource.Id.purchaseDateField);
			purchaseValueField = FindViewById<EditText> (Resource.Id.purchaseValueField);
			//appraisalDateField = FindViewById<EditText> (Resource.Id.appraisalDateField);
			appraisalValueField = FindViewById<EditText> (Resource.Id.appraisalValueField);
			modelNumberField = FindViewById<EditText> (Resource.Id.modelNumberField);
			serialNumberField = FindViewById<EditText> (Resource.Id.serialNumberField);

			purchaseDateDisplay = FindViewById<TextView> (Resource.Id.purchaseDateDisplay);
			appraisalDateDisplay = FindViewById<TextView> (Resource.Id.appraisalDateDisplay);

			roomLabelSpinner = FindViewById<Spinner> (Resource.Id.roomLabelSpinner);
			categoryLabelSpinner = FindViewById<Spinner> (Resource.Id.categoryLabelSpinner);


			CurrentDate = DateTime.Today;



			Button roomAddButton = FindViewById<Button> (Resource.Id.roomPlusButton);
			roomAddButton.Click += (sender, e) => 
			{

				SaveItemInfo ();
				Intent newRoomIntent = new Intent (this,typeof(ViewRoomListActivity));
				newRoomIntent.PutExtra ("roomID",Item.ID);
				StartActivityForResult (newRoomIntent,roomUpdateRequestCode);
			};

			Button categoryAddButton = FindViewById<Button> (Resource.Id.categoryPlusButton);
			categoryAddButton.Click += (sender, e) => 
			{
				SaveItemInfo ();
				Intent newCategoryIntent = new Intent (this,typeof(ViewCategoryListActivity));
				newCategoryIntent.PutExtra ("catID",Item.ID);
				StartActivityForResult (newCategoryIntent,categoryUpdateRequestCode);
			};

			Button setPurchaseDateButton = FindViewById<Button> (Resource.Id.setPurchaseDateButton);
			setPurchaseDateButton.Click += delegate{ ShowDialog (Date_Dialog_ID1);};

			Button setAppraisalDateButton = FindViewById<Button> (Resource.Id.setAppraisalDateButton);
			setAppraisalDateButton.Click += delegate{ ShowDialog (Date_Dialog_ID2);};

			Button receiptButton = FindViewById<Button> (Resource.Id.receiptButton);
			receiptButton.Click += (sender, e) => 
			{

			};

			Button goToPhotosButton = FindViewById<Button> (Resource.Id.goToPhotosButton);
			goToPhotosButton.Click += (sender, e) => 
			{
				SaveItemInfo ();

				Intent PassPropertyID = new Intent (this,typeof(PhotoBrowserActivity));
				PassPropertyID.PutExtra (Item.ItemIDKey, Item.ID);
				StartActivity (PassPropertyID);
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
				SaveItemInfo ();
				SetResult (Result.Ok);
				Finish ();
			};

			Button deleteItemButton = FindViewById<Button> (Resource.Id.deleteItemButton);
			deleteItemButton.Click += (sender, e) => 
			{
				Property.ItemList.Remove (Item);
				Finish();
			};

			RoomSpinnerAdapter roomAdapter = new RoomSpinnerAdapter (this,Property);

			roomLabelSpinner.Adapter = roomAdapter;

			CategorySpinnerAdapter categoryAdapter = new CategorySpinnerAdapter (this, Property);
			categoryLabelSpinner.Adapter = categoryAdapter;

			DisplayItemInfo ();
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			if (requestCode == roomUpdateRequestCode)
			{
				((RoomSpinnerAdapter)roomLabelSpinner.Adapter).NotifyDataSetChanged ();
			}
			else if (requestCode == categoryUpdateRequestCode)
			{
				((CategorySpinnerAdapter)categoryLabelSpinner.Adapter).NotifyDataSetChanged ();
			}
		}

		private void DisplayItemInfo()
		{
			if (Item == null) {
				return;
			}

			itemNameField.Text = Item.ItemName;
			//purchaseDateField.Text = Item.PurchaseDate.ToString ();
			purchaseDateDisplay.Text = Item.PurchaseDate.ToShortDateString ();
			purchaseValueField.Text = Item.PurchaseValue.ToString ();
			//appraisalDateField.Text = Item.AppraisalDate.ToString ();
			appraisalDateDisplay.Text = Item.AppraisalDate.ToShortDateString ();
			appraisalValueField.Text = Item.AppraisalValue.ToString ();
			modelNumberField.Text = Item.ModelNumber;
			serialNumberField.Text = Item.SerialNumber;

			RoomSpinnerAdapter roomAdapter = (RoomSpinnerAdapter)roomLabelSpinner.Adapter;

			int roomIndex = -1;
			foreach (Room room in roomAdapter.Rooms)
			{
				if (room.ID == Item.RoomID)
				{
					roomIndex = roomAdapter.Rooms.IndexOf (room);
				}
			}
			if (roomIndex > -1)
			{
				roomLabelSpinner.SetSelection (roomIndex + 1);
			}
			categoryLabelSpinner.SetSelection (Item.CategoryID);

		}

		private void SaveItemInfo()
		{
			if (Item == null)
			{
				Item = new Item ();
				Property.ItemList.Add (Item);
				Property.ItemList.Save (Item);
			}

			Item.ItemName = itemNameField.Text;
			Item.PurchaseDate = itemPurchaseDate;
//			Item.PurchaseValue = purchaseValueField.Text;
			Item.AppraisalDate = itemAppraisalDate;
//			Item.AppraisalValue = appraisalValueField.Text;
			Item.ModelNumber = modelNumberField.Text;
			Item.SerialNumber = serialNumberField.Text;


			var room = ((RoomSpinnerAdapter)roomLabelSpinner.Adapter) [roomLabelSpinner.SelectedItemPosition];
			if (room == null)
			{
				Item.RoomID = 0;
			}
			else
			{
			Item.RoomID = ((RoomSpinnerAdapter) roomLabelSpinner.Adapter) [roomLabelSpinner.SelectedItemPosition].ID;
			}

			var category = ((CategorySpinnerAdapter)categoryLabelSpinner.Adapter) [categoryLabelSpinner.SelectedItemPosition];
			if (category == null)
			{
				Item.CategoryID = 0;
			}
			else
			{
			Item.CategoryID = ((CategorySpinnerAdapter)categoryLabelSpinner.Adapter) [categoryLabelSpinner.SelectedItemPosition].ID;
			}
			Property.ItemList.Save (Item);
		}

		private void UpdatePurchaseDateDisplay()
		{
			purchaseDateDisplay.Text = itemPurchaseDate.ToShortDateString ();
		}

		private void UpdateAppraisalDateDisplay()
		{
			appraisalDateDisplay.Text = itemAppraisalDate.ToShortDateString ();
		}

		void OnPurchaseDateSet (object sender,DatePickerDialog.DateSetEventArgs e)
		{
			itemPurchaseDate = e.Date;
			UpdatePurchaseDateDisplay ();
		}

		void OnAppraisalDateSet (object sender,DatePickerDialog.DateSetEventArgs e)
		{

			itemAppraisalDate = e.Date;
			UpdateAppraisalDateDisplay ();
		}

		protected override Dialog OnCreateDialog (int id)
		{
			switch (id) {
			case Date_Dialog_ID1:
				return new DatePickerDialog (this, OnPurchaseDateSet, CurrentDate.Year, CurrentDate.Month - 1,
				                       CurrentDate.Day);
			case Date_Dialog_ID2:
				return new DatePickerDialog (this, OnAppraisalDateSet, CurrentDate.Year, CurrentDate.Month-1,
				                       CurrentDate.Day);
			}
			return null;
		}


	}
}

