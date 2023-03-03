using System;
using System.Reflection;
using System.Windows.Controls;

namespace wpfgui;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class Gui : Attribute
{
    private object _value;
    private double[] _range;

    public object Value
    {
        get => _value;
        private set => _value = value;
    }

    public Gui()
    {
    }

    public Gui(double rangeMin = 0, double rangeMax = 1)
    {
        this._range = new double[] { rangeMin, rangeMax };
    }

    public void RenderField(FieldInfo fieldInfo, object obj, Panel panel)
    {
        this._value = fieldInfo.GetValue(obj);

        //ENUM
        if (this.IsEnum(this._value))
        {
            Array options = Enum.GetValues(fieldInfo.FieldType);
            var selectedIndex = Array.IndexOf(options, this._value);

            var comboBox = new ComboBox();
            comboBox.ItemsSource = options;
            comboBox.SelectedIndex = selectedIndex;

            var control = new GUIControl();
            control.Label = fieldInfo.Name;
            control.SetInput(comboBox);

            panel.Children.Add(control);

            return;
        }

        //NUMERIC
        if (this.IsNumericType(this._value) && this._range != null)
        {
            var slider = new GUIControls.GUISlider(fieldInfo.Name, this._value, this._range[1], this._range[0]);
            slider.Changed += (object value) =>
            {
                this.Value = value;

                //set the value on class instance's field, using Reflection
                fieldInfo.SetValue(obj, Convert.ChangeType(value,
                    fieldInfo.FieldType
                ));
            };

            panel.Children.Add(slider.Control);

            return;
        }

        //STRING
        var input = new GUIControls.GUIInput(fieldInfo.Name, this._value);
        input.Changed += (object value) =>
        {
            this.Value = value;

            //set the value on class instance's field, using Reflection
            fieldInfo.SetValue(obj, Convert.ChangeType(value,
                fieldInfo.FieldType
            ));
        };

        panel.Children.Add(input.Control);
    }


    public void RenderMethod(MethodInfo methodInfo, object obj, Panel panel)
    {
        var button = new Button();
        button.Content = methodInfo.Name;
        button.Click += (s, e) =>
        {
            methodInfo.Invoke(obj, null);
        };

        var control = new GUIControl();
        control.Label = methodInfo.Name;
        control.SetInput(button);

        panel.Children.Add(control);
    }


    public bool IsEnum(object o) { return o is Enum; }

    public bool IsNumericType(object o)
    {
        switch (Type.GetTypeCode(o.GetType()))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return true;
            default:
                return false;
        }
    }
}//class
