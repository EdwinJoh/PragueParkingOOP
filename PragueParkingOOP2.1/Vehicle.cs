using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class Vehicle
    {
        
        public string RegNumber { get; set; }
        public int size { get; set; }
        public DateTime TimeIn { get; set; }
        public int Price { get; set; }
        public Vehicle(string aRegnum)
        {
            TimeIn = DateTime.Now;
            RegNumber = aRegnum;
        }
        
    }
}
