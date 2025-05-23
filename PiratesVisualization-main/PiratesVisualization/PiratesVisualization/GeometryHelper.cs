using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratesVisualization
{
    static class GeometryHelper
    {
        private static bool OnSegment(Point p, Point q, Point r)
        {
            return q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                   q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y);
        }

        private static int Orientation(Point p, Point q, Point r)
        {
            long val = (long)(q.Y - p.Y) * (r.X - q.X) -
                       (long)(q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;  
            return (val > 0) ? 1 : 2;
        }

        public static bool CheckSegmentsIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);

            // General case: segments intersect
            if (o1 != 0 && o2 != 0 && o3 != 0 && o4 != 0 && 
                o1 != o2 && o3 != o4) 
            {
                return true;
            }

            // Special Cases: segments are collinear and overlap

            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;

            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;

            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;

            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

            return false;
        }
    }
}
