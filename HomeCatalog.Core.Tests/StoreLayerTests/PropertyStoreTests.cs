using System;
using NUnit.Framework;
using HomeCatalog.Core;
using System.Collections.Generic;
using NSubstitute;
using SQLite;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class PropertyStoreTests
	{
		ISQLiteConnection MockDB;
		PropertyStore SUT;
		[SetUp()]
		public void Setup()
		{
			MockDB = Substitute.For<ISQLiteConnection> ();
			SUT = new PropertyStore (MockDB);
		}
		[Test()]
		public void ItTriesToCreatePropertyTable ()
		{
			MockDB.Received ().CreateTable<Property> ();
		}

		[Test()]
		public void ItTriesToCreateRoomsTable ()
		{
			MockDB.Received ().CreateTable<Room> ();
		}

		[Test()]
		public void ItTriesToCreateCategoryTable ()
		{
			MockDB.Received ().CreateTable<Category> ();
		}

		[Test()]
		public void ItTriesToCreateItemTable ()
		{
			MockDB.Received ().CreateTable<Item> ();
		}
		[Test()]
		public void ItReturnsANewPropertyFromBlankTable ()
		{
			MockDB.Table<Property> ().Returns(new MockTableQuery<Property> (null, null));
			Assert.NotNull(SUT.Property);
		}
	}
}

