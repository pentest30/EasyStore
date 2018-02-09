// Decompiled with JetBrains decompiler
// Type: EChannelizer.RibbonView
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace EasyStore.helpers
{
  public class RibbonView : UserControl, IComponentConnector
  {
    internal MetroRibbon Ribbon;
    internal MetroFlatButton BrowsePicons;
    private bool _contentLoaded;

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/E-Channelizer;component/views/main/ribbonview.xaml", UriKind.Relative));
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal Delegate _CreateDelegate(Type delegateType, string handler)
    {
      return Delegate.CreateDelegate(delegateType, (object) this, handler);
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      if (connectionId != 1)
      {
        if (connectionId == 2)
          this.BrowsePicons = (MetroFlatButton) target;
        else
          this._contentLoaded = true;
      }
      else
        this.Ribbon = (MetroRibbon) target;
    }
  }
}
