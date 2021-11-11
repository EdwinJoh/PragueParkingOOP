using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace PragueParkingOOP
{
    public class Menu
    {

        private string MenuOption()
        {
            var choice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("[grey]Prague Parking 2.0[/]")
               .PageSize(7)
               .AddChoices(new[] {
                    "Park Vehicle", "Move vehicle", "Our prices", "Search for vehicle", "Print Vehicles", "Remove","Exit Program" }));
            return choice;
        }
        public void MenuSelection()
        {
            string menuChoise = MenuOption();
            ParkingHouse pHouse = new ParkingHouse();
        }

    }
}
