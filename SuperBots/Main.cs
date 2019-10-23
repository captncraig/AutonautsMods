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
                original: AccessTools.Method(typeof(Worker),"InitStats"),
                postfix: new HarmonyMethod(typeof(Main), nameof(Main.InitStats))
                );

            Type type = typeof(Worker);
            FieldInfo info = type.GetField("m_LoseEnergy", BindingFlags.NonPublic | BindingFlags.Static);
            info.SetValue(null, false);

            return true;
        }

        [HarmonyPostfix]
        static void InitStats()
        {
            Worker.m_AllHeadInfo[0].m_MaxInstructions = 64;
            Worker.m_AllHeadInfo[1].m_MaxInstructions = 256;
            Worker.m_AllHeadInfo[2].m_MaxInstructions = 1024;
            Worker.m_AllHeadInfo[0].m_FindNearestRange = 25;
            Worker.m_AllHeadInfo[1].m_FindNearestRange = 50;
            Worker.m_AllHeadInfo[2].m_FindNearestRange = 75;
            Worker.m_AllHeadInfo[0].m_FindNearestDelay = 20;
            Worker.m_AllHeadInfo[1].m_FindNearestDelay = 15;
            Worker.m_AllHeadInfo[2].m_FindNearestDelay = 10;
        }

    }
}
