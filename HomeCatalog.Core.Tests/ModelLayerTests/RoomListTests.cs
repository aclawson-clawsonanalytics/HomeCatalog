using System;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
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
		public void CanListRoomsAlphabetically ()
		{
			Room newRoom1 = new Room ();
			Room newRoom2 = new Room ();
			Room newRoom3 = new Room ();
			newRoom1.Label = "C";
			newRoom2.Label = "A";
			newRoom3.Label = "B";
			SUT.Add (newRoom1);
			SUT.Add (newRoom2);
			SUT.Add (newRoom3);
			IList<Room> rooms = SUT.AllRoomsByLabel (ascending: true);
			Assert.That (rooms [0].Label == "A");
			Assert.That (rooms [1].Label == "B");
			Assert.That (rooms [2].Label == "C");
		}
	}
}

