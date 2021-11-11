using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    internal class MC:Vehicle
    {
        public MC(string _RegNum):base(_RegNum)
        {
            size = Configuration.McSize;

        }
    }
}
