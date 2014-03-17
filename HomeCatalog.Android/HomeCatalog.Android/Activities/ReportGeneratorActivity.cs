using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Content;
using Android.Content.PM;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	[Activity (Label = "ReportGeneratorActivity")]			
	public class ReportGeneratorActivity : StandardActivity
	{
		private Property Property { get; set; }

		private Spinner propertySpinner { get; set; }

		private Spinner roomLabelSpinner { get; set; }

		private Spinner categoryLabelSpinner { get ; set; }

		private Spinner exportMethodSpinner { get; set; }
		//int filenameCount = 1;
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
			exportMethodSpinner = FindViewById<Spinner> (Resource.Id.exportMethodSpinner);


			RoomSpinnerAdapter roomAdapter = new RoomSpinnerAdapter (this, Property, "All Rooms");
			roomLabelSpinner.Adapter = roomAdapter;

			CategorySpinnerAdapter categoryAdapter = new CategorySpinnerAdapter (this, Property, "All Categories");
			categoryLabelSpinner.Adapter = categoryAdapter;

			string[] exportChoices = { ".csv", ".pdf" };

			exportMethodSpinner.Adapter = new ArrayAdapter (this, Resource.Layout.GenericSpinnerItem, exportChoices);

			Button GenerateReportButton = FindViewById<Button> (Resource.Id.GenerateReportButton);
			GenerateReportButton.Click += (sender, e) => {
				IEnumerable<Item> exportItems = null;
				if (roomLabelSpinner.SelectedItemPosition == 0 && categoryLabelSpinner.SelectedItemPosition == 0) {
					exportItems = Property.ItemList.AllItems ();
				} else if (roomLabelSpinner.SelectedItemPosition == 0 && categoryLabelSpinner.SelectedItemPosition != 0) {
					var catID = categoryAdapter [categoryLabelSpinner.SelectedItemPosition].ID;
					exportItems = Property.ItemList.InternalTable.Where (item => item.CategoryID == catID);
				} else if (roomLabelSpinner.SelectedItemPosition != 0 && categoryLabelSpinner.SelectedItemPosition == 0) {
					var roomID = roomAdapter [roomLabelSpinner.SelectedItemPosition].ID;
					exportItems = Property.ItemList.InternalTable.Where (item => item.RoomID == roomID);
				} else if (roomLabelSpinner.SelectedItemPosition != 0 && categoryLabelSpinner.SelectedItemPosition != 0) {
					var roomID = roomAdapter [roomLabelSpinner.SelectedItemPosition].ID;
					var catID = categoryAdapter [categoryLabelSpinner.SelectedItemPosition].ID;
					exportItems = 
						Property.ItemList.InternalTable.Where (item => item.CategoryID == catID && item.RoomID == roomID);
				}

				if (exportMethodSpinner.SelectedItemPosition == 0) {
					CsvExporter exporter = new CsvExporter (exportItems);
					String filename = FilePathFromDate ();
					exporter.ConstructOutput (filename);
					//var file = new Java.IO.File (filename);
					//var uri = FileProvider.GetUriForFile (this, "com.clawsonanalytics.home_catalog.fileprovider", file);

					var uri = global::Android.Net.Uri.Parse ("content://com.clawsonanalytics.home_catalog.fileprovider/my_reports/" + Path.GetFileName (filename));


					Intent sendIntent = new Intent ();
					sendIntent.SetAction (Intent.ActionSend);
					sendIntent.PutExtra (Intent.ExtraStream, uri);
					sendIntent.SetData (uri);
					sendIntent.SetType ("text/csv");
					StartActivity (sendIntent);
				} else {

				}
			};

		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			if (requestCode == roomUpdateRequestCode) {
				((RoomSpinnerAdapter)roomLabelSpinner.Adapter).NotifyDataSetChanged ();
			} else if (requestCode == categoryUpdateRequestCode) {
				((CategorySpinnerAdapter)categoryLabelSpinner.Adapter).NotifyDataSetChanged ();
			}
		}

		private String CreateDateString (DateTime date)
		{
			String month = date.Month.ToString ();
			String day = date.Day.ToString ();
			String year = date.Year.ToString ();
			String hour = date.Hour.ToString ();
			String minute = date.Minute.ToString ();
			String seconds = date.Second.ToString ();

			String returnString = month + '-' + day + '-' + year + '_' + hour + '_' + minute + '_' + seconds;
			return (returnString);
		}

		private void SortByRoomLabel ()
		{


		}

		private void SortByCategoryLabel ()
		{



		}

		private String FilePathFromDate ()
		{

			var filename = CreateDateString (DateTime.Now);
			var directory = this.FilesDir;
			var reportsDirectory = System.IO.Path.Combine (directory.ToString (), "reports");
			if (!File.Exists (reportsDirectory)) {
				Directory.CreateDirectory (reportsDirectory);
			}

			var filepath = System.IO.Path.Combine (reportsDirectory, filename);

			String extension = ".csv";
			String filestring;
			filestring = filepath + extension;
			//Console.WriteLine (filestring);
			return (filestring);
		}
	}
}

