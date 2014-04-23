using System;
using System.Linq;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HomeCatalog.Core;

namespace HomeCatalog.Core
{
	public interface ISQLListItem
	{
		int ID { get; set; }
	}

	public class SQLList<T> where T : ISQLListItem, new()
	{
		public TableQuery<T> InternalTable { get; private set; }
		public SQLList (TableQuery<T> aTable)
		{
			InternalTable = aTable;
		}

		public virtual void Add (T item) {
			InternalTable.Connection.Insert (item);
		}

		public virtual void Remove (T item) {
			InternalTable.Connection.Delete (item);
		}

		public virtual void Save (T item) {
			if (InternalTable.FirstOrDefault (x => x.ID == item.ID) == null) {
				throw new System.ArgumentException ("Item must be added first");
			}
			InternalTable.Connection.Update (item);
		}

		public virtual T ItemWithID (int id) {
			return InternalTable.FirstOrDefault (x => x.ID == id);
		}

		public virtual ReadOnlyCollection<T> AllItems () {
			return (from item in InternalTable select item).ToList ().AsReadOnly ();
		}



	}

	public class InvalidObjectException : System.Exception {
		public InvalidObjectException (string message, List<string> validationErrorList) : base (message) {

		}
	}
}

