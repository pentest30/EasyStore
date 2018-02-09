// Decompiled with JetBrains decompiler
// Type: EChannelizer.MetroFlatButton
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.IconPacks;

namespace EasyStore.helpers
{
  public class MetroFlatButton : Button
  {
    public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register("IconKind", typeof (PackIconMaterialKind), typeof (MetroFlatButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) (PackIconMaterialKind) 0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof (double), typeof (MetroFlatButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public PackIconMaterialKind IconKind
    {
      get
      {
        return (PackIconMaterialKind) this.GetValue(MetroFlatButton.IconKindProperty);
      }
      set
      {
        this.SetValue(MetroFlatButton.IconKindProperty, (object) value);
      }
    }

    public double IconSize
    {
      get
      {
        return (double) this.GetValue(MetroFlatButton.IconSizeProperty);
      }
      set
      {
        this.SetValue(MetroFlatButton.IconSizeProperty, (object) value);
      }
    }

    static MetroFlatButton()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (MetroFlatButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (MetroFlatButton)));
    }
  }
}
