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
	[Activity (Label = "ReportGeneratorActivity")]			
	public class ReportGeneratorActivity : Activity
	{
		private Property Property { get; set; }
		private Spinner propertySpinner { get; set; }
		private Spinner roomLabelSpinner { get; set; }
		private Spinner categoryLabelSpinner { get ; set; }

		const int roomUpdateRequestCode = 1;
		const int categoryUpdateRequestCode = 2;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			Property = PropertyStore.CurrentStore.Property;

			SetContentView (Resource.Layout.ReportGeneratorView);
			roomLabelSpinner = FindViewById<Spinner> (Resource.Id.roomQuerrySpinner);
			categoryLabelSpinner = FindViewById<Spinner> (Resource.Id.categoryQuerrySpinner);


			RoomSpinnerAdapter roomAdapter = new RoomSpinnerAdapter (this, Property);
			roomLabelSpinner.Adapter = roomAdapter;

			CategorySpinnerAdapter categoryAdapter = new CategorySpinnerAdapter (this, Property);
			categoryLabelSpinner.Adapter = categoryAdapter;

		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			if (requestCode == roomUpdateRequestCode)
			{
				((RoomSpinnerAdapter)roomLabelSpinner.Adapter).NotifyDataSetChanged ();
			}
			else if (requestCode == categoryUpdateRequestCode)
			{
				((CategorySpinnerAdapter)categoryLabelSpinner.Adapter).NotifyDataSetChanged ();
			}
		}


	}
}

