using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratesVisualization
{
    public class Island
    {
        public double pTresure { get; set; } // must be a number between 0 and 1
        public float Reward { get; set; } // the reward earned if you got the tresure
        public float Damage { get; set; } // the Damage from not getting any tresure
                                          // (losing additional resources)

        public Island(double pTresure, float reward, float damage)
        {
            this.pTresure = pTresure;
            Reward = reward;
            Damage = damage;
        }

        public void setAsStartIsland()
        {
            this.pTresure = 1;
            Reward = 0;
            Damage = 0;
        }

        public float ExpectedVal() // Expected val is the expected reward from
                                   // visiting an island
        {
            return Reward * (float)pTresure - Damage * (float)(1 - pTresure);
        }

        public override string ToString()
        {
            return $"pTresure = {pTresure}    reward = {Reward}    damage = {Damage}";
        }
    }
}
