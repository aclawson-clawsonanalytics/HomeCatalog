using System;
using System.Collections.Generic;
using System.Collections;

namespace HomeCatalog.Core
{
	public class Property
	{
		public Property ()
		{
			PropertyID = Guid.NewGuid ().ToString ();
		}

		// Define Metadata elements
		public string PropertyID { get; private set; }
		public string PropertyName { get; set; }
		public string Address { get; set; }
		public string City {get;set;}
		public string State { get; set; }
		public string ZipCode {get;set;}

	}
}

