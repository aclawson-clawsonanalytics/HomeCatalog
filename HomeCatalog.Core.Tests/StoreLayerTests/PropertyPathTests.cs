using System;
using HomeCatalog.Core;
using NUnit.Framework;
using System.IO;

namespace HomeCatalog.Core.Tests
{	
	[TestFixture()]
	public class PropertyPathTests
	{
		string TempDBPath;
		string TempDirectory;

		[SetUp()]
		public void Setup ()
		{
			TempDirectory = Path.Combine (Path.GetTempPath (), Path.GetRandomFileName ());
			Directory.CreateDirectory (TempDirectory);
			TempDBPath = Path.Combine (TempDirectory, Path.GetRandomFileName ());
		}

		[TearDown()]
		public void Teardown ()
		{
			File.Delete (TempDBPath);
			Directory.Delete (TempDirectory);
		}

		[Test()]
		public void ThrowsErrorIfNotExists ()
		{
			Assert.Throws<FileNotFoundException>(delegate { new PropertyPath (TempDBPath); });
		}

		[Test()]
		public void ItReadsThePropertyID ()
		{
			PropertyStore store = new PropertyStore (TempDBPath);
			string id = store.Property.PropertyID;

			PropertyPath path = new PropertyPath (TempDBPath);
			Assert.That (path.ID == id);
		}

		[Test()]
		public void ItReadsThePropertyName ()
		{
			PropertyStore store = new PropertyStore (TempDBPath);
			store.Property.PropertyName = "Test";
			store.SaveProperty ();
			
			PropertyPath path = new PropertyPath (TempDBPath);
			Assert.That (path.Name == "Test");
		}

		[Test()]
		public void ItToleratesUninitializedFiles ()
		{
			File.Create(TempDBPath).Dispose();
			Assert.DoesNotThrow (delegate { new PropertyPath (TempDBPath); });
		}
	}
}

