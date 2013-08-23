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
	[Activity (Label = "ItemsDetailActivity")]			
	public class ItemsDetailActivity : Activity
	{
		private Property Property { get; set; }
		private string itemID { get; set; }
		private Item Item {get;set;}


		private TextView itemNameText { get; set; }
		private TextView purchaseDateText { get; set; }
		private TextView purchaseValueText { get; set; }
		private TextView appraisalDateText { get; set; }
		private TextView appraisalValueText { get; set; }
		private TextView modelNumberText { get; set; }
		private TextView serialNumberText { get; set; }
		private TextView itemRoomText {get;set;}
		private TextView itemCategoryText { get; set; }

		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			int itemID = Intent.GetIntExtra (Item.ItemIDKey,0);


			Property = PropertyStore.CurrentStore.Property;
			Item = Property.ItemList.ItemWithID (itemID);

			SetContentView (Resource.Layout.ItemDetailsView);

			itemNameText = FindViewById<TextView> (Resource.Id.itemNameText);
			purchaseDateText = FindViewById<TextView> (Resource.Id.purchaseDateText);
			purchaseValueText = FindViewById<TextView> (Resource.Id.purchaseValueText);
			appraisalDateText = FindViewById<TextView> (Resource.Id.appraisalDateText);
			appraisalValueText = FindViewById<TextView> (Resource.Id.appraisalValueText);
			modelNumberText = FindViewById<TextView> (Resource.Id.modelNumberText);
			serialNumberText = FindViewById<TextView> (Resource.Id.serialNumberText);
			itemRoomText = FindViewById<TextView> (Resource.Id.itemRoomText);
			itemCategoryText = FindViewById<TextView> (Resource.Id.itemCategoryText);


			FillItemData ();

			Button itemReceiptButton = FindViewById<Button> (Resource.Id.itemReceiptButton);
			itemReceiptButton.Click += (sender, e) => 
			{

			};

			Button itemPhotosButton = FindViewById<Button> (Resource.Id.itemPhotosButton);
			itemPhotosButton.Click += (sender, e) => 
			{

			};

			Button cancelItemDetailButton = FindViewById<Button> (Resource.Id.cancelItemDetailButton);
			cancelItemDetailButton.Click += (sender, e) => 
			{
				SetResult(Result.Canceled);
				Finish ();
			};

			Button deleteItemButton = FindViewById<Button> (Resource.Id.deleteItemButton);
			deleteItemButton.Click += (sender, e) => 
			{
				Property.ItemList.Remove (Item);
			};

		}

		private void FillItemData ()
		{
			itemNameText.Text = Item.ItemName;
			purchaseDateText.Text = Item.PurchaseDate.ToString ();
			purchaseValueText.Text = Item.PurchaseValue.ToString ();
			appraisalDateText.Text = Item.AppraisalDate.ToString ();
			appraisalValueText.Text = Item.AppraisalValue.ToString ();
			modelNumberText.Text = Item.ModelNumber;
			serialNumberText.Text = Item.SerialNumber;

			itemRoomText.Text = Property.RoomList.ItemWithID (Item.RoomID).Label;
			itemCategoryText.Text = Property.CategoryList.ItemWithID (Item.CategoryID).Label;


		}
	}
}

