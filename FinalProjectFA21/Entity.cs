using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalProjectFA21.Utility;

namespace FinalProjectFA21
{
    class Entity
    {
        string name;
        string species;
        int count;
        string foodsource;
        string identity;
        int dailyintake;
        string changeMessage;

        public string Species { get => species; set => species = value; }
        public string Name { get => name; set => name = value; }
        public string Foodsource { get => foodsource; set => foodsource = value; }
        public string Identity { get => identity; set => identity = value; }
        public int Dailyintake { get => dailyintake; set => dailyintake = value; }

        public virtual void Growth(int weatherImpact)
        {
            count += (count / weatherImpact);
        }

        
        public int RequiredFood()
        {
            int i;
            i = Count * Dailyintake;
            return i;
        }

        public event EventHandler<CountChangedEventArgs> CountChanged;

        protected virtual void OnCountChanged(CountChangedEventArgs e)
        {
            CountChanged?.Invoke(this, e);
        }

        public int Count
        {
            get { return count; }
            set
            {
                if (count == value) return;
                int oldCount = count;
                count = value;
                OnCountChanged(new CountChangedEventArgs(oldCount, count));
            }
        }

        public string ChangeMessage { get => changeMessage; set => changeMessage = value; }
        

        public void species_CountChanged(object sender, CountChangedEventArgs e)
        {
            if (e.NewCount != e.LastCount)
                ChangeMessage = $"{species} count has changed from {e.LastCount} to {e.NewCount}.\n";
            
        }
    }

    public class CountChangedEventArgs : EventArgs
    {
        public readonly int LastCount;
        public readonly int NewCount;

        public CountChangedEventArgs(int lastCount, int newCount)
        {
            LastCount = lastCount; NewCount = newCount;
        }
    }
}
