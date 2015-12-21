using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace HomeCatalog.iOS
{
	public partial class PropertyTableViewCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("PropertyTableViewCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("PropertyTableViewCell");

		public PropertyTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public static PropertyTableViewCell Create ()
		{
			return (PropertyTableViewCell)Nib.Instantiate (null, null) [0];
		}
	}
}

