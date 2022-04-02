using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using EquipBestItem.Models.Enums;
using Messages.FromLobbyServer.ToClient;
using NetworkMessages.FromClient;
using TaleWorlds.Core;

namespace EquipBestItem.Models;

public static class Extensions
{
    private static float ApplyModifier(this object value, ItemModifier modifier, string propName) =>
        propName switch
        {
            "HeadArmor" => modifier.ModifyArmor((int)value).ToFloatNotLessZero(),
            "BodyArmor" => modifier.ModifyArmor((int)value).ToFloatNotLessZero(),
            "ArmArmor" => modifier.ModifyArmor((int)value).ToFloatNotLessZero(),
            "LegArmor" => modifier.ModifyArmor((int)value).ToFloatNotLessZero(),
            "ChargeDamage" => modifier.ModifyMountCharge((int)value).ToFloatNotLessZero(),
            "HitPoints" => modifier.ModifyMountHitPoints((int)value).ToFloatNotLessZero(),
            "Maneuver" => modifier.ModifyMountManeuver((int)value).ToFloatNotLessZero(),
            "Speed" => modifier.ModifyMountSpeed((int)value).ToFloatNotLessZero(),
            "MaxDataValue" => modifier.ModifyHitPoints((short)value).ToFloatNotLessZero(),
            "ThrustSpeed" => modifier.ModifySpeed((int)value).ToFloatNotLessZero(),
            "SwingSpeed" => modifier.ModifySpeed((int)value).ToFloatNotLessZero(),
            "MissileSpeed" => modifier.ModifyMissileSpeed((int)value).ToFloatNotLessZero(),
            "MissileDamage" => modifier.ModifyDamage((int)value).ToFloatNotLessZero(),
            "ThrustDamage" => modifier.ModifyDamage((int)value).ToFloatNotLessZero(),
            "SwingDamage" => modifier.ModifyDamage((int)value).ToFloatNotLessZero(),
            _ => value switch
            {
                int v => v,     // Accuracy, Handling, WraponLength without modifiers
                float v => v,   // Weight without modifier
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            }
        };

    private static float ToFloatNotLessZero(this int value)
    {
        return value < 0 ? 0 : value;
    }
    
    private static float ToFloatNotLessZero(this short value)
    {
        return value < 0 ? 0 : value;
    }
    
    /// <summary>
    /// Return flags to foreach
    /// </summary>
    /// <param name="itemType">ItemParams enumerable</param>
    /// <returns>Return flags as enumerable</returns>
    public static IEnumerable<ItemParams> GetFlags(this ItemParams itemType)
    {
        return Enum
            .GetValues(typeof(ItemParams))
            .Cast<ItemParams>()
            .Where(p => itemType.HasFlag(p));
    }
    
    /// <summary>
    /// Get property value by name
    /// </summary>
    /// <param name="item">object</param>
    /// <param name="propName">Property name</param>
    /// <returns>Float value</returns>
    public static float GetPropValue<T>(this T item, string propName)
    {
        var propertyValue = typeof(T).GetProperty(propName)?.GetValue(item);
        return Convert.ToSingle(propertyValue);
    }

    /// <summary>
    /// Get property value with modifier by name
    /// </summary>
    /// <param name="item">EquipmentElement object</param>
    /// <param name="itemModifier">Item modifier</param>
    /// <param name="propName">Property name</param>
    /// <returns>Float value</returns>
    public static float GetPropModValue<T>(this T item, ItemModifier? itemModifier, string propName)
    {
        var propertyValue = typeof(EquipmentElement).GetProperty(propName)?.GetValue(item) ?? new ArgumentNullException(propName);
        return itemModifier is null
            ? Convert.ToSingle(propertyValue)
            : propertyValue.ApplyModifier(itemModifier, propName);
    }
}