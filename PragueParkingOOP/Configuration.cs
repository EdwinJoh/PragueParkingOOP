using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class Configuration
    {
        private const string configPath = @"../../../Files/ConfigSettings.json";
        private const string ParkingListPath = @"../../../Files/Parkedvehicles.json";
        public int ParkingSpotSize { get; set; }        // [DEFAULT]  4
        public int ParkingHouseSize { get; set; }           // [DEFAULT]  100
        public int McSize { get; set; }
        public int CarSize { get; set; }
        public int CarPrice { get; set; }
        public int McPrice { get; set; }
        public int FreeMin { get; set; }



        public static Configuration? ReadSettingsFromFile(string filePath = "../../../Files/ConfigSettings.json")
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
                //Skapa en ny tom lista åt användaren
            }
            
        }
        public static List<ParkingSpot>? ReadParkingList()
        {
            string temp = File.ReadAllText(ParkingListPath);
            var Templist = JsonConvert.DeserializeObject<List<ParkingSpot>>(temp);
            return Templist;
        }
        public void UpdateParkingList<T>(List<T> list)
        {
            string parkingHouse = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(ParkingListPath, parkingHouse);
        }
    }
}

