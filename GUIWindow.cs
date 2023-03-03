using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace wpfgui;

public static class GUIWindow
{
    private static Window window;
    private static Panel panel;

    public static void Register(object obj)
    {
        Type t = obj.GetType();
        MemberInfo[] members = t.GetMembers();

        if (window == null)
        {
            panel = new StackPanel();
            window = new Window();
            window.Content = panel;
            window.Title = "GUI";

            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FF232323");
            window.Background = brush;
            window.Show();
        }

        for (int i = 0; i < members.Length; i++)
        {
            var member = members[i];
            var guiField = (Gui)Attribute.GetCustomAttribute(members[i], typeof(Gui));

            if (guiField == null)
            {
                continue;
            }

            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    guiField.RenderField(((FieldInfo)member), obj, panel);
                    break;
                case MemberTypes.Method:
                    guiField.RenderMethod(((MethodInfo)member), obj, panel);
                    break;
                default:
                    throw new NotImplementedException();

            }
        }
    }
}
