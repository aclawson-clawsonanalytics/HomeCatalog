using NUnit.Framework;
using System;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class PropertyCollectionTests
	{

		private PropertyCollection sut;
		private Property property;

		[SetUp()]
		public void SetUp ()
		{
			PropertyCollection.Reset ();
			sut = PropertyCollection.SharedCollection;
			property = new Property ();
		}
		[Test()]
		public void CanAddProperty ()
		{

			sut.AddProperty (property);
			Assert.That (sut.Properties.Contains (property));
		}

		[Test()]
		public void CanRemoveProperty()
		{
			sut.AddProperty (property);
			sut.RemoveProperty (property);
			Assert.That (sut.Properties.Contains (property) == false);
		}

		[Test()]
		public void PropertiesReturnsACopiedList()
		{
			sut.AddProperty (property);
			sut.Properties.Remove (property);
			Assert.That (sut.Properties.Contains (property));
		}

		[Test()]
		public void CollectionReturnsSharedInstance()
		{
			Assert.NotNull (sut);
		}

		[Test()]
		public void CollectionHasOneInstance()
		{
			Assert.That (sut == PropertyCollection.SharedCollection);
		}

		[Test()]
		public void CollectionResets ()
		{
			PropertyCollection.Reset ();
			Assert.That (sut != PropertyCollection.SharedCollection);
		}

		[Test ()]
		public void CanFindPropertyById ()
		{
			Property property2 = new Property ();
			Property property3 = new Property ();
			Property property4 = new Property ();
			string ID1 = property.PropertyID;
			string ID2 = property2.PropertyID;
			string ID3 = property3.PropertyID;
			string ID4 = property4.PropertyID;
			sut.AddProperty (property);
			sut.AddProperty (property2);
			sut.AddProperty (property3);
			sut.AddProperty (property4);

			Assert.That (property3 == sut.FindPropertyWithId(ID3));
		}

		[Test()]
		public void FindPropertyReturnsNullWithNonExistentId()
		{
			string FalseId = "1";
			Assert.That (sut.FindPropertyWithId (FalseId) == null);
		}


	}
}

