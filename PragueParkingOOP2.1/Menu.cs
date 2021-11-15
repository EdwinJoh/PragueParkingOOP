using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Spectre.Console;

namespace PragueParkingOOP
{
    public class Menu
    {
        public DialogToUser Message { get; set; } = new DialogToUser();
        public ParkingHouse parkingHouse = new ParkingHouse();
        public Configuration Settings = Configuration.ReadSettingsFromFile();

        public string MenuOption()
        {
            Console.Clear();
            PrintParkingLot();
            string choice;
            do
            {
                choice = AnsiConsole.Prompt(
                   new SelectionPrompt<string>()
                   .Title("[grey]Prague Parking 2.0[/]")
                   .PageSize(7)
                   .AddChoices(new[] {
                    "Park Vehicle", "Remove Vehicle", "Move Vehicle", "Prices", "Search Vehicle", "Print Vehicles","Change Settings","Exit Program" }));
                return choice;

            } while (choice != "Exit Program");

        }
        public void MenuSelection()
        {
            string menuChoise = MenuOption();
            switch (menuChoise)
            {
                case "Park Vehicle":
                    ParkVehicleType();
                    break;
                case "Remove Vehicle":
                    RemoveVehicle();
                    break;
                case "Move Vehicle":
                    MoveVehicle();
                    break;
                case "Prices":
                    Message.PrintPriceList();
                    break;
                case "Search Vehicle":
                    SearchVehicle();
                    break;
                case "Print Vehicles":
                    Message.PrintVehicles();
                    break;
                case "Change Settings":
                    ChangeSettings();
                    break;
                case "Exit Program":
                    Console.Clear();
                    Console.WriteLine("Exit program...");
                    Thread.Sleep(1500);
                    Environment.Exit(1);
                    break;
            }
            Thread.Sleep(2500);
        }
        private void PrintParkingLot()
        {

            Table t1 = new Table();

            t1.AddColumns("[grey]EMPTY SPOT =[/] [green]GREEN[/]", "[grey]FULL SPOT =[/] [red]RED[/]").Centered().Alignment(Justify.Center);
            AnsiConsole.Write(t1);
            Table newTable = new Table().Centered();
            var parkingSpotColorMarking = "";
            var printResult = "";

            for (int i = 0; i < parkingHouse.ParkingList.Count; i++)
            {
                if (parkingHouse.ParkingList[i].AvailableSize == Settings.ParkingSpotSize)
                {
                    parkingSpotColorMarking = "green";
                }
                else
                {
                    parkingSpotColorMarking = "red";
                }
                printResult += ($"[{parkingSpotColorMarking}] {i + 1}[/] ");
            }
            newTable.AddColumn(new TableColumn(printResult));
            AnsiConsole.Write(newTable);
        }
        public void ParkVehicleType()
        {
            var regNumber = ValidateRegNumber();
            (Vehicle? notFound, ParkingSpot? spot) = parkingHouse.ExistRegnumber(regNumber);// ?= value can be null
            if (regNumber != null && notFound == null)
            {
                var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[grey]What vehicle would you like to add?[/]")
                .PageSize(7)
                .AddChoices(new[] { "Car", "MC", "Bike", "Bus" }));
                switch (choice)
                {
                    case "Car":
                        parkingHouse.CreateVehicle(regNumber, "Car");
                        break;
                    case "MC":
                        parkingHouse.CreateVehicle(regNumber, "Mc");
                        break;
                    case "Bike":
                        parkingHouse.CreateVehicle(regNumber, "Bike");
                        break;
                    case "Bus":
                        parkingHouse.CreateVehicle(regNumber, "Bus");
                        break;
                    default:
                        Console.WriteLine("Vehicle type does not exist yet...");
                        break;
                }
            }
            else
            {
                Message.ErrorCheckReg(regNumber);
            }

        }
        private string? ValidateRegNumber()
        {
            Console.Write("Enter Registration number:");
            string regNum = Console.ReadLine().ToUpper();
            string specialChar = @"^[^\s!.,;¨^:#¤%&/\()=?`´@'£$$€{}[\]]{4,10}$";
            Regex reg = new Regex(specialChar);
            Match match = reg.Match(regNum);
            if (match.Success)
            {

                return regNum;

            }
            return null;
        }
        public bool RemoveVehicle()
        {
            var regNum = ValidateRegNumber();

            if (regNum != null)
            {
                (Vehicle found, ParkingSpot spot) = parkingHouse.ExistRegnumber(regNum);
                if (found != null)
                {
                    parkingHouse.RemoveVehicle(found, spot, out int price);
                    Message.SuccsessMessage("Removed", spot);
                    Message.PriceMessage(price);
                    return true;
                }
                else
                {
                    Message.ErrorCheckReg(regNum);
                }
            }
            else
            {
                Message.ErrorCheckReg(regNum);
            }
            return false;
        }
        public bool MoveVehicle()
        {
            var RegNumber = ValidateRegNumber();
            (Vehicle vehicleFound, ParkingSpot spot) = parkingHouse.ExistRegnumber(RegNumber);
            if (RegNumber != null)
            {
                if (vehicleFound != null && vehicleFound.size <= Settings.ParkingSpotSize && parkingHouse.MoveVehicle(vehicleFound, spot))
                {
                    return true;
                }
                else if (vehicleFound != null && vehicleFound.size > Settings.ParkingSpotSize && parkingHouse.MoveVehicle(vehicleFound, spot))
                {

                }
                else
                {
                    Message.MoveNotCompleted(RegNumber);
                }
            }
            else
            {
                Message.ErrorCheckReg(RegNumber);
            }

            return false;
        }
        public void SearchVehicle()
        {
            var regNumber = ValidateRegNumber();
            (Vehicle found, ParkingSpot spot) = parkingHouse.ExistRegnumber(regNumber);
            if (found != null)
            {
                Message.SearchSuccses(found, spot);
            }
            else
            {
                Message.ErrorCheckReg(regNumber);
            }
        }
        public string ChangeSettings()
        {

            string choice = AnsiConsole.Prompt(
                  new SelectionPrompt<string>()
                  .Title("[yellow]What setting would you like to modify?[/]")
                  .AddChoices(new[] {
                    "Vehicle Settings","Parkinghouse Settings","Price Settings"}));
            switch (choice)
            {
                case "Vehicle Settings":

                    VehicleSettings();
                    break;
                case "Parkinghouse Settings":
                    ParkingHouseSettings();
                    break;
                case "Price Settings":
                    PriceSettings();
                    break;
                default:
                    break;
            }
            return choice;

        }
        public void VehicleSettings()
        {

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[yellow]What Vehicle settings would you like to modify?[/]")
                .AddChoices(new[] {
                   "Car Size" ,"Mc Size","Bike Size","Bus Size"}));
            int newValue = Message.AskForNewValue();
            switch (choice)
            {
                case "Car Size":
                    if (newValue > Settings.CarSize)
                    {
                        Settings.CarSize = newValue;
                        Console.WriteLine(Message.SettingChangeCompleted("Car Size",newValue));
                    }
                    else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));// gör om till ansiconsole
                    }
                    break;
                case "Mc Size":
                    if (newValue > Settings.McSize)
                    {
                        Settings.McSize = newValue;
                    }
                    else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));
                    }
                    break;
                case "Bike Size":
                    if (newValue > Settings.BikeSize)
                    {
                        Settings.BikeSize = newValue;
                    }
                    else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));
                    }
                    break;
                case "Bus Size":
                    if (newValue > Settings.BusSize)
                    {
                        Settings.BusSize = newValue;
                    }
                    else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));
                    }
                    break;
                case "Back":
                    break;
                default:
                    Console.WriteLine("The menu selection does not exist yet");
                    break;
            }
            parkingHouse.WriteSettingsToFile(Settings);
        }
        public void ParkingHouseSettings()
        {
            string choice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("[yellow]What Parkinghouse settings would you like to modify?[/]")
               .AddChoices(new[] {
                   "Parkinghouse Size","Parkingspot Size"}));
            int newValue = Message.AskForNewValue();
            switch (choice)
            {
                case "Parkinghouse Size":
                    if (newValue > Settings.ParkingHouseSize)
                    {
                        Settings.ParkingHouseSize = newValue;
                    }
                    else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));
                    }
                    break;
                case "Parkingspot Size":
                    if (newValue > Settings.ParkingSpotSize)
                    {
                        Settings.ParkingSpotSize = newValue;
                    }
                    else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));
                    }
                    break;
                case "Back":

                    break;
                default:
                    Console.WriteLine("The menu selection does not exist yet");
                    break;
            }
            parkingHouse.WriteSettingsToFile(Settings);
        }
        public void PriceSettings()
        {
            string choice = AnsiConsole.Prompt(
              new SelectionPrompt<string>()
              .Title("[yellow]What Price settings would you like to modify?[/]")
              .AddChoices(new[] {
                  "Car Price","Mc Price","Bike Price","Bus Price","Free minutes"}));
            int newValue = Message.AskForNewValue();
            switch (choice)
            {
                case "Car Price":
                    if (newValue > Settings.CarPrice)
                    {
                        Settings.CarPrice = newValue;
                    }else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));
                    }
                    break;
                case "Mc Price":
                    if (newValue > Settings.McPrice)
                    {
                        Settings.McPrice = newValue;
                    }
                    else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));
                    }
                    break;
                case "Bike Price":
                    if (newValue > Settings.BikePrice)
                    {
                        Settings.BikePrice = newValue;
                    }else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));
                    }
                    break;
                case "Bus Price":
                    if (newValue > Settings.BusPrice)
                    {
                        Settings.BusPrice = newValue;
                    }
                    else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));
                    }
                    break;
                case "Free minutes":
                    if (newValue > Settings.FreeMin)
                    {
                        Settings.FreeMin = newValue;
                    }else
                    {
                        Console.WriteLine(Message.ErrorChangeSettings(newValue));
                    }
                    break;
                default:
                    Console.WriteLine("The menu selection does not exist yet");
                    break;
            }
            parkingHouse.WriteSettingsToFile(Settings);
        }
    }

}
