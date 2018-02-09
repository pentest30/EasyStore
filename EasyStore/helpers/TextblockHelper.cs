using System.Windows;

namespace EasyStore.helpers
{
    public class TextblockHelper
    {
        public static readonly DependencyProperty AutoTooltipProperty = DependencyProperty.RegisterAttached("AutoTooltip", typeof (object), typeof (TextblockHelper), new PropertyMetadata(default(object)));

        public static object GetAutoTooltip (UIElement element)
        {
            return (object) element.GetValue(AutoTooltipProperty);
        }

        public static void SetAutoTooltip(UIElement element, object value)
        {
            element.SetValue(AutoTooltipProperty, value);
        }
    }
}