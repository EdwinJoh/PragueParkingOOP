using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingOOP
{
    public class ParkingHouse
    {

        public List<ParkingSpot> ParkingList { get; set; } = Configuration.ReadParkingList();
        public Configuration Settings { get; set; } = Configuration.ReadSettingsFromFile();
        public DialogToUser Message { get; set; } = new DialogToUser();

        public ParkingHouse()
        {

            if (ParkingList == null)
            {
                ParkingList = new List<ParkingSpot>(capacity: 100);
                NewParkingHouse();
            }
            else
            {
                ExpandParkinghouse();
            }
            //Message = new DialogToUser();
        }
        public void CreateVehicle(string regNum, string type)
        {
            switch (type)
            {
                case "Car":
                    Car newcar = new Car(regNum);
                    AddVehicle(newcar);
                    break;
                case "Mc":
                    MC newMc = new MC(regNum);
                    AddVehicle(newMc);
                    break;
                case "Bike":
                    Bike newBike = new Bike(regNum);
                    AddVehicle(newBike);
                    break;
                case "Bus":
                    Bus newBus = new Bus(regNum);
                    AddVehicle(newBus);
                    break;
                default:
                    break;
            }


        }
        public bool AddVehicle(Vehicle vehicle)
        {
            var set = Configuration.ReadSettingsFromFile();
            ParkingSpot spot = FirstAvailableSpace(vehicle);
            if (spot != null && vehicle.size <= set.ParkingSpotSize)
            {
                spot.AddVehicle(vehicle);
                Settings.UpdateParkingList(ParkingList);
                Message.SuccsessMessage("Parked", spot);
                return true;
            }
            else if (spot != null && vehicle.size > set.ParkingSpotSize)
            {

                return true;
            }
            else
            {
                Message.ParkinghouseFull();

            }
            return false;
        }
        public bool RemoveVehicle(Vehicle vehicle, ParkingSpot spot, out int price)
        {

            string checkIn = vehicle.TimeIn.ToString();
            CalculatePrice(checkIn, vehicle, out price);
            spot.Remove(vehicle);
            Settings.UpdateParkingList(ParkingList);
            return true;

            price = 0;
            return false;
        }
        public ParkingSpot FirstAvailableSpace(Vehicle vehicle)
        {
            List<ParkingSpot> Templist = new List<ParkingSpot>();
            var set = Configuration.ReadSettingsFromFile();
            if (vehicle.size <= set.ParkingSpotSize)
            {
                ParkingSpot spot = ParkingList.Find(x => x.AvailableSize >= vehicle.size);
            }
            else
            {
                for (int i = 0; i < set.SpacesForLargeVehicle; i++)
                {
                    if (ParkingList[i].AvailableSize == set.ParkingSpotSize)
                    {
                        Templist.Add(ParkingList[i]);
                    }
                    else if (ParkingList[i].AvailableSize < set.ParkingSpotSize)
                    {
                        Templist.Clear();
                        continue;
                    }
                    if (Templist.Count == vehicle.size / set.ParkingSpotSize)
                    {
                        foreach (var spots in Templist)
                        {

                        }
                    }
                }
            }
            
            return null;
        }
        public void NewParkingHouse()
        {
            for (int i = 0; i < Settings.ParkingHouseSize; i++)
            {
                ParkingList.Add(new ParkingSpot { SpotNumber = i + 1 });
            }
        }
        public void ExpandParkinghouse()
        {
            for (int i = ParkingList.Count; i < Settings.ParkingHouseSize; i++)
            {
                ParkingList.Add(new ParkingSpot { SpotNumber = i + 1 });
            }
        }
        public bool MoveVehicle(Vehicle vehicle, ParkingSpot oldSpot)
        {
            Message.AskForNewSpot(out int newSpot);
            if (newSpot != 0)
            {
                if (CheckNewSpot(newSpot, vehicle, out ParkingSpot spot))
                {
                    RemoveVehicle(vehicle, oldSpot, out int price);
                    spot.AddVehicle(vehicle);
                    Settings.UpdateParkingList(ParkingList);
                    Message.SuccsessMessage("Moved", spot);
                    return true;
                }
            }
            return false;
        }
        public bool CheckNewSpot(int newspot, Vehicle vehicle, out ParkingSpot newSpot)
        {
            foreach (var spot in ParkingList)
            {
                if (spot.SpotNumber == newspot && spot.AvailableSize >= vehicle.size)
                {
                    newSpot = spot;
                    return true;

                }
            }
            newSpot = null;
            return false;
        }
        public (Vehicle?, ParkingSpot?) ExistRegnumber(string RegNumber)
        {
            foreach (var spot in ParkingList)
            {
                foreach (Vehicle vehicle in spot.vehicles)
                {
                    if (vehicle.RegNumber == RegNumber)
                    {
                        return (vehicle, spot);
                    }
                    continue;
                }
            }
            return (null, null);
        }
        public int CalculatePrice(string checkIn, Vehicle vehicle, out int price)
        {

            TimeSpan span = DateTime.Now - vehicle.TimeIn;
            Configuration? set = Configuration.ReadSettingsFromFile();
            if (span.TotalMinutes <= set.FreeMin)
            {
                price = 0;
            }
            else
            {
                int hours;
                if (span.TotalMinutes < 60)// one hour
                {
                    price = vehicle.Price;
                }
                else
                {
                    hours = (int)span.TotalMinutes / 60;
                    hours++;
                    price = hours * vehicle.Price;
                }
            }
            return price;
        }
        public List<Vehicle> GetParkedVehicles()
        {
            var list = new List<Vehicle>();
            foreach (var parkingspots in ParkingList)
            {
                foreach (var v in parkingspots.vehicles)
                {
                    if (v != null)
                    {
                        list.Add(v);
                    }
                }
            }
            return list;
        }
    }

}
