using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using TaleWorlds.Core;

namespace EquipBestItem.Extensions;

public static class Extensions
{
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

    private static int ApplyModifier(this int value, ItemModifier modifier, ItemParams itemParams) =>
        itemParams switch
        {
            ItemParams.HeadArmor => Math.Max(0, modifier.ModifyArmor(value)),
            ItemParams.BodyArmor => Math.Max(0, modifier.ModifyArmor(value)),
            ItemParams.ArmArmor => Math.Max(0, modifier.ModifyArmor(value)),
            ItemParams.LegArmor => Math.Max(0, modifier.ModifyArmor(value)),
            ItemParams.ChargeDamage => Math.Max(0, modifier.ModifyMountCharge(value)),
            ItemParams.HitPoints => Math.Max(0, modifier.ModifyMountHitPoints(value)),
            ItemParams.Maneuver => Math.Max(0, modifier.ModifyMountManeuver(value)),
            ItemParams.Speed => Math.Max(0, modifier.ModifyMountSpeed(value)),
            ItemParams.MaxDataValue => Math.Max((short)0, modifier.ModifyHitPoints((short)value)),
            ItemParams.ThrustSpeed => Math.Max(0, modifier.ModifySpeed(value)),
            ItemParams.SwingSpeed => Math.Max(0, modifier.ModifySpeed(value)),
            ItemParams.MissileSpeed => Math.Max(0, modifier.ModifyMissileSpeed(value)),
            ItemParams.MissileDamage => Math.Max(0, modifier.ModifyDamage(value)),            
            ItemParams.ThrustDamage => Math.Max(0, modifier.ModifyDamage(value)),
            ItemParams.SwingDamage => Math.Max(0, modifier.ModifyDamage(value)),
            ItemParams.WeaponLength => value,
            ItemParams.Accuracy => value,
            ItemParams.Handling => value,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    
    private static IEnumerable<ItemParams> GetFlags(this ItemParams itemType)
    {
        return Enum.GetValues(typeof(ItemParams))
            .Cast<ItemParams>()
            .Where(p => itemType.HasFlag(p));
    }

    private static float GetPropValue(this Coefficients item, string propName)
    {
        var propertyValue = item.GetType().GetProperty(propName)?.GetValue(item);
        return Convert.ToSingle(propertyValue);
    }

    private static float GetPropModValue<T>(this T item, ItemModifier? itemModifier, ItemParams itemParams)
    {
        var propertyValue = item?.GetType().GetProperty(itemParams.ToString())?.GetValue(item);
        
        return itemModifier is null
            ? Convert.ToSingle(propertyValue)
            : propertyValue switch
            {
                int v => v.ApplyModifier(itemModifier, itemParams),                 // All params except float weight
                float v => v,                                                       // Weight haven't modifier
                _ => throw new ArgumentOutOfRangeException()
            };
    }
    
    public static float GetItemValue(this EquipmentElement equipmentElement, Coefficients coefficients)
    {
        var itemObject = equipmentElement.Item;

        if (itemObject.HasArmorComponent) return GetComponentValue(itemObject.ArmorComponent, ItemTypes.Armor);
        if (itemObject.HasHorseComponent) return GetComponentValue(itemObject.HorseComponent, ItemTypes.Horse);
        if (!itemObject.HasWeaponComponent) return 0;
        
        var pw = itemObject.WeaponComponent.PrimaryWeapon;
        
        if (pw.IsRangedWeapon) return GetComponentValue(pw, pw.IsConsumable ? ItemTypes.Comsumable : ItemTypes.RangedWeapon);
        if (pw.IsMeleeWeapon) return GetComponentValue(pw, ItemTypes.MeleeWeapon);
        if (pw.IsShield) return GetComponentValue(pw, ItemTypes.Shield);
        
        return 0;

        float GetComponentValue<T>(T itemComponent, ItemParams itemParams)
        {
            float sumCoefficients = 0;
            float value = 0;

            foreach (var param in itemParams.GetFlags())
            {
                var coefficient = coefficients.GetPropValue(param.ToString());
                sumCoefficients += coefficient;
                value += itemComponent.GetPropModValue(equipmentElement.ItemModifier, param) * coefficient;
            }

            return value / sumCoefficients;
        }
    }
}