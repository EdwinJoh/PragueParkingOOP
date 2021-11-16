using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class Bus: Vehicle
    {
        public Bus(string aRegNum) : base(aRegNum) //Set Regnumber in vehcle class (parent class)
        {
            var settings = Configuration.ReadSettingsFromFile();//instance the settings class to get our values from our json file
            size = settings.BusSize;
            Price = settings.BusPrice;
        }
    }
}
