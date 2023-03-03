using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace wpfgui.GUIControls;

public class GUIControlBase
{
    public GUIControl Control;
}

public class GUISlider : GUIControlBase
{
    public double Value;
    public Slider Slider;

    public delegate void ChangeDelegate(object value);
    public event ChangeDelegate Changed;

    public GUISlider(string name, object value, double max = 1, double min = 0)
    {
        this.Control = new GUIControl();
        this.Control.Label = name;

        var textBox = new TextBox() { Text = Convert.ToString(value) };

        this.Slider = new Slider() { Value = Convert.ToDouble(value) };
        this.Slider.Minimum = min;
        this.Slider.Maximum = max;
        this.Slider.HorizontalAlignment = HorizontalAlignment.Stretch;
        this.Slider.VerticalAlignment = VerticalAlignment.Stretch;
        this.Slider.Foreground = Brushes.Red;

        this.Slider.ValueChanged += (s, e) =>
        {
            this.Value = this.Slider.Value;
            textBox.Text = this.Slider.Value.ToString("F");
            this.Changed?.Invoke(this.Slider.Value);
        };

        this.Control.SetInput(this.Slider);
        this.Control.SetDecorator(textBox);

    }
}

public class GUIInput : GUIControlBase
{
    public string Value;
    public TextBox TextBox;

    public delegate void ChangeDelegate(object value);
    public event ChangeDelegate Changed;

    public GUIInput(string name, object value)
    {
        this.Control = new GUIControl();
        this.Control.Label = name;

        this.TextBox = new TextBox() { Text = Convert.ToString(value) };
        this.TextBox.TextChanged += (s, e) =>
        {
            this.Value = this.TextBox.Text;
            this.Changed?.Invoke(this.Value);
        };

        this.Control.SetInput(this.TextBox);
    }
}
