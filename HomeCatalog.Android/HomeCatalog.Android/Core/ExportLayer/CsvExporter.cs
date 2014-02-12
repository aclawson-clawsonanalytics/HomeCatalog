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
	class CsvExporter
	{
		private IEnumerable<Item> DisplayItems { get; set; }

		public CsvExporter (IEnumerable<Item> itemsToDisplay)
		{
			DisplayItems = itemsToDisplay;
		}

	

		public String ConstructOutput ()
		{
			foreach (Item itemToDisplay in DisplayItems)
			{
				Console.WriteLine (itemToDisplay.ItemName);
			}
			return (null);
		}


	}


}

