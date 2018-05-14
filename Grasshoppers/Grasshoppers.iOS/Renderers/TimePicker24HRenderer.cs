using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Grasshoppers.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TimePicker), typeof(TimePicker24HRenderer))]
namespace Grasshoppers.iOS.Renderers
{
    public class TimePicker24HRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            var timePicker = (UIDatePicker)Control.InputView;
            timePicker.Locale = new NSLocale("no_nb");
        }
    }
}