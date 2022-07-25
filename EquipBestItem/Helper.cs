using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem;

internal static class Helper
{
    internal static T ParseEnum<T>(string value)
    {
        return (T) Enum.Parse(typeof(T), value, true);
    }

    internal static void ShowMessage(string text, Color? color = null)
    {
        InformationManager.DisplayMessage(new InformationMessage($"{text}"));
    }
}