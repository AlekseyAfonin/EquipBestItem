using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using Messages.FromLobbyServer.ToClient;
using NetworkMessages.FromClient;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.Extensions;

public static class Extensions
{
    private static int ApplyModifier(this int value, ItemModifier modifier, ItemParams itemParams) =>
        itemParams switch
        {
            ItemParams.HeadArmor => modifier.ModifyArmor(value).NotLessZero(),
            ItemParams.BodyArmor => modifier.ModifyArmor(value).NotLessZero(),
            ItemParams.ArmArmor => modifier.ModifyArmor(value).NotLessZero(),
            ItemParams.LegArmor => modifier.ModifyArmor(value).NotLessZero(),
            ItemParams.ChargeDamage => modifier.ModifyMountCharge(value).NotLessZero(),
            ItemParams.HitPoints => modifier.ModifyMountHitPoints(value).NotLessZero(),
            ItemParams.Maneuver => modifier.ModifyMountManeuver(value).NotLessZero(),
            ItemParams.Speed => modifier.ModifyMountSpeed(value).NotLessZero(),
            ItemParams.MaxDataValue => modifier.ModifyHitPoints((short)value).NotLessZero(),
            ItemParams.ThrustSpeed => modifier.ModifySpeed(value).NotLessZero(),
            ItemParams.SwingSpeed => modifier.ModifySpeed(value).NotLessZero(),
            ItemParams.MissileSpeed => modifier.ModifyMissileSpeed(value).NotLessZero(),
            ItemParams.MissileDamage => modifier.ModifyDamage(value).NotLessZero(),
            ItemParams.WeaponLength => value,
            ItemParams.ThrustDamage => modifier.ModifyDamage(value).NotLessZero(),
            ItemParams.SwingDamage => modifier.ModifyDamage(value).NotLessZero(),
            ItemParams.Accuracy => value,
            ItemParams.Handling => value,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };

    private static int NotLessZero(this int value)
    {
        return value < 0 ? 0 : value;
    }
    
    private static int NotLessZero(this short value)
    {
        return value < 0 ? 0 : value;
    }
    
    public static IEnumerable<ItemParams> GetFlags(this ItemParams itemType)
    {
        return Enum.GetValues(typeof(ItemParams))
            .Cast<ItemParams>()
            .Where(p => itemType.HasFlag(p));
    }
    
    public static float GetPropValue(this Coefficients item, string propName)
    {
        var propertyValue = typeof(Coefficients).GetProperty(propName)?.GetValue(item);
        return Convert.ToSingle(propertyValue);
    }
    
    public static float GetPropModValue<T>(this T item, ItemModifier? itemModifier, ItemParams itemParams)
    {
        var propertyValue = typeof(EquipmentElement).GetProperty(itemParams.ToString())?.GetValue(item) 
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
    
    public static float GetItemValueByCoefficient(this EquipmentElement item, Coefficients coefficients)
    {
        if (item.IsEmpty) return -9999f;

        float getItemValue<T>(T obj, ItemParams itemParams)
        {
            var sumCoef = 0f;
            var value = 0f;
        
            foreach (var param in itemParams.GetFlags())
            {
                var coef = coefficients.GetPropValue(param.ToString());
                sumCoef += coef;
                value += obj.GetPropModValue(item.ItemModifier, param) * coef;
            }

            return value / sumCoef;
        }
        
        ItemObject itemObject = item.Item;
        
        if (itemObject.HasArmorComponent)
        {
            return getItemValue(itemObject.ArmorComponent, ItemTypes.Armor);
        }

        if (itemObject.HasWeaponComponent)
        {
            var primaryWeapon = itemObject.WeaponComponent.PrimaryWeapon;

            if (primaryWeapon.IsRangedWeapon)
            {
                return getItemValue(primaryWeapon, 
                    primaryWeapon.IsConsumable 
                        ? ItemTypes.Comsumable 
                        : ItemTypes.RangedWeapon);
            }

            if (primaryWeapon.IsMeleeWeapon)
            {
                return getItemValue(primaryWeapon, ItemTypes.MeleeWeapon);
            }
            
            if (primaryWeapon.IsShield)
            {
                return getItemValue(primaryWeapon, ItemTypes.Shield);
            }  
        }

        if (itemObject.HasHorseComponent)
        {
            return getItemValue(itemObject.HorseComponent, ItemTypes.Shield);
        }

        return 0f; //TODO
    }
}