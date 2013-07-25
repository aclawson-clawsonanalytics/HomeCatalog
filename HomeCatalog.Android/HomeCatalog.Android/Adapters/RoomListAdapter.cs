using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	class RoomListAdapter : BaseAdapter<Room>
	{
		Room[] Rooms;
		private Property Property {get;set;}
		Activity context;

		public RoomListAdapter(Activity context,Property aProperty) : base() {
			Property = aProperty;
			this.context = context;
			this.Rooms = Property.RoomList.ToArray ();
		}



		public override long GetItemId(int position)
		{
			return position;
		}
		public override Room this[int position] {  
			get { return Rooms[position]; }
		}
		public override int Count {
			get { return Rooms.Length; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.RoomListItem, null);
			view.FindViewById<TextView>(Android.Resource.Id.propertyTextItem).Text = Rooms[position].Label;
			return view;
		}
		public override void NotifyDataSetChanged ()
		{
			Rooms = Property.RoomList.ToArray ();

			base.NotifyDataSetChanged ();
		}

	}
}



