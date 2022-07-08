using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EquipBestItem.Models.Enums;
using TaleWorlds.Core;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.Extensions;

public static class CommonExtensions
{
    
    internal static T? FindLayer<T>(this IEnumerable<ScreenLayer>? screenLayers) where T : ScreenLayer
    {
        if (screenLayers == null) return default;
        
        foreach (var layer in screenLayers)
            if (layer is T targetLayer)
                return targetLayer;
        return default;
    }
    
    internal static void GetMethod(this object o, string methodName, params object[] args)
    {
        var mi = o.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (mi == null) return;
        try
        {
            mi.Invoke(o, args);
        }
        catch
        {
            throw new MBException($"{methodName} method retrieval error");
        }
    }
    
    internal static IEnumerable<ItemParams> GetFlags(this ItemParams itemType)
    {
        return Enum.GetValues(typeof(ItemParams))
            .Cast<ItemParams>()
            .Where(p => itemType.HasFlag(p));
    }
}