using System;
using EquipBestItem.Settings;
using EquipBestItem.ViewModels;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.Models
{
    public class FiltersSettingsModel
    {
        private readonly SPInventoryVM _inventory;
        private readonly FiltersSettingsVM _vm;
        private readonly EquipmentIndex _currentSlot;
        private readonly CharacterSettings _characterSettings;
        private FilterElement _filterElement;


        public FiltersSettingsModel(FiltersSettingsVM vm, SPInventoryVM inventory, EquipmentIndex currentSlot)
        {
            _currentSlot = currentSlot;
            _vm = vm;
            _vm.PropertyChangedWithValue += VMPropertyChangedWithValue;
            _inventory = inventory;
            _characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(_inventory.CurrentCharacterName,
                _inventory.IsInWarSet);
            _filterElement = _characterSettings.Filters[_currentSlot];
            LoadFiltersSettings();
        }

        public void LoadFiltersSettings()
        {
            if (_characterSettings.Name != _inventory.CurrentCharacterName)
                SettingsLoader.Instance.GetCharacterSettingsByName(_inventory.CurrentCharacterName,
                    _inventory.IsInWarSet);

            switch (_currentSlot)
            {
                case EquipmentIndex.Head:
                {
                    _vm.HeadArmorValue = _filterElement.HeadArmor;
                    _vm.WeightValue = _filterElement.Weight;
                    break;
                }
                case EquipmentIndex.Cape:
                {
                    _vm.BodyArmorValue = _filterElement.ArmorBodyArmor;
                    _vm.WeightValue = _filterElement.Weight;
                    break;
                }
                case EquipmentIndex.Body:
                {
                    _vm.HeadArmorValue = _filterElement.HeadArmor;
                    _vm.BodyArmorValue = _filterElement.ArmorBodyArmor;
                    _vm.ArmArmorValue = _filterElement.ArmArmor;
                    _vm.LegArmorValue = _filterElement.LegArmor;
                    _vm.WeightValue = _filterElement.Weight;
                    break;
                }
                case EquipmentIndex.Gloves:
                {
                    _vm.ArmArmorValue = _filterElement.ArmArmor;
                    _vm.WeightValue = _filterElement.Weight;
                    break;
                }
                case EquipmentIndex.Leg:
                {
                    _vm.LegArmorValue = _filterElement.LegArmor;
                    _vm.WeightValue = _filterElement.Weight;
                    break;
                }
                case EquipmentIndex.Horse:
                {
                    _vm.ManeuverValue = _filterElement.Maneuver;
                    _vm.SpeedValue = _filterElement.Speed;
                    _vm.HitPointsValue = _filterElement.HitPoints;
                    _vm.ChargeDamageValue = _filterElement.ChargeDamage;
                    break;
                }
                case EquipmentIndex.HorseHarness:
                {
                    _vm.BodyArmorValue = _filterElement.ArmorBodyArmor;
                    _vm.ChargeBonusValue = _filterElement.ChargeBonus;
                    _vm.ManeuverBonusValue = _filterElement.ManeuverBonus;
                    _vm.SpeedBonusValue = _filterElement.SpeedBonus;
                    _vm.WeightValue = _filterElement.Weight;
                    break;
                }
                case EquipmentIndex.Weapon0:
                {
                    WeaponFilterSettingsLoad();
                    break;
                }
                case EquipmentIndex.Weapon1:
                {
                    WeaponFilterSettingsLoad();
                    break;
                }
                case EquipmentIndex.Weapon2:
                {
                    WeaponFilterSettingsLoad();
                    break;
                }
                case EquipmentIndex.Weapon3:
                {
                    WeaponFilterSettingsLoad();
                    break;
                }
            }
            void WeaponFilterSettingsLoad()
            {
                _vm.MaxDataValue = _filterElement.MaxDataValue;
                _vm.ThrustSpeedValue = _filterElement.ThrustSpeed;
                _vm.SwingSpeedValue = _filterElement.SwingSpeed;
                _vm.MissileSpeedValue = _filterElement.MissileSpeed;
                _vm.WeaponLengthValue = _filterElement.WeaponLength;
                _vm.ThrustDamageValue = _filterElement.ThrustDamage;
                _vm.SwingDamageValue = _filterElement.SwingDamage;
                _vm.AccuracyValue = _filterElement.Accuracy;
                _vm.HandlingValue = _filterElement.Handling;
                _vm.WeightValue = _filterElement.Weight;
                _vm.WeaponBodyArmorValue = _filterElement.WeaponBodyArmor;
            }
        }

        public void SaveFiltersSettings()
        {
            switch (_currentSlot)
            {
                case EquipmentIndex.Head:
                {
                    _filterElement.HeadArmor = _vm.HeadArmorValue;
                    _filterElement.Weight = _vm.WeightValue;
                    break;
                }
                case EquipmentIndex.Cape:
                {
                    _filterElement.ArmorBodyArmor = _vm.BodyArmorValue;
                    _filterElement.Weight = _vm.WeightValue;
                    break;
                }
                case EquipmentIndex.Body:
                {
                    _filterElement.HeadArmor = _vm.HeadArmorValue;
                    _filterElement.ArmorBodyArmor = _vm.BodyArmorValue;
                    _filterElement.ArmArmor = _vm.ArmArmorValue;
                    _filterElement.LegArmor = _vm.LegArmorValue;
                    _filterElement.Weight = _vm.WeightValue;
                    break;
                }
                case EquipmentIndex.Gloves:
                {
                    _filterElement.ArmArmor = _vm.ArmArmorValue;
                    _filterElement.Weight = _vm.WeightValue;
                    break;
                }
                case EquipmentIndex.Leg:
                {
                    _filterElement.LegArmor = _vm.LegArmorValue;
                    _filterElement.Weight = _vm.WeightValue;
                    break;
                }
                case EquipmentIndex.Horse:
                {
                    _filterElement.Maneuver = _vm.ManeuverValue;
                    _filterElement.Speed = _vm.SpeedValue;
                    _filterElement.HitPoints = _vm.HitPointsValue;
                    _filterElement.ChargeDamage = _vm.ChargeDamageValue;
                    break;
                }
                case EquipmentIndex.HorseHarness:
                {
                    _filterElement.ArmorBodyArmor = _vm.BodyArmorValue;
                    _filterElement.ChargeBonus = _vm.ChargeBonusValue;
                    _filterElement.ManeuverBonus = _vm.ManeuverBonusValue;
                    _filterElement.SpeedBonus = _vm.SpeedBonusValue;
                    _filterElement.Weight = _vm.WeightValue;
                    break;
                }
                case EquipmentIndex.Weapon0:
                {
                    WeaponFilterSettingsLoad();
                    break;
                }
                case EquipmentIndex.Weapon1:
                {
                    WeaponFilterSettingsLoad();
                    break;
                }
                case EquipmentIndex.Weapon2:
                {
                    WeaponFilterSettingsLoad();
                    break;
                }
                case EquipmentIndex.Weapon3:
                {
                    WeaponFilterSettingsLoad();
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
            void WeaponFilterSettingsLoad()
            {
                _filterElement.MaxDataValue = _vm.MaxDataValue;
                _filterElement.ThrustSpeed = _vm.ThrustSpeedValue;
                _filterElement.SwingSpeed = _vm.SwingSpeedValue;
                _filterElement.MissileSpeed = _vm.MissileSpeedValue;
                _filterElement.WeaponLength = _vm.WeaponLengthValue;
                _filterElement.ThrustDamage = _vm.ThrustDamageValue;
                _filterElement.SwingDamage = _vm.SwingDamageValue;
                _filterElement.Accuracy = _vm.AccuracyValue;
                _filterElement.Handling = _vm.HandlingValue;
                _filterElement.Weight = _vm.WeightValue;
                _filterElement.WeaponBodyArmor = _vm.WeaponBodyArmorValue;
            }
        }
        
        private void VMPropertyChangedWithValue(object sender, PropertyChangedWithValueEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_vm.AccuracyValue): _filterElement.Accuracy = (float)e.Value;
                    break;
                case nameof(_vm.HandlingValue): _filterElement.Handling = (float)e.Value;
                    break;
                case nameof(_vm.ManeuverValue): _filterElement.Maneuver = (float)e.Value;
                    break;
                case nameof(_vm.WeightValue): _filterElement.Weight = (float)e.Value;
                    break;
                case nameof(_vm.ArmArmorValue): _filterElement.ArmArmor = (float)e.Value;
                    break;
                case nameof(_vm.ChargeBonusValue): _filterElement.ChargeBonus = (float)e.Value;
                    break;
                case nameof(_vm.ChargeDamageValue): _filterElement.ChargeDamage = (float)e.Value;
                    break;
                case nameof(_vm.HeadArmorValue): _filterElement.HeadArmor = (float)e.Value;
                    break;
                case nameof(_vm.HitPointsValue): _filterElement.HitPoints = (float)e.Value;
                    break;
                case nameof(_vm.LegArmorValue): _filterElement.LegArmor = (float)e.Value;
                    break;
                case nameof(_vm.ManeuverBonusValue): _filterElement.ManeuverBonus = (float)e.Value;
                    break;
                case nameof(_vm.MissileSpeedValue): _filterElement.MissileSpeed = (float)e.Value;
                    break;
                case nameof(_vm.SpeedBonusValue): _filterElement.SpeedBonus = (float)e.Value;
                    break;
                case nameof(_vm.SpeedValue): _filterElement.Speed = (float)e.Value;
                    break;
                case nameof(_vm.SwingDamageValue): _filterElement.SwingDamage = (float)e.Value;
                    break;
                case nameof(_vm.SwingSpeedValue): _filterElement.SwingSpeed = (float)e.Value;
                    break;
                case nameof(_vm.ThrustDamageValue): _filterElement.ThrustDamage = (float)e.Value;
                    break;
                case nameof(_vm.ThrustSpeedValue): _filterElement.ThrustSpeed = (float)e.Value;
                    break;
                case nameof(_vm.WeaponLengthValue): _filterElement.WeaponLength = (float)e.Value;
                    break;
                case nameof(_vm.BodyArmorValue): _filterElement.ArmorBodyArmor = (float)e.Value;
                    break;
                case nameof(_vm.MaxDataValue): _filterElement.MaxDataValue = (float)e.Value;
                    break;
                case nameof(_vm.WeaponBodyArmorValue): _filterElement.WeaponBodyArmor = (float)e.Value;
                    break;
            }
        }

        public void OnFinalize()
        {
            _filterElement = null;
        }
    }
}