using System.Windows;
using System.Windows.Controls;

public class PointButton : Button
{
    public PointButton()
    {
        Name = "DrawPoint";
        Content = "Point";
        //Margin = new Thickness(5);
        Padding = new Thickness(5);
        HorizontalAlignment = HorizontalAlignment.Center;
        VerticalAlignment = VerticalAlignment.Center;
    }
}