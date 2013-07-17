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


	}
}

