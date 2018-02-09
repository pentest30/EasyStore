// Decompiled with JetBrains decompiler
// Type: EChannelizer.TextboxHelper
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyStore.helpers
{
  public static class TextboxHelper
  {
    public static readonly DependencyProperty AllowedCharsProperty = DependencyProperty.RegisterAttached("AllowedChars", typeof (string), typeof (TextboxHelper), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(TextboxHelper.OnAllowedCharsChanged)));
    public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached("IsFocused", typeof (bool), typeof (TextboxHelper), new PropertyMetadata((object) false, new PropertyChangedCallback(TextboxHelper.OnIsFocusedChanged)));

    public static string GetAllowedChars(TextBox textBox)
    {
      return (string) textBox.GetValue(TextboxHelper.AllowedCharsProperty);
    }

    public static void SetAllowedChars(TextBox textBox, string value)
    {
      textBox.SetValue(TextboxHelper.AllowedCharsProperty, (object) value);
    }

    private static void OnAllowedCharsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      TextBox textBox = d as TextBox;
      if (textBox == null)
        throw new InvalidOperationException("This property can only be applied to TextBox");
      if (((string) e.NewValue) =="")
        textBox.PreviewTextInput -= new TextCompositionEventHandler(TextboxHelper.OnPreviewTextInput);
      else
        textBox.PreviewTextInput += new TextCompositionEventHandler(TextboxHelper.OnPreviewTextInput);
    }

    public static bool GetIsFocused(TextBox textBox)
    {
      return (bool) textBox.GetValue(TextboxHelper.IsFocusedProperty);
    }

    public static void SetIsFocused(TextBox textBox, bool value)
    {
      textBox.SetValue(TextboxHelper.IsFocusedProperty, (object) value);
    }

    private static void OnIsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      TextBox textBox = d as TextBox;
      if (textBox == null)
        throw new InvalidOperationException("This property can only be applied to TextBox");
      if (!(bool) e.NewValue || (bool) e.OldValue || textBox.IsFocused)
        return;
      textBox.Focus();
      textBox.LostFocus += (RoutedEventHandler) ((sender, args) => textBox.SetValue(TextboxHelper.IsFocusedProperty, (object) false));
    }

    private static void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
      e.Handled = !e.Text.All<char>(new Func<char, bool>(((string) TextboxHelper.GetAllowedChars(sender as TextBox)).Contains<char>));
    }
  }
}
