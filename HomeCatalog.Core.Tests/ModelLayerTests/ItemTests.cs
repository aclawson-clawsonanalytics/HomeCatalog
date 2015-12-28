using NUnit.Framework;
using System;

namespace HomeCatalog.Core.Tests
{
	[TestFixture()]
	public class ItemTests
	{
		private Item sut;

		[SetUp ()]
		public void SetUp ()
		{
			sut = new Item();
			sut.PurchaseDate = new DateTime (2013, 7, 26);
			sut.AppraisalDate = new DateTime (2013, 7, 26);
		}

		[Test()]
		public void ItemHasID ()
		{
			sut.ID = 1;
			Assert.That (sut.ID == 1);
		}

		[Test ()]
		public void ItemHasName ()
		{
			sut.ItemName = "Item1";

			Assert.That (sut.ItemName == "Item1");
		}

		[Test ()]
		public void PurchaseYearIsCorrect ()
		{
			Assert.That (sut.PurchaseDate.Year == 2013);
		}

		[Test ()]
		public void PurchaseMonthIsCorrect ()
		{
			Assert.That (sut.PurchaseDate.Month == 7);
		}

		[Test ()]
		public void PurchaseDayIsCorrect()
		{
			Assert.That (sut.PurchaseDate.Day == 26);
		}

		[Test ()]
		public void ItemHasPurchaseValue ()
		{
			sut.PurchaseValue = 1;
			Assert.That (sut.PurchaseValue == 1);
		}

		[Test ()]
		public void ItemHasAppraisalDate ()
		{
			Assert.That (sut.AppraisalDate == sut.PurchaseDate);
		}

		[Test()]
		public void PhotoListThrowsIfInvalidID ()
		{
			Assert.Throws<InvalidOperationException> (delegate { 
				PhotoList list = sut.PhotoList; 
			});
		}
	}
}

