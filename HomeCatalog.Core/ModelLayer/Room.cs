using System;
using System.Collections.Generic;
using SQLite;
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
				ValidationErrors.Add ("Room must have a Label assigned to it.");
			}
			foreach (Room room in PropertyStore.CurrentStore) {
				if (Label == room.Label && ID != room.ID) {
					ValidationErrors.Add("Room is not unique");
				}
			}
			return null;

		}
	}
		

}

