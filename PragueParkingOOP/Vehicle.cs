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
        public int CalculatePrice(string checkIn, Vehicle vehicle)
        {
            var settings = new Configuration();
            TimeSpan span = DateTime.Now - vehicle.TimeIn;
            int totalPrice;
            if (span.TotalMinutes <= settings.FreeMin)
            {
                totalPrice = 0;
            }
            else
            {
                int hours;
                if (span.TotalMinutes <60)// one hour
                {
                    totalPrice = 
                }
            }

            return Price;
        }
    }
}
