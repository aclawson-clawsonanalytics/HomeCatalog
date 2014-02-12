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
	class CsvExporter
	{
		private List<Item> DisplayItems { get; set; }

		public CsvExporter (List<Item> data)
		{
			DisplayItems = data;
		}
	}
}

