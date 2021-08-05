﻿using System.Collections.Generic;

namespace Curve
{
    public class PointD
    {
        public PointD()
        {
        }

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public PointD(PointD p)
        {
            X = p.X;
            Y = p.Y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public static PointD operator +(PointD p1, PointD p2)
        {
            return new(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static PointD operator *(PointD p, double d)
        {
            return new(p.X * d, p.Y * d);
        }

        public static PointD operator *(double d, PointD p)
        {
            return p * d;
        }
    }

    public static class InterpolateUtils
    {
        public static List<PointD> GetSmoothCurve(this List<PointD> points, double tension, int iterationCount)
        {
            if (points == null || points.Count < 3)
            {
                return null;
            }

            iterationCount = iterationCount switch
            {
                < 1 => 1,
                > 10 => 10,
                _ => iterationCount
            };

            tension = tension switch
            {
                < 0 => 0,
                > 1 => 1,
                _ => tension
            };

            double cutDistance = 0.05 + (tension * 0.4);
            
            List<PointD> result = new List<PointD>();

            for (int i = 0; i <= points.Count - 1; i++)
            {
                result.Add(new PointD(points[i]));
            }

            for (int i = 1; i <= iterationCount; i++)
            {
                result = GetSmoothCurve(result, cutDistance);
            }

            return result;
        }

        public static List<PointD> GetSmoothCurve(this List<PointD> points, double cutDistance)
        {
            List<PointD> result = new List<PointD>
            {
                new (points[0])
            };

            for (int i = 0; i <= points.Count - 2; i++)
            {
                var p1 = (1 - cutDistance) * points[i] + cutDistance * points[i + 1];
                var p2 = cutDistance * points[i] + (1 - cutDistance) * points[i + 1];

                result.Add(p1);
                result.Add(p2);
            }
            
            result.Add(new PointD(points[^1]));

            return result;
        }
    }
}
