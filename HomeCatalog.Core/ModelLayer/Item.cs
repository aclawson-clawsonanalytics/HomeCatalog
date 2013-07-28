using System;

namespace HomeCatalog.Core
{
	public class Item
	{

		public Item ()
		{
			ItemID = Guid.NewGuid ().ToString ();
		}

		public string ParentID { get; set; }
		public string ItemID {get;set;}

		public string ItemName { get; set; }

		public DateTime PurchaseDate { get; set; }
		public float PurchaseValue { get; set; }
		public float AppraisalValue {get;set;}
		public DateTime AppraisalDate {get;set;}
		public string ModelNumber { get; set; }
		public string SerialNumber {get;set;}

		public string RoomLabel { get; set; }
		public string CategoryLabel {get;set;}

	}
}

