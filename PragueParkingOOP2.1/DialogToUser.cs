using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Spectre.Console;

namespace PragueParkingOOP
{
    public class DialogToUser
    {
        /// <summary>
        /// Here is out dialog class where we show messages to the user deppending on what they do in the program.
        /// </summary>
        public Configuration? settings { get; set; } = Configuration.ReadSettingsFromFile(); //instance the settings class to get our values from our json file
        public void SuccsessMessage(string option, ParkingSpot spot)
        {
            var table = new Table();
            table.AddColumn($"[green] We succsesfully {option} your vehicle! Parked at parkingspot:{spot.SpotNumber}[/] ");
            AnsiConsole.Write(table);
        }
        public void ParkingHouseFull()
        {
            var tabel = new Table();
            tabel.AddColumn($"[red]We do not have any available space for your vehicle.Please comback Later..[/]");
            AnsiConsole.Write(tabel);
        }
        public void ErrorCheckReg(string regNum)
        {
            var tabel = new Table();
            tabel.AddColumn($"[red]Something went wrong. Check if {regNum} is parked here or if there is any special characters in the regnumber[/]");
            AnsiConsole.Write(tabel);
        }
        public int AskForNewSpot(out int newSpot)
        {
            Console.Write("Enter new ParkingSpot:");
            string userInput = Console.ReadLine();
            bool check = int.TryParse(userInput, out newSpot);
            if (check && newSpot != 0)
            {
                return newSpot;
            }
            else
            {
                var tabel = new Table();
                tabel.AddColumn($"[red]Something went wrong. Please check your input:{userInput}. It can only be an integer.[/]");
                AnsiConsole.Write(tabel);
            }
            return 0;
        }
        public void PriceMessage(int price)
        {
            var table = new Table();
            table.AddColumn($"[green]Price to pay: {price}:{settings.Currency}[/] ");
            AnsiConsole.Write(table); 
        }
        public void PrintPriceList()
        {
            List<string> prices = settings.ReadPriceFile();
            var table = new Table();
            table.Expand();
            table.AddColumn(new TableColumn(new Markup("[yellow] PRICE LIST [/]").Alignment(Justify.Center)));
            foreach (var price in prices)
            {
                table.AddRow(price);
            }
            AnsiConsole.Write(table);
            var newTable = new Table();
            newTable.AddColumn(new TableColumn("[grey] Press any key to get back to the menu[/]").Centered()).Alignment(Justify.Center);
            AnsiConsole.Write(newTable);
            Console.ReadKey();
        }
        public void MoveNotCompleted(string regNum)
        {
            var table = new Table();
            table.AddColumn($"[red]Someing went wrong, we could not complete the move. Check if there is space left in the parkingspot you tried to move the vehicle:{regNum}[/] ");
            AnsiConsole.Write(table);
        }
        public void SearchSuccses(Vehicle vehicle, ParkingSpot spot)
        {
            var table = new Table();
            table.AddColumn($"[green]Found vehicle:{vehicle.RegNumber}. Its parked at Parkingspot> {spot.SpotNumber}[/] ");
            AnsiConsole.Write(table);
        }
        public void PrintVehicles()
        {
            var pHouse = new ParkingHouse();

            List<Vehicle> list = pHouse.GetParkedVehicles();
            var table = new Table();
            table.AddColumn("[yellow]Parked Vehicle[/]");
            table.AddColumn("[yellow]parking stated [/]");
            table.AddColumn("[yellow]Price per hour[/]");
            table.Expand();

            foreach (var vehicle in list)
            {
                table.AddRow(vehicle.RegNumber, vehicle.TimeIn.ToString("G"), vehicle.Price.ToString());
            }

            AnsiConsole.Write(table);
            Table newtable = new Table();
            newtable.AddColumn("Press any key to go back to the menu").Alignment(Justify.Center);
            AnsiConsole.Write(newtable);
            Console.ReadKey();

        }
        public int AskForNewValue()
        {
            Console.WriteLine("Changing settings value");
            Console.Write("Enter new settings Value: ");
            string userInput = Console.ReadLine();
            bool check = int.TryParse(userInput, out int newValue);
            if (check)
            {
                return newValue;
            }
            else
            {
                var tabel = new Table();
                tabel.AddColumn($"[red]Something went wrong. Please check your input:{userInput}. It can only be an integer.[/]");
                AnsiConsole.Write(tabel);
            }
            return 0;
        }
        public void ErrorChangeSettings(int value)
        {
            var table = new Table();
            table.AddColumn($"[red]There have been an error trying to change the setting. \nValue '{value}' is smaller then the original setting. We can only adjust it to a higher number.\nRestart the program.[/] ");
            AnsiConsole.Write(table);
        }
        public string SettingChangeCompleted(string setting, int value)
        {
            return $"Settings '{setting}' was succsessfully changed to new value{value}. Please restarting the program";
        }
        public void BigVehiceSuccsess(string option, int newspot, ParkingSpot spot)
        {
            var table = new Table();
            table.AddColumn($"[green] We succsesfully {option} your vehicle! Parked at parkingspot:{newspot}-{+spot.SpotNumber}[/] ");
            AnsiConsole.Write(table);
        }
    }
}
