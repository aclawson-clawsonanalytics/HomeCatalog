using System;
using SQLite;
namespace HomeCatalog.Core
{
	public class Room
	{
		public Room ()
		{

		}
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Label { get; set; }
	}


}

