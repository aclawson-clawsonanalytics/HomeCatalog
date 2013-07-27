using System;
using NUnit.Framework;
using System.IO;
using System.Linq;
using HomeCatalog.Core;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class RoomListTests
	{
		PropertyStore Store;
		string TempDBPath;
		string TempDirectory;

		RoomList SUT;

		[SetUp()]
		public void Setup ()
		{
			TempDirectory = Path.Combine (Path.GetTempPath (), Path.GetRandomFileName ());
			Directory.CreateDirectory (TempDirectory);
			TempDBPath = Path.Combine (TempDirectory, Path.GetRandomFileName ());
			Store = new PropertyStore (TempDBPath);
			SUT = Store.Property.RoomList;
		}

		[TearDown()]
		public void Teardown ()
		{
			Store.Dispose ();
			File.Delete (TempDBPath);
			Directory.Delete (TempDirectory);
		}

		[Test()]
		public void PropertyReturnsARoomList ()
		{
			Assert.NotNull (Store.Property.RoomList);
		}

		[Test()]
		public void ItHasATableQuery ()
		{
			Assert.NotNull (SUT.InternalTable);
		}

		[Test()]
		public void CanAddRoom ()
		{
			Room newRoom = new Room ();
			SUT.Add (newRoom);
			Assert.NotNull (SUT.RoomWithID (newRoom.ID));
		}

		[Test()]
		public void CanRemoveRoom ()
		{
			Room newRoom = new Room ();
			SUT.Add (newRoom);
			SUT.Remove (newRoom);
			Assert.Null (SUT.RoomWithID (newRoom.ID));
		}

		[Test()]
		public void RoomListIsPersistent ()
		{
			Room newRoom = new Room ();
			SUT.Add (newRoom);
			newRoom.Label = "ARoom";
			SUT.Save (newRoom);

			Store.Dispose ();

			PropertyStore newStore = new PropertyStore (TempDBPath);
			Assert.That (newStore.Property.RoomList.AllRooms ().First ().Label == "ARoom");
			newStore.Dispose ();
		}

		[Test()]
		public void ThrowsErrorTryingToSaveUninsertedObject ()
		{
			Room newRoom = new Room ();
			newRoom.Label = "ARoom";
			Assert.Throws<ArgumentException> (delegate { SUT.Save (newRoom); });
		}
	}
}

