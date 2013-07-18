using NUnit.Framework;
using System;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class PropertyTests
	{
		private Property sut = new Property();

		[SetUp()]
		public void SetUp ()
		{
			Property sut = new Property ();
		}

		[Test()]
		public void PropertyHasName ()
		{
			Assert.That (sut.PropertyName == null);
		}

		[Test()]
		public void PropertyNameIsCorrect()
		{
			sut.PropertyName = "Name";
			Assert.That (sut.PropertyName == "Name");
		}

		[Test]
		public void PropertyHasID()
		{
			Assert.That (sut.PropertyID != null);
		}

		[Test()]
		public void PropertyHasAddress()
		{
			Assert.That (sut.Address != null);
		}

		[Test ()]
		public void PropertyAddressIsCorrect ()
		{
			sut.Address = "19859 E. Garden Pl";
			Assert.That (sut.Address == "19859 E. Garden Pl");
		}

		[Test()]
		public void PropertyHasCity ()
		{
			Assert.That (sut.City != null);
		}

		[Test()]
		public void PropertyCityIsCorrect ()
		{
			sut.City = "Centennial";
			Assert.That (sut.City == "Centennial");
		}

		[Test()]
		public void PropertyHasState ()
		{
			Assert.That (sut.State == null);
		}

		[Test()]
		public void PropertyStateIsCorrect()
		{
			sut.State = "Colorado";
			Assert.That (sut.State== "Colorado");
		}
	}
}

