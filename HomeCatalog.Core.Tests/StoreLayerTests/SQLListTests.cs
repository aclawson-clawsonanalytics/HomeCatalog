using System;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using HomeCatalog.Core;
using SQLite;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class SQLListTests
	{
		SQLiteConnection Store;
		string TempDBPath;
		string TempDirectory;
		SQLList<Room> SUT;

		[SetUp()]
		public void Setup ()
		{
			TempDirectory = Path.Combine (Path.GetTempPath (), Path.GetRandomFileName ());
			Directory.CreateDirectory (TempDirectory);
			TempDBPath = Path.Combine (TempDirectory, Path.GetRandomFileName ());
			Store = new SQLiteConnection (TempDBPath);
			Store.CreateTable<Room> ();
			SUT = new SQLList<Room> (Store.Table<Room> ());
		}

		[TearDown()]
		public void Teardown ()
		{
			Store.Dispose ();
			Directory.Delete (TempDirectory, recursive:true);
		}

		[Test()]
		public void ItHasATableQuery ()
		{
			Assert.NotNull (SUT.InternalTable);
		}

		[Test()]
		public void CanAddItem ()
		{
			Room newRoom = new Room ();
			SUT.Add (newRoom);
			Assert.NotNull (SUT.ItemWithID (newRoom.ID));
		}

		[Test()]
		public void CanRemoveItem ()
		{
			Room newRoom = new Room ();
			SUT.Add (newRoom);
			SUT.Remove (newRoom);
			Assert.Null (SUT.ItemWithID (newRoom.ID));
		}

		[Test()]
		public void ListIsPersistent ()
		{
			Room newRoom = new Room ();
			SUT.Add (newRoom);
			newRoom.Label = "ARoom";
			SUT.Save (newRoom);

			Store.Dispose ();

			var newStore = new SQLiteConnection (TempDBPath);
			var newList = new SQLList<Room> (newStore.Table<Room> ());
			Assert.That (newList.AllItems ().First ().Label == "ARoom");
			newStore.Dispose ();
		}

		[Test()]
		public void ThrowsErrorTryingToSaveUninsertedObject ()
		{
			Room newRoom = new Room ();
			newRoom.Label = "ARoom";
			Assert.Throws<ArgumentException> (delegate {
				SUT.Save (newRoom);
			});
		}
	}
}

