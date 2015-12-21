using NUnit.Framework;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class PropertyCollectionTests
	{

		PropertyCollection SUT;
		string TempDirectory;

		[SetUp()]
		public void SetUp ()
		{
			TempDirectory = Path.Combine (Path.GetTempPath (), Path.GetRandomFileName ());
			Directory.CreateDirectory (TempDirectory);
			SUT = new PropertyCollection (TempDirectory);
		}

		[TearDown()]
		public void Teardown ()
		{
			Directory.Delete (TempDirectory, true);
		}

		[Test()]
		public void AddPropertyStoreUpdatesPaths ()
		{
			SUT.NewPropertyStore ();
			Assert.That (SUT.PropertyPaths.Count == 1);
		}

		[Test()]
		public void RemovePropertyStoreUpdatesPaths ()
		{
			var id = SUT.NewPropertyStore ().Property.PropertyID;
			SUT.RemovePropertyStoreWithID (id);
			Assert.That (SUT.PropertyPaths.Count == 0);
		}

		[Test()]
		public void CollectionReturnsSharedInstance ()
		{
			Assert.NotNull (PropertyCollection.SharedCollection);
		}

		[Test()]
		public void CollectionHasOneInstance ()
		{
			Assert.That (PropertyCollection.SharedCollection == PropertyCollection.SharedCollection);
		}

		[Test ()]
		public void CanFindPropertyPathById ()
		{
			Property property1 = SUT.NewPropertyStore ().Property;
			Property property2 = SUT.NewPropertyStore ().Property;
			Property property3 = SUT.NewPropertyStore ().Property;
			Property property4 = SUT.NewPropertyStore ().Property;
			string ID1 = property1.PropertyID;
			string ID2 = property2.PropertyID;
			string ID3 = property3.PropertyID;
			string ID4 = property4.PropertyID;

			Assert.That (property3.PropertyID == SUT.FindPathWithID (ID3).ID);
		}

		[Test ()]
		public void PropertyPathsByNameReturnsOrderedList ()
		{
			PropertyStore store1 = SUT.NewPropertyStore ();
			PropertyStore store2 = SUT.NewPropertyStore ();
			PropertyStore store3 = SUT.NewPropertyStore ();
			PropertyStore store4 = SUT.NewPropertyStore ();
			store1.Property.PropertyName = "C";
			store2.Property.PropertyName = "A";
			store3.Property.PropertyName = "B";
			store4.Property.PropertyName = "D";
			store1.SaveProperty ();
			store2.SaveProperty ();
			store3.SaveProperty ();
			store4.SaveProperty ();

			Assert.That (SUT.PropertyPathsByName ()[1].Name == "B");
		}

		[Test ()]
		public void CanFindPropertyStoreById ()
		{
			Property property1 = SUT.NewPropertyStore ().Property;
			Property property2 = SUT.NewPropertyStore ().Property;
			Property property3 = SUT.NewPropertyStore ().Property;
			Property property4 = SUT.NewPropertyStore ().Property;
			string ID1 = property1.PropertyID;
			string ID2 = property2.PropertyID;
			string ID3 = property3.PropertyID;
			string ID4 = property4.PropertyID;
				
			Assert.That (property3.PropertyID == SUT.FindPropertyStoreWithID (ID3).Property.PropertyID);
		}

		[Test()]
		public void FindPropertyReturnsNullWithNonExistentId ()
		{
			Property property1 = SUT.NewPropertyStore ().Property;
			Property property2 = SUT.NewPropertyStore ().Property;
			string FalseId = "1";
			Assert.That (SUT.FindPropertyStoreWithID (FalseId) == null);
		}

		[Test()]
		public void FindPathReturnsNullWithNonExistentId ()
		{
			Property property1 = SUT.NewPropertyStore ().Property;
			Property property2 = SUT.NewPropertyStore ().Property;
			string FalseId = "1";
			Assert.That (SUT.FindPathWithID (FalseId) == null);
		}
	}
}

