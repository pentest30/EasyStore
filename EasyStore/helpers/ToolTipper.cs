// Decompiled with JetBrains decompiler
// Type: EChannelizer.ToolTipper
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.IconPacks;

namespace EasyStore.helpers
{
  public class ToolTipper : Control
  {
    public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register("IconKind", typeof (PackIconMaterialKind), typeof (ToolTipper), (PropertyMetadata) new FrameworkPropertyMetadata((object) (PackIconMaterialKind) 0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof (double), typeof (ToolTipper), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty TipTitleProperty = DependencyProperty.Register("TipTitle", typeof (string), typeof (ToolTipper), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty TipContentProperty = DependencyProperty.Register("TipContent", typeof (string), typeof (ToolTipper), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public PackIconMaterialKind IconKind
    {
      get
      {
        return (PackIconMaterialKind) this.GetValue(ToolTipper.IconKindProperty);
      }
      set
      {
        this.SetValue(ToolTipper.IconKindProperty, (object) value);
      }
    }

    public double IconSize
    {
      get
      {
        return (double) this.GetValue(ToolTipper.IconSizeProperty);
      }
      set
      {
        this.SetValue(ToolTipper.IconSizeProperty, (object) value);
      }
    }

    public string TipTitle
    {
      get
      {
        return (string) this.GetValue(ToolTipper.TipTitleProperty);
      }
      set
      {
        this.SetValue(ToolTipper.TipTitleProperty, (object) value);
      }
    }

    public string TipContent
    {
      get
      {
        return (string) this.GetValue(ToolTipper.TipContentProperty);
      }
      set
      {
        this.SetValue(ToolTipper.TipContentProperty, (object) value);
      }
    }

    static ToolTipper()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (ToolTipper), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (ToolTipper)));
    }
  }
}
