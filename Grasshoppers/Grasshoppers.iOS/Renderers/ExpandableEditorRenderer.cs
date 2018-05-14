using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Grasshoppers.CustomControls;
using Grasshoppers.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExpandableEditor), typeof(ExpandableEditorRenderer))]
namespace Grasshoppers.iOS.Renderers
{
    public class ExpandableEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.ScrollEnabled = false;
        }
    }
}