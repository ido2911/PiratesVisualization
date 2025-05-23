using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratesVisualization
{
    internal class ACO_Solver
    {
        Random rand = new Random();

        // constants

        // will remain unchanged due to high net values in simulation
        private const double ALPHA = 0.05; // Influence of pheromone
        private const double BETA = 0.02;  // Influence of heuristic
        private const double Q_FACTOR = 1.0; // Scaling factor for pheromone deposit based on Net Value


        // dynamic values

        public double[,] edges { get; }
        public double[,] pheromoneTrails { get; }
        public Island[] islands { get; }
        public PirateShip[] pirateShips { get; }

        public int numOfShips { get; }
        public int numOfIslands { get; }

        // Aco parameters

        public int iterations { get; } // num of iterations
        public double evaporation { get; } // evaporation

        // pirate states to keep for each iteration

        public PirateState[] pirateStates { get; private set; }

        // values to keep the best results

        public double maxNetValue { get; private set; }
        public Path[] bestPaths { get; private set; }


        public ACO_Solver(Graph graph, PirateShip[] pirateShips, int iterations = 1000, double evaporation = 0.5)
        {
            this.edges = graph.edges;
            this.islands = graph.islands;
            this.pirateShips = pirateShips;
            this.pheromoneTrails = graph.pheremones;

            numOfShips = pirateShips.Length;
            numOfIslands = islands.Length;

            this.iterations = iterations;
            this.evaporation = evaporation;

            pirateStates = new PirateState[numOfShips];
            for (int i = 0; i < numOfShips; i++)
            {
                pirateStates[i] = new PirateState(pirateShips[i], edges, islands);
            }

            maxNetValue = -1;
            bestPaths = new Path[numOfShips];

        }

        public Path[] Solve()
        {

            for (int k = 0; k < iterations; k++)
            {
                RunIteration();
            }

            return bestPaths;
        }

        public void RunIteration()
        {
            PirateState[] pirateStates = new PirateState[numOfShips];
            bool[] visited = new bool[numOfIslands];
            double totalResources = 0;

            for (int i = 0; i < numOfIslands; i++)
                visited[i] = false;

            for (int i = 0; i < numOfShips; i++)
            {
                pirateStates[i] = new PirateState(pirateShips[i], edges, islands);
                visited[pirateShips[i].StartIsland] = true;
                totalResources += pirateShips[i].Resources;
            }

            while (totalResources > 0 && !AllFinished(pirateStates))
            {
                for (int ship = 0; ship < numOfShips; ship++)
                {
                    PirateState currState = pirateStates[ship];

                    int currIsland = currState.currentIslandIndex;
                    List<int> allowedIslands = currState.GetAllowedIslands(visited);

                    if (allowedIslands.Count > 0)
                    {
                        int nextIsland = ChoseNextIsland(currIsland, allowedIslands);
                        double cost = edges[currIsland, nextIsland];


                        // expected val is saved in cost
                        // update on is finished also done here
                        currState.AddStep(currIsland, nextIsland);
                        totalResources -= cost;
                        visited[nextIsland] = true;

                    }
                }
            }

            // pheremones update - evapotation
            for (int i = 0; i < numOfIslands; i++)
            {
                for (int j = i + 1; j < numOfIslands; j++)
                {
                    pheromoneTrails[i, j] = (1 - evaporation) * pheromoneTrails[i, j];
                    pheromoneTrails[j, i] = (1 - evaporation) * pheromoneTrails[j, i];
                }
            }

            double netValueThisIteration = 0;

            // Calculate total net value
            for (int i = 0; i < pirateStates.Length; i++)
            {
                Path path = pirateStates[i].currentPath;
                netValueThisIteration += path.NetValue();
            }

            double collectiveDepositFactor = 0;

            // decide deposit
            if (netValueThisIteration > 0)
                collectiveDepositFactor = Q_FACTOR * netValueThisIteration;


            // collective contribution for pheremone trails
            for (int i = 0; i < pirateStates.Length; i++)
            {
                Path path = pirateStates[i].currentPath;
                foreach (var step in path.islandsPath)
                {
                    pheromoneTrails[step.from, step.to] =
                                pheromoneTrails[step.from, step.to] +
                                collectiveDepositFactor;
                }
            }

            // copy states this iteration
            for (int i = 0; i < pirateStates.Length; i++)
            {
                PirateState state = pirateStates[i];
                this.pirateStates[i] = new PirateState(state);
            }

            if (netValueThisIteration > maxNetValue)
            {
                maxNetValue = netValueThisIteration;

                // copy path to best pathif its better
                for (int i = 0; i < pirateStates.Length; i++)
                {
                    Path path = new Path(pirateStates[i].currentPath);
                    bestPaths[i] = path.Clone();
                }
            }

        }

        #region helper_functions


        private int ChoseNextIsland(int island, List<int> allowed)
        {
            // calculate attractiveness for each island avalible

            Dictionary<int, double> attractivenessMap = new Dictionary<int, double>();
            double totalAtractiveNess = 0;

            foreach (int nextIsland in allowed)
            {
                double pheremone = pheromoneTrails[island, nextIsland];

                double cost = edges[island, nextIsland];
                double heuristic = 1 / (double)cost; // cost cant be zero

                // Standard ACO attractiveness formula (pheromone ^ alpha * heuristic ^ beta)
                double attractiveness = Math.Pow(pheremone, ALPHA) *
                                            Math.Pow(heuristic, BETA);

                attractivenessMap.Add(nextIsland, attractiveness);
                totalAtractiveNess += attractiveness;
            }

            // calculate probalilities
            Dictionary<int, double> probabilitiesMap = new Dictionary<int, double>();

            foreach (int nextIsland in allowed)
            {
                double probability = attractivenessMap[nextIsland] / totalAtractiveNess;
                probabilitiesMap.Add(nextIsland, probability);
            }

            // Select next island using roulette wheel selection
            double randVal = rand.NextDouble(); // random number between 0 and 1
            double cumulativeProbability = 0.0;

            foreach (int allowedIsland in allowed)
            {
                cumulativeProbability += probabilitiesMap[allowedIsland];
                if (randVal < cumulativeProbability)
                    return allowedIsland;
            }

            // in case no island was chosen
            return allowed[allowed.Count - 1];
        }

        public bool AllFinished(PirateState[] pirateStates)
        {
            for (int i = 0; i < numOfShips; i++)
            {
                if (!pirateStates[i].isFinished)
                    return false;
            }
            return true;
        }

        #endregion

    }
}
