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
	class PropertyListAdapter : BaseAdapter<Property>
	{
		Property[] Properties;
		Activity context;
		public PropertyListAdapter(Activity context, Property[] items) : base() {
			this.context = context;
			this.Properties = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Property this[int position] {  
			get { return Properties[position]; }
		}
		public override int Count {
			get { return Properties.Length; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.PropertyViewListItem, null);
			view.FindViewById<TextView>(Android.Resource.Id.propertyTextItem).Text = Properties[position].PropertyName;
			return view;
		}
   }
}

