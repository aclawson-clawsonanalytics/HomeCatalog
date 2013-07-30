using System;
using SQLite;

namespace HomeCatalog.Core
{
	public class ItemList : SQLList<Item>
	{
		public ItemList (TableQuery<Item> aTable) : base (aTable)
		{

		}
	}
}

