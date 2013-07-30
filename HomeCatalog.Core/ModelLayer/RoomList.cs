using System;
using System.Linq;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HomeCatalog.Core;

namespace HomeCatalog.Core
{
	public class RoomList : SQLList<Room>
	{
		public RoomList (TableQuery<Room> aTable) : base (aTable)
		{

		}

		public ReadOnlyCollection<Room> AllRoomsByLabel (bool ascending) {
			if (ascending) {
				return (from room in InternalTable orderby room.Label select room).ToList ().AsReadOnly ();
			} else {
				return (from room in InternalTable orderby room.Label descending select room).ToList ().AsReadOnly ();
			}
		}
	}
}

