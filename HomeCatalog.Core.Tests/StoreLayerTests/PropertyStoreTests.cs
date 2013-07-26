using System;
using NUnit.Framework;
using HomeCatalog.Core;
using NSubstitute;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class PropertyStoreTests
	{
		[Test()]
		public void ItAsksDatabaseForProperty ()
		{
			S mockDB = Substitute.For<PropertyDatabase> ();

			PropertyStore sut = new PropertyStore (mockDB);
			sut.Property;
			mockDB.Received().GetProperty
		}
	}
}

