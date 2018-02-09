// Decompiled with JetBrains decompiler
// Type: EChannelizer.MetroToolTip
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.IconPacks;

namespace EasyStore.helpers
{
  public class MetroToolTip : ToolTip
  {
    public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register("IconKind", typeof (PackIconMaterialKind), typeof (MetroToolTip), (PropertyMetadata) new FrameworkPropertyMetadata((object) (PackIconMaterialKind) 0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof (string), typeof (MetroToolTip), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public PackIconMaterialKind IconKind
    {
      get
      {
        return (PackIconMaterialKind) this.GetValue(MetroToolTip.IconKindProperty);
      }
      set
      {
        this.SetValue(MetroToolTip.IconKindProperty, (object) value);
      }
    }

    public string Title
    {
      get
      {
        return (string) this.GetValue(MetroToolTip.TitleProperty);
      }
      set
      {
        this.SetValue(MetroToolTip.TitleProperty, (object) value);
      }
    }

    static MetroToolTip()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (MetroToolTip), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (MetroToolTip)));
    }
  }
}
