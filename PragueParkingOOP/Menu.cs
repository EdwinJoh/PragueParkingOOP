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
        public ParkingHouse house = new ParkingHouse();
        public Configuration configuration { get; set; }

        public string MenuOption()
        {
            Console.Clear();
            ParkingHouse pHouse = new ParkingHouse();
            PrintParkingLot();
            string choice;
            do
            {
                choice = AnsiConsole.Prompt(
                   new SelectionPrompt<string>()
                   .Title("[grey]Prague Parking 2.0[/]")
                   .PageSize(7)
                   .AddChoices(new[] {
                    "Park Vehicle", "Remove Vehicle", "Move Vehicle", "Prices", "Search Vehicle", "Print Vehicles","Exit Program" }));
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
                    break;
                case "Print Vehicles":
                    break;
                case "Exit Program":
                    break;
            }
            Thread.Sleep(1500);
        }
        private void PrintParkingLot()
        {

            Table t1 = new Table();
            var settings = Configuration.ReadSettingsFromFile();
            t1.AddColumns("[grey]EMPTY SPOT =[/] [green]GREEN[/]", "[grey]FULL SPOT =[/] [red]RED[/]").Centered().Alignment(Justify.Center);
            AnsiConsole.Write(t1);

            Table newTable = new Table().Centered();
            var parkingSpotColorMarking = "";
            var printResult = "";

            for (int i = 0; i < house.ParkingList.Count; i++)
            {
                if (house.ParkingList[i].AvailableSize == settings.ParkingSpotSize)
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
            string regNumber = ValidateRegNumber();
            var message = new DialogToUser();
            if (regNumber != null && house.CheckReg(regNumber) == false)
            {
                var choice = AnsiConsole.Prompt(
              new SelectionPrompt<string>()
              .Title("[grey]What vehicle would you like to add?[/]")
              .PageSize(7)
              .AddChoices(new[] {
                    "Car", "MC" }));
                switch (choice)
                {
                    case "Car":
                        house.AddCar(regNumber);
                        break;
                    case "MC":
                        house.AddMc(regNumber);
                        break;
                    default:
                        Console.WriteLine("Vehicle type does not exist yet...");
                        break;
                }
            }
            else
            {
                message.ErrorCheckReg(regNumber);
            }

        }
        private string ValidateRegNumber()
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
            string regNum = ValidateRegNumber();

            if (regNum != null)
            {
                if (house.CheckReg(regNum))
                {
                    house.RemoveVehicle(regNum, out int price);
                    Message.SuccsessMessage("Removed");
                    Message.PriceMessage(price);
                    return true;
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
            string vehicleReg = ValidateRegNumber();
            if (vehicleReg != null)
            {
                if (house.CheckReg(vehicleReg) && house.MoveVehicle(vehicleReg))
                {
                    return true;
                }
                else
                {
                    Message.MoveNotCompleted(vehicleReg);
                }
            }
            else
            {
                Message.ErrorCheckReg(vehicleReg);
            }

            return false;
        }
    }

}
