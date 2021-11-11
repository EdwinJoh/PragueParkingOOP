using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class ParkingSpot
    {
        List<ParkingSpot> parkingSpots = new List<ParkingSpot>();
        private int seed = 1;
        public int AvailableSize { get; set; }
        public int SpotNumber { get; set; }
        public ParkingSpot()
        {
        var set = new Configuration();
            AvailableSize = set.ParkingSpotSize;
            SpotNumber = seed;
            seed++;
        }
    }
}
