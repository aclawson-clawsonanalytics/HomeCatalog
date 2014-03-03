using System;
using SQLite;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace HomeCatalog.Core
{
	public class PropertyStore : IDisposable
	{
		Property _property;

		public SQLiteConnection DB { get; private set; }

		public static PropertyStore CurrentStore {
			get;
			set;
		}

		public static PropertyStore NewPropertyStoreInDirectory (String dbDirectory)
		{
			string propertyID = Guid.NewGuid ().ToString ();
			var documentRoot = Path.Combine (dbDirectory, propertyID);
			Directory.CreateDirectory (documentRoot);
			string dbPath = Path.Combine (documentRoot, "data.sqlite");
			// Preload the store with the id so that its intialized only once
			PropertyStore tempStore = new PropertyStore (dbPath);
			tempStore.Property.PropertyID = propertyID;
			tempStore.SaveProperty ();
			tempStore.Dispose ();
			return new PropertyStore (dbPath);
		}

		public PropertyStore (String aDBpath)
		{
			DB = new SQLiteConnection (aDBpath);
			DB.CreateTable<Property> ();
			DB.CreateTable<Room> ();
			DB.CreateTable<Category> ();
			DB.CreateTable<Item> ();
			DB.CreateTable<Photo> ();
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
					_property.Store = this;
				}
				return _property;
			}
		}

		private void AddListsToProperty ()
		{
			Property.RoomList = new RoomList (DB.Table<Room> ());
			Property.CategoryList = new CategoryList (DB.Table<Category> ());
			Property.ItemList = new ItemList (DB.Table<Item> ());
		}

		public void SaveProperty ()
		{
			DB.Update (Property);
		}

		AssetStore _AssetStore;
		public AssetStore Assets {
			get {
				if (_AssetStore == null) {
					var storePath = Path.Combine (Path.GetDirectoryName(DB.DatabasePath), "assets");
					_AssetStore = new AssetStore (storePath);
				}
				return _AssetStore;
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

