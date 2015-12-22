using System;
using System.IO;
using SQLite;

namespace HomeCatalog.Core
{
	public class PropertyPath
	{
		public PropertyPath (string path)
		{
			BasePath = path;
			DataPath = System.IO.Path.Combine (path, "data.sqlite");
			if (!File.Exists (DataPath)) {
				throw new FileLoadException ("No data at path" + DataPath);
			}
		}

		public string ID { 
			get {
				string id = null;
				SQLiteConnection conn = new SQLiteConnection (DataPath);
				try {
					Property property = conn.Table<Property> ().FirstOrDefault ();
					if (property != null) {
						id = property.PropertyID;
					}
				} catch {

				} finally {
					conn.Dispose ();
				}
				return id;
			}
		}

		public string Name {
			get {
				string name = null;
				SQLiteConnection conn = new SQLiteConnection (DataPath);
				try {
					Property property = conn.Table<Property> ().FirstOrDefault ();
					if (property != null) {
						name = property.PropertyName;
					}
				} catch {

				} finally {
					conn.Dispose ();
				}
				return name;
			}
		}

		public string BasePath { get; private set; }

		public string DataPath { get; private set; }
	}
}

