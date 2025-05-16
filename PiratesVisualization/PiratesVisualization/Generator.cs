using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratesVisualization
{
    static class Generator
    {

        /// <summary>
        /// responsible for data generation
        /// </summary>

        public const int MIN_COST = 30;
        public const int MAX_COST = 150;
        public const double SMALL_TRAILS_VALUE = 0.1;
        public const double CONNECTION_CHANCE = 0.5;
        public const int MIN_TRESURE = 350;
        public const int MAX_TRESURE = 700;
        public const int MIN_DAMAGE = 200;
        public const int MAX_DAMAGE = 300;
        public const int MIN_RESOURCES = 400;
        public const int MAX_RESOURCES = 450;
        public const int MAX_SPLIT = 4;


        private static Random rand = new Random();
        
        public static int[,] GenerateMap(int dimentions)
        {
            int[,] generatedMap = new int[dimentions, dimentions];

            HashSet<int>[] madeConnections = new HashSet<int>[dimentions];

            for (int i = 0; i < dimentions; i++)
            {
                // init hash sets
                madeConnections[i] = new HashSet<int>();

                // make not connected for simplicity
                for (int j = i + 1; j < dimentions; j++)
                {
                    generatedMap[i, j] = int.MaxValue;
                    generatedMap[j, i] = int.MaxValue;
                }
            }

            // Guarantees Connectivity: Build a simple path connecting all nodes
            // This ensures that there is a path from any node to any other node.
            for (int i = 0; i < dimentions - 1; i++)
            {
                int randCost = rand.Next(MIN_COST, MAX_COST);
                generatedMap[i, i + 1] = randCost;
                generatedMap[i + 1, i] = randCost;

                madeConnections[i].Add(i + 1);
                madeConnections[i + 1].Add(i);
            }

            for (int i = 0; i < dimentions; i++)
            {
                for (int j = i + 1; j < dimentions; j++)
                {
                    // Check if a connection already exists
                    if (!madeConnections[i].Contains(j))
                    {
                        double connectionChance = rand.NextDouble();

                        if (connectionChance < CONNECTION_CHANCE)
                        {
                            int randCost = rand.Next(MIN_COST, MAX_COST);
                            generatedMap[i, j] = randCost;
                            generatedMap[j, i] = randCost;

                            madeConnections[i].Add(j);
                            madeConnections[j].Add(i);
                        }
                        // If chance is >= CONNECTION_CHANCE and no edge existed, it remains int.MaxValue (no connection)
                    }
                }
            }


            return generatedMap;
        }

        public static double[,] GenerateEdgeCostsFromLocations(Point[] islandLocations, Size gridSize)
        {
            double component = Math.Pow(gridSize.Width, 2) + Math.Pow(gridSize.Height, 2);
            double maxDistance = Math.Sqrt(component);
            int numIslands = islandLocations.Length;
            double[,] edges = new double[numIslands, numIslands];

            for (int i = 0; i < numIslands; i++)
                edges[i, i] = 0;

            for (int i = 0; i < numIslands; i++)
            {
                for (int j = i + 1; j < numIslands; j++)
                {
                    double distance = Math.Sqrt(Math.Pow(islandLocations[i].X - islandLocations[j].X, 2) + 
                                            Math.Pow(islandLocations[i].Y - islandLocations[j].Y, 2));
                    double finalCost = (double)(distance / maxDistance) * (MAX_COST - MIN_COST) + MIN_COST;
                    edges[i, j] = finalCost;
                    edges[j, i] = finalCost;
                }
            }

            return edges;
        }

        public static double[,] GeneratePheramones(int islandsNum, double[,] edges)
        {
            double[,] trails = new double[islandsNum, islandsNum];

            for (int i = 0; i < islandsNum; i++)
            {
                // same island, no connection
                trails[i, i] = 0;
            }

            for (int i = 0; i < islandsNum; i++)
            {
                for (int j = i + 1; j < islandsNum; j++)
                {
                    trails[i, j] = SMALL_TRAILS_VALUE;
                    trails[j, i] = SMALL_TRAILS_VALUE;
                }
            }

            return trails;
        }

        public static Island[] GenerateIslands(int islandsNum)
        {
            Island[] list = new Island[islandsNum];
            for (int i = 0; i < islandsNum; i++)
            {
                double randPtresure = rand.NextDouble();
                int randomReward = rand.Next(MIN_TRESURE, MAX_TRESURE);
                int randomDamage = rand.Next(MIN_DAMAGE, MAX_DAMAGE);
                list[i] = new Island(randPtresure, randomReward, randomDamage);
            }

            return list;
        }

        public static PirateShip[] GeneratePirates(int piratesNum, Island[] islands)
        {
            int islandsNum = islands.Length;
            if (piratesNum > islandsNum / MAX_SPLIT)
            {
                // makes sure a maximum amount of pirates is made
                piratesNum = islandsNum / MAX_SPLIT;
            }


            PirateShip[] list = new PirateShip[piratesNum];
            bool[] arcupied = new bool[islandsNum];

            for (int i = 0; i < piratesNum; i++)
            {
                int randStartIsland = rand.Next(0, islandsNum);
                int tries = 0;
                while (arcupied[randStartIsland] && tries < 1000)
                {
                    randStartIsland = rand.Next(0, islandsNum);
                    tries++;
                }
                arcupied[randStartIsland] = true;
                int randResources = rand.Next(MIN_RESOURCES, MAX_RESOURCES);
                list[i] = new PirateShip(randStartIsland, randResources);
            }

            for (int ship = 0; ship < piratesNum; ship++)
            {
                islands[list[ship].StartIsland].setAsStartIsland();
            }

            return list;
        }
    }
}
