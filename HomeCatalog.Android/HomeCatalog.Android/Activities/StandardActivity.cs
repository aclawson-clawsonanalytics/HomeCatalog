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

namespace HomeCatalog.Android
{
	public abstract class StandardActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			this.ActionBar.SetDisplayHomeAsUpEnabled (true);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.Order == 0) {
				SetResult (Result.Canceled);
				Finish ();
			}
			return base.OnOptionsItemSelected (item);
		}
	}
}

