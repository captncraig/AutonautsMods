using Harmony;
using System;
using System.Reflection;
using UnityModManagerNet;

namespace SuperBots
{
    public static class Main
    {
        public static UnityModManager.ModEntry mod;
        
        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            mod = modEntry;
            var harmony = HarmonyInstance.Create(modEntry.Info.Id);
            harmony.Patch(
                original: AccessTools.Method(typeof(TeachWorkerScriptEdit),
                                            nameof(TeachWorkerScriptEdit.GetFreeMemory)),
                postfix: new HarmonyMethod(typeof(Main), nameof(GetFreeMemory))
                );

            Type type = typeof(Worker);
            FieldInfo info = type.GetField("m_LoseEnergy", BindingFlags.NonPublic | BindingFlags.Static);
            info.SetValue(null, false);

            return true;
        }

        [HarmonyPostfix]
        static void GetFreeMemory(ref int __result)
        {
            __result = 100;
        }
    }
}
