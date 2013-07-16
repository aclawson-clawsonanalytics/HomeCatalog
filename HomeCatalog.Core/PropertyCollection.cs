using System;
using System.Collections.Generic;


namespace HomeCatalog.Core
{
	public class PropertyCollection
	{
		public PropertyCollection ()
		{
		}

		private List<Property> properties = new List<Property> (); 
		public List<Property> Properties { 
			get{
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






	}
}

