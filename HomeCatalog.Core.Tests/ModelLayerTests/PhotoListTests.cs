using System;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using HomeCatalog.Core;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class PhotoListTests
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
		public void AddAssignsOwnerID ()
		{
			Item item1 = new Item ();
			Store.Property.ItemList.Add (item1);
			Photo photo1 = new Photo ();
			item1.PhotoList.Add (photo1);
			Assert.That (photo1.ItemID == item1.ID);
		}

		[Test()]
		public void RetrievesPhotosOnlyFromOwner ()
		{
			Item item1 = new Item ();
			Item item2 = new Item ();
			Store.Property.ItemList.Add (item1);
			Store.Property.ItemList.Add (item2);
			Photo photo1 = new Photo ();
			item1.PhotoList.Add (photo1);
			Photo photo2 = new Photo ();
			item2.PhotoList.Add (photo2);
			Assert.That (item2.PhotoList.AllItems () [0].ID == photo2.ID);
		}
	}
}

