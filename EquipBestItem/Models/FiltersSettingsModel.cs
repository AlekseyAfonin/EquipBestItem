using System;
using EquipBestItem.Settings;
using EquipBestItem.ViewModels;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.Models
{
    public class FiltersSettingsModel
    {
        private readonly CharacterSettings _characterSettings;
        private readonly EquipmentIndex _currentSlot;
        private readonly SPInventoryVM _inventory;
        private readonly FiltersSettingsVM _vm;
        private FilterElement _filterElement;

        public FiltersSettingsModel(FiltersSettingsVM vm, EquipmentIndex currentSlot)
        {
            _currentSlot = currentSlot;
            _vm = vm;
            _vm.PropertyChangedWithValue += VMPropertyChangedWithValue;
            _inventory = EquipBestItemManager.Instance.Inventory;
            _characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(_inventory.CurrentCharacterName,
                _inventory.IsInWarSet);
            _filterElement = _characterSettings.Filters[_currentSlot];
            LoadFiltersSettings();
        }

        public Filters DefaultFilter => SettingsLoader.Instance
            .GetCharacterSettingsByName("default_equipbestitem", _inventory.IsInWarSet).Filters;

        private void LoadFiltersSettings()
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
                _vm.MissileDamageValue = _filterElement.MissileDamage;
                _vm.WeaponLengthValue = _filterElement.WeaponLength;
                _vm.ThrustDamageValue = _filterElement.ThrustDamage;
                _vm.SwingDamageValue = _filterElement.SwingDamage;
                _vm.AccuracyValue = _filterElement.Accuracy;
                _vm.HandlingValue = _filterElement.Handling;
                _vm.WeightValue = _filterElement.Weight;
                _vm.WeaponBodyArmorValue = _filterElement.WeaponBodyArmor;
            }
        }

        private void VMPropertyChangedWithValue(object sender, PropertyChangedWithValueEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_vm.AccuracyValue):
                    _filterElement.Accuracy = (float) e.Value;
                    break;
                case nameof(_vm.HandlingValue):
                    _filterElement.Handling = (float) e.Value;
                    break;
                case nameof(_vm.ManeuverValue):
                    _filterElement.Maneuver = (float) e.Value;
                    break;
                case nameof(_vm.WeightValue):
                    _filterElement.Weight = (float) e.Value;
                    break;
                case nameof(_vm.ArmArmorValue):
                    _filterElement.ArmArmor = (float) e.Value;
                    break;
                case nameof(_vm.ChargeBonusValue):
                    _filterElement.ChargeBonus = (float) e.Value;
                    break;
                case nameof(_vm.ChargeDamageValue):
                    _filterElement.ChargeDamage = (float) e.Value;
                    break;
                case nameof(_vm.HeadArmorValue):
                    _filterElement.HeadArmor = (float) e.Value;
                    break;
                case nameof(_vm.HitPointsValue):
                    _filterElement.HitPoints = (float) e.Value;
                    break;
                case nameof(_vm.LegArmorValue):
                    _filterElement.LegArmor = (float) e.Value;
                    break;
                case nameof(_vm.ManeuverBonusValue):
                    _filterElement.ManeuverBonus = (float) e.Value;
                    break;
                case nameof(_vm.MissileSpeedValue):
                    _filterElement.MissileSpeed = (float) e.Value;
                    break;
                case nameof(_vm.MissileDamageValue):
                    _filterElement.MissileDamage = (float) e.Value;
                    break;
                case nameof(_vm.SpeedBonusValue):
                    _filterElement.SpeedBonus = (float) e.Value;
                    break;
                case nameof(_vm.SpeedValue):
                    _filterElement.Speed = (float) e.Value;
                    break;
                case nameof(_vm.SwingDamageValue):
                    _filterElement.SwingDamage = (float) e.Value;
                    break;
                case nameof(_vm.SwingSpeedValue):
                    _filterElement.SwingSpeed = (float) e.Value;
                    break;
                case nameof(_vm.ThrustDamageValue):
                    _filterElement.ThrustDamage = (float) e.Value;
                    break;
                case nameof(_vm.ThrustSpeedValue):
                    _filterElement.ThrustSpeed = (float) e.Value;
                    break;
                case nameof(_vm.WeaponLengthValue):
                    _filterElement.WeaponLength = (float) e.Value;
                    break;
                case nameof(_vm.BodyArmorValue):
                    _filterElement.ArmorBodyArmor = (float) e.Value;
                    break;
                case nameof(_vm.MaxDataValue):
                    _filterElement.MaxDataValue = (float) e.Value;
                    break;
                case nameof(_vm.WeaponBodyArmorValue):
                    _filterElement.WeaponBodyArmor = (float) e.Value;
                    break;
            }
        }

        public void OnFinalize()
        {
            _filterElement = null;
        }

        public void SetFiltersSettingsDefault(EquipmentIndex currentSlot)
        {
            _characterSettings.Filters.SetDefault(currentSlot, _inventory.IsInWarSet);
            LoadFiltersSettings();
        }

        public void SetFiltersSettingsLock(EquipmentIndex currentSlot)
        {
            _characterSettings.Filters.SetLock(currentSlot);
            LoadFiltersSettings();
        }

        public void SetEveryCharacterNewDefaultValue(string filterFieldName, float value)
        {
            const double tolerance = 0.00000001f;
            foreach (var rosterElement in InventoryManager.InventoryLogic.RightMemberRoster.GetTroopRoster())
                if (rosterElement.Character.IsHero)
                {
                    var filters = SettingsLoader.Instance.GetCharacterSettingsByName(
                        rosterElement.Character.Name.ToString(),
                        _inventory.IsInWarSet).Filters;

                    switch (filterFieldName)
                    {
                        case "HeadArmor":
                            if (Math.Abs(filters[_currentSlot].HeadArmor - DefaultFilter[_currentSlot].HeadArmor) <
                                tolerance)
                                filters[_currentSlot].HeadArmor = value;
                            break;
                        case "ArmorBodyArmor":
                            if (Math.Abs(filters[_currentSlot].ArmorBodyArmor -
                                         DefaultFilter[_currentSlot].ArmorBodyArmor) < tolerance)
                                filters[_currentSlot].ArmorBodyArmor = value;
                            break;
                        case "LegArmor":
                            if (Math.Abs(filters[_currentSlot].LegArmor - DefaultFilter[_currentSlot].LegArmor) <
                                tolerance)
                                filters[_currentSlot].LegArmor = value;
                            break;
                        case "ArmArmor":
                            if (Math.Abs(filters[_currentSlot].ArmArmor - DefaultFilter[_currentSlot].ArmArmor) <
                                tolerance)
                                filters[_currentSlot].ArmArmor = value;
                            break;
                        case "ManeuverBonus":
                            if (Math.Abs(
                                    filters[_currentSlot].ManeuverBonus - DefaultFilter[_currentSlot].ManeuverBonus) <
                                tolerance)
                                filters[_currentSlot].ManeuverBonus = value;
                            break;
                        case "SpeedBonus":
                            if (Math.Abs(filters[_currentSlot].SpeedBonus - DefaultFilter[_currentSlot].SpeedBonus) <
                                tolerance)
                                filters[_currentSlot].SpeedBonus = value;
                            break;
                        case "ChargeBonus":
                            if (Math.Abs(filters[_currentSlot].ChargeBonus - DefaultFilter[_currentSlot].ChargeBonus) <
                                tolerance)
                                filters[_currentSlot].ChargeBonus = value;
                            break;
                        case "ChargeDamage":
                            if (Math.Abs(filters[_currentSlot].ChargeDamage -
                                         DefaultFilter[_currentSlot].ChargeDamage) < tolerance)
                                filters[_currentSlot].ChargeDamage = value;
                            break;
                        case "HitPoints":
                            if (Math.Abs(filters[_currentSlot].HitPoints - DefaultFilter[_currentSlot].HitPoints) <
                                tolerance)
                                filters[_currentSlot].HitPoints = value;
                            break;
                        case "Maneuver":
                            if (Math.Abs(filters[_currentSlot].Maneuver - DefaultFilter[_currentSlot].Maneuver) <
                                tolerance)
                                filters[_currentSlot].Maneuver = value;
                            break;
                        case "Speed":
                            if (Math.Abs(filters[_currentSlot].Speed - DefaultFilter[_currentSlot].Speed) < tolerance)
                                filters[_currentSlot].Speed = value;
                            break;
                        case "MaxDataValue":
                            if (Math.Abs(filters[_currentSlot].MaxDataValue -
                                         DefaultFilter[_currentSlot].MaxDataValue) < tolerance)
                                filters[_currentSlot].MaxDataValue = value;
                            break;
                        case "ThrustSpeed":
                            if (Math.Abs(filters[_currentSlot].ThrustSpeed - DefaultFilter[_currentSlot].ThrustSpeed) <
                                tolerance)
                                filters[_currentSlot].ThrustSpeed = value;
                            break;
                        case "SwingSpeed":
                            if (Math.Abs(filters[_currentSlot].SwingSpeed - DefaultFilter[_currentSlot].SwingSpeed) <
                                tolerance)
                                filters[_currentSlot].SwingSpeed = value;
                            break;
                        case "MissileSpeed":
                            if (Math.Abs(filters[_currentSlot].MissileSpeed -
                                         DefaultFilter[_currentSlot].MissileSpeed) < tolerance)
                                filters[_currentSlot].MissileSpeed = value;
                            break;
                        case "MissileDamage":
                            if (Math.Abs(filters[_currentSlot].MissileDamage -
                                         DefaultFilter[_currentSlot].MissileDamage) < tolerance)
                                filters[_currentSlot].MissileDamage = value;
                            break;
                        case "WeaponLength":
                            if (Math.Abs(filters[_currentSlot].WeaponLength -
                                         DefaultFilter[_currentSlot].WeaponLength) < tolerance)
                                filters[_currentSlot].WeaponLength = value;
                            break;
                        case "ThrustDamage":
                            if (Math.Abs(filters[_currentSlot].ThrustDamage -
                                         DefaultFilter[_currentSlot].ThrustDamage) < tolerance)
                                filters[_currentSlot].ThrustDamage = value;
                            break;
                        case "SwingDamage":
                            if (Math.Abs(filters[_currentSlot].SwingDamage - DefaultFilter[_currentSlot].SwingDamage) <
                                tolerance)
                                filters[_currentSlot].SwingDamage = value;
                            break;
                        case "Accuracy":
                            if (Math.Abs(filters[_currentSlot].Accuracy - DefaultFilter[_currentSlot].Accuracy) <
                                tolerance)
                                filters[_currentSlot].Accuracy = value;
                            break;
                        case "Handling":
                            if (Math.Abs(filters[_currentSlot].Handling - DefaultFilter[_currentSlot].Handling) <
                                tolerance)
                                filters[_currentSlot].Handling = value;
                            break;
                        case "WeaponBodyArmor":
                            if (Math.Abs(filters[_currentSlot].WeaponBodyArmor -
                                         DefaultFilter[_currentSlot].WeaponBodyArmor) < tolerance)
                                filters[_currentSlot].WeaponBodyArmor = value;
                            break;
                        case "Weight":
                            if (Math.Abs(filters[_currentSlot].Weight - DefaultFilter[_currentSlot].Weight) < tolerance)
                                filters[_currentSlot].Weight = value;
                            break;
                    }
                }
        }

        public void RefreshValues()
        {
            //throw new NotImplementedException();
        }
    }
}