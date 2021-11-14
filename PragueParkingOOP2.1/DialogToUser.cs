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
        //public Menu menu;
        public Menu menu { get; set; }
        public Configuration? settings { get; set; } = Configuration.ReadSettingsFromFile();
        public void SuccsessMessage(string option, ParkingSpot spot)
        {
            var table = new Table();
            table.AddColumn($"[green] We succsesfully {option} your vehicle! Parked at parkingspot:{spot.SpotNumber}[/] ");
            AnsiConsole.Write(table);
        }
        public void ParkinghouseFull()
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
            if (check)
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
            table.AddColumn($"[green]Price to pay :{price}[/] ");
            AnsiConsole.Write(table); ;
        }
        public void PrintPriceList()
        {
            List<string> prices = settings.ReadPriceFile();
            var table = new Table();
            table.Expand();
            table.AddColumn(new TableColumn(new Markup("[yellow] PRICE LIST[/]").Alignment(Justify.Center)));
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
            table.AddColumn($"[green]Found vehicle:{vehicle.RegNumber}. Its parked at Parkingspot{spot.SpotNumber}[/] ");
            AnsiConsole.Write(table);

        }
        public void PrintVehicles()
        {
            var pHouse = new ParkingHouse();
            List<Vehicle> list = pHouse.GetParkedVehicles();
            var table = new Table();
            table.Expand();
            table.AddColumn(new TableColumn(new Markup("[yellow] PARKED VEHICLES [/]").Alignment(Justify.Center)));
            foreach (var vehicle in list)
            {
                table.AddRow(vehicle.RegNumber);
            }
            AnsiConsole.Write(table);
           
        }
       
        public int AskForNewValue()
        {
            Console.WriteLine(settings);// ändra text
            Console.Write("Enter new settings Value");
            string userInput = Console.ReadLine();
            bool check = int.TryParse(userInput, out int newValue);
            if (check)
            {
                return newValue;
            }else
            {
                var tabel = new Table();
                tabel.AddColumn($"[red]Something went wrong. Please check your input:{userInput}. It can only be an integer.[/]");
                AnsiConsole.Write(tabel);
            }
            return 0;
        }

    }
}
