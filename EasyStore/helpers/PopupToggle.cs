// Decompiled with JetBrains decompiler
// Type: EChannelizer.PopupToggle
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
  public class PopupToggle : ToggleButton
  {
    public static readonly DependencyProperty IconKindProperty = DependencyProperty.Register("IconKind", typeof (PackIconMaterialKind), typeof (PopupToggle), (PropertyMetadata) new FrameworkPropertyMetadata((object) (PackIconMaterialKind) 0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof (double), typeof (PopupToggle), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty PopupWidthProperty = DependencyProperty.Register("PopupWidth", typeof (double), typeof (PopupToggle), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static DependencyProperty PopupTemplateProperty = DependencyProperty.Register("PopupTemplate", typeof (ControlTemplate), typeof (PopupToggle), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty PopupPlacmentProperty = DependencyProperty.Register("PopupPlacment", typeof (PopupToggle.PopupPlacementMode), typeof (PopupToggle), (PropertyMetadata) new FrameworkPropertyMetadata((object) PopupToggle.PopupPlacementMode.BottomLeft, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(PopupToggle.OnPopupPlacementChanged)));
    private const string PART_Popup = "PART_Popup";
    private Popup part_Popup;

    public PackIconMaterialKind IconKind
    {
      get
      {
        return (PackIconMaterialKind) this.GetValue(PopupToggle.IconKindProperty);
      }
      set
      {
        this.SetValue(PopupToggle.IconKindProperty, (object) value);
      }
    }

    public double IconSize
    {
      get
      {
        return (double) this.GetValue(PopupToggle.IconSizeProperty);
      }
      set
      {
        this.SetValue(PopupToggle.IconSizeProperty, (object) value);
      }
    }

    public double PopupWidth
    {
      get
      {
        return (double) this.GetValue(PopupToggle.PopupWidthProperty);
      }
      set
      {
        this.SetValue(PopupToggle.PopupWidthProperty, (object) value);
      }
    }

    public ControlTemplate PopupTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(PopupToggle.PopupTemplateProperty);
      }
      set
      {
        this.SetValue(PopupToggle.PopupTemplateProperty, (object) value);
      }
    }

    public PopupToggle.PopupPlacementMode PopupPlacment
    {
      get
      {
        return (PopupToggle.PopupPlacementMode) this.GetValue(PopupToggle.PopupPlacmentProperty);
      }
      set
      {
        this.SetValue(PopupToggle.PopupPlacmentProperty, (object) value);
      }
    }

    static PopupToggle()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (PopupToggle), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (PopupToggle)));
    }

    public PopupToggle()
    {
      this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.OnIsVisibleChanged);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.part_Popup = this.GetTemplateChild("PART_Popup") as Popup;
      if (this.part_Popup == null)
        throw new NullReferenceException("'{0}' template part is not found on 'PopupToggle' control " +((object) "PART_Popup"));
      PopupToggle.SetPopupPlacement(this.part_Popup, this.PopupPlacment);
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
      if (this.part_Popup==null)
        return;
      this.part_Popup.IsOpen &= e.Key != Key.Escape;
      base.OnPreviewKeyDown(e);
    }

    protected override void OnVisualParentChanged(DependencyObject oldParent)
    {
      if (oldParent != null)
        this.IsVisibleChanged -= new DependencyPropertyChangedEventHandler(this.OnIsVisibleChanged);
      base.OnVisualParentChanged(oldParent);
    }

    private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      if (this.part_Popup==null)
        return;
      this.part_Popup.IsOpen &= (bool) e.NewValue;
    }

    private static void OnPopupPlacementChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
      PopupToggle popupToggle = dependencyObject as PopupToggle;
      Popup partPopup = popupToggle.part_Popup;
      if (partPopup==null)
        return;
      PopupToggle.SetPopupPlacement(partPopup, popupToggle.PopupPlacment);
    }

    private static void SetPopupPlacement(Popup popup, PopupToggle.PopupPlacementMode mode)
    {
      switch (mode)
      {
        case PopupToggle.PopupPlacementMode.Left:
          popup.Placement = PlacementMode.Left;
          popup.HorizontalOffset = -1.0;
          break;
        case PopupToggle.PopupPlacementMode.Right:
          popup.Placement = PlacementMode.Right;
          popup.HorizontalOffset = 1.0;
          break;
        case PopupToggle.PopupPlacementMode.TopLeft:
          popup.Placement = PlacementMode.Top;
          popup.VerticalOffset = -1.0;
          break;
        case PopupToggle.PopupPlacementMode.TopRight:
          popup.Placement = PlacementMode.Custom;
          popup.CustomPopupPlacementCallback += (CustomPopupPlacementCallback) ((popupSize, targetSize, offset) => new CustomPopupPlacement[1]
          {
            new CustomPopupPlacement()
            {
              Point = new Point(targetSize.Width - popupSize.Width, -popupSize.Height)
            }
          });
          popup.VerticalOffset = -1.0;
          break;
        case PopupToggle.PopupPlacementMode.BottomLeft:
          popup.Placement = PlacementMode.Bottom;
          popup.VerticalOffset = 1.0;
          break;
        case PopupToggle.PopupPlacementMode.BottomRight:
          popup.Placement = PlacementMode.Custom;
          popup.CustomPopupPlacementCallback += (CustomPopupPlacementCallback) ((popupSize, targetSize, offset) => new CustomPopupPlacement[1]
          {
            new CustomPopupPlacement()
            {
              Point = new Point(targetSize.Width - popupSize.Width, targetSize.Height)
            }
          });
          popup.VerticalOffset = 1.0;
          break;
      }
    }

    public enum PopupPlacementMode
    {
      Left,
      Right,
      TopLeft,
      TopRight,
      BottomLeft,
      BottomRight,
    }
  }
}
