using Harmony;
using System;
using System.Reflection;
using UnityEngine;
using UnityModManagerNet;


namespace SuperBots
{
    internal static class Main
    {
        public static UnityModManager.ModEntry mod;
        
        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            mod = modEntry;
            var harmony = HarmonyInstance.Create(modEntry.Info.Id);
            harmony.Patch(
                original: AccessTools.Method(typeof(Worker), nameof(Worker.GetTotalMaxInstuctions)),
                prefix: new HarmonyMethod(typeof(Main), nameof(Main.GetTotalMaxInstuctions))
            );

            Type type = typeof(Worker);
            FieldInfo info = type.GetField("m_LoseEnergy", BindingFlags.NonPublic | BindingFlags.Static);
            info.SetValue(null, false);
            Debug.Log("MOD LOADED");
            return true;
        }

        static bool GetTotalMaxInstuctions(Worker __instance, ref int __result)
        {
            Debug.Log("HARMONY PATCH CALLED");
            __result = 100;
            return false;
        }
    }
}
