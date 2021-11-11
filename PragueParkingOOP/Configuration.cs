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
        private string path = @"../../../Files/ConfigSettings.json";
        public int ParkingSpotSize { get; set; }        // [DEFAULT]  4
        public int ParkingHouseSize { get; set; }           // [DEFAULT]  100
        public int McSize { get; set; }
        public int CarSize { get; set; }
        public int CarPrice { get; set; }

        //public Configuration(int carSize, int mcSize, int carPrice, int parkingSpotSize, int parkingHouseSize)
        //{

        //    this.CarSize = carSize;
        //    this.McSize = mcSize;
        //    this.CarPrice = carPrice;
        //    this.ParkingHouseSize = parkingHouseSize;
        //    this.ParkingSpotSize = parkingSpotSize;
        //}
       
        public static Configuration ReadSettingsFromFile(string filePath = "../../../Files/ConfigSettings.json")
        {
            string settingsJson = File.ReadAllText(filePath);
            var configurations = JsonConvert.DeserializeObject<Configuration>(settingsJson);
            return configurations;
        }
    }
}

