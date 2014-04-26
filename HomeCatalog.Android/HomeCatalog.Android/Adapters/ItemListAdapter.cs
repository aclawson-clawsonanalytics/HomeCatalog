using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HomeCatalog.Core;
using HomeCatalog.Android;

namespace HomeCatalog.Android
{
	class ItemListAdapter : BaseAdapter<Item>
	{
		IList<Item> items;
		private Property Property {get;set;}
		Activity context;
		int SORT_FLAG;

		public ItemListAdapter(Activity context,Property aProperty,int sortFlag) : base() {
			Property = aProperty;
			SORT_FLAG = sortFlag;
			this.context = context;
			//this.items = Property.ItemList.AllItems();
			if (SORT_FLAG == 0) {
				this.items = Property.ItemList.AllItems ();
			}else if (SORT_FLAG == 1) {
				this.items = OrderItemsByRoomLabel ();
			} else if (SORT_FLAG == 2) {
				this.items = OrderItemsByCategoryLabel ();
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}
		public override Item this[int position] {  
			get { return items[position]; }
		}
		public override int Count {
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Resource.Layout.ItemListItem, null);
			view.FindViewById<TextView> (Resource.Id.itemListText).Text = items[position].ItemName + " ID:" + items[position].ID;
			return view;
		}

		public override void NotifyDataSetChanged ()
		{
			items = Property.ItemList.AllItems ();

			base.NotifyDataSetChanged ();
		}

		public ReadOnlyCollection<Item> OrderItemsByRoomLabel (){
			items = Property.ItemList.AllItems ();

			IList<Room> sortedRooms= Property.RoomList.AllRoomsByLabel (true);
			IList<Item> sortedItems = new List<Item>();

			// Add rooms that have "None" as the label
			foreach (Item item in items) {
				if (item.RoomID == 0) {
					sortedItems.Add (item);
				}
			}

			foreach (Room room in sortedRooms){

				foreach (Item item in items) {
					
					if (item.RoomID == room.ID) {
						sortedItems.Add (item);
					}
				}

			}
			ReadOnlyCollection<Item> SortedReadOnly = new ReadOnlyCollection<Item> (sortedItems);
			return (SortedReadOnly);
		}

		public ReadOnlyCollection<Item> OrderItemsByCategoryLabel (){

			items = Property.ItemList.AllItems ();

			IList<Category> sortedCategories= Property.CategoryList.AllCategoriesByLabel (true);
			IList<Item> sortedItems = new List<Item>();

			// Add rooms that have "None" as the label
			foreach (Item item in items) {
				if (item.CategoryID == 0) {
					sortedItems.Add (item);
				}
			}

			// Look through all other rooms
			foreach (Category cat in sortedCategories){
				foreach (Item item in items) {
					if (item.CategoryID == cat.ID) {
						sortedItems.Add (item);
					}
				}
			}
			ReadOnlyCollection<Item> SortedReadOnly = new ReadOnlyCollection<Item> (sortedItems);
			return (SortedReadOnly);
		}
	}
}



