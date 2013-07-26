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
	[Activity (Label = "ViewRoomListActivity")]
	public class ViewCategoryListActivity : Activity
	{
		private RoomListAdapter ListAdapter { get; set; }
		private Property Property { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			String propertyID = Intent.GetStringExtra (Property.PropertyIDKey);

			Property = PropertyCollection.SharedCollection.FindPropertyWithId (propertyID);

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
			};

			listView.ItemClick += (sender, e) => 
			{
				Property.CategoryList.Remove (ListAdapter[e.Position]);
				ListAdapter.NotifyDataSetChanged ();
			};


		}

	}
}


