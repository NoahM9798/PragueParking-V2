using System;
using System.IO;
using Newtonsoft.Json;

namespace ConfigPragueParking
{
    public class ConfigManager
    {
        private static readonly string configPath = "../../../../config.json";

        public static GarageConfig LoadConfig()
        {
            if (!File.Exists(configPath))
            {
                // File does not exist → create default config
                var defaultConfig = new GarageConfig
                {
                    SpotSize = 4,
                    GarageSize = 100,
                    CarSize = 4,
                    MCSize = 2
                };

                SaveConfig(defaultConfig);
                return defaultConfig;
            }

            var json = File.ReadAllText(configPath);

            // Deserialize using Newtonsoft.Json
            return JsonConvert.DeserializeObject<GarageConfig>(json);
        }

        public static void SaveConfig(GarageConfig config)
        {
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, json);
        }

        public static void SaveGarage(string fileName, string json)
        {
            File.WriteAllText(fileName, json);
        }

        public static string LoadGarage(string fileName)
        {
            return File.ReadAllText(fileName);
        }
    }
}