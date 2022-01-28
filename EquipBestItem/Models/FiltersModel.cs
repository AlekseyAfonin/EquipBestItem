using EquipBestItem.Layers;
using EquipBestItem.Settings;
using EquipBestItem.ViewModels;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;

namespace EquipBestItem.Models
{
    public class FiltersModel
    {
        private readonly SPInventoryVM _inventory;
        private CharacterSettings _characterSettings;
        private FiltersVM _vm;

        public FiltersModel(FiltersVM vm)
        {
            _inventory = EquipBestItemManager.Instance.Inventory;
            _vm = vm;
            _characterSettings =
                SettingsLoader.Instance.GetCharacterSettingsByName(_inventory.CurrentCharacterName,
                    _inventory.IsInWarSet);
        }

        private Filters _filters => _characterSettings.Filters;

        public void RefreshValues()
        {
            if (_characterSettings.Name != _inventory.CurrentCharacterName)
                _characterSettings =
                    SettingsLoader.Instance.GetCharacterSettingsByName(_inventory.CurrentCharacterName,
                        _inventory.IsInWarSet);

            try
            {
                //Icon state
                _vm.IsHelmFilterSelected = !_filters.IsDefault(EquipmentIndex.Head, _inventory.IsInWarSet);
                _vm.IsHelmFilterLocked = _filters.IsLocked(EquipmentIndex.Head);
                _vm.IsCloakFilterSelected = !_filters.IsDefault(EquipmentIndex.Cape, _inventory.IsInWarSet);
                _vm.IsCloakFilterLocked = _filters.IsLocked(EquipmentIndex.Cape);
                _vm.IsArmorFilterSelected = !_filters.IsDefault(EquipmentIndex.Body, _inventory.IsInWarSet);
                _vm.IsArmorFilterLocked = _filters.IsLocked(EquipmentIndex.Body);
                _vm.IsGloveFilterSelected = !_filters.IsDefault(EquipmentIndex.Gloves, _inventory.IsInWarSet);
                _vm.IsGloveFilterLocked = _filters.IsLocked(EquipmentIndex.Gloves);
                _vm.IsBootFilterSelected = !_filters.IsDefault(EquipmentIndex.Leg, _inventory.IsInWarSet);
                _vm.IsBootFilterLocked = _filters.IsLocked(EquipmentIndex.Leg);
                _vm.IsMountFilterSelected = !_filters.IsDefault(EquipmentIndex.Horse, _inventory.IsInWarSet);
                _vm.IsMountFilterLocked = _filters.IsLocked(EquipmentIndex.Horse);
                _vm.IsHarnessFilterSelected = !_filters.IsDefault(EquipmentIndex.HorseHarness, _inventory.IsInWarSet);
                _vm.IsHarnessFilterLocked = _filters.IsLocked(EquipmentIndex.HorseHarness);
                _vm.IsWeapon1FilterSelected = !_filters.IsDefault(EquipmentIndex.Weapon0, _inventory.IsInWarSet);
                _vm.IsWeapon1FilterLocked = _filters.IsLocked(EquipmentIndex.Weapon0);
                _vm.IsWeapon2FilterSelected = !_filters.IsDefault(EquipmentIndex.Weapon1, _inventory.IsInWarSet);
                _vm.IsWeapon2FilterLocked = _filters.IsLocked(EquipmentIndex.Weapon1);
                _vm.IsWeapon3FilterSelected = !_filters.IsDefault(EquipmentIndex.Weapon2, _inventory.IsInWarSet);
                _vm.IsWeapon3FilterLocked = _filters.IsLocked(EquipmentIndex.Weapon2);
                _vm.IsWeapon4FilterSelected = !_filters.IsDefault(EquipmentIndex.Weapon3, _inventory.IsInWarSet);
                _vm.IsWeapon4FilterLocked = _filters.IsLocked(EquipmentIndex.Weapon3);
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
        }

        public void ShowHideFilterSettingsLayer(EquipmentIndex selectedSlot)
        {
            var filtersSettingsLayer = EquipBestItemManager.Instance.FindLayer<FiltersSettingsLayer>();

            if (filtersSettingsLayer == null)
            {
                var _filterLayer = new FiltersSettingsLayer(17, selectedSlot);
                EquipBestItemManager.Instance.AddLayer(_filterLayer);
                _filterLayer.InputRestrictions.SetInputRestrictions();

                return;
            }

            if (filtersSettingsLayer.SelectedSlot == selectedSlot)
            {
                EquipBestItemManager.Instance.RemoveLayer(filtersSettingsLayer);
            }
            else
            {
                EquipBestItemManager.Instance.RemoveLayer(filtersSettingsLayer);

                var _filterLayer = new FiltersSettingsLayer(17, selectedSlot);
                EquipBestItemManager.Instance.AddLayer(_filterLayer);
                _filterLayer.InputRestrictions.SetInputRestrictions();
            }
        }

        public void OnFinalize()
        {
            _vm = null;
        }
    }
}