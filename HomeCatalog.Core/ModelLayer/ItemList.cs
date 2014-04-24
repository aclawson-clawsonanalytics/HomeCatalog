using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HomeCatalog.Android;
using HomeCatalog.Core;

namespace HomeCatalog.Core
{
	public class ItemList : SQLList<Item>
	{
		Property Property { get; set; }
		public ItemList (TableQuery<Item> aTable) : base (aTable)
		{

		}
		public override void Remove (Item item)
		{
			foreach (Photo photo in item.PhotoList.AllItems ()) {
				item.PhotoList.Remove (photo);
			}
			base.Remove (item);
		}

//		public ReadOnlyCollection<Item> AllItemsByRoomLabel (bool asc){
//			if (asc) {
//				Property property = PropertyStore.CurrentStore.Property;
//
//				//return (from item in InternalTable orderby property.RoomList select item).ToList ().AsReadOnly();
//
//			} else{
//				return (from item in InternalTable orderby item.RoomID descending select item).ToList ().AsReadOnly();
//			}
//		}

		public override void Save(Item anItem){
			List<string> ValidationErrors = new List<string> ();

			if (anItem.GetValidationErrors != null) {
				ValidationErrors.AddRange (anItem.GetValidationErrors ());
			}

			foreach (Item item in AllItems ()) {
				if (anItem.ItemName == item.ItemName && anItem.ID != item.ID) {
					ValidationErrors.Add ("Item is not unique");
				}
			}

			if (ValidationErrors.Count () > 0) {
				throw new InvalidObjectException ("Invalid item", ValidationErrors)
			} else{
				base.Save (anItem);
			}

		}
	}
}

