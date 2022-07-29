using System;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;

namespace EquipBestItem.Models.BestItemCalculator;

public class CoefficientsCalculator : BestItemCalculatorBase
{
    private readonly CharacterCoefficientsRepository _repository;
    
    public CoefficientsCalculator(string name, CharacterCoefficientsRepository repository) : base(name)
    {
        _repository = repository;
    }
    
    public override float GetItemValue(EquipmentElement equipmentElement, CalculatorContext context)
    {
        var itemObject = equipmentElement.Item;
        var characterName = context.Character.Name.ToString();

        var coefficients = context.IsInWarSet
            ? _repository.Read(characterName).WarCoefficients[(int) context.EquipmentIndex]
            : _repository.Read(characterName).CivilCoefficients[(int) context.EquipmentIndex];

        if (itemObject.HasArmorComponent) return GetComponentValue(0, ItemTypes.Armor);
        if (itemObject.HasHorseComponent) return GetComponentValue(0, ItemTypes.Horse);
        if (!itemObject.HasWeaponComponent) return 0;

        var pw = equipmentElement.Item.PrimaryWeapon;

        return GetComponentValue(0, ItemTypes.GetParamsByWeaponClass(pw.WeaponClass));

        float GetComponentValue(int indexUsage = 0, params ItemParams[] itemParams)
        {
            float sumCoefficients = 0;
            float value = 0;

            foreach (var param in itemParams)
            {
                var coefficientValue = GetCoefficientValue(coefficients, param);

                if (coefficientValue == 0) continue;

                sumCoefficients += coefficientValue;

                value += GetModifiedValue(equipmentElement, param, indexUsage) * coefficientValue;
            }

            return sumCoefficients > 0 ? value / sumCoefficients : 0;
        }
    }

    public override bool IsItemNotValid(SPItemVM item, CalculatorContext context)
    {
        var index = context.EquipmentIndex;
        var character = context.Character;
        var characterName = context.Character.Name.ToString();
        var equipment = context.IsInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;
        var coefficients = context.IsInWarSet
            ? _repository.Read(characterName).WarCoefficients[(int) index]
            : _repository.Read(characterName).CivilCoefficients[(int) index];
        
        var itemPrimaryWeapon = item.ItemRosterElement.EquipmentElement.Item?.PrimaryWeapon;
        var currentPrimaryWeapon = equipment[index].Item?.PrimaryWeapon;

        // If the selected weapon class is not defined, we look at the weapon class from the slot,
        // otherwise by the selected
        if (coefficients.WeaponClass == WeaponClass.Undefined)
        {
            // If the classes do not match, we skip
            if (itemPrimaryWeapon?.WeaponClass != currentPrimaryWeapon?.WeaponClass) return true;

            // Additional filter for short and long bows
            if (currentPrimaryWeapon?.WeaponClass == WeaponClass.Bow &&
                itemPrimaryWeapon?.ItemUsage != currentPrimaryWeapon.ItemUsage) return true;

            var currentWeaponComponents = item.ItemRosterElement.EquipmentElement.Item?.Weapons;
            var itemWeaponComponents = equipment[index].Item?.Weapons;

            // If they differ in the number of components skip
            if (itemWeaponComponents?.Count != currentWeaponComponents?.Count)
                return true;

            // If they have the same number of components, then we enumerate each one and compare by class
            for (var i = 0; i < itemWeaponComponents?.Count; i++)
                if (currentWeaponComponents?[i].ItemUsage != itemWeaponComponents?[i].ItemUsage)
                    return true;
        }
        else
        {
            if (coefficients.WeaponClass != itemPrimaryWeapon?.WeaponClass) return true;
        }
        
        if (IsShieldNotValid()) return true;

        if (itemPrimaryWeapon?.WeaponClass == WeaponClass.Banner) return true;
        
        return false;
        
        bool IsShieldNotValid()
        {
            if (!IsShield(coefficients.WeaponClass)) return false;

            // Exclude shields from the search if the shield is already on
            for (var i = EquipmentIndex.Weapon0; i <= EquipmentIndex.ExtraWeaponSlot; i++)
                if (IsShield(equipment[i].Item?.PrimaryWeapon?.WeaponClass)) return true;
                    
            return false;

            bool IsShield(WeaponClass? weaponClass)
            {
                return weaponClass is WeaponClass.SmallShield or WeaponClass.LargeShield;
            }
        }
    }
    
    // Checking the compliance of the slot's weapon class, if a weapon class other than WeaponClass.Undefined
    public override bool IsSlotItemNotValid(EquipmentElement item, CalculatorContext context)
    {
        var index = context.EquipmentIndex;
        var characterName = context.Character.Name.ToString();
        var coefficients = context.IsInWarSet
            ? _repository.Read(characterName).WarCoefficients[(int) index]
            : _repository.Read(characterName).CivilCoefficients[(int) index];
                
        return item.Item?.PrimaryWeapon?.WeaponClass != coefficients.WeaponClass &&
               coefficients.WeaponClass != WeaponClass.Undefined;
    }
    
    private static float GetCoefficientValue(Coefficients coefficients, ItemParams itemParam)
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

    private static float GetModifiedValue(EquipmentElement item, ItemParams itemParam, int weaponUsage = 0)
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
            ItemParams.Weight => item.GetEquipmentElementWeight() * -1f,
            _ => 0
        };
    }
}