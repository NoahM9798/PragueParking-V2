using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Figgle.Fonts;
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
                    string carReg = askRegNumber();
                    Car car = new Car(carReg);
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
                    string mcReg = askRegNumber();
                    MC mc = new MC(mcReg);
                    break;
            }
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

    }
}
