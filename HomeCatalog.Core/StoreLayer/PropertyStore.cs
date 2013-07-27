using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;

namespace HomeCatalog.Core
{
	public class PropertyStore : IDisposable
	{
		public SQLiteConnection DB { get; private set;}
		public PropertyStore (String aDBpath)
		{
			DB = new SQLiteConnection(aDBpath);
			DB.CreateTable<Property> ();
			DB.CreateTable<Room> ();
			DB.CreateTable<Category> ();
			DB.CreateTable<Item> ();
		}

		public Property Property {
			get {
				if (DB.Table<Property> ().Count () == 0) {
					//Inser
				}
				return null;
			}
		}

		~PropertyStore ()
		{
			Dispose (false);
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			DB.Dispose ();
		}
	}
}

