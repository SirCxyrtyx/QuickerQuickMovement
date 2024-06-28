using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityModManagerNet;

namespace QuickerQuickMovement;

public class Main
{
    public static bool Load(UnityModManager.ModEntry modEntry)
    {
        new Harmony(modEntry.Info.Id).PatchAll(Assembly.GetExecutingAssembly());
        return true;
    }

    internal class SettingsStarter
    {
        [HarmonyPatch(typeof(BlueprintsCache), nameof(BlueprintsCache.Init))]
        internal static class BlueprintsCache_Init_Patch
        {
            private static bool _initialized;

            [HarmonyPostfix]
            static void Postfix()
            {
                if (_initialized) return;
                _initialized = true;
                SettingsModMenu.Initialize();
            }
        }
    }
}