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
    /// <summary>
    /// Here we mange our settings for our parkinglot. Size, Price, Read and write to our json files
    /// </summary>
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

        internal static Configuration? ReadSettingsFromFile(string filePath = "../../../Files/ConfigSettings.json") // Read from settings file
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Configfile does not exist. Creating an new jsonSettings file: Restart program and write the value in the new file to run the program");
                File.Create(filePath).Close();
                return null;
            }
            else
            {
                string settingsJson = File.ReadAllText(filePath);
                var configurations = JsonConvert.DeserializeObject<Configuration>(settingsJson);
                return configurations;
            }
        }
        internal static List<ParkingSpot>? ReadParkingList() // Read from our json file that have our parkinglist with our parked vehicles
        {
            if (File.Exists(ParkingListPath))
            {
                string temp = File.ReadAllText(ParkingListPath);
                var Templist = JsonConvert.DeserializeObject<List<ParkingSpot>>(temp);
                return Templist;
            }
            else
            {
                Console.WriteLine("Parkinglist file does not exist, we will create an new one for you");
                File.Create(PriceListPath).Close();
                return null;
            }
        }
        internal void UpdateParkingList<T>(List<T> list) // Saves/update the parkinglist every time we do some changes in it. Remove, add, Moves vehicles
        {
            string parkingHouse = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(ParkingListPath, parkingHouse);
        }
        internal List<string> ReadPriceFile() // Read pricelist from json file and return the value in it
        {
            List<string> priceFile = File.ReadAllLines(PriceListPath).ToList();
            return priceFile;
        }

    }
}

