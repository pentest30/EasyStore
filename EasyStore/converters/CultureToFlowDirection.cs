// Decompiled with JetBrains decompiler
// Type: EChannelizer.CultureToFlowDirection
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
  public class CultureToFlowDirection : MarkupExtension, IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (object) (FlowDirection) ((value as CultureInfo).TextInfo.IsRightToLeft ? 1 : 0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException("ConvertBack() of CultureToFlowDirection is not implemented");
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return (object) this;
    }
  }
}
