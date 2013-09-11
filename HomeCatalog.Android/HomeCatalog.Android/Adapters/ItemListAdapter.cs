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
	class ItemListAdapter : BaseAdapter<Item>
	{
		IList<Item> items;
		private Property Property {get;set;}
		Activity context;

		public ItemListAdapter(Activity context,Property aProperty) : base() {
			Property = aProperty;
			this.context = context;
			this.items = Property.ItemList.AllItems ();
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
			view.FindViewById<TextView> (Resource.Id.itemListText).Text = items[position].ItemName;
			return view;
		}
		public override void NotifyDataSetChanged ()
		{
			items = Property.ItemList.AllItems ();

			base.NotifyDataSetChanged ();
		}

	}
}



