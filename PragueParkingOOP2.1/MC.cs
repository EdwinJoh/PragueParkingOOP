using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class MC : Vehicle
    {
        public MC(string _RegNum) : base(_RegNum)
        {
            var settings = Configuration.ReadSettingsFromFile(); //instance the settings class to get our values from our json file
            Price = settings.McPrice;
            size = settings.McSize;
        }
    }
}
