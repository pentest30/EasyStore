// Decompiled with JetBrains decompiler
// Type: EChannelizer.MetroCircleButton
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.IconPacks;

namespace EasyStore.helpers
{
  public class MetroCircleButton : Button
  {
    public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register("IconKind", typeof (PackIconMaterialKind), typeof (MetroCircleButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) (PackIconMaterialKind) 0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof (double), typeof (MetroCircleButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public PackIconMaterialKind IconKind
    {
      get
      {
        return (PackIconMaterialKind) this.GetValue(MetroCircleButton.IconKindProperty);
      }
      set
      {
        this.SetValue(MetroCircleButton.IconKindProperty, (object) value);
      }
    }

    public double IconSize
    {
      get
      {
        return (double) this.GetValue(MetroCircleButton.IconSizeProperty);
      }
      set
      {
        this.SetValue(MetroCircleButton.IconSizeProperty, (object) value);
      }
    }

    static MetroCircleButton()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (MetroCircleButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (MetroCircleButton)));
    }
  }
}
