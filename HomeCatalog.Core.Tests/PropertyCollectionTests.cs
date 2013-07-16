using NUnit.Framework;
using System;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class PropertyCollectionTests
	{
		[Test()]
		public void CanAddProperty ()
		{
			PropertyCollection sut = new PropertyCollection ();
			Property property = new Property ();
			sut.AddProperty (property);
			Assert.That (sut.Properties.Contains (property));
		}

		[Test()]
		public void CanRemoveProperty()
		{
			PropertyCollection sut = new PropertyCollection ();
			Property property = new Property ();
			sut.AddProperty (property);
			sut.RemoveProperty (property);
			Assert.That (sut.Properties.Contains (property) == false);
		}

		[Test()]
		public void PropertiesReturnsACopiedList()
		{
			PropertyCollection sut = new PropertyCollection ();
			Property property = new Property ();
			sut.AddProperty (property);
			sut.Properties.Remove (property);
			Assert.That (sut.Properties.Contains (property));
		}
	}
}

