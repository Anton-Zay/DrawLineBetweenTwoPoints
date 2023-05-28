using System.Windows;
using System.Windows.Controls;

public class LineButton : Button
{
    public LineButton()
    {
        Name = "DrawLine";
        Content = "Line";
        //Margin = new Thickness(5);
        Padding = new Thickness(5);
        HorizontalAlignment = HorizontalAlignment.Center;
        VerticalAlignment = VerticalAlignment.Center;
    }
}