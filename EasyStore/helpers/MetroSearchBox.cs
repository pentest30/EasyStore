// Decompiled with JetBrains decompiler
// Type: EChannelizer.MetroSearchBox
// Assembly: E-Channelizer, Version=4.0.1.5000, Culture=neutral, PublicKeyToken=null
// MVID: 191E6C62-4C33-48FF-AF5C-42FDD799ADB2
// Assembly location: C:\Program Files (x86)\E-Channelizer\E-Channelizer.exe

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace EasyStore.helpers
{
  [TemplatePart(Name = "PART_MatchCaseToggle", Type = typeof (ToggleButton))]
  [TemplatePart(Name = "PART_MatchWordsToggle", Type = typeof (ToggleButton))]
  [TemplatePart(Name = "PART_MatchDiacriticsToggle", Type = typeof (ToggleButton))]
  [TemplatePart(Name = "PART_ProgressBar", Type = typeof (ProgressBar))]
  public class MetroSearchBox : TextBox
  {
    public static readonly DependencyProperty MatchCaseProperty = DependencyProperty.Register("MatchCase", typeof (bool), typeof (MetroSearchBox), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    public static readonly DependencyProperty MatchCaseTipProperty = DependencyProperty.Register("MatchCaseTip", typeof (string), typeof (MetroSearchBox), (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty MatchWordsProperty = DependencyProperty.Register("MatchWords", typeof (bool), typeof (MetroSearchBox), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    public static readonly DependencyProperty MatchWordsTipProperty = DependencyProperty.Register("MatchWordsTip", typeof (string), typeof (MetroSearchBox), (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));
    public static readonly DependencyProperty MatchDiacriticsProperty = DependencyProperty.Register("MatchDiacritics", typeof (bool), typeof (MetroSearchBox), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    public static readonly DependencyProperty MatchDiacriticsTipProperty = DependencyProperty.Register("MatchDiacriticsTip", typeof (string), typeof (MetroSearchBox), (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));


        //public string WaterMark
        //{
        //    get { return (this.Text=="")? (string)GetValue(WaterMarkProperty):""; }
        //    set { SetValue(WaterMarkProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for WaterMark.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty WaterMarkProperty =
        //    DependencyProperty.Register("WaterMark", typeof(string), typeof(MetroSearchBox), new PropertyMetadata(0));


        private const string PART_MatchCaseToggle = "PART_MatchCaseToggle";
    private const string PART_MatchWordsToggle = "PART_MatchWordsToggle";
    private const string PART_MatchDiacriticsToggle = "PART_MatchDiacriticsToggle";
    private const string PART_ProgressBar = "PART_ProgressBar";
    private ToggleButton part_MatchCaseToggle;
    private ToggleButton part_MatchWordsToggle;
    private ToggleButton part_MatchDiacriticsToggle;
    private ProgressBar part_ProgressBar;

    public bool MatchCase
    {
      get
      {
        return (bool) this.GetValue(MetroSearchBox.MatchCaseProperty);
      }
      set
      {
        this.SetValue(MetroSearchBox.MatchCaseProperty, (object) value);
      }
    }

    public string MatchCaseTip
    {
      get
      {
        return (string) this.GetValue(MetroSearchBox.MatchCaseTipProperty);
      }
      set
      {
        this.SetValue(MetroSearchBox.MatchCaseTipProperty, (object) value);
      }
    }

    public bool MatchWords
    {
      get
      {
        return (bool) this.GetValue(MetroSearchBox.MatchWordsProperty);
      }
      set
      {
        this.SetValue(MetroSearchBox.MatchWordsProperty, (object) value);
      }
    }

    public string MatchWordsTip
    {
      get
      {
        return (string) this.GetValue(MetroSearchBox.MatchWordsTipProperty);
      }
      set
      {
        this.SetValue(MetroSearchBox.MatchWordsTipProperty, (object) value);
      }
    }

    public bool MatchDiacritics
    {
      get
      {
        return (bool) this.GetValue(MetroSearchBox.MatchDiacriticsProperty);
      }
      set
      {
        this.SetValue(MetroSearchBox.MatchDiacriticsProperty, (object) value);
      }
    }

    public string MatchDiacriticsTip
    {
      get
      {
        return (string) this.GetValue(MetroSearchBox.MatchDiacriticsTipProperty);
      }
      set
      {
        this.SetValue(MetroSearchBox.MatchDiacriticsTipProperty, (object) value);
      }
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.part_MatchCaseToggle = this.GetTemplateChild("PART_MatchCaseToggle") as ToggleButton;
      if (this.part_MatchCaseToggle == null)
        throw new NullReferenceException("'{0}' template part is not found on 'MetroSearchBox' control"+((object) "PART_MatchCaseToggle"));
      this.part_MatchWordsToggle = this.GetTemplateChild("PART_MatchWordsToggle") as ToggleButton;
      if (this.part_MatchWordsToggle == null)
        throw new NullReferenceException("'{0}' template part is not found on 'MetroSearchBox' control"+((object) "PART_MatchWordsToggle"));
      this.part_MatchDiacriticsToggle = this.GetTemplateChild("PART_MatchDiacriticsToggle") as ToggleButton;
      if (this.part_MatchDiacriticsToggle == null)
        throw new NullReferenceException("'{0}' template part is not found on 'MetroSearchBox' control"+((object) "PART_MatchDiacriticsToggle"));
      this.part_ProgressBar = this.GetTemplateChild("PART_ProgressBar") as ProgressBar;
      if (this.part_ProgressBar == null)
        throw new NullReferenceException("'{0}' template part is not found on 'MetroSearchBox' control"+((object) "PART_ProgressBar"));
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      Duration duration = new Duration(TimeSpan.FromMilliseconds(350.0));
      doubleAnimation1.Duration = duration;
      double? nullable1 = new double?(1.0);
      doubleAnimation1.From = nullable1;
      double? nullable2 = new double?(0.0);
      doubleAnimation1.To = nullable2;
      DoubleAnimation doubleAnimation2 = doubleAnimation1;
      Storyboard.SetTarget((DependencyObject) doubleAnimation2, (DependencyObject) this.part_ProgressBar);
      Storyboard.SetTargetProperty((DependencyObject) doubleAnimation2, new PropertyPath("Opacity", new object[0]));
      Storyboard storyboard = new Storyboard();
      storyboard.Children.Add((Timeline) doubleAnimation2);
      storyboard.Begin();
      this.part_MatchCaseToggle.IsEnabled = this.part_MatchWordsToggle.IsEnabled = this.part_MatchDiacriticsToggle.IsEnabled = this.Text!="";
      if (this.Text=="")
        this.MatchCase = this.MatchWords = this.MatchDiacritics = false;
      base.OnTextChanged(e);
    }
  }
}
