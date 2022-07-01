using System;
using System.Collections.Generic;
using System.Linq;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using TaleWorlds.Core;

namespace EquipBestItem.Extensions;

public static class Extensions
{
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
        var propertyValue = item?.GetType().GetProperty(itemParams.ToString())?.GetValue(item) 
                            ?? new ArgumentNullException(itemParams.ToString());
        
        return itemModifier is null
            ? Convert.ToSingle(propertyValue)
            : propertyValue switch
            {
                int v => v.ApplyModifier(itemModifier, itemParams),
                float v => v,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
    
    public static float GetItemValue(this EquipmentElement item, Coefficients coefficients)
    {
        ItemObject itemObject = item.Item;
        
        if (itemObject.HasArmorComponent)
        {
            return _GetComponentValue(itemObject.ArmorComponent, ItemTypes.Armor);
        }

        if (itemObject.HasWeaponComponent)
        {
            var primaryWeapon = itemObject.WeaponComponent.PrimaryWeapon;

            if (primaryWeapon.IsRangedWeapon)
            {
                return _GetComponentValue(primaryWeapon, 
                    primaryWeapon.IsConsumable 
                        ? ItemTypes.Comsumable 
                        : ItemTypes.RangedWeapon);
            }

            if (primaryWeapon.IsMeleeWeapon)
            {
                return _GetComponentValue(primaryWeapon, ItemTypes.MeleeWeapon);
            }
            
            if (primaryWeapon.IsShield)
            {
                return _GetComponentValue(primaryWeapon, ItemTypes.Shield);
            }  
        }

        if (itemObject.HasHorseComponent)
        {
            return _GetComponentValue(itemObject.HorseComponent, ItemTypes.Horse);
        }

        return 0f;

        float _GetComponentValue<T>(T itemComponent, ItemParams itemParams)
        {
            float sumCoef = 0;
            float value = 0;

            foreach (var param in itemParams.GetFlags())
            {
                var coef = coefficients.GetPropValue(param.ToString());
                sumCoef += coef;
                value += itemComponent.GetPropModValue(item.ItemModifier, param) * coef;
            }

            return value / sumCoef;
        }
    }
}