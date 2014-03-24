using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	class CsvExporter
	{
		private IEnumerable<Item> DisplayItems { get; set; }

		private int fileNumberCount { get; set; }

		public CsvExporter (IEnumerable<Item> itemsToDisplay)
		{
			DisplayItems = itemsToDisplay;
		}

		public String ConstructOutput (String filepath)
		{
			//var directory = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);

			//var filestring = directory + ".csv";

			//var filepath = System.IO.Path.Combine (directory, filename);

			// Change file name if filepath exists



			
			var outputFile = new StreamWriter (filepath, false);
			var heading = "Name,Purchase Date,Purchase Value,Appraisal Date,Appraisal Value,Model Number,Serial Number";
			outputFile.WriteLine (heading);
			//Console.WriteLine (heading);
			foreach (Item itemToDisplay in DisplayItems) {


				//System.IO.File csvFile = new System.IO.File ();

				var escapedName = itemToDisplay.ItemName.Replace ("\"", "\"\"");
				string escapedPurchaseDate;
				string escapedAppraisalDate;

				if (itemToDisplay.PurchaseDate != DateTime.MinValue) {
					escapedPurchaseDate = itemToDisplay.PurchaseDate.ToShortDateString ().Replace ("\"", "\"\"");
				} else {
					escapedPurchaseDate = "None";
				}

				var escapedPurchaseValue = itemToDisplay.PurchaseValue.ToString ().Replace ("\"", "\"\"");

				if (itemToDisplay.AppraisalDate != DateTime.MinValue) {
					escapedAppraisalDate = itemToDisplay.AppraisalDate.ToShortDateString ().Replace ("\"", "\"\"");
				} else {
					escapedAppraisalDate = "None";
				}

				var escapedAppraisalValue = itemToDisplay.AppraisalValue.ToString ().Replace ("\"", "\"\"");
				var escapedModelNumber = itemToDisplay.ModelNumber.ToString ().Replace ("\"", "\"\"");
				var escapedSerialNumber = itemToDisplay.SerialNumber.ToString ().Replace ("\"", "\"\"");
				string record = "\"" + escapedName + "\"" + ',' + "\"" + escapedPurchaseDate + "\"" + ',' + "\"" + escapedPurchaseValue + "\"" + "," + "\"" +
				                escapedAppraisalDate + "\"" + escapedAppraisalValue + "\"" + ',' + "\"" + escapedModelNumber + "\"" + ',' +
				                "\"" + escapedSerialNumber + "\"";
				//Console.WriteLine (record);
				outputFile.WriteLine (record);
				//Console.WriteLine (itemToDisplay.ItemName);
			}
			outputFile.Close ();
			Console.Write (File.ReadAllText (filepath));
			//Console.WriteLine (filepath);
			return (null);
		}
	}
}

