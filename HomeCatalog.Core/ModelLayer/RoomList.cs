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

		public Room RoomWithName (string label)
		{
			return InternalTable.FirstOrDefault (room => room.Label == label);
		}

		public Room RoomWithID (int ID)
		{
			return InternalTable.FirstOrDefault (room => room.ID == ID);
		}

		public override void Save (Room aRoom){
			List<string> ValidationErrors = new List<string> ();
			if (aRoom.GetValidationErrors != null) {
				ValidationErrors.AddRange (aRoom.GetValidationErrors);
			}
			foreach (Room room in AllRoomsByLabel()) {
				// - Test that room label doesn't match any in the RoomList and that it is not testing itself
				// - since a room must be added to a list before persisting.
				if (aRoom.Label == room.Label && aRoom.ID != room.ID) {
					ValidationErrors.Add("Room is not unique");
				}
			}

			// - Check count of Validation Errors
			if (ValidationErrors.Count > 0) {
				throw new InvalidObjectException ("Invalid Room", ValidationErrors);
			}
		}
	}
}

