using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class Bike: Vehicle
    {
        public Bike(string aRegnum):base(aRegnum) //Set Regnumber in vehcle class (parent class)
        {
            var settings = Configuration.ReadSettingsFromFile();// instance the settings class to get our values from our json file
            size = settings.BikeSize;
            Price = settings.BikePrice;
        }
    }
}
