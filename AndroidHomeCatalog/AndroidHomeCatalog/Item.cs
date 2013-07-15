using System;
using System.Collections;
using System.Collections.Generic;


namespace AndroidHomeCatalog
{
	[Serializable]
	public class Item
	{
		public Item ()
		{
		}

		// Create dictionary to contain metadata for Item
		public int parentID { get; set; }
		Dictionary<string,string> Data = new Dictionary<string,string>();

		public static Item InitializeItem (ItemContainer property, Item item)
		{
			// set parent Id for property
			item.parentID = property.ID;
			// Define metadata for Item class
			item.Data.Add ("Description", "");
			item.Data.Add ("Purchase Date", "");
			item.Data.Add ("Purchase Value", "");
			item.Data.Add ("Appraisal Value", "");
			item.Data.Add ("Appraisal Date", "");
			item.Data.Add ("Model Number", "");
			item.Data.Add ("Serial Number", "");

			return item;
		}


	}

}

