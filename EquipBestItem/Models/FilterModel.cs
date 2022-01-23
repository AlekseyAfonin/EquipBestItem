using System;
using EquipBestItem.Layers;
using EquipBestItem.Settings;
using EquipBestItem.ViewModels;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;

namespace EquipBestItem.Models
{
    public class FilterModel
    {
        private SPInventoryVM _inventory;
        private FilterVM _vm;

        public FilterModel(FilterVM vm, SPInventoryVM inventory)
        {
            _inventory = inventory;
            _vm = vm;
        }

        private CharacterSettings _characterSettings;
        
        public void RefreshValues()
        {
            try
            {
                //Icon state
                _vm.IsHelmFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Head, _inventory.IsInWarSet);
                _vm.IsHelmFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Head);
                _vm.IsCloakFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Cape, _inventory.IsInWarSet);
                _vm.IsCloakFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Cape);
                _vm.IsArmorFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Body, _inventory.IsInWarSet);
                _vm.IsArmorFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Body);
                _vm.IsGloveFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Gloves, _inventory.IsInWarSet);
                _vm.IsGloveFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Gloves);
                _vm.IsBootFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Leg, _inventory.IsInWarSet);
                _vm.IsBootFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Leg);
                _vm.IsMountFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Horse, _inventory.IsInWarSet);
                _vm.IsMountFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Horse);
                _vm.IsHarnessFilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.HorseHarness, _inventory.IsInWarSet);
                _vm.IsHarnessFilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.HorseHarness);
                _vm.IsWeapon1FilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Weapon0, _inventory.IsInWarSet);
                _vm.IsWeapon1FilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Weapon0);
                _vm.IsWeapon2FilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Weapon1, _inventory.IsInWarSet);
                _vm.IsWeapon2FilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Weapon1);
                _vm.IsWeapon3FilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Weapon2, _inventory.IsInWarSet);
                _vm.IsWeapon3FilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Weapon2);
                _vm.IsWeapon4FilterSelected = !_characterSettings.Filters.IsDefault(EquipmentIndex.Weapon3, _inventory.IsInWarSet);
                _vm.IsWeapon4FilterLocked = _characterSettings.Filters.IsLocked(EquipmentIndex.Weapon3);
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
        private FilterArmorLayer _currentFilterLayer;

        public void ShowHideArmorLayer(InventoryGauntletScreen inventoryScreen, EquipmentIndex selectedSlot)
        {
            if (_currentFilterLayer != null)
                inventoryScreen.RemoveLayer(_currentFilterLayer);
            
            switch (selectedSlot)
            {
                case EquipmentIndex.Head:
                {
                    AddLayer("Head Filter");
                    break;
                }
                case EquipmentIndex.Cape:
                {
                    AddLayer("Cape Filter");
                    break;
                }
                case EquipmentIndex.Body:
                {
                    AddLayer("Body Filter");
                    break;
                }
                case EquipmentIndex.Gloves:
                {
                    AddLayer("Gloves Filter");
                    break;
                }
                case EquipmentIndex.Leg:
                {
                    AddLayer("Leg Filter");
                    break;
                }
            }

            void AddLayer(string header)
            {
                if (_currentSlot != selectedSlot)
                {
                    _currentFilterLayer = new FilterArmorLayer(16, header, _inventory, selectedSlot);
                    inventoryScreen?.AddLayer(_currentFilterLayer);
                    _currentFilterLayer.InputRestrictions.SetInputRestrictions();
                    _currentSlot = selectedSlot;
                }
                else
                {
                    _currentSlot = EquipmentIndex.None;
                }
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