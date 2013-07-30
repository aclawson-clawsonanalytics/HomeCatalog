using System;
using System.IO;
using SQLite;

namespace HomeCatalog.Core
{
	public class PropertyPath
	{
		public PropertyPath (string path)
		{
			if (!File.Exists (path)) {
				throw new FileNotFoundException ();
			}
			Path = path;
		}

		public string ID { 
			get {
				string id = null;
				SQLiteConnection conn = new SQLiteConnection (Path);
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
				SQLiteConnection conn = new SQLiteConnection (Path);
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

		public string Path { get; private set; }
	}
}

