using Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

public class DrawLine : Window
{
    private PointButton pointButton;
    private LineButton lineButton;
    private CancelButton cancelButton;
    private Canvas canvas;
    private List<PointForDrawing> PointList = new List<PointForDrawing>();
    private Rectangle SelectRectangle;
    private Point SelectRectangleStartPoint;
    private Point SelectRectangleEndPoint;


    [STAThread]
    public static void Main()
    {
        new Application().Run(new DrawLine());
    }

    public DrawLine()
    {
        Title = "Draw Lines";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        var grid = new Grid();

        for (int i = 0; i < 2; i++)
        {
            var rowDef = new RowDefinition();
            rowDef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowDef);
        }

        for (int i = 0; i < 4; i++)
        {
            var colDef = new ColumnDefinition();
            colDef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(colDef);
        }

        //grid.ShowGridLines = true;

        Content = grid;

        grid.ColumnDefinitions[grid.ColumnDefinitions.Count - 1].Width = new GridLength(1, GridUnitType.Star);
        var stack = new StackPanel();
        stack.Background = Brushes.LightGray;
        Grid.SetRow(stack, 0);
        Grid.SetColumn(stack, grid.ColumnDefinitions.Count - 1);
        grid.Children.Add(stack);

        pointButton = new PointButton();
        Grid.SetRow(pointButton, 0);
        Grid.SetColumn(pointButton, 0);
        grid.Children.Add(pointButton);
        pointButton.Click += PointButtonOnCkick;

        lineButton = new LineButton();
        Grid.SetRow(lineButton, 0);
        Grid.SetColumn(lineButton, 1);
        grid.Children.Add(lineButton);
        lineButton.Click += LineButtonOnClick;

        cancelButton = new CancelButton();
        Grid.SetRow(cancelButton, 0);
        Grid.SetColumn(cancelButton, 2);
        grid.Children.Add(cancelButton);
        cancelButton.Click += CancelButtonOnClick;


        canvas = new Canvas();
        canvas.Background = Brushes.White;
        grid.RowDefinitions[grid.RowDefinitions.Count - 1].Height = new GridLength(1, GridUnitType.Star);
        Grid.SetRow(canvas, 1);
        Grid.SetColumnSpan(canvas, grid.ColumnDefinitions.Count);
        grid.Children.Add(canvas);
    }

    private void PointButtonOnCkick(object sender, RoutedEventArgs e)
    {
        Unsubscribe();
        canvas.MouseDown += MouseDownDrawPoint;
    }

    private void MouseDownDrawPoint(object sender, MouseButtonEventArgs e)
    {
        var pointForDrawing = new PointForDrawing((int)Mouse.GetPosition(canvas).X, (int)Mouse.GetPosition(canvas).Y);
        PointList.Add(pointForDrawing);
        canvas.Children.Add(pointForDrawing.Ellipse);
    }

    private void LineButtonOnClick(object sender, RoutedEventArgs e)
    {
        Unsubscribe();

        canvas.MouseDown += MouseDownForStartSelecting;
        canvas.MouseMove += MouseMoveForDrawSelectRectangle;
        canvas.MouseUp += MouseUpForFinishSelecting;
    }

    private void MouseDownForStartSelecting(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            SelectRectangle = new Rectangle();
            SelectRectangle.Stroke = Brushes.Black;
            SelectRectangle.StrokeThickness = 2;
            SelectRectangleStartPoint = e.GetPosition(canvas);

            canvas.Children.Add(SelectRectangle);
        }
    }

    private void MouseMoveForDrawSelectRectangle(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Released || SelectRectangle == null)
            return;

        var pos = e.GetPosition(canvas);
        SelectRectangleEndPoint = pos;

        var x = Math.Min(pos.X, SelectRectangleStartPoint.X);
        var y = Math.Min(pos.Y, SelectRectangleStartPoint.Y);

        var w = Math.Max(pos.X, SelectRectangleStartPoint.X) - x;
        var h = Math.Max(pos.Y, SelectRectangleStartPoint.Y) - y;

        SelectRectangle.Width = w;
        SelectRectangle.Height = h;

        Canvas.SetLeft(SelectRectangle, x);
        Canvas.SetTop(SelectRectangle, y);
    }

    private void MouseUpForFinishSelecting(object sender, MouseButtonEventArgs e)
    {
        Thread.Sleep(10);
        canvas.Children.Remove(SelectRectangle);
        SelectRectangle = null;
        DrawLineBetweenPoints(PointList);
    }

    private void DrawLineBetweenPoints(List<PointForDrawing> list)
    {
        List<PointForDrawing> tmpList = new List<PointForDrawing>();
        foreach (var point in PointList)
        {
            if (point.X <= Math.Max(SelectRectangleStartPoint.X, SelectRectangleEndPoint.X) &&
                point.X >= Math.Min(SelectRectangleStartPoint.X, SelectRectangleEndPoint.X) &&
                    point.Y <= Math.Max(SelectRectangleStartPoint.Y, SelectRectangleEndPoint.Y) &&
                    point.Y >= Math.Min(SelectRectangleStartPoint.Y, SelectRectangleEndPoint.Y))
            {
                tmpList.Add(point);
            }
        }

        if (tmpList.Count == 2)
        {
            var lineForDrawing = new LineForDrawing(tmpList[0], tmpList[1]);
            canvas.Children.Add(lineForDrawing.line);
        }
        else if (tmpList.Count > 0 && tmpList.Count != 2)
        {
            MessageBox.Show("Yor select more or less than 2 points", "Select error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CancelButtonOnClick(object sender, RoutedEventArgs e)
    {
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        canvas.MouseDown -= MouseDownDrawPoint;
        canvas.MouseDown -= MouseDownForStartSelecting;
        canvas.MouseMove -= MouseMoveForDrawSelectRectangle;
        canvas.MouseUp -= MouseUpForFinishSelecting;
    }
}