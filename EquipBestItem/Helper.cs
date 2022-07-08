using System;
using System.Collections.Generic;
using System.Reflection;
using Messages.FromClient.ToLobbyServer;
using Newtonsoft.Json;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace EquipBestItem;

internal static class Helper
{
    internal static T ParseEnum<T>(string value)
    {
        return (T) Enum.Parse(typeof(T), value, true);
    }

    internal static void ShowMessage(string text)
    {
        InformationManager.DisplayMessage(new InformationMessage($"{text}"));
    }
}