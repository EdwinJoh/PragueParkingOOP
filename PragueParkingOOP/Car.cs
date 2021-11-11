using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    internal class Car: Vehicle
    {
        
        public Car(string aRegnum):base(aRegnum)
        {
            var settings = Configuration.ReadSettingsFromFile();
            size = settings.CarSize;
           
        }
    }
}
