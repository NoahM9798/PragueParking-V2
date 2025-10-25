//Skriv ut menyn
using ConfigPragueParking;
using PragueParking_V2._0;
using Spectre.Console;
Console.WriteLine($"Does file exist? {File.Exists(".. / .. / .. / .. /config.json")}");
Console.WriteLine($"Looking for config at: {Path.GetFullPath("../../../../")}");
ParkingGarage Garage = new ParkingGarage();



while (!Menu.Exit)
{    
    Menu.ShowChoices();
    switch (Menu.Choice)
    {
        case "Park Vehicle":
            Menu.parkOptions(Garage);
            break;
        case "Retrieve Vehicle":
            break;
        case "Move Vehicle":
            break;
        case "Search Vehicle":
            break;
        case "Show Garage":
            break;
        case "Reload price list":
            Vehicle v = new Vehicle("");
            Console.WriteLine(v.VehicleType);
            Console.ReadLine();
            break;
        case "Exit":
            Menu.Exit = true;
            Data.SaveData(Garage);
            break;
    }
}


