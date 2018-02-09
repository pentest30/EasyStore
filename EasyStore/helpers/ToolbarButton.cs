// Decompiled with JetBrains decompiler
// Type: EChannelizer.ToolbarButton
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System.Windows;

namespace EasyStore.helpers
{
  public class ToolbarButton : MetroFlatButton
  {
    public static readonly DependencyProperty ShowSeparatorProperty = DependencyProperty.Register("ShowSeparator", typeof (bool), typeof (ToolbarButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public bool ShowSeparator
    {
      get
      {
        return (bool) this.GetValue(ToolbarButton.ShowSeparatorProperty);
      }
      set
      {
        this.SetValue(ToolbarButton.ShowSeparatorProperty, (object) value);
      }
    }

    static ToolbarButton()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (ToolbarButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (ToolbarButton)));
    }
  }
}
