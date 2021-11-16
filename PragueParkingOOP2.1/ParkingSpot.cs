using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class ParkingSpot
    {
        /// <summary>
        /// Metod use for our invidial parkingspots. Add and Remove vehicles from the spot.
        /// </summary>
        public  List<Vehicle> vehicles { get; set; } = new List<Vehicle>();
        public int AvailableSize { get; set; }
        public int SpotNumber { get; set; }
        public string Status { get; set; }                          // Use to seperate for our larger vehicles 
                public ParkingSpot()                                        // Construct the parkingspot, sets the value necessary for one spot
        {
            var set = Configuration.ReadSettingsFromFile();
            AvailableSize = set.ParkingSpotSize;
            Status = "";

        }
        public bool AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
            AvailableSize -= vehicle.size;
            return true;
        }                  // Adding vehicles that take one or less space 
        public bool Remove(Vehicle vehicle)                         // Removing vehiclees that take one or less space 
        {
            vehicles.Remove(vehicle);
            AvailableSize += vehicle.size;
            return true;
        }               
        public bool Addlargevehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
            return true;
        }             // adding larger vehicles that take up more then one parkingspot
        public bool RemoveLargeVehicle(Vehicle vehicle)
        {
           
            vehicles.Remove(vehicle);
            return true;
        }          // removing larger vehicles that take up more the one parkingspot
    }
}
