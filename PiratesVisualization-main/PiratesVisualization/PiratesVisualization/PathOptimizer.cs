using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratesVisualization
{
    static class PathOptimizer
    {

        public static bool PerformTwoOptSwap(List<int> pathNodes, Point[] islandLocations)
        {
            if (pathNodes == null || pathNodes.Count < 4)
            {
                return false;
            }

            for (int i = 0; i < pathNodes.Count - 2; i++) 
            {
                Point p1_loc = islandLocations[pathNodes[i]];
                Point q1_loc = islandLocations[pathNodes[i + 1]];

                for (int j = i + 2; j < pathNodes.Count - 1; j++) 
                {
                    Point p2_loc = islandLocations[pathNodes[j]];
                    Point q2_loc = islandLocations[pathNodes[j + 1]];


                    if (p1_loc.Equals(p2_loc) || p1_loc.Equals(q2_loc) || q1_loc.Equals(p2_loc) || q1_loc.Equals(q2_loc))
                    {
                        continue; 
                    }

                    // Check if the two segments intersect
                    if (GeometryHelper.CheckSegmentsIntersect(p1_loc, q1_loc, p2_loc, q2_loc))
                    {

                        List<int> reversedSegment = new List<int>();
                        for (int k = j; k >= i + 1; k--)
                        {
                            reversedSegment.Add(pathNodes[k]);
                        }


                        List<int> newPathNodes = pathNodes.Take(i + 1).ToList();
                        newPathNodes.AddRange(reversedSegment);
                        newPathNodes.AddRange(pathNodes.Skip(j + 1));

                        pathNodes.Clear();
                        pathNodes.AddRange(newPathNodes);

                        // A swap was performed, so return true
                        return true;
                    }
                }
            }
            return false;
        }

        public static void OptimizePath(List<int> pathNodes, Point[] islandLocations, int maxPasses = -1)
        {
            bool improved;
            int currentPass = 0;
            do
            {
                improved = PerformTwoOptSwap(pathNodes, islandLocations);
                currentPass++;
            } while (improved && (maxPasses == -1 || currentPass < maxPasses));
        }
    }
}
