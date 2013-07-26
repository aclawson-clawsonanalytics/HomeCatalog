using System;

namespace HomeCatalog.Core
{
	public class PropertyStore
	{
		private PropertyDatabase DB { get; set; }
		String DBPath;
		public PropertyStore (String aDBpath)
		{
			DBPath = aDBpath;
			Initialize ();
		}
		public PropertyStore (PropertyDatabase db)
		{
			Initialize ();
		}
		protected void Initialize () {

		}
	}
}

