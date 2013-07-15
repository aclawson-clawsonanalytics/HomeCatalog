using System;
using System.Collections;
using System.Collections.Generic;

namespace AndroidHomeCatalog
{
	[Serializable]
	public class ItemContainer
	{
		public ItemContainer ()
		{
		}

		public int ID { get; set; }
		public Dictionary<string,string> Data = new Dictionary<string,string> ();

		public Dictionary<string,bool> roomLabels = new Dictionary<string,bool> ();

		public Dictionary<string,bool> categoryLabels = new Dictionary<string,bool> ();

		public List<Item> itemList = new List<Item> ();


	// Class methods

	/*-------------------------------------displayData function ----------------------------*/
		public static void DisplayData (ItemContainer ic)
		{
			foreach (KeyValuePair<string,string> kvp in ic.Data) {
				Console.WriteLine (kvp.Value);
			}

			foreach (KeyValuePair<string,bool> kvp in ic.roomLabels) {
				if (kvp.Value == true) {
					Console.WriteLine (kvp.Key);
				}
			}
		}
// End displayData function

	/*--------------------------------------initializeCollection function -------------------------*/
		public static ItemContainer initializeCollection (ItemContainer newCollection, ItemContainer property)
		{
			// Initialize Room Labels and Category Labels
			//roomLabels
			foreach (KeyValuePair<string,bool> kvp in property.roomLabels) {
				if (kvp.Value == true) {
					newCollection.roomLabels.Add (kvp.Key,true);
				}
			}

			foreach (KeyValuePair<string,bool> kvp in property.categoryLabels) {
				if (kvp.Value == true) {
					newCollection.categoryLabels.Add (kvp.Key,true);
				}
			}

			return newCollection;
		}

		public static ItemContainer InitializeProperty (ItemContainer newProperty)
		{
			// Create and fill metadata for property
			newProperty.Data.Add ("Property Name", "");
			newProperty.Data.Add ("Address", "");
			newProperty.Data.Add ("City", "");
			newProperty.Data.Add ("State", "");
			newProperty.Data.Add ("Zip Code", "");

			// Initialize Room Labels and Category Labels
			//roomLabels
			newProperty.roomLabels.Add ("Kitchen", true);
			newProperty.roomLabels.Add ("Living Room", true);
			newProperty.roomLabels.Add ("Study", true);
			newProperty.roomLabels.Add ("Garage", true);
			newProperty.roomLabels.Add ("Basement", true);
			newProperty.roomLabels.Add ("Storage", true);

			//categoryLabels
			newProperty.categoryLabels.Add ("Electronics", true);
			newProperty.categoryLabels.Add ("Furniture", true);
			newProperty.categoryLabels.Add ("Lawn Equipment", true);
			newProperty.categoryLabels.Add ("Dishes", true);
			newProperty.categoryLabels.Add ("Appliances", true);
			newProperty.categoryLabels.Add ("Miscelaneous", true);

			return newProperty;
		}
	}

}



