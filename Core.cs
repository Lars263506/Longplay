using Il2CppScheduleOne.Growing;
using MelonLoader;
using Newtonsoft.Json;
using MelonLoader.Utils;
using HarmonyLib;
using Il2CppScheduleOne.ObjectScripts;
using Il2CppScheduleOne.ItemFramework;
using Il2CppScheduleOne.StationFramework;
using Il2CppSystem.Drawing;
using UnityEngine;
using Il2CppFluffyUnderware.DevTools.Extensions;

[assembly: MelonInfo(typeof(Longplay.Core), "Longplay", "1.0.0", "Freshairkaboom", null)]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace Longplay
{
    public class Core : MelonMod
    {
        private static int newGrowthTime;
        private static int newCookDuration;

        public override void OnApplicationStart()
        {
            GameObject.DontDestroyOnLoad(new GameObject("Longplay"));
            LoggerInstance.Msg("Longplay initialized.");
            LoadConfig();

            var harmony = new HarmonyLib.Harmony("com.freshairkaboom.longplay");

            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Plant), "Initialize")]
        private class PlantPatches
        {
            private static void Postfix(Plant __instance)
            {
                __instance.GrowthTime = (int) (newGrowthTime + 0.5);
                MelonLogger.Msg($"Patched Plant Initialize. GrowthTime set to {(int) (newGrowthTime + 0.5)}.");
            }
        }

        [HarmonyPatch(typeof(ChemistryStation), "SetCookOperation")]
        public class ChemistryStationPatches
        {
            private static void Postfix(ChemistryStation __instance)
            {
                if (__instance.CurrentCookOperation == null)
                {
                    MelonLogger.Warning("No current cook operation on chemistry station. Skipping patch.");
                    return;
                }

                // Calculate the adjusted cook time
                int adjustedCookTime = CalculateAdjustedCookTime();

                // Apply the adjusted cook time
                __instance.CurrentCookOperation.Recipe.CookTime_Mins = adjustedCookTime;

                // Log the operation with additional context
                MelonLogger.Msg($"Patched ChemistryStation SetCookOperation. Recipe ID: {__instance.CurrentCookOperation.Recipe.RecipeID}, CookTime set to: {adjustedCookTime}");
            }

            private static int CalculateAdjustedCookTime()
            {
                return (int)(newCookDuration + 0.5);
            }
        }

        private void LoadConfig()
        {
            try
            {
                string configPath = Path.Combine(MelonEnvironment.UserDataDirectory, "LongplayConfig.json");
                if (File.Exists(configPath))
                {
                    // Load existing configuration
                    string json = File.ReadAllText(configPath);
                    var config = JsonConvert.DeserializeObject<Config>(json);
                    newGrowthTime = (int)(config.GrowthTime + 0.5);
                    newCookDuration = (int)(config.CookDuration + 0.5);
                    LoggerInstance.Msg($"Config loaded. GrowthTime set to {newGrowthTime}, CookDuration set to {newCookDuration}.");
                }
                else
                {
                    // Create default configuration
                    var defaultConfig = new Config { GrowthTime = 60, CookDuration = 180 };
                    File.WriteAllText(configPath, JsonConvert.SerializeObject(defaultConfig, Formatting.Indented));
                    newGrowthTime = defaultConfig.GrowthTime;
                    newCookDuration = defaultConfig.CookDuration;
                    LoggerInstance.Msg("Default config created with GrowthTime = 60.0 and CookDuration = 30.0.");
                }
            }
            catch (Exception ex)
            {
                LoggerInstance.Error($"Error loading config: {ex.Message}");
                // Fallback to default values
                newGrowthTime = 60;
                newCookDuration = 30;
            }
        }

        public override void OnDeinitializeMelon()
        {
            LoggerInstance.Msg("Longplay deinitialized.");
        }

        private class Config
        {
            public int GrowthTime { get; set; }
            public int CookDuration { get; set; }
        }
    }
}
