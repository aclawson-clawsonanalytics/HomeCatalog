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
	class RoomSpinnerAdapter : BaseAdapter<Room>
	{
		IList<Room> Rooms;

		private Property Property { get; set; }

		Activity Context;

		public RoomSpinnerAdapter (Activity context, Property aProperty) : base()
		{
			Property = aProperty;
			Context = context;
			Rooms = Property.RoomList.AllRoomsByLabel (ascending: true);
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Room this [int position] {  
			get {
				if (position == 0) {
					return null;
				}
				return Rooms [position - 1];
			}
		}

		public override int Count {
			get { return Rooms.Count + 1; }
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			string text;
			if (position == 0) {
				text = "No Room";
			} else {
				text = Rooms [position - 1].Label;
			}

			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = Context.LayoutInflater.Inflate (Android.Resource.Layout.RoomListItem, null);
			view.FindViewById<TextView> (Android.Resource.Id.roomTextItem).Text = text;
			return view;
		}

		//public override void NotifyDataSetChanged ()
		public override void NotifyDataSetChanged ()
		{
			Rooms = Property.RoomList.AllRoomsByLabel (ascending: true);

			base.NotifyDataSetChanged ();
		}

		public override View GetDropDownView (int position, View convertView, ViewGroup parent)
		{
			string text;
			if (position == 0) {
				text = "No Room";
			} else {
				text = Rooms [position - 1].Label;
			}

			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = Context.LayoutInflater.Inflate (Android.Resource.Layout.RoomListItem, null);
			view.FindViewById<TextView> (Android.Resource.Id.roomTextItem).Text = text;
			return view;
		}
	}
}



