using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;

namespace HomeCatalog.Core
{
	public class PropertyStore : IDisposable
	{
		Property _property;

		public SQLiteConnection DB { get; private set; }

		public PropertyStore (String aDBpath)
		{
			DB = new SQLiteConnection (aDBpath);
			DB.CreateTable<Property> ();
			DB.CreateTable<Room> ();
			DB.CreateTable<Category> ();
			DB.CreateTable<Item> ();
		}

		public Property Property {
			get {
				if (_property == null) {
					if (DB.Table<Property> ().Count () == 0) {
						_property = new Property ();
						DB.Insert (_property);
					} else {
						_property = DB.Table<Property> ().First ();
					}
					AddListsToProperty ();
				}
				return _property;
			}
		}

		void AddListsToProperty ()
		{
			Property.RoomList = new RoomList (DB.Table<Room> ());
		}

		public void SaveProperty () {
			DB.Update (Property);
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

