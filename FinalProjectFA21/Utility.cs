using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectFA21
{
    class Utility
    {
        public static Random random = new Random();

        public static Entity FindEntity(List<Entity> entities, string name)
        {
            Entity p = null;
            foreach (Entity i in entities)
            {
                if (i.Name == name)
                {
                    p = i;

                }
                
            }
            return p;
        }

        public static int FindIndex(List<Entity> entities, string species)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].Species == species)
                {
                    return i;
                }
                
            }
            return 0;
        }

        
    }
}
