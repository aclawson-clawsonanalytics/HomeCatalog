using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace HomeCatalog.Core
{
	public class ItemList : SQLList<Item>
	{

		public ItemList (TableQuery<Item> aTable) : base (aTable)
		{

		}
		public override void Remove (Item item)
		{
			foreach (Photo photo in item.PhotoList.AllItems ()) {
				item.PhotoList.Remove (photo);
			}
			base.Remove (item);
		}

//		public ReadOnlyCollection<Item> AllItemsByRoomLabel (bool asc){
//			if (asc) {
//				return (from item in InternalTable orderby 
//			}
//		}
	}
}

