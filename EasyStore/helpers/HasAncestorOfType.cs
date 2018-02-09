// Decompiled with JetBrains decompiler
// Type: EChannelizer.HasAncestorOfType
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace EasyStore.helpers
{
  public class HasAncestorOfType : MarkupExtension, IValueConverter
  {
    public Type AncestorType { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value==null)
        return (object) null;
      DependencyObject reference = value as DependencyObject;
      do
      {
        reference = VisualTreeHelper.GetParent(reference);
        if (reference == null)
          return (object) false;
      }
      while (!(reference.GetType() == this.AncestorType));
      return (object) true;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException("ConvertBack() of IsChildOfConverter is not implemented");
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return (object) this;
    }
  }
}
