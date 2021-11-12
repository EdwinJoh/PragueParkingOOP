using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class Bike: Vehicle
    {
        public Bike(string aRegnum):base(aRegnum)
        {
            var settings = Configuration.ReadSettingsFromFile();
            size = settings.BikeSize;
            Price = settings.BikePrice;
        }
    }
}
