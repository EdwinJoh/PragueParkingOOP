using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Converters;

namespace PragueParkingOOP
{
    public class Configuration
    {
        
        private const string ParkingListPath = @"../../../Files/Parkedvehicles.json";
        private const string PriceListPath = @"../../../Files/PriceList.txt";
        public int ParkingSpotSize { get; set; }            // [DEFAULT]  4
        public int ParkingHouseSize { get; set; }           // [DEFAULT]  100
        public int McSize { get; set; }
        public int CarSize { get; set; }
        public int BusSize { get; set; }
        public int BikeSize { get; set; }
        public int CarPrice { get; set; }
        public int McPrice { get; set; }
        public int BikePrice { get; set; }
        public int BusPrice { get; set; }
        public int FreeMin { get; set; }
        public int SpacesForLargeVehicle { get; set; }
        public string? Currency { get; set; }


        internal static Configuration? ReadSettingsFromFile(string filePath = "../../../Files/ConfigSettings.json")
        {

            if (File.Exists(filePath))
            {
                string settingsJson = File.ReadAllText(filePath);
                var configurations = JsonConvert.DeserializeObject<Configuration>(settingsJson);
                return configurations;
            }
            else
            {
                throw new Exception("File does not exist");
                //TODO: Skapa en ny tom lista åt användaren
            }
        }
        internal static List<ParkingSpot>? ReadParkingList()
        {
            string temp = File.ReadAllText(ParkingListPath);
            var Templist = JsonConvert.DeserializeObject<List<ParkingSpot>>(temp);
            return Templist;
        }
        internal void UpdateParkingList<T>(List<T> list)
        {
            string parkingHouse = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(ParkingListPath, parkingHouse);
        }
        internal List<string> ReadPriceFile()
        {
            List<string> priceFile = File.ReadAllLines(PriceListPath).ToList();
            return priceFile;
        }
       
    }
}

