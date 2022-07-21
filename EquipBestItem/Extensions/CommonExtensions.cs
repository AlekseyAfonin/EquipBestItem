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
    
    // internal static IEnumerable<ItemParams> GetFlags(this ItemParams itemType)
    // {
    //     return Enum.GetValues(typeof(ItemParams))
    //         .Cast<ItemParams>()
    //         .Where(p => itemType.HasFlag(p));
    // }
    
    internal static IEnumerable<T> GetUniqueFlags<T>(this T flags) where T : Enum
    {
        ulong flag = 1;
        foreach (var value in Enum.GetValues(flags.GetType()).Cast<T>())
        {
            ulong bits = Convert.ToUInt64(value);
            while (flag < bits)
            {
                flag <<= 1;
            }

            if (flag == bits && flags.HasFlag(value as Enum))
            {
                yield return value;
            }
        }
    }
    
    public static object GetPropValue<T>(this T @this, string propertyName)
    {
        var type = @this?.GetType();
        var property = type?.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

        return property?.GetValue(@this, null) ?? throw new InvalidOperationException("Get property value exception");
    }
    
    public static void SetPropValue<T>(this T @this, string propertyName, object value)
    {
        var type = @this?.GetType();
        var property = type?.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        property?.SetValue(@this, value);
    }
}