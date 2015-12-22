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
	[Activity (Label = "View Catiegories")]
	public class ViewCategoryListActivity : StandardActivity
	{
		private CategoryListAdapter ListAdapter { get; set; }
		private Property Property { get; set; }
		private string categoryIDText { get; set; }
		private string categoryLabel { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.CategoriesView);

			ListView listView = FindViewById<ListView> (Resource.Id.categoryList);
			ListAdapter = new CategoryListAdapter (this,Property);
			listView.Adapter = ListAdapter;
			listView.ItemClick += (Object sender, AdapterView.ItemClickEventArgs e) =>
			{
				//roomLabel = ListAdapter[e.Position].Label;
				Intent CategoryEdit = new Intent(this,typeof(CategoryEditActivity));
				CategoryEdit.PutExtra ("catID",ListAdapter[e.Position].ID);
				StartActivityForResult (CategoryEdit,0);
			};

//			Button addCategory = FindViewById<Button> (Resource.Id.addCategoryButton);
//			addCategory.Click += (sender, e) => 
//			{
////				Intent PassPropertyID = new Intent (this,typeof(EditRoomsActivity));
////				PassPropertyID.PutExtra (Property.PropertyIDKey,Property.PropertyID);
////				StartActivity (PassPropertyID);
//			};


			Button AddCategoryButton = FindViewById<Button> (Resource.Id.addCategoryButton);
			AddCategoryButton.Click += (sender,e) =>
			{
				var transaction = FragmentManager.BeginTransaction();
				CategoryListDialogFragment catDialog = new CategoryListDialogFragment ();
				catDialog.Show(transaction,"catalogListDialog");

				catDialog.OnItemSelected += (DialogClickEventArgs a) =>
				{
					switch (a.Which)
					{
						case 0:
					{
						Category newCat = new Category();
						newCat.Label = "Appliance";
						Property.CategoryList.Add (newCat);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 1:
					{
						Category newCat = new Category();
						newCat.Label = "Bathroom Appliance";
						Property.CategoryList.Add (newCat);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 2:
					{
						Category newCat = new Category();
						newCat.Label = "Collection";
						Property.CategoryList.Add (newCat);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 3:
					{
						Category newCat = new Category ();
						newCat.Label = "Electronics";
						Property.CategoryList.Add (newCat);
						ListAdapter.NotifyDataSetChanged();
						break;
					}
						case 4:
					{
						Category newCat = new Category ();
						newCat.Label = "Furniture";
						Property.CategoryList.Add (newCat);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 5:
					{
						Category newCat = new Category ();
						newCat.Label = "Hobby";
						Property.CategoryList.Add (newCat);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 6:
					{
						Category newCat = new Category ();
						newCat.Label = "Kitchen Appliance";
						Property.CategoryList.Add (newCat);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 7:
					{
						Category newCat = new Category ();
						newCat.Label = "Storage";
						Property.CategoryList.Add (newCat);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 8:
					{
						Category newCat = new Category ();
						newCat.Label = "Tools";
						Property.CategoryList.Add (newCat);
						ListAdapter.NotifyDataSetChanged ();
						break;
					}
						case 9:
					{
						// Add Code to go to the Edit Room View for custom room
						Category newCat = new Category ();
						newCat.Label = "Custom";

						Property.CategoryList.Add (newCat);
						Property.CategoryList.Save (newCat);

						Intent createCustomCategory = new Intent(this,typeof(CategoryEditActivity));
						createCustomCategory.PutExtra ("catID",newCat.ID);
						StartActivityForResult (createCustomCategory,0);
						break;
					}
					}
			};
			
//			listView.ItemClick += (Object sender, AdapterView.ItemClickEventArgs e) =>
//			{
//				var transaction = FragmentManager.BeginTransaction ();
//				DeleteDialogFragment deleteDialog = new DeleteDialogFragment ();
//				deleteDialog.Show (transaction, "deleteDialog");
//
//				deleteDialog.OnItemSelected += (DialogClickEventArgs a) =>
//				{
//					Property.RoomList.Remove (ListAdapter [e.Position]);
//					ListAdapter.NotifyDataSetChanged ();
//				};
//			};

			};

			// listView ItemClick event
			// Clicking on an existing room takes the user to a roomEditActivity
//			listView.ItemClick += (Object sender, AdapterView.ItemClickEventArgs e) =>
//			{
//
//
//				//				var PropertyDetails = new Intent (this,typeof(PropertyDetailActivity));
//				//				PropertyStore store = PropertyCollection.SharedCollection.FindPropertyStoreWithID (ListAdapter[e.Position].ID);
//				//				PropertyStore.CurrentStore = store;
//				//				StartActivity (PropertyDetails);
//			};
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			if (requestCode == 0)
			{
				ListAdapter.NotifyDataSetChanged ();
			}
		}


//		private void AddBathroom()
//		{
//			if (RoomLabelIsTaken("Bathroom") == false)
//			{
//				Room newRoom = new Room ();
//				newRoom.Label = "Bathroom";
//				Property.RoomList.Add (newRoom);
//			}
//			else
//			{
//				int bathCount = 2;
//				string bathString = "Bathroom" + bathCount.ToString ();
//				while (RoomLabelIsTaken (bathString) == false
//			}
//		}


//		private bool CategoryLabelIsTaken(string label)
//		{
//
//			foreach (Room rm in Property.RoomList)
//			{
//				if (rm.Label == label)
//				{
//					return true;
//				}
//			}
//			return false;
//		}
		 
	}
}


