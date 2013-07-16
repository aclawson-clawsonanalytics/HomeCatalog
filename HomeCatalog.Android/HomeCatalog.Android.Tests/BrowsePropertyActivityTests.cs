using System;
using NUnit.Framework;

namespace HomeCatalog.Android
{
	[TestFixture]
	public class BrowsePropertyActivityTests
	{
		
		[SetUp]
		public void Setup ()
		{
		}

		[TearDown]
		public void Tear ()
		{
		}

		[Test]
		public void itShouldHaveAddPropertyButton () {
			BrowsePropertyActivity sut = new BrowsePropertyActivity ();
		}

//		[Test]
//		public void clickingAddPropertyShouldStartAddEditActivity ()
//		{
//
//		}
	}
}

