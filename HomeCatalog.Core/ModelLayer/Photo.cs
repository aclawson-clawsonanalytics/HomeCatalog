using System;
using SQLite;



namespace HomeCatalog.Core
{
	[FlagsAttribute]
	public enum PhotoFlags
	{
		Poster = 0,
		Receipt = 1,
		SerialNumber = 2,
		ModelNumber = 3
	};

	public class Photo : ISQLListItem
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		[Indexed]
		public int ItemID { get; set; }
		public string AssetID { get; set; }
		public DateTime DateAdded { get; set; }
		public PhotoFlags flags { get; set; }

		public Photo ()
		{

		}

		public Photo (string anAssetID)
		{
			AssetID = anAssetID;
		}
	}
}

