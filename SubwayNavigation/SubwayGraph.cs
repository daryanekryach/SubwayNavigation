using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml.Linq;

namespace SubwayNavigation
{
    public class SubwayGraph
    {
        public Dictionary<int, List<int>> railwayList { get; set; }
        public List<Station> stations { get; set; }

        public SubwayGraph()
        {
            railwayList = new Dictionary<int, List<int>>();
            stations = new List<Station>();
        }

        public List<Station> GetShortestPath(Station startStation, Station destinationStation)
        {
            Queue<Station> queue = new Queue<Station>();
            List<Station> visited = new List<Station>();
            List<Station> path = new List<Station>();
            Dictionary<int, int> previous = new Dictionary<int, int>();
            queue.Enqueue(startStation);

            while (queue.Count > 0)
            {
                var station = queue.Dequeue();
                foreach (var node in railwayList[station.stationId])
                {
                    if (previous.ContainsKey(node))
                    {
                        continue;
                    }

                    previous[node] = station.stationId;
                    queue.Enqueue(stations[node]);
                }
            }

            var currentStation = destinationStation;
            while (!currentStation.Equals(startStation))
            {
                path.Add(currentStation);
                currentStation = stations[previous[currentStation.stationId]];
            };

            path.Add(startStation);
            path.Reverse();

            return path;
        }

        public void GetGraphDataFromXML()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            XDocument graphData = XDocument.Parse(Properties.Resources.graphdataxml);

            foreach (XElement station in graphData.Descendants("station"))
            {
                Station currentStation = new Station();
                currentStation.stationId = int.Parse(station.Attribute("id").Value);
                currentStation.stationLine = station.Attribute("line").Value;
                currentStation.stationName = station.Element("nameEng").Value;
                double[] location = GetStationLocation(station.Element("location").Value.Split(','));
                currentStation.stationLocation = new Point(location[0], location[1]);
                currentStation.stationRailways = FormateRailwayData(station.Element("railways").Value.Split(','));
                SetStationLineColor(currentStation);
                stations.Add(currentStation);
            }

            foreach (Station station in stations)
                railwayList.Add(station.stationId, station.stationRailways);
        }

        private List<int> FormateRailwayData(string[] data)
        {
            List<int> railways = new List<int>();
            for (int i = 0; i < data.Length; i++)
            {
                railways.Add(int.Parse(data[i]));
            }
            return railways;
        }

        private double[] GetStationLocation(string[] locationData)
        {
            double[] location = new double[2];
            for (int i = 0; i < locationData.Length; i++)
            {
                location[i] = double.Parse(locationData[i], CultureInfo.InvariantCulture);
            }
            return location;
        }

        private void SetStationLineColor(Station station)
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources");
            station.stationLineColor = filePath + "\\" + station.stationLine + ".png";
        }
    }
}
