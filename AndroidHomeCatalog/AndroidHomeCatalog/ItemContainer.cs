using System;
using System.Collections;
using System.Collections.Generic;

namespace AndroidHomeCatalog
{
	public class ItemContainer
	{
		public ItemContainer ()
		{
		}

	
	// Class methods
		public static ItemContainer initializeProperty (ItemContainer newProperty)
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
	/*-------------------------------------displayData function ----------------------------*/
		public static string displayData (itemContainer ic)
		{
			foreach (KeyValuePair<string,bool> kvp in ic.Data) {
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
		public static itemContainer initializeCollection (itemContainer newCollection, itemContainer property)
		{
			// Initialize Room Labels and Category Labels
			//roomLabels
			foreach (KeyValuePair<string,bool> kvp in property.roomLabels) {
				if (kvp.Value == true) {
					newCollection.roomLabels.Add (kvp);
				}
			}

			foreach (KeyValuePair<string,bool> kvp in property.categoryLabels) {
				if (kvp.Value == true) {
					newCollection.categoryLabels.Add (kvp);
				}
			}
		}
	}
}



