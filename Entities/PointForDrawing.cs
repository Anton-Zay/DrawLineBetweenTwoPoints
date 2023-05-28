using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Entities
{
    public class PointForDrawing
    {
        public static int Id;
        public int X;
        public int Y;
        public Ellipse Ellipse;
        public PointForDrawing(int x, int y)
        {
            this.X = x;
            this.Y = y;
            Id++;

            Ellipse = new Ellipse();
            Ellipse.Width = 4;
            Ellipse.Height = 4;
            Ellipse.StrokeThickness = 2;
            Ellipse.Stroke = Brushes.Black;
            Ellipse.Margin = new Thickness(X - 2, Y - 2, 0, 0);
        }
    }
}