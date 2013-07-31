using System;
using SQLite;

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
	}
}

