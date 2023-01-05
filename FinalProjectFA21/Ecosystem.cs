using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using static FinalProjectFA21.DataLoad;
using static FinalProjectFA21.Utility;

namespace FinalProjectFA21
{
    enum EnvironmentStatus
    {
        Balanced,
        Unbalanced,
        Critical
    }
    class Ecosystem
    {
        public TemperatureReading currentTemperature = CurrentTemperature();
        public List<Entity> entities = LoadEntities("../../data/entities.xml");
        public EnvironmentStatus status = EnvironmentStatus.Balanced;
        int weatherImpact;
        string handlerWrite;

        public string ReturnTemp()
        {
            return currentTemperature.Temperature.ToString();
        }

        public EnvironmentStatus SetStatus()
        {
            foreach (Entity i in entities)
            {
                if (i.Count < 10)
                {
                    if (i.Count < 5)
                    {
                        status = EnvironmentStatus.Critical;
                        return status;
                    }
                    status = EnvironmentStatus.Unbalanced;
                    return status;
                }
                // do fix
                status = EnvironmentStatus.Balanced;
            }
            return status;
        }

        public string WriteList()
        {
            string contents = "";

            for (int i = 0; i < entities.Count; i++)
            {
                contents += ($"{i}: {entities[i].Name} : {entities[i].Species}\n");
            }
            return contents;
        }
        //Divide everything by a year to slow changes?
        
        public void IterateEcosystem()
        {
            SetStatus();
            currentTemperature.ChangeTemp();
            weatherImpact = 365 / (int)currentTemperature.Temperature;
            foreach (Entity i in entities)
            {
                NoMax(i);
                NoNegative(i);
                CheckCountChange(i);
                foreach (Entity n in entities)
                {
                    if (n.Identity == i.Foodsource)
                    {
                        if (n.Count > i.RequiredFood())
                        {
                            n.Count -= (i.RequiredFood()) / weatherImpact;
                            Waste(entities, i, n, weatherImpact);
                            EntityGrowth(i, weatherImpact);
                        }
                        else
                        {
                            //this might be the problem, check it out
                            FindEntity(entities, "Biotic").Count += i.Count / weatherImpact;
                            i.Count -= (i.RequiredFood() - n.Count) / weatherImpact;
                        }
                    }
                }
            } 
        }

        public string HandlerWrite()
        {
            handlerWrite = "";
            foreach (Entity i in entities)
            {
                handlerWrite += i.ChangeMessage;
                
            }
            return handlerWrite;
        }

        public void CheckCountChange(Entity i)
        {
            i.CountChanged += i.species_CountChanged;
        }

        public void NoNegative(Entity i)
        {
            if (i.Count < 1)
            {
                i.Count = 1;
            }
        }

        public void NoMax(Entity i)
        {
            if (i.Count >= 2047483647)
            {
                i.Count = 2047483647;
            }
        }

        public void ProduceWaste(List<Entity> entities, Entity i, Entity n, string species, int weatherImpact)
        {
            FindEntity(entities, species).Count += (n.Count + i.RequiredFood()) / weatherImpact;
        }

        public void Waste(List<Entity> entities, Entity i, Entity n, int weatherImpact)
        {
            if (i.Identity == "Bat")
            {
                ProduceWaste(entities, i, n, "Guano", weatherImpact);
            }
            else if (i.Identity == "Beetle")
            {
                ProduceWaste(entities, i, n, "Soil", weatherImpact);
            }
            else
            {
                ProduceWaste(entities, i, n, "Biotic", weatherImpact);
            }
        }

        public void EntityGrowth(Entity i, int weatherImpact)
        {
            Producer p;
            Consumer c;
            Decomposer d;
            if (i is Producer producer)
            {
                p = producer;
                p.Growth(weatherImpact);
            }
            else if (i is Consumer consumer)
            {
                if (i is Guano guano)
                {
                    FindEntity(entities, "Guano").Count += guano.GuanoMultiplier();
                }
                c = consumer;
                c.Growth(weatherImpact);
            }
            else if (i is Decomposer decomposer)
            {
                d = decomposer;
                d.Growth(weatherImpact);
            }
        }


    }
}
