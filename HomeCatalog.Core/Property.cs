using System;

namespace HomeCatalog.Core
{
	public class Property
	{
		public Property ()
		{
			PropertyID = Guid.NewGuid ().ToString ();
		}

		public string PropertyID { get; private set; }
		public string PropertyName { get; set;}
	}
}

