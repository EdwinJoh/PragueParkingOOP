using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class Bus: Vehicle
    {
        public Bus(string aRegNum) : base(aRegNum)
        {
            var settings = Configuration.ReadSettingsFromFile();
            size = settings.BusSize;
            Price = settings.BusPrice;
        }
    }
}
