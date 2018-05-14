using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Grasshoppers.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(ViewCellSelectedColor))]
namespace Grasshoppers.iOS.Renderers
{
    public class ViewCellSelectedColor : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            if (cell != null)
            {
                // Disable native cell selection color style - set as *Transparent*
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            }
            return cell;
        }
    }
}