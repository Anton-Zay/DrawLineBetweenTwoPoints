using System.Windows;
using System.Windows.Controls;

public class CancelButton : Button
{
    public CancelButton()
    {
        Name = "CancelButtonn";
        Content = "Cancel";
        //Margin = new Thickness(5);
        Padding = new Thickness(5);
        HorizontalAlignment = HorizontalAlignment.Center;
        VerticalAlignment = VerticalAlignment.Center;
        IsCancel = true;
    }
}