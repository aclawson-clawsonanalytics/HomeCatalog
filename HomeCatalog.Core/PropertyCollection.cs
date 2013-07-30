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

		public static PropertyCollection SharedCollection
		{
			get {
				if (instance == null) {
					instance = new PropertyCollection(Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments));
				}
				return instance;
			}
		}
	
		public ReadOnlyCollection<PropertyPath> PropertyPaths { 
			get;
			private set;
		}

		public ReadOnlyCollection<PropertyPath> PropertyPathsByName () { 
			return (from path in PropertyPaths orderby path.Name select path).ToList().AsReadOnly();
		}

		public void RefreshCollection () {
			var files = Directory.EnumerateFiles(_directory);
			List<PropertyPath> paths = new List<PropertyPath> ();
			foreach (var file in files) {
				PropertyPath path = new PropertyPath (file);
				paths.Add (path);
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
			PropertyPath p = FindPathWithID(id);
			File.Delete (p.Path);
			RefreshCollection ();
		}

		public PropertyPath FindPathWithID (string SearchId)
		{
			foreach (PropertyPath prop in PropertyPaths) 
			{
				if (prop.ID == SearchId) {
					return prop;
				} 
			}
			return null;
		}

		public PropertyStore FindPropertyStoreWithID (string SearchId)
		{
			foreach (PropertyPath prop in PropertyPaths) 
			{
				if (prop.ID == SearchId) {
					return new PropertyStore(prop.Path);
				} 
			}
			return null;
		}
	}
}

