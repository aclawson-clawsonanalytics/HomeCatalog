using System;

namespace HomeCatalog.Core
{
	public class Item
	{

		public Item ()
		{
			ItemID = GetNewID ();
		}

		static int IDCounter = 0;

		public static int GetNewID() {
			++IDCounter;
			return IDCounter;
		}

//		public string ParentID { get; set; }
		public int ItemID {get;set;}
//
		public string ItemName { get; set; }
//
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

