using System.Windows.Media;
using System.Windows.Shapes;

namespace Entities
{
    public class LineForDrawing
    {
        public static int Id;
        public PointForDrawing StartPoint;
        public PointForDrawing EndPoint;
        public Line line;
        public LineForDrawing(PointForDrawing startPoint, PointForDrawing endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Id++;

            line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 2;
            line.X1 = StartPoint.X;
            line.X2 = EndPoint.X;
            line.Y1 = StartPoint.Y;
            line.Y2 = EndPoint.Y;
        }
    }
}