using System;
using SQLite;

namespace HomeCatalog.Core
{
	public class CategoryList : SQLList<Category>
	{
		public CategoryList (TableQuery<Category> aTable) : base (aTable)
		{

		}
	}
}

