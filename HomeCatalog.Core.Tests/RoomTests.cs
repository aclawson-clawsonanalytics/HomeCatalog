using NUnit.Framework;
using System;

namespace HomeCatalog.Core.Tests
{


	[TestFixture()]
	public class RoomTests
	{
		private Room sut = new Room ();

		[SetUp ()]
		public void SetUp ()
		{
			sut = new Room ();
		}
		//[Test()]
		//public void RoomHasID()
		//{
		//	Assert.That (sut.RoomID != null);
		//}

		[Test ()]
		public void RoomHasLabel()
		{
			sut.Label = "Kitchen";
			Assert.That (sut.Label != null);
		}
		[Test ()]
		public void RoomLabelIsCorrect()
		{
			sut.Label = "Kitchen";
			Assert.That (sut.Label == "Kitchen");
		}
	}
}

