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

		private PhotoList _PhotoList;
		[Ignore]
		public PhotoList PhotoList {
			get {
				if (ID == 0) {
					throw new InvalidOperationException ("You need to add the item to a list first");
				}
				if (_PhotoList == null) {
					_PhotoList = new PhotoList (PropertyStore.CurrentStore.DB.Table<Photo> (), this.ID);
				}
				return _PhotoList;
			}
		}
	}


}

