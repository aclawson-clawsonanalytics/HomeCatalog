using System;
using SQLite;

namespace HomeCatalog.Core
{
	public class Category : ISQLListItem
	{
		public Category ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID {get;set;}
		public string Label{ get; set;}
	}


}

