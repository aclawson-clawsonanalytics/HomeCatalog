using System;
using SQLite;
using System.Linq;
using System.Collections.ObjectModel;

namespace HomeCatalog.Core
{
	public class PhotoList : SQLList<Photo>
	{
		int _OwnerID;

		public PhotoList (TableQuery<Photo> aTable, int ownerItemID) : base (aTable)
		{
			_OwnerID = ownerItemID;
		}

		public override void Add (Photo photo)
		{
			photo.ItemID = _OwnerID;
			base.Add (photo);
		}

		public override ReadOnlyCollection<Photo> AllItems ()
		{
			return (from photo in InternalTable where photo.ItemID == _OwnerID select photo).ToList ().AsReadOnly ();
		}

		public ReadOnlyCollection<Photo> AllItemsByDate ()
		{
			return (from photo in InternalTable where photo.ItemID == _OwnerID orderby photo.DateAdded select photo).ToList ().AsReadOnly ();
		}
	}
}

