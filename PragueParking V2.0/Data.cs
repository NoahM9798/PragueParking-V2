using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ConfigPragueParking;

namespace PragueParking_V2._0
{
    public class Data
    {
        private static readonly string dataPath = "../../../../garageData.json";

        public static void SaveData(ParkingGarage garage)
        {
            string json = JsonConvert.SerializeObject(garage.ParkingSpots, Formatting.Indented);
            ConfigManager.SaveGarage(dataPath, json);
        }

        public static void LoadData(ParkingGarage garage)
        {
            if (!File.Exists(dataPath))
                return; // No saved data yet → garage will initialize fresh

            string json = ConfigManager.LoadGarage(dataPath);
            var parkingSpots = JsonConvert.DeserializeObject<List<ParkingSpot>>(json);

            if (parkingSpots != null && parkingSpots.Count > 0)
            {
                garage.ParkingSpots = parkingSpots;
            }
        }
    }
}