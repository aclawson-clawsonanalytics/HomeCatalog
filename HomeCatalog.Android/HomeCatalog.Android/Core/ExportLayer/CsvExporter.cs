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

		public CsvExporter (IEnumerable<Item> itemsToDisplay)
		{
			DisplayItems = itemsToDisplay;
		}

	

		public String ConstructOutput (String filename)
		{
			//var currentDirectory = System.Environment.CurrentDirectory;
			var directory = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);
			var filestring = directory + ".csv";
			var filepath = System.IO.Path.Combine (directory, filestring);

			var outputFile = new StreamWriter (filepath);
			var heading = "Name,Purchase Date,Purchase Value,Appraisal Date,Appraisal Value,Model Number,Serial Number";
			outputFile.WriteLine (heading);
			//Console.WriteLine (heading);
			foreach (Item itemToDisplay in DisplayItems)
			{


				//System.IO.File csvFile = new System.IO.File ();

				var escapedName = itemToDisplay.ItemName.Replace ("\"", "\"\"");
				var escapedPurchaseDate = itemToDisplay.PurchaseDate.ToShortDateString ().Replace ("\"","\"\"");
				var escapedPurchaseValue = itemToDisplay.PurchaseValue.ToString ().Replace ("\"","\"\"");
				var escapedAppraisalDate = itemToDisplay.AppraisalDate.ToShortDateString ().Replace ("\"","\"\"");
				var escapedAppraisalValue = itemToDisplay.AppraisalValue.ToString ().Replace ("\"","\"\"");
				var escapedModelNumber = itemToDisplay.ModelNumber.ToString ().Replace ("\"","\"\"");
				var escapedSerialNumber = itemToDisplay.SerialNumber.ToString ().Replace ("\"","\"\"");
				string record = "\"" + escapedName + "\"" + ',' + "\"" + escapedPurchaseDate + "\"" + ',' + "\"" +
					escapedAppraisalDate + "\"" + ',' + "\"" + ',' + "\"" + escapedModelNumber + "\"" + ',' +
					"\"" + escapedSerialNumber + "\"";
				//Console.WriteLine (record);
				outputFile.WriteLine (record);
				//Console.WriteLine (itemToDisplay.ItemName);
			}
			outputFile.Close ();
			Console.Write (File.ReadAllText(filepath));
			Console.WriteLine (filepath);
			return (null);
		}



	}


}

