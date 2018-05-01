using System.Collections.Generic;
using System.Windows;

namespace SubwayNavigation
{
    public class Station
    {
        public int stationId { get; set; }
        public string stationName { get; set; }
        public Point stationLocation { get; set; }
        public string stationLine { get; set; }
        public List<int> stationRailways { get; set; }
        public string stationStatus { get; set; }
        public string stationLineColor { get; set; }

        public Station()
        {
            stationRailways = new List<int>();
        }
    }
}
