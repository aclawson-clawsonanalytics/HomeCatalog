using System;
using SQLite;

namespace HomeCatalog.Core
{
	public class PropertyDatabaseConnection : SQLiteConnection, ISQLiteConnection
	{
		public PropertyDatabaseConnection (string path) : base (path) { }
	}
}