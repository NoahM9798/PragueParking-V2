using ConfigPragueParking;
using PragueParking_V2._0;
ParkingGarage Garage = new ParkingGarage();



while (!Menu.Exit)
{    
    Menu.ShowChoices();
    switch (Menu.Choice)
    {
        case "Park Vehicle":
            Menu.showParkInterface(Garage);
            Data.SaveData(Garage);
            break;
        case "Retrieve Vehicle":
            Menu.showRetrieveInterface(Garage);
            Data.SaveData(Garage);
            break;
        case "Move Vehicle":
            Menu.showMoveInterface(Garage);
            Data.SaveData(Garage);
            break;
        case "Search Vehicle":
            break;
        case "Show Garage":
            Menu.showGarage(Garage);
            break;
        case "Reload Config":
            Garage = Menu.resetGarageInterface(Garage);
            break;
        case "Exit":
            Menu.Exit = true;
            Data.SaveData(Garage);
            break;
    }
}


