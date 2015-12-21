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
			Directory.Delete (TempDirectory, recursive:true);
		}

		[Test()]
		public void ThrowsErrorIfNotExists ()
		{
			Assert.Throws<FileNotFoundException>(delegate { new PropertyPath (TempDBPath); });
		}

		[Test()]
		public void ItReadsThePropertyID ()
		{
			PropertyStore store = PropertyStore.NewPropertyStoreAtPath (TempDBPath);
			string id = store.Property.PropertyID;

			PropertyPath path = new PropertyPath (TempDBPath);
			Assert.That (path.ID == id);
		}

		[Test()]
		public void ItReadsThePropertyName ()
		{
			PropertyStore store = PropertyStore.NewPropertyStoreAtPath (TempDBPath);
			store.Property.PropertyName = "Test";
			store.SaveProperty ();
			
			PropertyPath path = new PropertyPath (TempDBPath);
			Assert.That (path.Name == "Test");
		}

		[Test()]
		public void ItToleratesUninitializedFiles ()
		{
			Directory.CreateDirectory (TempDBPath);
			File.Create(Path.Combine(TempDBPath, "data.sqlite")).Dispose();
			Assert.DoesNotThrow (delegate { new PropertyPath (TempDBPath); });
		}
	}
}

