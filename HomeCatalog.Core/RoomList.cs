using System;
using System.Linq;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HomeCatalog.Core;

namespace HomeCatalog.Core
{
	public class RoomList
	{
		public TableQuery<Room> InternalTable { get; private set; }
		public RoomList (TableQuery<Room> aTable)
		{
			InternalTable = aTable;
		}

		public void Add (Room room) {
			InternalTable.Connection.Insert (room);
		}

		public void Remove (Room room) {
			InternalTable.Connection.Delete (room);
		}

		public void Save (Room room) {
			if (InternalTable.FirstOrDefault (x => x.ID == room.ID) == null) {
				throw new System.ArgumentException ("Room must be added first");
			}
			InternalTable.Connection.Update (room);
		}

		public Room RoomWithID (int id) {
			return InternalTable.FirstOrDefault (x => x.ID == id);
		}

		public ReadOnlyCollection<Room> AllRooms () {
			return (from room in InternalTable select room).ToList ().AsReadOnly ();
		}
	}
}

