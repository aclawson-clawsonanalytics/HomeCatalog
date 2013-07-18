using System;
using System.Collections.Generic;


namespace HomeCatalog.Core
{
	public class PropertyCollection
	{
		private static PropertyCollection instance;
		private PropertyCollection ()
		{
		}

		public static PropertyCollection SharedCollection
		{
			get {
				if (instance == null) {
					instance = new PropertyCollection();
				}
				return instance;
			}
		}
	
		private List<Property> properties = new List<Property> (); 
		public List<Property> Properties 
		{ 
			get {
				return new List<Property>(properties);
			}
		}

		public void AddProperty (Property property)
		{
			properties.Add (property);
		}

		public void RemoveProperty (Property property)
		{
			properties.Remove (property);
		}

		public static void Reset ()
		{
			instance = null;
		}

		public Property FindPropertyWithId (string SearchId)
		{
			foreach (Property prop in PropertyCollection.SharedCollection.Properties) 
			{
				if (prop.PropertyID == SearchId) {
					return prop;
				} 

			}
			return null;
		}
	}
}

