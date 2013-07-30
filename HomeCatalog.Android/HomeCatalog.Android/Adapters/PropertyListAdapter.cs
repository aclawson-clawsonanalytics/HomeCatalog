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
	class PropertyListAdapter : BaseAdapter<PropertyPath>
	{
		IList<PropertyPath> Properties;
		Activity context;
		public PropertyListAdapter(Activity context) : base() {
			this.context = context;
			this.Properties = PropertyCollection.SharedCollection.PropertyPathsByName ();
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override PropertyPath this[int position] {  
			get { return Properties[position]; }
		}
		public override int Count {
			get { return Properties.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.PropertyViewListItem, null);
			view.FindViewById<TextView>(Android.Resource.Id.propertyTextItem).Text = Properties[position].Name;
			return view;
		}
		public override void NotifyDataSetChanged ()
		{
			this.Properties = PropertyCollection.SharedCollection.PropertyPathsByName ();

			base.NotifyDataSetChanged ();
		}
		 
   }
}

