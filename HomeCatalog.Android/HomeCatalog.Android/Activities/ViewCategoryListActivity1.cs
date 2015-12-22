using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HomeCatalog.Core;


namespace HomeCatalog.Android
{
	[Activity (Label = "View Categories")]
	public class ViewCategoryListActivity1 : StandardActivity
	{
		private CategoryListAdapter ListAdapter { get; set; }
		private Property Property { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.CategoryView);

			ListView listView = FindViewById<ListView> (Resource.Id.categoryList);
			ListAdapter = new CategoryListAdapter (this,Property);
			listView.Adapter = ListAdapter;

			Button editCategoriesButton = FindViewById<Button> (Resource.Id.editCategoriesButton2);
			editCategoriesButton.Click += (sender, e) => 
			{
				Intent PassPropertyID = new Intent (this,typeof(EditCategoriesActivity));
				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
				StartActivity (PassPropertyID);
			};

			Button backButton = FindViewById<Button> (Resource.Id.BackButton);
			backButton.Click += (sender,e) =>
			{
				SetResult (Result.Canceled);
				Finish ();
			};

			listView.ItemClick += (Object sender, AdapterView.ItemClickEventArgs e) =>
			{
				var transaction = FragmentManager.BeginTransaction ();
				DeleteDialogFragment deleteDialog = new DeleteDialogFragment ();
				deleteDialog.Show (transaction, "deleteDialog");

				deleteDialog.OnItemSelected += (DialogClickEventArgs a) =>
				{
					Property.CategoryList.Remove (ListAdapter [e.Position]);
					ListAdapter.NotifyDataSetChanged ();
				};
			};


		}

	}
}


