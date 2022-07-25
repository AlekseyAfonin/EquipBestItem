using System;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using TaleWorlds.Core;

namespace EquipBestItem.Extensions;

internal static class CalculateExtensions
{
    internal static float GetItemValue(this EquipmentElement equipmentElement, Coefficients coefficients)
    {
        var itemObject = equipmentElement.Item;

        if (itemObject.HasArmorComponent) return GetComponentValue(0, ItemTypes.Armor);
        if (itemObject.HasHorseComponent) return GetComponentValue(0, ItemTypes.Horse);
        if (!itemObject.HasWeaponComponent) return 0f;

        var pw = equipmentElement.Item.PrimaryWeapon;

        return GetComponentValue(0, ItemTypes.GetParamsByWeaponClass(pw.WeaponClass));

        float GetComponentValue(int indexUsage = 0, params ItemParams[] itemParams)
        {
            float sumCoefficients = 0;
            float value = 0;

            foreach (var param in itemParams)
            {
                var coefficientValue = coefficients.GetCoefficientValue(param);

                if (coefficientValue == 0) continue;

                sumCoefficients += coefficientValue;

                // The weight is not in the properties of the component, so we take it from the parent object
                value += param == ItemParams.Weight
                    ? equipmentElement.GetEquipmentElementWeight() * coefficientValue
                    : equipmentElement.GetModifiedValue(param, indexUsage) * coefficientValue;
            }

            return sumCoefficients > 0 ? value / sumCoefficients : 0;
        }
    }

    private static float GetCoefficientValue(this Coefficients coefficients, ItemParams itemParam)
    {
        return itemParam switch
        {
            ItemParams.HeadArmor => coefficients.HeadArmor,
            ItemParams.BodyArmor => coefficients.BodyArmor,
            ItemParams.ArmArmor => coefficients.ArmArmor,
            ItemParams.LegArmor => coefficients.LegArmor,
            ItemParams.ChargeDamage => coefficients.ChargeDamage,
            ItemParams.HitPoints => coefficients.HitPoints,
            ItemParams.Maneuver => coefficients.Maneuver,
            ItemParams.Speed => coefficients.Speed,
            ItemParams.MaxDataValue => coefficients.MaxDataValue,
            ItemParams.ThrustSpeed => coefficients.ThrustSpeed,
            ItemParams.SwingSpeed => coefficients.SwingSpeed,
            ItemParams.MissileSpeed => coefficients.MissileSpeed,
            ItemParams.MissileDamage => coefficients.MissileDamage,
            ItemParams.WeaponLength => coefficients.WeaponLength,
            ItemParams.ThrustDamage => coefficients.ThrustDamage,
            ItemParams.SwingDamage => coefficients.SwingDamage,
            ItemParams.Accuracy => coefficients.Accuracy,
            ItemParams.Handling => coefficients.Handling,
            ItemParams.Weight => coefficients.Weight,
            _ => throw new ArgumentOutOfRangeException(nameof(itemParam), itemParam, null)
        };
    }

    private static int GetModifiedValue(this EquipmentElement item, ItemParams itemParam, int weaponUsage = 0)
    {
        return itemParam switch
        {
            ItemParams.HeadArmor => item.GetModifiedHeadArmor(),
            ItemParams.BodyArmor when item.Item.Type is ItemObject.ItemTypeEnum.HorseHarness => item
                .GetModifiedMountBodyArmor(),
            ItemParams.BodyArmor when item.Item.Type is not ItemObject.ItemTypeEnum.HorseHarness => item
                .GetModifiedBodyArmor(),
            ItemParams.ArmArmor => item.GetModifiedArmArmor(),
            ItemParams.LegArmor => item.GetModifiedLegArmor(),
            ItemParams.ChargeDamage => item.GetModifiedMountCharge(EquipmentElement.Invalid),
            ItemParams.HitPoints => item.GetModifiedMountHitPoints(),
            ItemParams.Maneuver => item.GetModifiedMountManeuver(EquipmentElement.Invalid),
            ItemParams.Speed => item.GetModifiedMountSpeed(EquipmentElement.Invalid),
            ItemParams.MaxDataValue when item.Item.Weapons[weaponUsage].IsShield =>
                item.GetModifiedMaximumHitPointsForUsage(weaponUsage),
            ItemParams.MaxDataValue when item.Item.Weapons[weaponUsage].IsConsumable =>
                item.GetModifiedStackCountForUsage(weaponUsage),
            ItemParams.MaxDataValue when item.Item.Type is ItemObject.ItemTypeEnum.Crossbow
                or ItemObject.ItemTypeEnum.Musket
                or ItemObject.ItemTypeEnum.Pistol => item.Item.Weapons[weaponUsage].MaxDataValue,
            ItemParams.ThrustSpeed => item.GetModifiedThrustDamageForUsage(weaponUsage),
            ItemParams.SwingSpeed => item.GetModifiedSwingSpeedForUsage(weaponUsage),
            ItemParams.MissileSpeed => item.GetModifiedMissileSpeedForUsage(weaponUsage),
            ItemParams.MissileDamage => item.GetModifiedMissileDamageForUsage(weaponUsage),
            ItemParams.WeaponLength => item.Item.Weapons[weaponUsage].WeaponLength,
            ItemParams.ThrustDamage => item.GetModifiedThrustDamageForUsage(weaponUsage),
            ItemParams.SwingDamage => item.GetModifiedSwingDamageForUsage(weaponUsage),
            ItemParams.Accuracy => item.Item.Weapons[weaponUsage].Accuracy,
            ItemParams.Handling => item.GetModifiedHandlingForUsage(weaponUsage),
            _ => 0
        };
    }
}