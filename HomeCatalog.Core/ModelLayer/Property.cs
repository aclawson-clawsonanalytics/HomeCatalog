using System;
using System.Collections.Generic;
using System.Collections;
using SQLite;

namespace HomeCatalog.Core
{
	public class Property
	{
		//public static string PropertyIDKey = "PropertyID";
		public string PropertyIDKey = "PropertyID";
		public Property ()
		{
			PropertyID = Guid.NewGuid ().ToString ();
		}

		// Define Metadata elements
		[PrimaryKey]
		public string PropertyID { get; set; }
		public string PropertyName { get; set; }
		public string Address { get; set; }
		public string City {get;set;}
		public string State { get; set; }
		public string ZipCode {get;set;}
		public string Country { get; set; }

		public int NumberBaths { get; set; }
		public int NumberBeds { get; set; }

		// Define Standard set of room and category labels
		[Ignore]
		public RoomList RoomList { get; set; }
		[Ignore]
		public CategoryList CategoryList { get; set; }
		[Ignore]
		public ItemList ItemList { get; set; }
		[Ignore]
		public PropertyStore Store { get; set; }

		public Room CreateRoom(Room room, string label)
		{
			room.Label = label;

			return room;
		}

		public Category CreateCategory(Category cat, string label)
		{
			cat.Label = label;

			return cat;
		}

		public List<string> GetValidationErrors ()
		{
			List<string> ErrorList = new List<string> ();
			// Check that PropertyName is not empty
			if (String.IsNullOrWhiteSpace (PropertyName)) {
				ErrorList.Add ("Property name cannot be empyt");
			}

//			// Check that property name is unique
//			foreach (Property property in PropertyStore) {
//				if (property.PropertyName == PropertyName) {
//					ErrorList.Add ("Property name must be unique");
//				}
//			}

			if (ErrorList.Count > 0) {
				return ErrorList;
			} else {
				return null;
			}



		}
	}
}

