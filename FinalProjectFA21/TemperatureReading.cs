using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalProjectFA21.Utility;

namespace FinalProjectFA21
{
    class TemperatureReading
    {
        public string WeatherDescription { get; set; } = "weather description unavailable";
        public float Temperature { get; set; } = 75.5f;
        public TemperatureReading(string _description, float _temperature)
        {
            WeatherDescription = _description;
            Temperature = _temperature;
        }

        public void ChangeTemp()
        {
            Temperature += random.Next(0, 10);
            Temperature -= random.Next(0, 10);
            if (Temperature > 80)
            {
                Temperature = 80;
            }
            else if (Temperature < -10)
            {
                Temperature = -10;
            }
        }
    }
}
