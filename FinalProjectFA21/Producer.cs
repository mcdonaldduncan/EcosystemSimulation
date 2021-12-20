using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectFA21
{
    class Producer : Entity
    {
        public override void Growth(int weatherImpact)
        {
            int sporeCreation;
            sporeCreation = Count * 5;
            Count = Count + sporeCreation / weatherImpact;

        }

      
    }
}
