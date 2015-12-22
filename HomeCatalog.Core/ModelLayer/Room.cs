using System;
using System.Collections.Generic;
using SQLite;
using HomeCatalog.Android;

namespace HomeCatalog.Core
{
	public class Room : ISQLListItem, IValidatable
	{
		public Room ()
		{

		}
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Label { get; set; }

		public List<string> GetValidationErrors (){
			List<string> ValidationErrors = new List<string> ();
			if (Label == null) {
				ValidationErrors.Add ("Room must have a label.");
			}

			if (ValidationErrors.Count > 0) {
				return ValidationErrors;
			} else {
				return null;
			}

		}
	}
		

}

