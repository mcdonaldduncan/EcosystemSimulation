using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalProjectFA21.Simulation;

namespace FinalProjectFA21
{
    class Powerup
    {
        private string name;
        private int costAmount;
        private string costSpecies;
        private string effect;

        public string Name { get => name; set => name = value; }
        public int CostAmount { get => costAmount; set => costAmount = value; }
        public string CostSpecies { get => costSpecies; set => costSpecies = value; }
        public string Effect { get => effect; set => effect = value; }

        public Powerup(string _name, string _effect, string _costSpecies, int _costAmount)
        {
            Name = _name;
            Effect = _effect;
            CostSpecies = _costSpecies;
            CostAmount = _costAmount;

        }
    }
}
