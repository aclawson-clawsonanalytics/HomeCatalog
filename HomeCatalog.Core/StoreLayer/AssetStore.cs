using System;
using System.IO;

namespace HomeCatalog.Core
{
	public class AssetStore
	{
		string _storeDirectory;

		public AssetStore (string directory)
		{
			if (!File.Exists (directory)) {
				Directory.CreateDirectory (directory);
			}
			_storeDirectory = directory;
		}

		public string PathForAsset (string id) {
			string path = Path.Combine (_storeDirectory, id);
			if (File.Exists (path)) {
				return path;
			}
			return null;
		}

		public string PathForEmptyAsset (string id) {
			return Path.Combine (_storeDirectory, id);
		}

		public string NewEmptyAsset () {
			string guid = Guid.NewGuid ().ToString ();
			return guid;
		}

	}
}

