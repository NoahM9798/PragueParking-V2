using System;
using System.IO;
using System.Text.Json;

namespace ConfigPragueParking
{
    public class ConfigManager
    {
        // Use a relative path or AppContext.BaseDirectory depending on your setup
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

            // Use System.Text.Json to deserialize
            return JsonSerializer.Deserialize<GarageConfig>(json);
        }

        public static void SaveConfig(GarageConfig config)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(config, options);
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