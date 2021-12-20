using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalProjectFA21.Utility;

namespace FinalProjectFA21
{
    class Hawk : Consumer , Guano
    {
        
        int Guano.GuanoMultiplier()
        {
            return Count;
        }

    }
}
