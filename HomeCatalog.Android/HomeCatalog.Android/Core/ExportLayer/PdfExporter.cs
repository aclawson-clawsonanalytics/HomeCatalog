using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;

using Android.Runtime;
using Android.Views;
using Android.Widget;
using HomeCatalog.Core;

namespace HomeCatalog.Android
{
	class PdfExporter
	{
		private IEnumerable<Item> DisplayItems { get; set; }
		private int fileNumberCount { get; set; }

		public PdfExporter (IEnumerable<Item> itemsToDisplay)
		{
			DisplayItems = itemsToDisplay;
		}

	

		public String ConstructOutput (String filepath)
		{
			// First we need to loop over all of our pages and draw their content

								
				// Use the PrintedPdfDocument object to start a page
				PdfDocument.Page page = mPdfDocument.startPage(index);
				Canvas canvas = page.getCanvas();

				// … draw some stuff to your page’s canvas…

				// … you may also want to write some PageRange information here so you can
				// determine all of the ranges you supplied pages for when you go to call
				// onWriteFinished

				mPdfDocument.finishPage(page);


			// Next we need to write out the updated PrintedPdfDocument to the specified
			// destination
			mPdfDocument.writeTo(new FileOutputStream(destination.getFileDescriptor()));


		}

	}



}




