// Decompiled with JetBrains decompiler
// Type: EChannelizer.RibbonGroup
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System.Windows;
using System.Windows.Controls;

namespace EasyStore.helpers
{
  public class RibbonGroup : ContentControl
  {
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof (string), typeof (RibbonGroup), (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public string Title
    {
      get
      {
        return (string) this.GetValue(RibbonGroup.TitleProperty);
      }
      set
      {
        this.SetValue(RibbonGroup.TitleProperty, (object) value);
      }
    }

    static RibbonGroup()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (RibbonGroup), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (RibbonGroup)));
    }
  }
}
