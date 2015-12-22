using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HomeCatalog.Core
{
	public class PropertyCollection
	{
		private static PropertyCollection instance;
		string _directory;

		public PropertyCollection (string directory)
		{
			_directory = directory;
			RefreshCollection ();
		}

		public static PropertyCollection SharedCollection {
			get {
				if (instance == null) {
					var path = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "properties");
					if (!Directory.Exists (path)) {
						Directory.CreateDirectory (path);
					}
					instance = new PropertyCollection (path);
				}
				return instance;
			}
		}

		public ReadOnlyCollection<PropertyPath> PropertyPaths { 
			get;
			private set;
		}

		public ReadOnlyCollection<PropertyPath> PropertyPathsByName ()
		{ 
			return (from path in PropertyPaths
			        orderby path.Name
			        select path).ToList ().AsReadOnly ();
		}

		public void RefreshCollection ()
		{
			var files = Directory.EnumerateDirectories (_directory);
			List<PropertyPath> paths = new List<PropertyPath> ();
			foreach (var file in files) {
				try {
					PropertyPath path = new PropertyPath (file);
					paths.Add (path);
				}
				catch (FileLoadException e){
					Console.WriteLine ("ERROR opening file! " + e.Message);
				}
			}
			PropertyPaths = paths.AsReadOnly ();
		}

		public PropertyStore NewPropertyStore ()
		{
			PropertyStore s = PropertyStore.NewPropertyStoreInDirectory (_directory);
			RefreshCollection ();
			return s;
		}

		public void RemovePropertyStoreWithID (string id)
		{
			PropertyPath p = FindPathWithID (id);
			Directory.Delete (p.BasePath, recursive:true);
			RefreshCollection ();
		}

		public PropertyPath FindPathWithID (string SearchId)
		{
			foreach (PropertyPath prop in PropertyPaths) {
				if (prop.ID == SearchId) {
					return prop;
				} 
			}
			return null;
		}

		public PropertyStore FindPropertyStoreWithID (string SearchId)
		{
			foreach (PropertyPath prop in PropertyPaths) {
				if (prop.ID == SearchId) {
					return new PropertyStore (prop.BasePath);
				} 
			}
			return null;
		}
	}
}

