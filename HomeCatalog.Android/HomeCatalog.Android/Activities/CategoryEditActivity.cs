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
	[Activity (Label = "RoomEditActivity")]			
	public class CategoryEditActivity : Activity
	{
		private Property Property { get; set; }
		private int categoryID { get; set; }
		private Category category { get; set; }

		private EditText categoryLabelField { get; set; }

		private TextView labelTest { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			int categoryID = Intent.GetIntExtra ("catID",0);
			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.CategoriesView);

			categoryLabelField = FindViewById<EditText> (Resource.Id.roomField);
			//room = Property.RoomList.RoomWithID (roomID);
			categoryLabelField.Text = Property.CategoryList.CategoryByID (categoryID).Label;

			labelTest = FindViewById<TextView> (Resource.Id.labelTest);

			Button saveButton = FindViewById<Button> (Resource.Id.saveRoomLabelButton);
			saveButton.Click += (sender, e) => 
			{
				//Property.RoomList.RoomWithID (roomID).Label = roomLabelField.Text;
				//Finish ();
				category = Property.CategoryList.CategoryByID (categoryID);
				category.Label = categoryLabelField.Text;
				labelTest.Text = category.Label;
				Finish ();
			};

			Button deleteButton = FindViewById<Button> (Resource.Id.deleteRoomButton);
			deleteButton.Click += (sender, e) => 
			{
				Property.CategoryList.Remove (Property.CategoryList.CategoryByID (categoryID));
				Finish ();
			};


			Button cancelButton = FindViewById<Button> (Resource.Id.cancelRoomEditButton);
			cancelButton.Click += (sender, e) => 
			{
				Finish ();
			};



		}


	}
}

