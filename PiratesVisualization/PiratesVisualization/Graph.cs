using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratesVisualization
{
    class Graph
    {
        public Island[] islands { get; }
        public double[,] edges { get; }
        public double[,] pheremones { get; }

        public Graph(int numOfNodes, Point[] islandLocations, Size gridSize)
        {
            islands = Generator.GenerateIslands(numOfNodes);
            edges = Generator.GenerateEdgeCostsFromLocations(islandLocations, gridSize);
            pheremones = Generator.GeneratePheramones(numOfNodes, edges);
        }

        public Graph(Island[] islands, Point[] islandLocations, Size gridSize)
        {
            this.islands = islands;
            this.edges = Generator.GenerateEdgeCostsFromLocations(islandLocations, gridSize);
            pheremones = Generator.GeneratePheramones(islands.Length, edges);
        }

        public Island GetIsland(int index)
        {
            if (index>= islands.Length)
                return null;
            return islands[index];
        }

        public double GetEdge(int source, int target)
        {
            if (source>= 0 && source < islands.Length && target>= 0 && target < islands.Length)
                return edges[source, target];
            return 0;
        }

        public double GetPheremone(int source, int target)
        {
            if (source >= 0 && source < islands.Length && target >= 0 && target < islands.Length)
                return pheremones[source, target];
            return 0;
        }
    }
}
