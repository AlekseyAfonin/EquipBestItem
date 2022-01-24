using System;
using EquipBestItem.Layers;
using EquipBestItem.Settings;
using EquipBestItem.ViewModels;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;

namespace EquipBestItem.Models
{
    public class FiltersModel
    {
        private SPInventoryVM _inventory;
        private FiltersVM _vm;
        private CharacterSettings _characterSettings;
        
        public FiltersModel(FiltersVM vm, SPInventoryVM inventory)
        {
            _inventory = inventory;
            _vm = vm;
            _characterSettings =
                SettingsLoader.Instance.GetCharacterSettingsByName(_inventory.CurrentCharacterName,
                    _inventory.IsInWarSet);
        }

        
        
        public void RefreshValues()
        {
            if (_characterSettings.Name != _inventory.CurrentCharacterName)
                _characterSettings =
                    SettingsLoader.Instance.GetCharacterSettingsByName(_inventory.CurrentCharacterName,
                        _inventory.IsInWarSet);
            
            try
            {
                //Icon state
                // _vm.IsHelmFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Head, _inventory.IsInWarSet);
                // _vm.IsHelmFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Head);
                // _vm.IsCloakFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Cape, _inventory.IsInWarSet);
                // _vm.IsCloakFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Cape);
                // _vm.IsArmorFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Body, _inventory.IsInWarSet);
                // _vm.IsArmorFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Body);
                // _vm.IsGloveFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Gloves, _inventory.IsInWarSet);
                // _vm.IsGloveFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Gloves);
                // _vm.IsBootFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Leg, _inventory.IsInWarSet);
                // _vm.IsBootFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Leg);
                // _vm.IsMountFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Horse, _inventory.IsInWarSet);
                // _vm.IsMountFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Horse);
                // _vm.IsHarnessFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.HorseHarness, _inventory.IsInWarSet);
                // _vm.IsHarnessFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.HorseHarness);
                // _vm.IsWeapon1FilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Weapon0, _inventory.IsInWarSet);
                // _vm.IsWeapon1FilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Weapon0);
                // _vm.IsWeapon2FilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Weapon1, _inventory.IsInWarSet);
                // _vm.IsWeapon2FilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Weapon1);
                // _vm.IsWeapon3FilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Weapon2, _inventory.IsInWarSet);
                // _vm.IsWeapon3FilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Weapon2);
                // _vm.IsWeapon4FilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Weapon3, _inventory.IsInWarSet);
                // _vm.IsWeapon4FilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Weapon3);
            }
            catch (MBException e)
            {
                if (SettingsLoader.Instance.Settings.Debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message + e.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
        }

        private EquipmentIndex _currentSlot;
        private FiltersSettingsLayer _currentFilterLayer;

        public void ShowHideFilterSettingsLayer(InventoryGauntletScreen inventoryScreen, EquipmentIndex selectedSlot)
        {
            if (_currentFilterLayer != null)
                inventoryScreen.RemoveLayer(_currentFilterLayer);
            
            if (_currentSlot != selectedSlot)
            {
                _currentFilterLayer = new FiltersSettingsLayer(17, _inventory, selectedSlot);
                inventoryScreen?.AddLayer(_currentFilterLayer);
                _currentFilterLayer.InputRestrictions.SetInputRestrictions();
                _currentSlot = selectedSlot;
            }
            else
            {
                _currentSlot = EquipmentIndex.None;
            }
        }

        public void OnFinalize()
        {
            _inventory = null;
            _vm = null;
            _currentFilterLayer = null;
        }
    }
}