using Il2CppScheduleOne.Growing;
using MelonLoader;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using MelonLoader.Utils; // Ensure you have this library referenced

[assembly: MelonInfo(typeof(Longplay.Core), "Longplay", "1.0.0", "Freshairkaboom", null)]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace Longplay
{
    public class Core : MelonMod
    {
        private float newGrowthTime;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Longplay initialized.");
            LoadConfig();
            ChangeGrowthTime();
        }

        private void LoadConfig()
        {
            try
            {
                string configPath = Path.Combine(MelonEnvironment.UserDataDirectory, "LongplayConfig.json");
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    var config = JsonConvert.DeserializeObject<Config>(json);
                    newGrowthTime = config.GrowthTime;
                    LoggerInstance.Msg($"Config loaded. GrowthTime set to {newGrowthTime}.");
                }
                else
                {
                    // Create a default config file if it doesn't exist
                    var defaultConfig = new Config { GrowthTime = 1.0f };
                    File.WriteAllText(configPath, JsonConvert.SerializeObject(defaultConfig, Formatting.Indented));
                    newGrowthTime = defaultConfig.GrowthTime;
                    LoggerInstance.Msg("Default config created.");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error("Error loading config: " + ex.Message);
                newGrowthTime = 1.0f; // Fallback to default value
            }
        }

        private void ChangeGrowthTime()
        {
            try
            {
                Type type = Type.GetType("Il2CppScheduleOne.Growing.Plant, Assembly-CSharp");
                if (type != null)
                {
                    PropertyInfo property = type.GetProperty("GrowthTime", BindingFlags.Instance | BindingFlags.Public);
                    if (property != null && property.CanWrite)
                    {
                        Plant[] array = UnityEngine.Object.FindObjectsOfType<Plant>(true).ToArray();
                        foreach (Plant obj in array)
                        {
                            property.SetValue(obj, (int)newGrowthTime);
                        }
                        LoggerInstance.Msg("GrowthTime successfully updated.");
                    }
                    else
                    {
                        MelonLogger.Error("GrowthTime property not found or is not writable.");
                    }
                }
                else
                {
                    MelonLogger.Error("Plant class not found.");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error("Error changing GrowthTime: " + ex.Message);
            }
        }

        private class Config
        {
            public float GrowthTime { get; set; }
        }
    }
}
