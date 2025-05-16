using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratesVisualization
{
    public class PirateShip
    {
        public int StartIsland { get; set; }
        public int Resources { get; set; }

        public PirateShip(int startIsland, int resources)
        {
            StartIsland = startIsland;
            Resources = resources;
        }

        public override string ToString()
        {
            return $"startIsland = {StartIsland}    resources = {Resources}";
        }
    }
}
