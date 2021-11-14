using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class ParkingSpot
    {
        public  List<Vehicle> vehicles { get; set; } = new List<Vehicle>();
        public int AvailableSize { get; set; }
        public int SpotNumber { get; set; }
        public string Status { get; set; }
        public ParkingSpot()
        {
            var set = Configuration.ReadSettingsFromFile();
            AvailableSize = set.ParkingSpotSize;
            Status = "";                  // standard value

        }
        public bool AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
            AvailableSize -= vehicle.size;
            return true;
        }
        public bool Remove(Vehicle vehicle)
        {
            vehicles.Remove(vehicle);
            AvailableSize += vehicle.size;
            return true;
        }
        public bool Addlargevehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
            return true;
        }
        public bool RemoveLargeVehicle(Vehicle vehicle)
        {
           
            vehicles.Remove(vehicle);
            return true;
        }
    }
}
