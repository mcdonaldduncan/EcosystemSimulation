using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalProjectFA21.Utility;

namespace FinalProjectFA21
{
    class Decomposer : Entity
    {
        public override void Growth(int weatherImpact)
        {
            int breedingPairs;
            breedingPairs = Count / 2;
            Count += (breedingPairs / random.Next(1, 3) / weatherImpact);

        }

    }
}
