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
	class CategorySpinnerAdapter : BaseAdapter<Category>
	{
		IList<Category> Categories;
		private Property Property {get;set;}
		Activity context;
		String NoSelectionText;

		public CategorySpinnerAdapter(Activity context,Property aProperty) : this (context,aProperty,"No Category")
		{

		}

		public CategorySpinnerAdapter(Activity context,Property aProperty,String noSelectionText) : base() {
			Property = aProperty;
			NoSelectionText = noSelectionText;
			this.context = context;
			this.Categories = Property.CategoryList.AllItems ();
		}


		public override long GetItemId(int position)
		{
			return position;
		}
		public override Category this[int position] { 

			get {
				if (position == 0) {
					return null;
				}
				return Categories [position - 1];
			}
		}
		public override int Count {
			get { return Categories.Count+1; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			string text;
			if (position == 0) {
				text = NoSelectionText;
			} else {
				text = Categories [position - 1].Label;
			}
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.CategoryListItem, null);
			view.FindViewById<TextView> (Android.Resource.Id.categoryTextItem).Text = text;
			return view;
		}
		public override void NotifyDataSetChanged ()
		{
			Categories = Property.CategoryList.AllItems ();

			base.NotifyDataSetChanged ();
		}

		public override View GetDropDownView(int position, View convertView, ViewGroup parent)
		{
			string text;
			if (position == 0) {
				text = NoSelectionText;
			} else {
				text = Categories [position - 1].Label;
			}
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.CategoryListItem, null);
			view.FindViewById<TextView> (Android.Resource.Id.categoryTextItem).Text = text;
			return view;
		}


	}
}



