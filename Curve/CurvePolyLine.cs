using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Curve
{
    public class CurvePolyLine : Shape
    {
        public CurvePolyLine()
        {
            Points = new PointCollection();
        }

        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register(
            "Points", typeof(PointCollection), typeof(CurvePolyLine), new PropertyMetadata(default(PointCollection)));

        public PointCollection Points
        {
            get => (PointCollection)GetValue(PointsProperty);
            set => SetValue(PointsProperty, value);
        }

        public static readonly DependencyProperty TensionProperty = DependencyProperty.Register(
            "Tension", typeof(double), typeof(CurvePolyLine), new PropertyMetadata(0.5));

        public double Tension
        {
            get => (double)GetValue(TensionProperty);
            set => SetValue(TensionProperty, value);
        }

        public static readonly DependencyProperty IterationCountProperty = DependencyProperty.Register(
            "IterationCount", typeof(int), typeof(CurvePolyLine), new PropertyMetadata(1));

        public int IterationCount
        {
            get => (int)GetValue(IterationCountProperty);
            set => SetValue(IterationCountProperty, value);
        }

        protected override Geometry DefiningGeometry => DefineGeometry();

        private Geometry DefineGeometry()
        {
            if (Points.Count < 1)
                return new PathGeometry();

            Point[] points = Points
                .Select(p => new PointD(p.X, p.Y))
                .ToList()
                .GetSmoothCurve(Tension, IterationCount)
                .Select(p => new Point(p.X, p.Y))
                .ToArray();

            PathFigure pathFigure = new PathFigure
            {
                StartPoint = points[0]
            };

            pathFigure.Segments.Add(new PolyBezierSegment(points, true));

            PathFigureCollection pthFigureCollection = new PathFigureCollection
            {
                pathFigure
            };

            PathGeometry pthGeometry = new PathGeometry
            {
                Figures = pthFigureCollection
            };

            return pthGeometry;
        }
    }
}
