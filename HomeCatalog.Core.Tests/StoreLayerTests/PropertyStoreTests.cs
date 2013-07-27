using System;
using NUnit.Framework;
using HomeCatalog.Core;
using System.Collections.Generic;
using NSubstitute;
using SQLite;
using System.IO;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class PropertyStoreTests
	{
		PropertyStore SUT;
		string TempDBPath;
		string TempDirectory;

		[SetUp()]
		public void Setup ()
		{
			TempDirectory = Path.Combine (Path.GetTempPath (), Path.GetRandomFileName ());
			Directory.CreateDirectory (TempDirectory);
			TempDBPath = Path.Combine (TempDirectory, Path.GetRandomFileName ());
			SUT = new PropertyStore (TempDBPath);
		}

		[TearDown()]
		public void Teardown ()
		{
			SUT.Dispose ();
			File.Delete (TempDBPath);
			Directory.Delete (TempDirectory);
		}

		[Test()]
		public void ItCreatesAPropertyTable ()
		{
			Assert.That (SUT.DB.GetTableInfo ("Property").Count > 0);
		}

		[Test()]
		public void ItCreatesAnItemTable ()
		{
			Assert.That (SUT.DB.GetTableInfo ("Item").Count > 0);
		}

		[Test()]
		public void ItCreatesRoomTable ()
		{
			Assert.That (SUT.DB.GetTableInfo ("Room").Count > 0);
		}

		[Test()]
		public void ItCreatesCategoryTable ()
		{
			Assert.That (SUT.DB.GetTableInfo ("Category").Count > 0);
		}

		[Test()]
		public void ItReturnsANewProperty ()
		{
			Assert.NotNull (SUT.Property);
		}

		[Test()]
		public void ItPersistsTheProperty ()
		{
			Property firstProperty = SUT.Property;
			Property secondProperty = SUT.Property;
			Assert.That (firstProperty == secondProperty);
		}

		[Test()]
		public void ItStoresANewProperty ()
		{
			Property aProperty = SUT.Property;
			Assert.That (SUT.DB.Table<Property> ().Count () == 1);
		}

		[Test()]
		public void MultipleCallsOnlyStoreOneProperty ()
		{
			Property aProperty = SUT.Property;
			SUT.Dispose ();

			PropertyStore secondStore = new PropertyStore (TempDBPath);
			Property anotherProperty = secondStore.Property;

			Assert.That (secondStore.DB.Table<Property> ().Count () == 1);

			secondStore.Dispose ();
		}

		[Test()]
		public void SavingPersistsPropertyChanges ()
		{
			Property aProperty = SUT.Property;
			aProperty.PropertyName = "SomeName";
			SUT.SaveProperty ();
			SUT.Dispose ();

			PropertyStore secondStore = new PropertyStore (TempDBPath);
			Property anotherProperty = secondStore.Property;

			Assert.That (anotherProperty.PropertyName == "SomeName");

			secondStore.Dispose ();
		}
	}
}

