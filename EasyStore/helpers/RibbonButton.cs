// Decompiled with JetBrains decompiler
// Type: EChannelizer.RibbonButton
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.IconPacks;

namespace EasyStore.helpers
{
  public class RibbonButton : Button
  {
    public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register("IconKind", typeof (PackIconMaterialKind), typeof (RibbonButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) (PackIconMaterialKind) 0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty IsEnlargedProperty = DependencyProperty.Register("IsEnlarged", typeof (bool), typeof (RibbonButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public PackIconMaterialKind IconKind
    {
      get
      {
        return (PackIconMaterialKind) this.GetValue(RibbonButton.IconKindProperty);
      }
      set
      {
        this.SetValue(RibbonButton.IconKindProperty, (object) value);
      }
    }

    public bool IsEnlarged
    {
      get
      {
        return (bool) this.GetValue(RibbonButton.IsEnlargedProperty);
      }
      set
      {
        this.SetValue(RibbonButton.IsEnlargedProperty, (object) value);
      }
    }

    static RibbonButton()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (RibbonButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (RibbonButton)));
    }
  }
}
