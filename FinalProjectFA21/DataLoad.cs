using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace FinalProjectFA21
{
    class DataLoad
    {
        public static List<Entity> LoadEntities(string fileName)
        {
            List<Entity> entities = new List<Entity>();
            if (File.Exists(fileName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode root = doc.DocumentElement;
                XmlNodeList entityList = root.SelectNodes("/environment/entity");
                foreach (XmlElement entity in entityList)
                {
                    Entity temp;
                    if (entity.GetAttribute("type") == "Producer")
                    {
                        temp = new Producer();
                    }
                    else if (entity.GetAttribute("type") == "Consumer")
                    {
                        temp = new Consumer();
                    }
                    else if (entity.GetAttribute("type") == "Decomposer")
                    {
                        temp = new Decomposer();
                    }
                    else if (entity.GetAttribute("type") == "Player" || entity.GetAttribute("type") == "Vendor")
                    {
                        temp = new Person();
                    }
                    else
                    {
                        temp = new Entity();
                    }
                    temp.Name = entity.GetAttribute("name");
                    temp.Species = entity.GetAttribute("species");
                    try
                    {
                        temp.Count = Int32.Parse(entity.GetAttribute("count"));
                        temp.Dailyintake = Convert.ToInt32(entity.GetAttribute("dailyintake"));
                    }
                    catch (Exception)
                    {
                        temp.Count = 0;
                        temp.Dailyintake = 0;
                    }
                    temp.Foodsource = entity.GetAttribute("foodsource");
                    temp.Identity = entity.GetAttribute("identity");
                    entities.Add(temp);
                }
                //Add count load in as an integer.
                //Add foodsource load.
            }
            return entities;
        }

        private static readonly string APIKey = "3d4080d90b477363d5aab27675306ac6";
        private static readonly string fileName =
            "http://api.openweathermap.org/data/2.5/weather?q=Chicago&mode=xml&units=imperial&APPID=" + APIKey;

        public static TemperatureReading CurrentTemperature()
        {
            string description = "Cloudy";
            string temperatureString = "60";
            float temperature = 0;
            XDocument xdoc = XDocument.Load(fileName);
            var tempList = xdoc.Descendants()
                          .Where(x => x.Name == "temperature" || x.Name == "weather");
            foreach (XElement node in tempList)
            {
                if (node.Name == "temperature")
                { temperatureString = node.Attribute("value").Value; }
                if (float.TryParse(temperatureString, out float t))
                { temperature = t; }
                if (node.Name == "weather")
                { description = node.Attribute("value").Value; }
            }
            return new TemperatureReading(description, temperature);
        }
    }
}
