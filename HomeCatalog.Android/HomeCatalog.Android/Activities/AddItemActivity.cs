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

			DisplayItemInfo ();

			CurrentDate = DateTime.Today;

			DisplayItemInfo ();

			Button roomAddButton = FindViewById<Button> (Resource.Id.roomPlusButton);
			roomAddButton.Click += (sender, e) => 
			{
				var transaction = FragmentManager.BeginTransaction();
				RoomListDialogFragment roomDialog = new RoomListDialogFragment ();
				roomDialog.Show(transaction,"roomListDialog");

				roomDialog.OnItemSelected += (DialogClickEventArgs a) =>
				{
					switch (a.Which)
					{
						case 0:
					{
						Room newRoom = new Room();
						newRoom.Label = "Attic";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 1:
					{
						Room newRoom = new Room();
						newRoom.Label = "Basement";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 2:
					{
						Room newRoom = new Room ();
						newRoom.Label = "Garage";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged();
						break;
					}
						case 3:
					{
						Room newRoom = new Room ();
						newRoom.Label = "Kitchen";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 4:
					{
						Room newRoom = new Room ();
						newRoom.Label = "Living Room";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 5:
					{
						Room newRoom = new Room ();
						newRoom.Label = "Office";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 6:
					{
						Room newRoom = new Room ();
						newRoom.Label = "Storage";
						Property.RoomList.Add (newRoom);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 7:
					{
						// Add Code to go to the Edit Room View for custom room
						Room newRoom = new Room ();
						newRoom.Label = "Custom";

						Property.RoomList.Add (newRoom);
						Property.RoomList.Save (newRoom);
						Intent createCustomRoom = new Intent(this,typeof(RoomEditActivity));
						createCustomRoom.PutExtra ("roomID",newRoom.ID);
						StartActivityForResult (createCustomRoom,0);
						break;
					}
					}
			};
			
			Button categoryAddButton = FindViewById<Button> (Resource.Id.categoryPlusButton);
			categoryAddButton.Click += (sender, e) => 
				{
					var transaction = FragmentManager.BeginTransaction();
					CategoryListDialogFragment catDialog = new CategoryListDialogFragment ();
					catDialog.Show(transaction,"catalogListDialog");

					catDialog.OnItemSelected += (DialogClickEventArgs a) =>
					{
						switch (a.Which)
						{
							case 0:
						{
							Category newCat = new Category();
							newCat.Label = "Appliance";
							Property.CategoryList.Add (newCat);
							ListAdapter.NotifyDataSetChanged ();
							break;
						}
							case 1:
						{
							Category newCat = new Category();
							newCat.Label = "Bathroom Appliance";
							Property.CategoryList.Add (newCat);
							ListAdapter.NotifyDataSetChanged ();
							break;
						}
							case 2:
						{
							Category newCat = new Category();
							newCat.Label = "Collection";
							Property.CategoryList.Add (newCat);
							ListAdapter.NotifyDataSetChanged ();
							break;
						}
							case 3:
						{
							Category newCat = new Category ();
							newCat.Label = "Electronics";
							Property.CategoryList.Add (newCat);
							ListAdapter.NotifyDataSetChanged();
							break;
						}
							case 4:
						{
							Category newCat = new Category ();
							newCat.Label = "Furniture";
							Property.CategoryList.Add (newCat);
							ListAdapter.NotifyDataSetChanged ();
							break;
						}
							case 5:
						{
							Category newCat = new Category ();
							newCat.Label = "Hobby";
							Property.CategoryList.Add (newCat);
							ListAdapter.NotifyDataSetChanged ();
							break;
						}
							case 6:
						{
							Category newCat = new Category ();
							newCat.Label = "Kitchen Appliance";
							Property.CategoryList.Add (newCat);
							ListAdapter.NotifyDataSetChanged ();
							break;
						}
							case 7:
						{
							Category newCat = new Category ();
							newCat.Label = "Storage";
							Property.CategoryList.Add (newCat);
							ListAdapter.NotifyDataSetChanged ();
							break;
						}
							case 8:
						{
							Category newCat = new Category ();
							newCat.Label = "Tools";
							Property.CategoryList.Add (newCat);
							ListAdapter.NotifyDataSetChanged ();
							break;
						}
							case 9:
						{
							// Add Code to go to the Edit Room View for custom room
							Category newCat = new Category ();
							newCat.Label = "Custom";

							Property.CategoryList.Add (newCat);
							Property.CategoryList.Save (newCat);

							Intent createCustomCategory = new Intent(this,typeof(CategoryEditActivity));
							createCustomCategory.PutExtra ("catID",newCat.ID);
							StartActivityForResult (createCustomCategory,0);
							break;
						}
						}
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
				var photoBrowserIntent = new Intent (this, typeof(PhotoBrowserActivity));
				photoBrowserIntent.PutExtra (Item.ItemIDKey, Item.ID);
				StartActivity (photoBrowserIntent);
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

		private void SaveItemInfo()
		{
			if (Item == null)
			{
				Item = new Item ();
				Property.ItemList.Add (Item);
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
			purchaseDateDisplay.Text = itemPurchaseDate.ToString ();
		}

		private void UpdateAppraisalDateDisplay()
		{
			appraisalDateDisplay.Text = itemAppraisalDate.ToString ();
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

