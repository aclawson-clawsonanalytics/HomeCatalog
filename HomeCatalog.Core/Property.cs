using System;
using System.Collections.Generic;
using System.Collections;

namespace HomeCatalog.Core
{
	public class Property
	{
		public static string PropertyIDKey = "PropertyID";
		public Property ()
		{
//			

			PropertyID = Guid.NewGuid ().ToString ();
			RoomList = new List<Room> ();
			CategoryList = new List<Category> ();
			ItemList = new List<Item> ();
		}

		// Define Metadata elements
		public string PropertyID { get; private set; }
		public string PropertyName { get; set; }
		public string Address { get; set; }
		public string City {get;set;}
		public string State { get; set; }
		public string ZipCode {get;set;}

		// Define Standard set of room and category labels
		public List<Room> RoomList { get; set; }
		public List<Category> CategoryList { get; set; }
		public List<Item> ItemList { get; set; }

		public static Room CreateRoom(Room room, string label)
		{
			room.Label = label;

			return room;
		}


	}


}

