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
	[Activity (Label = "ItemsDetailActivity")]			
	public class ItemsDetailActivity : Activity
	{
		private Property Property { get; set; }
		private Item Item {get;set;}

		private TextView itemNameText { get; set; }
		private TextView purchaseDateText { get; set; }
		private TextView purchaseValueText { get; set; }
		private TextView appraisalDateText { get; set; }
		private TextView appraisalValueText { get; set; }
		private TextView modelNumberText { get; set; }
		private TextView serialNumberText { get; set; }


		private Spinner roomLabelSpinner { get; set; }
		private Spinner categoryLabelSpinner { get; set; }
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
		}
	}
}

