using System;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using HomeCatalog.Core;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class ItemListTests
	{
		PropertyStore Store;
		string TempDBPath;
		string TempDirectory;

		[SetUp()]
		public void Setup ()
		{
			TempDirectory = Path.Combine (Path.GetTempPath (), Path.GetRandomFileName ());
			Directory.CreateDirectory (TempDirectory);
			TempDBPath = Path.Combine (TempDirectory, Path.GetRandomFileName ());
			Store = new PropertyStore (TempDBPath);
			PropertyStore.CurrentStore = Store;
		}

		[TearDown()]
		public void Teardown ()
		{
			PropertyStore.CurrentStore = null;
			Store.Dispose ();
			File.Delete (TempDBPath);
			Directory.Delete (TempDirectory);
		}

		[Test()]
		public void RemovingAnItemRemovesRelatedPhotos ()
		{
			Item item1 = new Item ();
			Store.Property.ItemList.Add (item1);
			Photo photo1 = new Photo ();
			item1.PhotoList.Add (photo1);
			Photo photo2 = new Photo ();
			item1.PhotoList.Add (photo2);
			Store.Property.ItemList.Remove (item1);
			Assert.That (Store.DB.Table<Photo> ().Count () == 0);
		}
	}
}

