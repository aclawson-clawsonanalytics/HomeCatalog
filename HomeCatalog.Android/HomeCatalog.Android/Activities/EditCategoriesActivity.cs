using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	[Activity (Label = "EditCategoriesActivity")]			
	public class EditCategoriesActivity : Activity
	{
		private Property Property {get;set;}

		private EditText CustomCategoryField {get;set;}

		private CheckBox ApplianceCheckBox {get;set;}
		private CheckBox BathApplianceCheckBox {get;set;}
		private CheckBox DishCheckBox {get;set;}
		private CheckBox ElectronicsCheckBox {get;set;}
		private CheckBox FurnitureCheckBox {get;set;}
		private CheckBox KitchenApplianceCheckBox {get;set;}
		private CheckBox StorageCheckBox {get;set;}
		private CheckBox ToolsCheckBox {get;set;}

		private List<CheckBox> CheckBoxList { get; set; }



		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Grab property ID from intent passed into activity

			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.SetUpCategoryView);

			// Set views of EditTexts
			CustomCategoryField = FindViewById<EditText> (Resource.Id.CustomCategoryField);

			// Load CheckBox Views
			ApplianceCheckBox = FindViewById<CheckBox> (Resource.Id.ApplianceCheckBox);
			BathApplianceCheckBox = FindViewById<CheckBox> (Resource.Id.BathApplianceCheckBox);
			DishCheckBox = FindViewById<CheckBox> (Resource.Id.DishCheckBox);
			ElectronicsCheckBox = FindViewById<CheckBox> (Resource.Id.ElectronicsCheckBox);
			FurnitureCheckBox = FindViewById<CheckBox> (Resource.Id.FurnitureCheckBox);
			KitchenApplianceCheckBox = FindViewById<CheckBox> (Resource.Id.KitApplianceCheckBox);
			StorageCheckBox = FindViewById<CheckBox> (Resource.Id.StorageCheckBox);
			ToolsCheckBox = FindViewById<CheckBox> (Resource.Id.ToolsCheckBox);


			// Load EditText view
			CustomCategoryField = FindViewById<EditText> (Resource.Id.CustomCategoryField);

			// Add Buttons
			Button addCustomCategoryButton = FindViewById<Button> (Resource.Id.AddCustomCategoryButton);

			addCustomCategoryButton.Click += (sender,e) =>
			{
				SaveCustomCategory ();
				CustomCategoryField.Text="";
			};

			Button cancelEditCategoriesButton = FindViewById<Button> (Resource.Id.CancelEditCategoriesButton);

			cancelEditCategoriesButton.Click += (sender,e) =>
			{
				SetResult (Result.Canceled);
				Finish ();
			};

			Button saveCategoriesButton = FindViewById<Button> (Resource.Id.SaveCategoriesButton);


			saveCategoriesButton.Click += (sender, e) => 
			{
				SaveCategories ();
				Finish ();
			};

			Button viewCategoryListButton = FindViewById<Button> (Resource.Id.viewCategoryListButton);

			viewCategoryListButton.Click += (sender,e) =>
			{
				SaveCategories ();
				Intent PassPropertyID = new Intent (this,typeof(ViewCategoryListActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);
			};
		

		}

		private bool CheckForCategoryByLabel(string label)
		{
			int count = 0;
			foreach (Category cat in Property.CategoryList.AllItems ())
			{
				if (cat.Label == label)
				{
					count = count + 1;
				}
			}
			if (count == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		private void SaveCategoryFromCheckBox(string label,CheckBox check)
		{
			if (check.Checked == true && CheckForCategoryByLabel(label) == false)
			{
				Category newCategory = new Room ();
				newCategory.Label = label;
				Property.CategoryList.Add (newCategory);
			}
			else if (check.Checked == false && CheckForCategoryByLabel(label) == true)
			{
				Category cat = Property.CategoryList.CategoryWithName (label);
				Property.CategoryList.Remove (cat);
			}
		}
//		private bool CheckForCategoryByLabel(Property prop, string label)
//		{
//			int count = 0;
//			foreach (Category cat in Property.CategoryList.AllItems ())
//			{
//				if (cat.Label == label)
//				{
//					count = count + 1;
//				}
//			}
//			if (count == 0)
//			{
//				return false;
//			}
//			else
//			{
//				return true;
//			}
//		}
//
//		private void SaveCategories()
//		{
//			//Save Appliance
//			if (ApplianceCheckBox.Checked == true)
//			{
//				//If not found, add to Property.RoomList
//				//If found, don't do anything
//				if (CheckForCategoryByLabel (Property,"Kitchen") == false)
//				{
//					Category Appliance = new Category ();
//					Appliance.Label = "Appliance";
//					Property.CategoryList.Add (Appliance);
//				}
//			}
//			//If box.checked == false, check for room and delete if existing
//			else
//			{
//				if (CheckForCategoryByLabel (Property, "Appliance") == true)
//				{
//					RemoveByCategoryLabel("Appliance");
//				}
//			}
//
//			//Save BathAppliance
//			if (BathApplianceCheckBox.Checked == true)
//			{
//				//If not found, add to Property.RoomList
//				//If found, don't do anything
//				if (CheckForCategoryByLabel (Property,"Bathroom Appliance") == false)
//				{
//					Category BathroomAppliance = new Category ();
//					BathroomAppliance.Label = "Bathroom Appliance";
//					Property.CategoryList.Add (BathroomAppliance);
//				}
//				else
//				{
//					RemoveByCategoryLabel("Bathroom Appliance");
//				}
//			}
//
//			// Save Dishes
//			if (DishCheckBox.Checked == true)
//			{
//				//If not found, add to Property.RoomList
//				//If found, don't do anything
//				if (CheckForCategoryByLabel (Property,"Dishes") == false)
//				{
//					Category Dishes = new Category ();
//					Dishes.Label = "Dishes";
//					Property.CategoryList.Add (Dishes);
//				}
//				else
//				{
//					RemoveByCategoryLabel("Dishes");
//				}
//			}
//
//			// Electronics
//			if (ElectronicsCheckBox.Checked == true)
//			{
//				//If not found, add to Property.RoomList
//				//If found, don't do anything
//				if (CheckForCategoryByLabel (Property,"Electronics") == false)
//				{
//					Category Electronics = new Category ();
//					Electronics.Label = "Electronics";
//					Property.CategoryList.Add (Electronics);
//				}
//				else
//				{
//					RemoveByCategoryLabel("Electronics");
//				}
//			}
//
//			//Furniture
//			if (FurnitureCheckBox.Checked == true)
//			{
//				//If not found, add to Property.RoomList
//				//If found, don't do anything
//				if (CheckForCategoryByLabel (Property,"Furniture") == false)
//				{
//					Category Furniture = new Category ();
//					Furniture.Label = "Furniture";
//					Property.CategoryList.Add (Furniture);
//				}
//				else
//				{
//					RemoveByCategoryLabel ("Furniture");
//				}
//			}
//
//			//Kitchen Appliance
//			if (KitchenApplianceCheckBox.Checked == true)
//			{
//				//If not found, add to Property.RoomList
//				//If found, don't do anything
//				if (CheckForCategoryByLabel (Property,"Kitchen Appliance") == false)
//				{
//					Category KitchenAppliance = new Category ();
//					KitchenAppliance.Label = "Kitchen Appliance";
//					Property.CategoryList.Add (KitchenAppliance);
//				}
//				else
//				{
//					RemoveByCategoryLabel("Kitchen Appliance");
//				}
//			}
//
//			if (StorageCheckBox.Checked == true)
//			{
//				//If not found, add to Property.RoomList
//				//If found, don't do anything
//				if (CheckForCategoryByLabel (Property,"Storage") == false)
//				{
//					Category Storage = new Category ();
//					Storage.Label = "Storage";
//					Property.CategoryList.Add (Storage);
//				}
//				else
//				{
//					RemoveByCategoryLabel ("Storage");
//				}
//			}
//
//			if (ToolsCheckBox.Checked == true)
//			{
//				//If not found, add to Property.RoomList
//				//If found, don't do anything
//				if (CheckForCategoryByLabel (Property,"Tools") == false)
//				{
//					Category Tools = new Category ();
//					Tools.Label = "Tools";
//					Property.CategoryList.Add (Tools);
//				}
//				else
//				{
//					RemoveByCategoryLabel ("Tools");
//				}
//			}
//
//			SaveCustomCategory ();
//
//		}
//
//
//		private bool SetCheckBoxByCategory (string label)
//		{
//			if (CheckForCategoryByLabel (Property,label) == true)
//			{
//				return true;
//			}
//			else
//			{
//				return false;
//			}
//
//		}
//
//		private void SaveCustomCategory ()
//		{
//			if (CustomCategoryField.Text != "")
//			{
//				Category NewCategory = new Category ();
//				NewCategory.Label = CustomCategoryField.Text;
//			}
//		}
//
//
//		private void RemoveByCategoryLabel (string label)
//		{
//			foreach (Category cat in Property.CategoryList.AllItems ())
//			{
//				if (cat.Label == label)
//				{
//					Property.CategoryList.Remove (cat);
//				}
//			}
//		}
		
		
	}
}

