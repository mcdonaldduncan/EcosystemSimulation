using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalProjectFA21.Utility;
using static FinalProjectFA21.Ecosystem;

namespace FinalProjectFA21
{
    class Bat : Consumer, Guano
    {
        //Guano is not getting called
        int Guano.GuanoMultiplier()
        {
            return Count;
        }

    }
}
