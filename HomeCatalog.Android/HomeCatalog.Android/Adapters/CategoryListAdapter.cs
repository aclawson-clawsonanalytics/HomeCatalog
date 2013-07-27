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
	class CategoryListAdapter : BaseAdapter<Category>
	{
		Category[] Categories;
		private Property Property {get;set;}
		Activity context;

		public CategoryListAdapter(Activity context,Property aProperty) : base() {
			Property = aProperty;
			this.context = context;
			this.Categories = Property.CategoryList.ToArray ();
		}



		public override long GetItemId(int position)
		{
			return position;
		}
		public override Category this[int position] {  
			get { return Categories[position]; }
		}
		public override int Count {
			get { return Categories.Length; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.CategoryListItem, null);
			view.FindViewById<TextView> (Android.Resource.Id.categoryTextItem).Text = Categories[position].Label;
			return view;
		}
		public override void NotifyDataSetChanged ()
		{
			Categories = Property.CategoryList.ToArray ();

			base.NotifyDataSetChanged ();
		}

	}
}



