using NUnit.Framework;
using System;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class CategoryTests
	{
		private Category sut = new Category ();

		[Test()]
		public void CategoryHasID ()
		{
			sut = new Category ();
		}

//		[Test ()]
//		public void CategoryHasLabel()
//		{
//			Assert.That (sut.CategoryID != null);
//		}

		[Test ()]
		public void CategoryHasLabel ()
		{
			sut.Label = "testLabel";
			Assert.That (sut.Label == "testLabel");
		}


	}
}

