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
        public DialogToUser Message { get; set; }

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
            Message = new DialogToUser();
        }
        public void AddCar(string regNum)
        {
            Car newCar = new Car(regNum);
            ParkingSpot spot = FirstAvailableSpace(newCar);
            if (spot != null)
            {
                spot.AddVehicle(newCar);
                Settings.UpdateParkingList(ParkingList);
                Message.SuccsessMessage("Parked");
            }
            else
            {
                Message.ParkinghouseFull();
            }
        }
        public void AddMc(string regNum)
        {
            MC newMc = new MC(regNum);
            ParkingSpot spot = FirstAvailableSpace(newMc);
            if (spot != null)
            {
                spot.AddVehicle(newMc);
                Settings.UpdateParkingList(ParkingList);
                Message.SuccsessMessage("Parked");
            }
            else
            {
                Message.ParkinghouseFull();
            }
        }
        public bool RemoveVehicle(string regNum, out int price)
        {
            foreach (var spot in ParkingList)
            {
                foreach (var vehicle in spot.vehicles)
                {
                    if (vehicle.RegNumber == regNum)
                    {
                        string checkIn = vehicle.TimeIn.ToString();
                        CalculatePrice(checkIn, vehicle, out price);
                        spot.Remove(vehicle);
                        Settings.UpdateParkingList(ParkingList);
                        return true;
                    }
                }
            }
            price = 0;
            return false;
        }
        public ParkingSpot FirstAvailableSpace(Vehicle vehicle)
        {
            ParkingSpot spot = ParkingList.Find(x => x.AvailableSize >= vehicle.size);
            return spot;
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
        public bool CheckReg(string regnum, out Vehicle vehicle)
        {
            foreach (var spots in ParkingList)
            {
                foreach (var vh in spots.vehicles)
                {
                    if (vh.RegNumber == regnum)
                    {
                        vehicle = vh;
                        return true;
                    }
                }
            }
            vehicle = null;
            return false;
        }
        public bool MoveVehicle(string RegNum)
        {
            Message.AskForNewSpot(out int newSpot);
            if (newSpot != 0)
            {
                Vehicle vehicle = FindVehicle(RegNum);
                if (CheckNewSpot(newSpot, vehicle, out ParkingSpot spot))
                {
                    RemoveVehicle(RegNum, out int price);
                    spot.AddVehicle(vehicle);
                    Settings.UpdateParkingList(ParkingList);
                    Console.Clear();
                    Message.SuccsessMessage("Moved");
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
        public Vehicle FindVehicle(string regNum)
        {
            foreach (var spot in ParkingList)
            {
                foreach (var vehicle in spot.vehicles)
                {
                    if (vehicle.RegNumber == regNum)
                    {
                        return vehicle;
                    }
                }
            }
            return null;
        }
        public (Vehicle?, ParkingSpot?) LookForVehicle(string RegNumber)
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

        }

    }
