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
			String PropertyID = Intent.GetStringExtra (Property.PropertyIDKey);

			Property = PropertyCollection.SharedCollection.FindPropertyWithId (PropertyID);

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
			Button AddCustomCategoryButton = FindViewById<Button> (Resource.Id.AddCustomCategoryButton);

			AddCustomCategoryButton.Click += (sender,e) =>
			{
				SaveCustomCategory ();
			};

			Button CancelEditCategoriesButton = FindViewById<Button> (Resource.Id.CancelEditCategoriesButton);

			CancelEditCategoriesButton.Click += (sender,e) =>
			{
				SetResult (Result.Canceled);
				Finish ();
			};

			Button SaveCategoriesButton = FindViewById<Button> (Resource.Id.SaveCategoriesButton);


			SaveCategoriesButton.Click += (sender, e) => 
			{
				WriteCategoriesToFile ("BeforeSave");
				SaveCategories ();
				Finish ();
				WriteCategoriesToFile ("AfterSave");
			};
		

		}

		private bool CheckForCategoryByLabel(string label)
		{
			int count = 1;
			foreach (Category cat in Property.CategoryList)
			{
				if (cat.Label == label)
				{
					count = count + 1;
				}
				
			}
			
			if (count > 0)
				{
					return true;
				}
			else
				{
					return false;
				}
			}

		private void WriteByCheckBoxandLabel(CheckBox check,string label)
		{
			if (check.Checked == true)
			{
				if (CheckForCategoryByLabel (label) == false)
				{
					Category NewCategory = new Category ();
					NewCategory.Label = label;
					Property.CategoryList.Add (NewCategory);
				}
			}
			else
			{
				if (CheckForCategoryByLabel(label) == true)
				{
					foreach (Category cat in Property.CategoryList)
					{
						if (cat.Label == label)
						{
							Property.CategoryList.Remove (cat);
						}
					}
				}
			}
		}

		private void SaveCategories()
		{
			WriteByCheckBoxandLabel (ApplianceCheckBox, "Appliance");
			WriteByCheckBoxandLabel (BathApplianceCheckBox, "Bathroom Appliance");
			WriteByCheckBoxandLabel (DishCheckBox, "Dishes");
			WriteByCheckBoxandLabel (ElectronicsCheckBox, "Electronics");
			WriteByCheckBoxandLabel (FurnitureCheckBox, "Furniture");
			WriteByCheckBoxandLabel (KitchenApplianceCheckBox, "Kitchen Appliance");
			WriteByCheckBoxandLabel (StorageCheckBox, "Storage");
			WriteByCheckBoxandLabel (ToolsCheckBox, "Tools");

			SaveCustomCategory ();
		}

		private void SaveCustomCategory ()
		{
			if (CustomCategoryField.Text != "")
			{
				Category NewCategory = new Category ();
				NewCategory.Label = CustomCategoryField.Text;
			}
		}

		private void WriteCategoriesToFile(string position)
		{
			string FileName = "CategoryTest_" + position + ".txt";
			StreamWriter file = new StreamWriter (FileName);
			file.WriteLine (Property.PropertyName);
			file.WriteLine (Property.Address);
			file.WriteLine (Property.City);
			file.WriteLine (Property.ZipCode);
			file.WriteLine ("Category List: ");
			file.WriteLine ();
			foreach (Category cat in Property.CategoryList)
			{
				file.WriteLine (cat.Label);
			}

			file.Close ();

		}
		
		
	}
}

