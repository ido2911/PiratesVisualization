using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PiratesVisualization
{
    class PirateState
    {
        public int currentIslandIndex { get; set; }
        public double currentResourcesLeft { get; set; }
        public Path currentPath {  get; private set; }
        public bool isFinished { get; set; }


        // global veriables
        private double[,] map;
        private Island[] islands;
        private int numOfIslands;


        public PirateState(PirateShip pirate, double[,] map, Island[] islands)
        {
            this.map = map;
            this.islands = islands;
            numOfIslands = islands.Length;

            currentIslandIndex = pirate.StartIsland;

            currentResourcesLeft = pirate.Resources;

            currentPath = new Path(map, islands);

            isFinished = pirate.Resources <= 0;
        }

        public PirateState(PirateState other)
        {
            this.map = other.map;
            this.islands = other.islands;
            numOfIslands = other.islands.Length;

            currentIslandIndex = other.currentIslandIndex;

            currentResourcesLeft = other.currentResourcesLeft;

            currentPath = other.currentPath.Clone();

            isFinished = other.currentResourcesLeft <= 0;
        }

        public void AddStep(int source, int target)
        {
            currentPath.AddStep(source, target);
            currentIslandIndex = target;
            currentResourcesLeft -= map[source, target];
            isFinished = currentResourcesLeft <= 0;
        }

        public List<int> GetAllowedIslands(bool[] visited)
        {

            const double COST_CONST = 1.0;
            const double MIN_PT = 0.05;

            List<int> allowed = new List<int>();
            for (int i = 0; i < numOfIslands; i++)
            {
                double cost = map[currentIslandIndex, i];
                double expectedVal = islands[i].ExpectedVal();
                if (currentIslandIndex != i  && !visited[i] && cost != int.MaxValue
                    && currentResourcesLeft >= map[currentIslandIndex, i]
                    && islands[i].pTresure > MIN_PT
                    && expectedVal - COST_CONST * cost >= 0
                    )
                    allowed.Add(i);

            }

            isFinished = allowed.Count == 0;
            return allowed;
        }

        public PirateState Clone()
        {
            PirateState state = new PirateState(this);
            return state;
        }

    }
}
