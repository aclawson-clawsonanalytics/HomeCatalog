using System;
using System.Collections.Generic;
using System.Collections;
using SQLite;

namespace HomeCatalog.Core
{
	public class Property
	{
		public static string PropertyIDKey = "PropertyID";
		public Property ()
		{
			PropertyID = Guid.NewGuid ().ToString ();
		}

		// Define Metadata elements
		[PrimaryKey]
		public string PropertyID { get; private set; }
		public string PropertyName { get; set; }
		public string Address { get; set; }
		public string City {get;set;}
		public string State { get; set; }
		public string ZipCode {get;set;}

		public int NumberBaths { get; set; }
		public int NumberBeds { get; set; }

		// Define Standard set of room and category labels
		[Ignore]
		public RoomList RoomList { get; set; }
		[Ignore]
		public List<Category> CategoryList { get; set; }
		[Ignore]
		public List<Item> ItemList { get; set; }

		public static Room CreateRoom(Room room, string label)
		{
			room.Label = label;

			return room;
		}


	}


}

