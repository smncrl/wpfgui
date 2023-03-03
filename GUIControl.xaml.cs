using System.Windows;
using System.Windows.Controls;

namespace wpfgui;

/// <summary>
/// Interaction logic for GUIControl.xaml
/// </summary>
public partial class GUIControl : UserControl
{
    public string Label
    {
        get => this.LabelTextBlock.Text;
        set => this.LabelTextBlock.Text = value;
    }

    public GUIControl()
    {
        InitializeComponent();
    }

    public void SetInput(UIElement input)
    {
        this.InputContent.Children.Clear();
        this.InputContent.Children.Add(input);
    }

    public void SetDecorator(UIElement input)
    {
        this.DecoratorContent.Children.Clear();
        this.DecoratorContent.Children.Add(input);
        this.DecoratorContent.Visibility = Visibility.Visible;
    }
}
