using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace HomeCatalog.iOS
{
	class PropertyListDataSource : UITableViewSource
	{
		static readonly NSString PROPERTY_CELL_IDENTIFIER = new NSString ("DataSourceCell");
		List<object> objects = new List<object> ();

		public PropertyListDataSource ()
		{

		}

		public IList<object> Objects {
			get { return objects; }
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return objects.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (PropertyTableViewCell)tableView.DequeueReusableCell (PROPERTY_CELL_IDENTIFIER, indexPath);

			cell.TextLabel.Text = objects [indexPath.Row].ToString ();

			return cell;
		}

		public override bool CanEditRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			return true;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete) {
				objects.RemoveAt (indexPath.Row);
				tableView.ReloadData ();
			}
		}
	}
}

