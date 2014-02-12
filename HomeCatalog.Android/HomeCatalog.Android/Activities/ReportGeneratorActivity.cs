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
	[Activity (Label = "ReportGeneratorActivity")]			
	public class ReportGeneratorActivity : Activity
	{
		private Property Property { get; set; }
		private Spinner propertySpinner { get; set; }
		private Spinner roomLabelSpinner { get; set; }
		private Spinner categoryLabelSpinner { get ; set; }

		const int roomUpdateRequestCode = 1;
		const int categoryUpdateRequestCode = 2;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.ReportGeneratorView);
			roomLabelSpinner = FindViewById<Spinner> (Resource.Id.roomQuerrySpinner);
			categoryLabelSpinner = FindViewById<Spinner> (Resource.Id.categoryQuerrySpinner);


			RoomSpinnerAdapter roomAdapter = new RoomSpinnerAdapter (this, Property,"All Rooms");
			roomLabelSpinner.Adapter = roomAdapter;

			CategorySpinnerAdapter categoryAdapter = new CategorySpinnerAdapter (this, Property,"All Categories");
			categoryLabelSpinner.Adapter = categoryAdapter;

			Button GenerateReportButton = FindViewById<Button> (Resource.Id.GenerateReportButton);
			GenerateReportButton.Click += (sender, e) => 
			{
				IEnumerable<Item> exportItems;
				if (roomLabelSpinner.SelectedItemPosition == 0 && categoryLabelSpinner.SelectedItemPosition == 0)
				{
					//var roomID = roomAdapter [roomLabelSpinner.SelectedItemPosition].ID;
					//exportItems = Property.ItemList.InternalTable.Where (item => item.RoomID == roomID);
					exportItems = Property.ItemList.AllItems ();
					CsvExporter exporter = new CsvExporter (exportItems);
					exporter.ConstructOutput ();
				}

				else if (roomLabelSpinner.SelectedItemPosition == 0 && categoryLabelSpinner.SelectedItemPosition != 0)
				{
					//var roomID = roomAdapter [roomLabelSpinner.SelectedItemPosition].ID;
					var catID = categoryAdapter [categoryLabelSpinner.SelectedItemPosition].ID;
					exportItems = Property.ItemList.InternalTable.Where (item => item.CategoryID == catID);
					//exportItems = Property.ItemList.AllItems ();
					CsvExporter exporter = new CsvExporter (exportItems);
					exporter.ConstructOutput ();
				}

				else if (roomLabelSpinner.SelectedItemPosition != 0 && categoryLabelSpinner.SelectedItemPosition == 0)
				{
					var roomID = roomAdapter [roomLabelSpinner.SelectedItemPosition].ID;
					//var catID = categoryAdapter [categoryLabelSpinner.SelectedItemPosition].ID;
					exportItems = Property.ItemList.InternalTable.Where (item => item.RoomID == roomID);
					//exportItems = Property.ItemList.AllItems ();
					CsvExporter exporter = new CsvExporter (exportItems);
					exporter.ConstructOutput ();
				}

				else if (roomLabelSpinner.SelectedItemPosition != 0 && categoryLabelSpinner.SelectedItemPosition != 0)
				{
					var roomID = roomAdapter [roomLabelSpinner.SelectedItemPosition].ID;
					var catID = categoryAdapter [categoryLabelSpinner.SelectedItemPosition].ID;
					exportItems = 
						Property.ItemList.InternalTable.Where (item => item.CategoryID == catID && item.RoomID == roomID);
					//exportItems = Property.ItemList.AllItems ();
					CsvExporter exporter = new CsvExporter (exportItems);
					exporter.ConstructOutput ();
				}

			};

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

		private void SortWithoutFilter()
		{

		}

		private void SortByRoomLabel()
		{


		}

		private void SortByCategoryLabel()
		{



		}






	}
}

