using System;
using SharpRepository.XmlRepository;
using EquipBestItem.Models.Entities;
using System.IO;
using TaleWorlds.Core;
using TaleWorlds.Library;
using EngineFilePaths = TaleWorlds.Engine.EngineFilePaths;
using SharpRepository.Repository;

namespace EquipBestItem.Models
{
    public static class Seeds
    {
        public static readonly string DefaultStoragePath =
            $"{Common.PlatformFileHelper.GetFileFullPath(new PlatformFilePath(EngineFilePaths.ConfigsPath, string.Empty))}/ModSettings/EquipBestItem/";

        public static readonly Settings[] DefaultSettings =
        {
            new Settings {Name = Settings.IsRightPanelLocked, Value = false},
            new Settings {Name = Settings.IsLeftPanelLocked, Value = true}
        };
        
        public static readonly CharacterCoefficients[] DefaultCharacterCoefficients =
        {
            new CharacterCoefficients
            {
                Name = CharacterCoefficients.Default,
                WarCoefficients = GetDefaultCoefficientsArray(),
                CivilCoefficients = GetDefaultCoefficientsArray()
            }
        };

        private static Coefficients[] GetDefaultCoefficientsArray()
        {
            var coefficients = new Coefficients[(int)EquipmentIndex.NumEquipmentSetSlots];
            
            for (var i = (int) EquipmentIndex.WeaponItemBeginSlot; i < (int) EquipmentIndex.NumEquipmentSetSlots; i++)
            {
                coefficients[i] = _GetDefaultCoefficient((EquipmentIndex)i);
            }

            return coefficients;

            Coefficients _GetDefaultCoefficient(EquipmentIndex equipmentIndex) => equipmentIndex switch
            {
                EquipmentIndex.WeaponItemBeginSlot => new Coefficients()
                {
                    Accuracy = 100f, Handling = 100f, MissileDamage = 100f, MissileSpeed = 100f, SwingDamage = 100f,
                    SwingSpeed = 100f, ThrustDamage = 100f, ThrustSpeed = 100f, WeaponLength = 100f, MaxDataValue = 100f
                },
                EquipmentIndex.Weapon1 => new Coefficients()
                {
                    Accuracy = 100f, Handling = 100f, MissileDamage = 100f, MissileSpeed = 100f, SwingDamage = 100f,
                    SwingSpeed = 100f, ThrustDamage = 100f, ThrustSpeed = 100f, WeaponLength = 100f, MaxDataValue = 100f
                },
                EquipmentIndex.Weapon2 => new Coefficients()
                {
                    Accuracy = 100f, Handling = 100f, MissileDamage = 100f, MissileSpeed = 100f, SwingDamage = 100f,
                    SwingSpeed = 100f, ThrustDamage = 100f, ThrustSpeed = 100f, WeaponLength = 100f, MaxDataValue = 100f
                },
                EquipmentIndex.Weapon3 => new Coefficients()
                {
                    Accuracy = 100f, Handling = 100f, MissileDamage = 100f, MissileSpeed = 100f, SwingDamage = 100f,
                    SwingSpeed = 100f, ThrustDamage = 100f, ThrustSpeed = 100f, WeaponLength = 100f, MaxDataValue = 100f
                },
                EquipmentIndex.Weapon4 => new Coefficients()
                {
                    Accuracy = 100f, Handling = 100f, MissileDamage = 100f, MissileSpeed = 100f, SwingDamage = 100f,
                    SwingSpeed = 100f, ThrustDamage = 100f, ThrustSpeed = 100f, WeaponLength = 100f, MaxDataValue = 100f
                },
                EquipmentIndex.Head => new Coefficients()
                {
                    HeadArmor = 100f
                },
                EquipmentIndex.Body => new Coefficients()
                {
                    BodyArmor = 100f, ArmArmor = 100f, LegArmor = 100f
                },
                EquipmentIndex.Leg => new Coefficients()
                {
                    LegArmor = 100f
                },
                EquipmentIndex.Gloves => new Coefficients()
                {
                    ArmArmor = 100f
                },
                EquipmentIndex.Cape => new Coefficients()
                {
                    BodyArmor = 100f, ArmArmor = 100f
                },
                EquipmentIndex.Horse => new Coefficients()
                {
                    Speed = 100f, Maneuver = 100f, HitPoints = 100f, ChargeDamage = 100f
                },
                EquipmentIndex.HorseHarness => new Coefficients()
                {
                    BodyArmor = 100f
                },
                _ => throw new ArgumentOutOfRangeException(nameof(equipmentIndex), equipmentIndex, null)
            };
        }

        public static void EnsurePopulated<T>(IRepository<T, string> repository, params T[] entities) where T : BaseEntity
        {
            Type type = typeof(T);
            var test = Path.Combine($"{DefaultStoragePath}{type.Name}.xml");
            if (!File.Exists(test))
            {

                foreach (var entity in entities)
                {
                    repository.Add(entity);
                }
            }
            else
            {
                foreach (var entity in entities)
                {
                    if (repository.Exists(entity.Name)) continue;
                    
                    repository.Add(entity);
                }
            }
        }
    }
}
