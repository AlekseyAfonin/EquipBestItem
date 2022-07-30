using System;
using System.IO;
using EquipBestItem.Models.Entities;
using EquipBestItem.XmlRepository;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using Path = System.IO.Path;

namespace EquipBestItem.Models;

internal static class Seeds
{
    internal static readonly string DefaultStoragePath =
        $"{Common.PlatformFileHelper.GetFileFullPath(new PlatformFilePath(EngineFilePaths.ConfigsPath, string.Empty))}/ModSettings/EquipBestItem/";

    internal static readonly Settings[] DefaultSettings =
    {
        new() {Key = Settings.IsRightPanelLocked, Value = false},
        new() {Key = Settings.IsLeftPanelLocked, Value = true},
        new() {Key = Settings.IsRightMenuVisible, Value = true},
        new() {Key = Settings.IsLeftMenuVisible, Value = true}
    };

    internal static readonly CharacterCoefficients[] DefaultCharacterCoefficients =
    {
        new()
        {
            Key = CharacterCoefficients.Default,
            WarCoefficients = GetDefaultCoefficientsArray(),
            CivilCoefficients = GetDefaultCoefficientsArray()
        }
    };

    private static Coefficients[] GetDefaultCoefficientsArray()
    {
        var coefficients = new Coefficients[(int) EquipmentIndex.NumEquipmentSetSlots];

        for (var i = (int) EquipmentIndex.WeaponItemBeginSlot; i < (int) EquipmentIndex.NumEquipmentSetSlots; i++)
            coefficients[i] = GetDefaultCoefficient((EquipmentIndex) i);

        return coefficients;

        Coefficients GetDefaultCoefficient(EquipmentIndex equipmentIndex)
        {
            return equipmentIndex switch
            {
                EquipmentIndex.WeaponItemBeginSlot => new Coefficients
                {
                    Accuracy = 100f, Handling = 100f, MissileDamage = 100f, MissileSpeed = 100f, SwingDamage = 100f,
                    SwingSpeed = 100f, ThrustDamage = 100f, ThrustSpeed = 100f, WeaponLength = 100f, MaxDataValue = 100f
                },
                EquipmentIndex.Weapon1 => new Coefficients
                {
                    Accuracy = 100f, Handling = 100f, MissileDamage = 100f, MissileSpeed = 100f, SwingDamage = 100f,
                    SwingSpeed = 100f, ThrustDamage = 100f, ThrustSpeed = 100f, WeaponLength = 100f, MaxDataValue = 100f
                },
                EquipmentIndex.Weapon2 => new Coefficients
                {
                    Accuracy = 100f, Handling = 100f, MissileDamage = 100f, MissileSpeed = 100f, SwingDamage = 100f,
                    SwingSpeed = 100f, ThrustDamage = 100f, ThrustSpeed = 100f, WeaponLength = 100f, MaxDataValue = 100f
                },
                EquipmentIndex.Weapon3 => new Coefficients
                {
                    Accuracy = 100f, Handling = 100f, MissileDamage = 100f, MissileSpeed = 100f, SwingDamage = 100f,
                    SwingSpeed = 100f, ThrustDamage = 100f, ThrustSpeed = 100f, WeaponLength = 100f, MaxDataValue = 100f
                },
                EquipmentIndex.Weapon4 => new Coefficients
                {
                    Accuracy = 100f, Handling = 100f, MissileDamage = 100f, MissileSpeed = 100f, SwingDamage = 100f,
                    SwingSpeed = 100f, ThrustDamage = 100f, ThrustSpeed = 100f, WeaponLength = 100f, MaxDataValue = 100f
                },
                EquipmentIndex.Head => new Coefficients
                {
                    HeadArmor = 100f
                },
                EquipmentIndex.Body => new Coefficients
                {
                    BodyArmor = 100f, ArmArmor = 100f, LegArmor = 100f
                },
                EquipmentIndex.Leg => new Coefficients
                {
                    LegArmor = 100f
                },
                EquipmentIndex.Gloves => new Coefficients
                {
                    ArmArmor = 100f
                },
                EquipmentIndex.Cape => new Coefficients
                {
                    BodyArmor = 100f, ArmArmor = 100f
                },
                EquipmentIndex.Horse => new Coefficients
                {
                    Speed = 100f, Maneuver = 100f, HitPoints = 100f, ChargeDamage = 100f
                },
                EquipmentIndex.HorseHarness => new Coefficients
                {
                    BodyArmor = 100f
                },
                _ => throw new ArgumentOutOfRangeException(nameof(equipmentIndex), equipmentIndex, null)
            };
        }
    }

    internal static void EnsurePopulated<T>(IRepository<T> repository, params T[] entities) where T : BaseEntity
    {
        var type = typeof(T);
        var test = Path.Combine($"{DefaultStoragePath}{type.Name}.xml");
        
        if (!File.Exists(test))
                repository.Create(entities);
        else
            foreach (var entity in entities)
            {
                if (repository.Exists(entity.Key)) continue;

                repository.Create(entity);
            }
    }
}