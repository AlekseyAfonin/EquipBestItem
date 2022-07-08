using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using TaleWorlds.Core;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.Extensions;

internal static class CalculateExtensions
{
    internal static float GetItemValue(this EquipmentElement equipmentElement, Coefficients coefficients)
    {
        var itemObject = equipmentElement.Item;

        if (itemObject.HasArmorComponent) return GetComponentValue(itemObject.ArmorComponent, ItemTypes.Armor);
        if (itemObject.HasHorseComponent) return GetComponentValue(itemObject.HorseComponent, ItemTypes.Horse);
        if (!itemObject.HasWeaponComponent) return 0;
        
        var pw = itemObject.WeaponComponent.PrimaryWeapon;
        
        if (pw.IsRangedWeapon) return GetComponentValue(pw, pw.IsConsumable ? ItemTypes.Consumable : ItemTypes.RangedWeapon);
        if (pw.IsMeleeWeapon) return GetComponentValue(pw, ItemTypes.MeleeWeapon);
        if (pw.IsShield) return GetComponentValue(pw, ItemTypes.Shield);
        
        return 0;

        float GetComponentValue<T>(T itemComponent, ItemParams itemParams)
        {
            float sumCoefficients = 0;
            float value = 0;

            foreach (var param in itemParams.GetFlags())
            {
                var coefficient = GetPropValue(coefficients, param.ToString());

                if (coefficient == 0) continue;
                
                sumCoefficients += coefficient;
                
                // The weight is not in the properties of the component, so we take it from the parent object
                value += param == ItemParams.Weight 
                    ? itemObject.Weight * coefficient 
                    : itemComponent.GetPropModValue(equipmentElement.ItemModifier, param) * coefficient;
            }
            
            return sumCoefficients > 0 ? value / sumCoefficients : 0;
        }
        
        float GetPropValue(Coefficients item, string propName)
        {
            var propertyValue = item.GetType().GetProperty(propName)?.GetValue(item);
            return Convert.ToSingle(propertyValue);
        }
    }
    
    private static float GetPropModValue<T>(this T item, ItemModifier? itemModifier, ItemParams itemParams)
    {
        var propertyValue = item?.GetType().GetProperty(itemParams.ToString())?.GetValue(item);
        
        return itemModifier is null
            ? Convert.ToSingle(propertyValue)
            : propertyValue switch
            {
                int v => ApplyIntModifier(v),                 
                short v => ApplyShortModifier(v),                                      
                _ => throw new ArgumentOutOfRangeException()
            };
        
        int ApplyIntModifier(int value) =>
            itemParams switch
            {
                ItemParams.HeadArmor => Math.Max(0, itemModifier.ModifyArmor(value)),
                ItemParams.BodyArmor => Math.Max(0, itemModifier.ModifyArmor(value)),
                ItemParams.ArmArmor => Math.Max(0, itemModifier.ModifyArmor(value)),
                ItemParams.LegArmor => Math.Max(0, itemModifier.ModifyArmor(value)),
                ItemParams.ChargeDamage => Math.Max(0, itemModifier.ModifyMountCharge(value)),
                ItemParams.HitPoints => Math.Max(0, itemModifier.ModifyMountHitPoints(value)),
                ItemParams.Maneuver => Math.Max(0, itemModifier.ModifyMountManeuver(value)),
                ItemParams.Speed => Math.Max(0, itemModifier.ModifyMountSpeed(value)),
                ItemParams.ThrustSpeed => Math.Max(0, itemModifier.ModifySpeed(value)),
                ItemParams.SwingSpeed => Math.Max(0, itemModifier.ModifySpeed(value)),
                ItemParams.MissileSpeed => Math.Max(0, itemModifier.ModifyMissileSpeed(value)),
                ItemParams.MissileDamage => Math.Max(0, itemModifier.ModifyDamage(value)),            
                ItemParams.ThrustDamage => Math.Max(0, itemModifier.ModifyDamage(value)),
                ItemParams.SwingDamage => Math.Max(0, itemModifier.ModifyDamage(value)),
                ItemParams.WeaponLength => value,
                ItemParams.Accuracy => value,
                ItemParams.Handling => value,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        
        short ApplyShortModifier(short value) => itemParams switch
            {
                ItemParams.MaxDataValue => Math.Max((short)0, itemModifier.ModifyHitPoints(value)),
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
    }
}