using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratesVisualization
{
    class Path
    {
        public List<(int from, int to)> islandsPath { get; private set; }
        public int count { get; private set; }
        public double totalCost { get; private set; }
        public double totalExcpectedValue { get; private set; }

        // global veriables
        private double[,] map;
        private Island[] islands;

        public Path(double[,] map, Island[] islands)
        {
            this.map = map;
            this.islands = islands;

            islandsPath = new List<(int from, int to)>();
            count = 0;
            totalCost = 0;
            totalExcpectedValue = 0;
        }

        public Path (Path other)
        {
            islandsPath = new List<(int from, int to)>(other.islandsPath);
            count = islandsPath?.Count ?? 0;
            totalCost = other.totalCost;
            totalExcpectedValue = other.totalExcpectedValue;
        }

        public void AddStep(int source, int target)
        {
            islandsPath.Add((source, target));
            count++;
            totalCost += map[source, target];
            totalExcpectedValue += islands[target].ExpectedVal();
        }

        public Path Clone()
        {
            Path path = new Path(this);

            return path;
        }

        public double NetValue()
        {
            const double EXP_CONST = 1.2;
            const double COST_CONST = 1.0;

            return EXP_CONST * totalExcpectedValue - COST_CONST * totalCost;
        }

        public void UpdateFromSegments(List<(int from, int to)> segments, double[,] edges, Island[] islands)
        {
            map = edges;
            this.islands = islands;
            islandsPath.Clear();
            totalCost = 0;
            totalExcpectedValue = 0;

            foreach (var step in segments)
            {
                islandsPath.Add(step);
                totalCost += map[step.from, step.to];
                totalExcpectedValue += islands[step.to].ExpectedVal();
            }
        }

        public override string ToString()
        {
            string str = "";

            str += $"{islandsPath[0].from}";
            foreach (var step in islandsPath)
            {
                str += $" -> {step.to}";
            }

            return str;
        }
    }
}
