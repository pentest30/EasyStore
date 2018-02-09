// Decompiled with JetBrains decompiler
// Type: EChannelizer.MetroRibbon
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace EasyStore.helpers
{
  public class MetroRibbon : TabControl
  {
    public static readonly DependencyProperty IsMinimizedProperty = DependencyProperty.Register("IsMinimized", typeof (bool), typeof (MetroRibbon), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty IsPinnedProperty = DependencyProperty.Register("IsPinned", typeof (bool), typeof (MetroRibbon), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    private bool _isSelectedTabClicked;
    private bool _isTabHeaderClicked;

    public bool IsMinimized
    {
      get
      {
        return (bool) this.GetValue(MetroRibbon.IsMinimizedProperty);
      }
      set
      {
        this.SetValue(MetroRibbon.IsMinimizedProperty, (object) value);
      }
    }

    public bool IsPinned
    {
      get
      {
        return (bool) this.GetValue(MetroRibbon.IsPinnedProperty);
      }
      set
      {
        this.SetValue(MetroRibbon.IsPinnedProperty, (object) value);
      }
    }

    static MetroRibbon()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (MetroRibbon), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (MetroRibbon)));
    }

    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
    {
      base.OnPreviewMouseLeftButtonDown(e);
      DependencyObject originalSource = e.OriginalSource as DependencyObject;
      if (originalSource == null)
        return;
      TabItem parent = (TabItem) TreeHelper.TryFindParent<TabItem>(originalSource);
      if (parent != null)
      {
        this._isTabHeaderClicked = !(parent.Content as UIElement).IsMouseOver;
        this._isSelectedTabClicked = parent.IsSelected;
      }
      if (!this._isTabHeaderClicked || e.ClickCount != 2)
        return;
      this.IsPinned = !this.IsPinned;
    }

    protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
    {
      base.OnPreviewMouseLeftButtonUp(e);
      if (MetroRibbon.IsIgnorable(e.OriginalSource as DependencyObject))
        return;
      if (this.IsPinned || e.ClickCount != 1)
        this.IsMinimized = false;
      else
        this.IsMinimized = this._isSelectedTabClicked ? !this.IsMinimized : !this._isTabHeaderClicked;
    }

    public static bool IsIgnorable(DependencyObject depObj)
    {
      //if (depObj != null && !depObj.<RibbonDropButton>() && (!depObj.IsOfType<TextBox>() && !depObj.IsOfType<MetroFlatButton>()) && (!depObj.IsOfType<CheckBox>() && !depObj.IsOfType<FlagsCheckBox>() && (!depObj.IsOfType<MetroToggle>() && ((object) TreeHelper.TryFindParent<ScrollBar>(depObj)).IsNull())))
      //  return !((object) TreeHelper.TryFindParent<ComboBox>(depObj)).IsNull();
      return true;
    }
  }
}
