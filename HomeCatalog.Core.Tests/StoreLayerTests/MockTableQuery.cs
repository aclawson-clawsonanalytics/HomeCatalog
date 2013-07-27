using System;
using System.Collections.Generic;
using SQLite;

namespace HomeCatalog.Core.Tests
{
	public class MockTableQuery<T> : TableQuery<T>
	{

		public MockTableQuery (SQLiteConnection conn, TableMapping mapping) : base (conn, mapping)
		{
			MockList = new List<T> ();
		}
		public List<T> MockList {
			get;
			set;
		}
		public new IEnumerator<T> GetEnumerator ()
		{
			return MockList.GetEnumerator ();
		}
	}
}

