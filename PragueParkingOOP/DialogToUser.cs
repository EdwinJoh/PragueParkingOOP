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
        public void SuccsessMessage(string option)
        {
            var table = new Table();
            table.AddColumn($"[green] We succsesfully {option} your vehicle![/] ");
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
            tabel.AddColumn($"[red]Something went wrong. Please check your input {regNum} so its valid or if there is space left in the parkinghouse[/]");
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
        
    }
}
