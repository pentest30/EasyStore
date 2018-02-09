// Decompiled with JetBrains decompiler
// Type: EChannelizer.MetroToggle
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System.Windows;
using System.Windows.Controls.Primitives;
using MahApps.Metro.IconPacks;

namespace EasyStore.helpers
{
  public class MetroToggle : ToggleButton
  {
    public static readonly DependencyProperty OffKindProperty = DependencyProperty.Register("OffKind", typeof (PackIconMaterialKind), typeof (MetroToggle), (PropertyMetadata) new FrameworkPropertyMetadata((object) (PackIconMaterialKind) 0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty OnKindProperty = DependencyProperty.Register("OnKind", typeof (PackIconMaterialKind), typeof (MetroToggle), (PropertyMetadata) new FrameworkPropertyMetadata((object) (PackIconMaterialKind) 0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof (double), typeof (MetroToggle), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public PackIconMaterialKind OffKind
    {
      get
      {
        return (PackIconMaterialKind) this.GetValue(MetroToggle.OffKindProperty);
      }
      set
      {
        this.SetValue(MetroToggle.OffKindProperty, (object) value);
      }
    }

    public PackIconMaterialKind OnKind
    {
      get
      {
        return (PackIconMaterialKind) this.GetValue(MetroToggle.OnKindProperty);
      }
      set
      {
        this.SetValue(MetroToggle.OnKindProperty, (object) value);
      }
    }

    public double IconSize
    {
      get
      {
        return (double) this.GetValue(MetroToggle.IconSizeProperty);
      }
      set
      {
        this.SetValue(MetroToggle.IconSizeProperty, (object) value);
      }
    }

    static MetroToggle()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (MetroToggle), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (MetroToggle)));
    }

    public override void OnApplyTemplate()
    {
      if (this.OnKind == null)
        this.OnKind = this.OffKind;
      base.OnApplyTemplate();
    }
  }
}
