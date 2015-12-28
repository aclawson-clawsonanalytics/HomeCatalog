using System;
using SQLite;
using System.Collections.Generic;

namespace HomeCatalog.Core
{
	public class Category : ISQLListItem, IValidatable
	{
		public Category ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID {get;set;}
		public string Label{ get; set;}

		public List<string> GetValidationErrors (){
			List<string> ErrorList = new List<string> ();
			// - Check that the room name is not null or whitespace
			if (String.IsNullOrWhiteSpace(Label)){
				ErrorList.Add("Category must have a label");
			}

//			// - Check that category label is unique
//			foreach (Category category in PropertyStore.CurrentStore.CategoryList){
//				if (Label == category.Label){
//					ErrorList.Add("Category label must be unique");
//				}
//			}

			return ErrorList;
		}
	}




}

