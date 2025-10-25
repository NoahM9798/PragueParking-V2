using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Figgle.Fonts;
using System.Diagnostics.Metrics;
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
        "Reload price list",
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

        public static void parkOptions(ParkingGarage garage)
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
                    string carReg = askRegNumber(garage);
                    Car car = new Car(carReg);
                    if (garage.VehicleExists(car.RegNumber))
                    {
                        //Vehicle with the same registration number already exists
                        AnsiConsole.MarkupLine("[red]A vehicle with the same registration number is already parked![/]\nPress any key to go back...");
                        Console.ReadKey();
                        break;
                    }
                    if (garage.ParkVehicle(car))
                    {
                        //Successfully parked
                        AnsiConsole.MarkupLine("[green]Car parked successfully![/]\nPress any key to go back...");
                        Console.ReadKey();
                        break;
                    }
                    AnsiConsole.MarkupLine("[red]No available spot for the car![/]\nPress any key to go back...");
                    Console.ReadKey();
                    break;
                case "Motorcycle":
                    string mcReg = askRegNumber(garage);
                    MC mc = new MC(mcReg);
                    if (garage.VehicleExists(mc.RegNumber))
                    {
                        //Vehicle with the same registration number already exists
                        AnsiConsole.MarkupLine("[red]A vehicle with the same registration number is already parked![/]\nPress any key to go back...");
                        Console.ReadKey();
                        break;
                    }
                    break;
                case "Bicycle":
                    AnsiConsole.MarkupLine("[yellow]Bicycle parking not implemented yet![/]\nPress any key to go back...");
                    Console.ReadKey();
                    break;
                case "Bus":
                    AnsiConsole.MarkupLine("[yellow]Bus parking not implemented yet![/]\nPress any key to go back...");
                    Console.ReadKey();
                    break;

            }
        }

        public static string askRegNumber(ParkingGarage garage)
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

    }
}
