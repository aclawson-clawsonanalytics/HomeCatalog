using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HomeCatalog.Core;

namespace HomeCatalog.Core
{
	public class CategoryList : SQLList<Category>
	{
		public CategoryList (TableQuery<Category> aTable) : base (aTable)
		{

		}

		public ReadOnlyCollection<Category> AllCategoriesByLabel (bool ascending) {
			if (ascending) {
				return (from cat in InternalTable orderby cat.Label select cat).ToList ().AsReadOnly ();
			} else {
				return (from cat in InternalTable orderby cat.Label descending select cat).ToList ().AsReadOnly ();
			}
		}

		public Category CategoryWithName (string label)
		{
			return InternalTable.FirstOrDefault (cat => cat.Label == label);
		}
	}
}

