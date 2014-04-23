using System;
using SQLite;
using System.Collections.Generic;

namespace HomeCatalog.Core
{
	public class Category : ISQLListItem, IValidatable;
	{
		public Category ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID {get;set;}
		public string Label{ get; set;}
	}

	public List<string> GetValidationErrors


}

