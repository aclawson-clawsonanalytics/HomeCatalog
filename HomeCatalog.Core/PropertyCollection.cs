using System;
using System.Collections.Generic;


namespace HomeCatalog.Core
{
	public class PropertyCollection
	{
		public PropertyCollection ()
		{
			Properties = new List<Property> (); 
		}

		public List<Property> Properties { get; private set;}


		public void AddProperty (Property property)
		{
			Properties.Add (property);
		}




	}
}

