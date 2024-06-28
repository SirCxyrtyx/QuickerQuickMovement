using Kingmaker.Localization;
using ModMenu.Settings;
using System;
using System.Collections.Generic;

namespace QuickerQuickMovement;

internal static class SettingsModMenu
{
    private const string ROOT_KEY = "sircxyrtyx.combat-relief";
    private const string SPEED_MULT_KEY = $"{ROOT_KEY}.quickmove-speed-mult";

    public static float QuickMoveSpeedMult => ModMenu.ModMenu.GetSettingValue<float>(SPEED_MULT_KEY);
    internal static void Initialize()
    {
        ModMenu.ModMenu.AddSettings(
          SettingsBuilder
            .New(GetKey("title"), CreateString("sircxyrtyx.quickquickMovement.title-name", "Quick Quick Movement"))
            .AddSliderFloat(
                SliderFloat
                    .New(SPEED_MULT_KEY, 1.8f, CreateString("mm-quickmove-speedup", "Quick Movement Multiplier"), 1, 10)
                    .WithDecimalPlaces(1).WithStep(0.1f)
                    .WithLongDescription(CreateString("mm-quickmove-speedup-desc", "Amount speed should be multiplied by when Quick Movement is enabled. (Un-modded default is 1.8)"))
            )
        );
    }

    private static LocalizedString CreateString(string partialKey, string text)
    {
        return CreateStringInner(GetKey(partialKey), text);
    }

    private static string GetKey(string partialKey)
    {
        return $"{ROOT_KEY}.{partialKey}";
    }

    private static LocalizedString CreateStringInner(string key, string value)
    {
        LocalizedString result = new()
        {
            Key = key
        };
        LocalizationManager.CurrentPack?.PutString(key, value);
        return result;
    }
}