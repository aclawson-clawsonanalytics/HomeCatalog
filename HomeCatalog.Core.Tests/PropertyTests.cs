using NUnit.Framework;
using System;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class PropertyTests
	{
		[Test()]
		public void PropertyHasName ()
		{
			Property sut = new Property ();
			sut.PropertyName = "Home";
			Assert.That (sut.PropertyName != null);
		}

		[Test()]
		public void PropertyNameIsCorrect()
		{
			Property sut = new Property ();
			sut.PropertyName = "Name";
			Assert.That (sut.PropertyName == "Name");
		}
	}
}

