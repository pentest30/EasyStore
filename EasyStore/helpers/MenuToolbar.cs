// Decompiled with JetBrains decompiler
// Type: EChannelizer.MenuToolbar
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyStore.helpers
{
  public class MenuToolbar : MenuItem
  {
    public static readonly DependencyPropertyKey ButtonsPropertyKey = DependencyProperty.RegisterReadOnly("Buttons", typeof (List<MetroFlatButton>), typeof (MenuToolbar), (PropertyMetadata) new FrameworkPropertyMetadata((object) new List<MetroFlatButton>(), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty ButtonsProperty = MenuToolbar.ButtonsPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey ColumnsCountPropertyKey = DependencyProperty.RegisterReadOnly("ColumnsCount", typeof (int), typeof (MenuToolbar), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty ColumnsCountProperty = MenuToolbar.ColumnsCountPropertyKey.DependencyProperty;
    private ContextMenu _contextMenu;

    public List<MetroFlatButton> Buttons
    {
      get
      {
        return (List<MetroFlatButton>) this.GetValue(MenuToolbar.ButtonsProperty);
      }
      set
      {
        this.SetValue(MenuToolbar.ButtonsProperty, (object) value);
      }
    }

    public int ColumnsCount
    {
      get
      {
        return (int) this.GetValue(MenuToolbar.ColumnsCountProperty);
      }
      set
      {
        this.SetValue(MenuToolbar.ColumnsCountPropertyKey, (object) value);
      }
    }

    static MenuToolbar()
    {
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (MenuToolbar), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (MenuToolbar)));
    }

    public MenuToolbar()
    {
      this.SetValue(MenuToolbar.ButtonsPropertyKey, (object) new List<MetroFlatButton>());
    }

    protected override void OnInitialized(EventArgs e)
    {
      base.OnInitialized(e);
      this._contextMenu = this.Parent as ContextMenu;
      this._contextMenu.Opened += (RoutedEventHandler) ((sender, e1) => this.SetColumnsCount());
    }

    protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
    {
      base.OnPreviewMouseLeftButtonUp(e);
      if (this._contextMenu == null || !(e.OriginalSource is MetroFlatButton))
        return;
      this._contextMenu.IsOpen = false;
    }

    private void SetColumnsCount()
    {
      int num = Math.Min(this.Buttons.Count, Math.Max(checked ((int) Math.Ceiling(unchecked (this._contextMenu.ActualWidth - 34.0 / 38.0))), 4));
      if (checked (this.Buttons.Count - num) == 1)
        num = this.Buttons.Count;
      this.ColumnsCount = num;
    }
  }
}
