using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EasyStore.helpers
{
    class CustomDatePicker :DatePicker
    {
        public string WatermarkText
        {
            get { return (string)GetValue(WatermarkTextProperty); }
            set { SetValue(WatermarkTextProperty, value); }
        }

        public static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.Register("WatermarkText", typeof(string), typeof(CustomDatePicker), new PropertyMetadata("Datum wählen..."));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DatePickerTextBox box = base.GetTemplateChild("PART_TextBox") as DatePickerTextBox;
            box.ApplyTemplate();

            ContentControl watermark = box.Template.FindName("PART_Watermark", box) as ContentControl;
            if (watermark != null) watermark.Content = WatermarkText;
        }
    
    }
}
