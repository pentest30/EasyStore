// Decompiled with JetBrains decompiler
// Type: EChannelizer.BoolToVisibility
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace EasyStore.converters
{
  public class BoolToVisibility : MarkupExtension, IValueConverter
  {
    public bool InvertBool { get; set; }

    public bool CollapseInvisible { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (object) (Visibility) ((!this.InvertBool ? ((bool) value ? 1 : 0) : (!(bool) value ? 1 : 0)) != 0 ? 0 : (this.CollapseInvisible ? 2 : 1));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException("ConvertBack() of BoolToVisibilityConverter is not implemented");
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return (object) this;
    }
  }
}
