using System;
using HomeCatalog.Core;
using NUnit.Framework;
using System.IO;

namespace HomeCatalog.Core.Tests
{	
	[TestFixture()]
	public class AssetStoreTests
	{
		string TempDirectory;

		[SetUp()]
		public void Setup ()
		{
			TempDirectory = Path.Combine (Path.GetTempPath (), Path.GetRandomFileName ());
			Directory.CreateDirectory (TempDirectory);
		}

		[TearDown()]
		public void Teardown ()
		{
			Directory.Delete (TempDirectory, true);
		}

		[Test()]
		public void ItGeneratesPathsForNewFilesInDirectory() 
		{
			AssetStore sut = new AssetStore (TempDirectory);
			string id = sut.NewEmptyAsset ();
			string path = sut.PathForEmptyAsset (id);
			Assert.That (path.Contains (TempDirectory));
		}

		[Test()]
		public void ItGeneratesPathsForNewFilesWithAFileName() 
		{
			AssetStore sut = new AssetStore (TempDirectory);
			string id = sut.NewEmptyAsset ();
			string path = sut.PathForEmptyAsset (id);
			Assert.That (Path.GetFileName(path) != Path.GetFileName(TempDirectory));
		}

		[Test()]
		public void ItGeneratesNewEmptyID() 
		{
			AssetStore sut = new AssetStore (TempDirectory);
			Assert.NotNull (sut.NewEmptyAsset ());
		}

		[Test()]
		public void AssetPathReturnsNullForNonexistantFiles() 
		{
			AssetStore sut = new AssetStore (TempDirectory);
			string id = sut.NewEmptyAsset ();
			string path = sut.PathForAsset (id);
			Assert.Null (path);
		}

		[Test()]
		public void AssetPathReturnsExistingPath() 
		{
			AssetStore sut = new AssetStore (TempDirectory);
			string id = sut.NewEmptyAsset ();
			string path = sut.PathForEmptyAsset (id);
			File.Create (path).Dispose ();
			Assert.NotNull (sut.PathForAsset (id));
		}
	}
}



