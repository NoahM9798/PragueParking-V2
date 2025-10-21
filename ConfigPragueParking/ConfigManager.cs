using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConfigPragueParking
{
    public class ConfigManager
    {
        private static readonly string configPath = Path.Combine(AppContext.BaseDirectory, "config.json");

        // Load configuration from JSON file, needs to return values set in GarageConfig class
        public static GarageConfig LoadConfig()
        {
            if (!File.Exists(configPath))
            {
                //File does not exist, create default config
                var defaultConfig = new GarageConfig
                {
                    SpotSize = 4,
                    GarageSize = 100,
                };
                SaveConfig(defaultConfig);
                return defaultConfig; 
            }
            var json = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<GarageConfig>(json);
        }

        public static void SaveConfig(GarageConfig config)
        {
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, json);    
        }
    }
}
