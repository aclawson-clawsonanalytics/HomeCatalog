using NUnit.Framework;
using System;

namespace HomeCatalog.Core.Tests
{
	[TestFixture ()]
	public class PropertyTests
	{
		private Property sut = new Property ();

		[SetUp ()]
		public void SetUp ()
		{
			sut = new Property ();
		}

		[Test ()]
		public void PropertyNameIsCorrect ()
		{
			sut.PropertyName = "Name";
			Assert.That (sut.PropertyName == "Name");
		}

		[Test]
		public void PropertyHasID ()
		{
			Assert.That (sut.PropertyID != null);
		}

		[Test ()]
		public void PropertyAddressIsCorrect ()
		{
			sut.Address = "19859 E. Garden Pl";
			Assert.That (sut.Address == "19859 E. Garden Pl");
		}

		[Test ()]
		public void PropertyCityIsCorrect ()
		{
			sut.City = "Centennial";
			Assert.That (sut.City == "Centennial");
		}

		[Test ()]
		public void PropertyStateIsCorrect ()
		{
			sut.State = "Colorado";
			Assert.That (sut.State == "Colorado");
		}

		[Test ()]
		public void PropertyZipIsCorrect ()
		{
			sut.ZipCode = "80015";
			Assert.That (sut.ZipCode == "80015");
		}

		[Test ()]
		public void PropertyCountryIsCorrect ()
		{
			sut.Country = "USA";
			Assert.That (sut.Country == "USA");
		}
	}
}

