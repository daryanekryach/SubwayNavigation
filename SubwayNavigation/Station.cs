using System.Collections.Generic;
using System.Windows;

namespace SubwayNavigation
{
    public class Station
    {
        public int stationId;
        public string stationName { get;set; }
        public Point stationLocation;
        public string stationLine { get; set; }
        public List<int> stationRailways = new List<int>();
        public string stationStatus { get; set; }
        public string stationLineColor { get; set; }
    }
}
