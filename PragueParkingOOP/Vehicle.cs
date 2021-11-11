using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class Vehicle
    {
        public Configuration Configuration { get; set; }
        public string RegNumber { get; set; }
        public int size { get; set; }
        public DateTime TimeIn { get; set; }
        public Vehicle(string aRegnum)
        { 
            TimeIn = DateTime.Now;
            RegNumber = aRegnum;
        }
    }
}
