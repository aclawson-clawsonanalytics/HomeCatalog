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


			RoomSpinnerAdapter roomAdapter = new RoomSpinnerAdapter (this, Property,"All Rooms");
			roomLabelSpinner.Adapter = roomAdapter;

			CategorySpinnerAdapter categoryAdapter = new CategorySpinnerAdapter (this, Property,"All Categories");
			categoryLabelSpinner.Adapter = categoryAdapter;

			Button GenerateReportButton = FindViewById<Button> (Resource.Id.GenerateReportButton);
			GenerateReportButton.Click += (sender, e) => 
			{
				IEnumerable<Item> exportItems = null;
				if (roomLabelSpinner.SelectedItemPosition == 0 && categoryLabelSpinner.SelectedItemPosition == 0)
				{
					//var roomID = roomAdapter [roomLabelSpinner.SelectedItemPosition].ID;
					//exportItems = Property.ItemList.InternalTable.Where (item => item.RoomID == roomID);
					exportItems = Property.ItemList.AllItems ();
				}

				else if (roomLabelSpinner.SelectedItemPosition == 0 && categoryLabelSpinner.SelectedItemPosition != 0)
				{
					//var roomID = roomAdapter [roomLabelSpinner.SelectedItemPosition].ID;
					var catID = categoryAdapter [categoryLabelSpinner.SelectedItemPosition].ID;
					exportItems = Property.ItemList.InternalTable.Where (item => item.CategoryID == catID);
					//exportItems = Property.ItemList.AllItems ();


				}

				else if (roomLabelSpinner.SelectedItemPosition != 0 && categoryLabelSpinner.SelectedItemPosition == 0)
				{
					var roomID = roomAdapter [roomLabelSpinner.SelectedItemPosition].ID;
					//var catID = categoryAdapter [categoryLabelSpinner.SelectedItemPosition].ID;
					exportItems = Property.ItemList.InternalTable.Where (item => item.RoomID == roomID);
					//exportItems = Property.ItemList.AllItems ();

				}

				else if (roomLabelSpinner.SelectedItemPosition != 0 && categoryLabelSpinner.SelectedItemPosition != 0)
				{
					var roomID = roomAdapter [roomLabelSpinner.SelectedItemPosition].ID;
					var catID = categoryAdapter [categoryLabelSpinner.SelectedItemPosition].ID;
					exportItems = 
						Property.ItemList.InternalTable.Where (item => item.CategoryID == catID && item.RoomID == roomID);
					//exportItems = Property.ItemList.AllItems ();
				}


				CsvExporter exporter = new CsvExporter (exportItems);
				//String filename = CreateDateString(DateTime.Now);
				//String extension = ".csv";
				String filename = FilePathFromDate ();
				exporter.ConstructOutput (filename);

				//var uri = FileProvider.GetUriForFile(this, "com.clawsonanalytics.fileprovider", new Java.IO.File(filename));
				var uri = global::Android.Net.Uri.FromFile (new Java.IO.File(filename));
				//this.GrantUriPermission ("com.clawsonanlytics.homecatalog",uri,
				Intent sendIntent = new Intent();
				sendIntent.AddFlags (ActivityFlags.GrantReadUriPermission);
				sendIntent.SetAction(Intent.ActionSend);
				sendIntent.SetData (uri);
				sendIntent.PutExtra(Intent.ExtraStream,uri);
				sendIntent.SetType ("text/csv");
				StartActivity (sendIntent);
				//StartActivity (Intent.CreateChooser (sendIntent,
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

		private String CreateDateString(DateTime date)
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

		private void SortByRoomLabel()
		{


		}

		private void SortByCategoryLabel()
		{



		}

		private String FilePathFromDate()
		{

			var filename = CreateDateString (DateTime.Now);
			var directory = this.FilesDir;
			var reportsDirectory = System.IO.Path.Combine (directory.ToString (), "reports");
			if (!File.Exists (reportsDirectory)) {
				Directory.CreateDirectory (reportsDirectory);
			}

			var filepath = System.IO.Path.Combine (reportsDirectory,filename);

			String extension = ".csv";
			String filestring;
			filestring = filepath + extension;
			//Console.WriteLine (filestring);
			return (filestring);
		}
	}
}

