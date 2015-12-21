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
		private Property Property = PropertyStore.CurrentStore.Property;

		public CsvExporter (IEnumerable<Item> itemsToDisplay)
		{
			DisplayItems = itemsToDisplay;
		}

		public String ConstructOutput (String filepath)
		{
			var outputFile = new StreamWriter (filepath, false);
			var heading = "Name,Room,Category,Purchase Date,Purchase Value,Appraisal Date,Appraisal Value,Model Number,Serial Number";
			outputFile.WriteLine (heading);
			foreach (Item itemToDisplay in DisplayItems) {
				String record = GenerateRecord (itemToDisplay);
				outputFile.WriteLine (record);
			}
			outputFile.Close ();
			Console.Write (File.ReadAllText (filepath));
			return (null);
		}

		private String EscapedString(String aString){
			String escapedString = aString.Replace ("\"", "\"\"");
			return (escapedString);
		}

		private String GenerateRecord (Item itemToWrite){
			var escapedName = EscapedString (itemToWrite.ItemName);
			String escapedRoomLabel;
			String escapedCategoryLabel;
			if (Property.RoomList.RoomWithID(itemToWrite.RoomID) == null){
				escapedRoomLabel = "None";
			}else{
				escapedRoomLabel = EscapedString (Property.RoomList.RoomWithID(itemToWrite.RoomID).Label);
			}

			if (Property.CategoryList.CategoryByID (itemToWrite.CategoryID) == null) {	
				escapedCategoryLabel = "None";
			} else {
				escapedCategoryLabel = EscapedString (Property.CategoryList.CategoryByID (itemToWrite.CategoryID).Label);

			}
			string escapedPurchaseDate;
			string escapedAppraisalDate;

			if (itemToWrite.PurchaseDate != DateTime.MinValue) {
				escapedPurchaseDate = EscapedString (itemToWrite.PurchaseDate.ToShortDateString());
			} else {
				escapedPurchaseDate = "None";
			}

			var escapedPurchaseValue = itemToWrite.PurchaseValue.ToString ().Replace ("\"", "\"\"");

			if (itemToWrite.AppraisalDate != DateTime.MinValue) {
				escapedAppraisalDate = EscapedString (itemToWrite.AppraisalDate.ToShortDateString ());
			} else {
				escapedAppraisalDate = "None";
			}
			var escapedAppraisalValue = EscapedString (itemToWrite.AppraisalValue.ToString ());
			var escapedModelNumber = EscapedString (itemToWrite.ModelNumber.ToString ());
			var escapedSerialNumber = EscapedString (itemToWrite.SerialNumber.ToString ());
			string record = "\"" + escapedName + "\"" + ',' + "\"" + escapedRoomLabel + "\"" + ',' +
				"\"" + escapedCategoryLabel + "\"" + ',' + "\"" + escapedPurchaseDate + "\"" + ',' + "\"" + escapedPurchaseValue + "\"" + "," + "\"" +
				escapedAppraisalDate + "\"" + ',' + "\"" + escapedAppraisalValue + "\"" + ',' + "\"" + escapedModelNumber + "\"" + ',' +
				"\"" + escapedSerialNumber + "\"";
			return (record);
		}
	}
}

