// See https://aka.ms/new-console-template for more information

using PragueParkingOOP;

var settings = Configuration.ReadSettingsFromFile();
Menu menu = new Menu();
do
{
    menu.MenuSelection();

} while (true);

//TODO: Fixa flytta buss
//TODO: Kolla igenom koden så allt grafisk är correct
//TODO: Fixa clear all parkingspots metod