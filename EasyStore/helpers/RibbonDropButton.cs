// Decompiled with JetBrains decompiler
// Type: EChannelizer.RibbonDropButton
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MahApps.Metro.IconPacks;

namespace EasyStore.helpers
{
  [TemplatePart(Name = "PART_Popup", Type = typeof (Popup))]
  public class RibbonDropButton : ToggleButton
  {
    public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register("IconKind", typeof (PackIconMaterialKind), typeof (RibbonDropButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) (PackIconMaterialKind) 0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty IsEnlargedProperty = DependencyProperty.Register("IsEnlarged", typeof (bool), typeof (RibbonDropButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static DependencyProperty DropTemplateProperty = DependencyProperty.Register("DropTemplate", typeof (ControlTemplate), typeof (RibbonDropButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    private const string PART_Popup = "PART_Popup";
    private Popup part_Popup;

    public PackIconMaterialKind IconKind
    {
      get
      {
        return (PackIconMaterialKind) this.GetValue(RibbonDropButton.IconKindProperty);
      }
      set
      {
        this.SetValue(RibbonDropButton.IconKindProperty, (object) value);
      }
    }

    public bool IsEnlarged
    {
      get
      {
        return (bool) this.GetValue(RibbonDropButton.IsEnlargedProperty);
      }
      set
      {
        this.SetValue(RibbonDropButton.IsEnlargedProperty, (object) value);
      }
    }

    public ControlTemplate DropTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(RibbonDropButton.DropTemplateProperty);
      }
      set
      {
        this.SetValue(RibbonDropButton.DropTemplateProperty, (object) value);
      }
    }

    static RibbonDropButton()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (RibbonDropButton), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (RibbonDropButton)));
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.part_Popup = this.GetTemplateChild("PART_Popup") as Popup;
      if (this.part_Popup == null)
        throw new NullReferenceException("'{0}' template part is not found on 'RibbonDropButton' control");
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
      base.OnPreviewKeyDown(e);
      this.part_Popup.IsOpen &= e.Key != Key.Escape;
    }

    protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
    {
      base.OnPreviewMouseLeftButtonUp(e);
      if (MetroRibbon.IsIgnorable(e.OriginalSource as DependencyObject))
        return;
      this.part_Popup.IsOpen = false;
    }
  }
}
