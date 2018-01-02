using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class PointEqualityComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point x, Point y)
        {
            return x.X.Equals(y.X) && x.Y.Equals(y.Y);
        }

        public int GetHashCode(Point obj)
        {
            return -2;
        }
    }
}
