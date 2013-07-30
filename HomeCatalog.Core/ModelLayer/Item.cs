using System;
using SQLite;

namespace HomeCatalog.Core
{
	public class Item : ISQLListItem
	{

		public Item ()
		{

		}

		public static string ItemIDKey = "ItemID";
		static int IDCounter = 0;

		public static int GetNewID() {
			++IDCounter;
			return IDCounter;
		}

		[PrimaryKey, AutoIncrement]
		public int ID {get;set;}
		public string ParentID { get; set; }
		public string ItemName { get; set; }

		public DateTime PurchaseDate { get; set; }
		public float PurchaseValue {get;set;}
		public DateTime AppraisalDate {get;set;}
		public float AppraisalValue { get; set; }
		public string ModelNumber { get; set; }
		public string SerialNumber {get;set;}

		public string RoomLabel { get; set; }
		public string CategoryLabel {get;set;}

	}


}

