using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;

namespace HomeCatalog.Core
{
	public class PropertyStore
	{
		private ISQLiteConnection DB { get; set; }
		String DBPath;
		public PropertyStore (String aDBpath)
		{
			DBPath = aDBpath;
			Initialize ();
		}

		public PropertyStore (ISQLiteConnection db)
		{
			DB = db;
			Initialize ();
		}

		public Property Property {
			get {
				if (DB.Table<Property> ().Count () == 0) {
					//Inser
				}
				return null;
			}
		}

		protected void Initialize () {
			DB.CreateTable<Property> ();
			DB.CreateTable<Room> ();
			DB.CreateTable<Category> ();
			DB.CreateTable<Item> ();
		}
	}
}

