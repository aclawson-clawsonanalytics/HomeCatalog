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

		public Category CategoryByID(int ID)
		{
			return InternalTable.FirstOrDefault (category => category.ID == ID);
		}

		public override void Save (Category aCategory){
			List<string> ValidationErrors = new List<string> ();
			if (aCategory.GetValidationErrors () != null) {
				ValidationErrors.AddRange (aCategory.GetValidationErrors ());
			}

			// - Check that category label doesn't match any existing and is not checking itself.
			foreach (Category category in AllCategoriesByLabel (true)) {
				if (aCategory.Label == category.Label && aCategory.ID != category.ID){
					ValidationErrors.Add ("Category is not unique");
				}
			}

			if (ValidationErrors.Count () > 0) {
				throw new InvalidObjectException ("Invalid category", ValidationErrors);
			} else {
				base.Save (aCategory);
			}


		}
	}
}

