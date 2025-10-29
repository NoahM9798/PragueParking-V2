using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Figgle.Fonts;
using System.Diagnostics.Metrics;
using ConfigPragueParking;
namespace PragueParking_V2._0
{

    //Displays the menu and handles user choices
    public class Menu
    {

        //Variables
        public static List<string> choices = [
        "Park Vehicle",
        "Retrieve Vehicle",
        "Move Vehicle",
        "Search Vehicle",
        "Show Garage",
        "Reload Config",
        "Exit"
        ];
        public static string Choice;
        public static bool Exit = false;

        //Method to display choices
        public static void ShowChoices()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
            new FigletText("Prague Parking V2")
                .Centered()
                .Color(Color.Cyan1));

            Choice = AnsiConsole.Prompt(
                       new SelectionPrompt<string>()
                       .Title("[red bold]Choose one of the following options:[/]")
                       .AddChoices(choices)
                       );
        }

        public static void showParkInterface(ParkingGarage garage)
        {
            var parkChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[red bold]Choose vehicle type to park:[/]")
            .AddChoices(new[] {
                           "Car",
                           "Motorcycle",
                           "Bicycle",
                           "Bus"
            })
            );
            switch (parkChoice)
            {
                case "Car":
                    string carReg = askRegNumber();
                    Car car = new Car(carReg);
                    if (garage.getVehicleByReg(car.RegNumber) != null)
                    {
                        //Vehicle with the same registration number already exists
                        AnsiConsole.MarkupLine("[red]A vehicle with the same registration number is already parked![/]");
                        pause();
                        break;
                    }
                    if (garage.TryParkVehicle(car))
                    {
                        //Successfully parked
                        AnsiConsole.MarkupLine("[green]Car parked successfully![/]");
                        pause();
                        break;
                    }
                    AnsiConsole.MarkupLine("[red]No available spot for the car![/]");
                    pause();
                    break;
                case "Motorcycle":
                    string mcReg = askRegNumber();
                    MC mc = new MC(mcReg);
                    if (garage.getVehicleByReg(mc.RegNumber) != null)
                    {
                        //Vehicle with the same registration number already exists
                        AnsiConsole.MarkupLine("[red]A vehicle with the same registration number is already parked![/]");
                        pause();
                        break;
                    }
                    if (garage.TryParkVehicle(mc))
                    {
                        //Successfully parked
                        AnsiConsole.MarkupLine("[green]MC parked successfully![/]");
                        pause();
                        break;
                    }
                    AnsiConsole.MarkupLine("[red]No available spot for the MC![/]");
                    pause();
                    break;
                case "Bicycle":
                    AnsiConsole.MarkupLine("[yellow]Bicycle parking not implemented yet![/]");
                    pause();
                    break;
                case "Bus":
                    AnsiConsole.MarkupLine("[yellow]Bus parking not implemented yet![/]");
                    pause();
                    break;

            }
        }
        
        public static ParkingGarage resetGarageInterface(ParkingGarage garage)
        {
            bool confirmResetGarage = AnsiConsole.Confirm("If you reload config with changes to spot or vehicle size, \n" +
                "or removed spots with vehicles on them,\n" +
                "the garage and its vehicles will reset for the renovation, " +
            "are you sure you want to continue?");
            if (confirmResetGarage)
            {
                Data.Config = ConfigManager.LoadConfig();
                //First we need to see if user only changed garage size and removed spots with no vehicles
                //Then we dont need to remove all the vehicles, we can just add/decrease number of spots.
                if (Data.canHaveGarageOpenDuringRenovation(garage))
                {
                    //We can keep the garage open, just need to adjust spots
                    garage.AdjustSize(Data.Config.GarageSize);
                    AnsiConsole.MarkupLine("[green]Garage spots changed successfully![/]");
                    pause();
                    return garage;
                }
                //Here sizes of spots or vehicles has been changed, we need to reset the whole garage
                ParkingGarage Garage = new ParkingGarage(true);
                AnsiConsole.MarkupLine("[green]Garage reset successfully![/]");
                pause();
                return Garage;
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Garage reset cancelled.[/]");
                pause();
                return garage;
            }
        }


        public static void showRetrieveInterface(ParkingGarage garage)
        {
            string reg = askRegNumber();
            Vehicle vehicleToGet = garage.getVehicleByReg(reg);
            if (vehicleToGet != null)
            {
                //It exists, lets geeet it'
                if (garage.TryRemoveVehicle(vehicleToGet))
                {
                    AnsiConsole.MarkupLine($"[green]Vehicle retrieved successfully! Total cost: {garage.FinalPrice(vehicleToGet, DateTime.Now)}CZK[/]");
                    pause();
                    return;
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No vehicle with such registration number found![/]");
                pause();
                return;
            }
        }
        public static void showMoveInterface(ParkingGarage garage)
        {
            string reg = askRegNumber();
            Vehicle vehicleToMove = garage.getVehicleByReg(reg);
            if (vehicleToMove != null)
            {
                //Vehicle exists, lets move it to the spot desired
                int newSpot = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter the spot number to move the vehicle to:")
                    .Validate((input) => input switch
                    {
                        _ when input < 1 || input > garage.ParkingSpots.Count => ValidationResult.Error("[red]Invalid spot number[/]"),
                        _ => ValidationResult.Success(),
                    })
                );
                if (garage.TryParkVehicle(vehicleToMove, newSpot))
                {
                    AnsiConsole.MarkupLine($"[green]Vehicle moved successfully to spot {newSpot}![/]");
                    pause();
                    return;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Failed to move vehicle. The spot may be full or too small.[/]");
                    pause();
                    return;
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No vehicle with such registration number found![/]");
                pause();
                return;
            }
        }

        public static void showGarage(ParkingGarage garage)
        {
            var table = new Table();
            table.AddColumn(new TableColumn("Spot Number").Centered());
            table.AddColumn(new TableColumn("Vehicles").Centered());
            table.AddColumn(new TableColumn("Arrival Time").Centered());
            foreach (var spot in garage.ParkingSpots)
            {
                if (spot.VehiclesParked.Count > 0)
                {
                    foreach (Vehicle v in spot.VehiclesParked)
                    {
                        table.AddRow(spot.SpotNumber.ToString(), v.ToString(), v.ArrivalTime.ToString());
                    }
                }
                else
                {
                    table.AddRow(spot.SpotNumber.ToString(), "Empty", "NULL");
                }
            }
            AnsiConsole.Write(table);

            pause();
        }

        public static string askRegNumber()
        {
            string reg = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the registration number:")
                .Validate((input) => input switch
                {
                    _ when input.Length < 6 => ValidationResult.Error("[red]Too short registration number[/]"),
                    _ when input.Length > 10 => ValidationResult.Error("[red]Too long registration number[/]"),
                    _ => ValidationResult.Success(),
                })
            );
            return reg;
        }

        public static string pause()
        {
            AnsiConsole.MarkupLine("[yellow]Press any key to go back...[/]");
            Console.ReadKey();
            return "";
        }

        public static void showSearchResult(Vehicle v, ParkingGarage garage)
        {
            AnsiConsole.MarkupLine("[blue]Search Result:\n[/]");
            AnsiConsole.MarkupLine($"[blue]{v.ToString()} is parked on spot {garage.getSpotNumberForVehicle(v)} and arrived there {v.ArrivalTime.ToString()}[/]");
            pause();
        }
    }
}
