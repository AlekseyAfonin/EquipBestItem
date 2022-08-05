using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    
    internal static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
    {
        return (from myType in Assembly.GetAssembly(typeof(T)).GetTypes()
            where myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))
            select myType into type
            select (T)(object)Activator.CreateInstance(type, constructorArgs)).ToList();
    }
}