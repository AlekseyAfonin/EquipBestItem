using System;
using System.ComponentModel;
using EquipBestItem.Behaviors;
using EquipBestItem.Layers;
using EquipBestItem.Models;
using EquipBestItem.Settings;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    /// <summary>
    /// The main part with general methods and fields
    /// </summary>
    public partial class FiltersSettingsVM : ViewModel
    {
        const float Tolerance = 0.000000001f;

        private string _headerText;
        
        [DataSourceProperty]
        public string HeaderText
        {
            get => _headerText;
            set
            {
                if (_headerText == value) return;
                _headerText = value;
                OnPropertyChanged();
            }
        }
        
        private float _weightValue;
        
        [DataSourceProperty]
        public float WeightValue
        {
            get => _weightValue;
            set
            {
                if (!(Math.Abs(_weightValue - value) > Tolerance)) return;
                _weightValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
                UpdateArmorProperties();
                UpdateHorseHarnessProperties();
                OnPropertyChanged("WeightValueText");
            }
        }
        
        [DataSourceProperty] 
        public string WeightValueText
        {
            get
            {
                if (_currentSlot >= EquipmentIndex.WeaponItemBeginSlot && _currentSlot < EquipmentIndex.NumAllWeaponSlots)
                {
                    return GetWeaponValuePercentText(WeightValue);
                }

                if (_currentSlot >= EquipmentIndex.ArmorItemBeginSlot && _currentSlot < EquipmentIndex.ArmorItemEndSlot)
                {
                    return GetArmorValuePercentText(WeightValue);
                }
                
                if (_currentSlot == EquipmentIndex.HorseHarness)
                {
                    return GetHorseHarnessValuePercentText(WeightValue);
                }

                return "err";
            }
        }

        private bool _weightValueIsDefault;
        
        [DataSourceProperty]
        public bool IsWeightValueIsDefault
        {
            get => _weightValueIsDefault;
            set
            {
                if (_weightValueIsDefault == value) return;
                _weightValueIsDefault = value;
                OnPropertyChanged();
            }
        }
        
        private bool _hiddenWeight;
        
        [DataSourceProperty]
        public bool IsHiddenWeight
        {
            get => _hiddenWeight;
            set
            {
                if (_hiddenWeight == value) return;
                _hiddenWeight = value;
                OnPropertyChanged();
            }
        }
        
        [DataSourceProperty]
        public string WeightText { get; } = "Weight";

        

        private EquipmentIndex _currentSlot;
        private SPInventoryVM _inventory;
        private FiltersSettingsModel _model;
        private FiltersSettingsLayer _layer;
        
        public FiltersSettingsVM(SPInventoryVM inventory, EquipmentIndex currentSlot, FiltersSettingsLayer layer)
        {
            _layer = layer;
            _currentSlot = currentSlot;
            _inventory = inventory;
            _model = new FiltersSettingsModel(this, _inventory, _currentSlot);
            PropertyChanged += FilterSettingsVM_PropertyChanged;
            //RefreshValues();
        }

        private void FilterSettingsVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RefreshValues();
        }

        public sealed override void RefreshValues()
        {
            base.RefreshValues();
            _model.RefreshValues();
            //TODO
            UpdateWeaponCheckBoxStates();
            UpdateArmorCheckBoxStates();
            UpdateHorseCheckBoxStates();
            UpdateHorseHarnessCheckBoxStates();
        }
        
        public void ExecuteWeightValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.Weight), WeightValue);
            _model.DefaultFilter[_currentSlot].Weight = WeightValue;
            RefreshValues();
        }
        
        public void ExecuteDefault()
        {
            _model.SetFiltersSettingsDefault(_currentSlot);
        }
        
        public void ExecuteLock()
        {
            _model.SetFiltersSettingsLock(_currentSlot);
        }
        
        public void ExecuteClose()
        {
            //TODO
            _layer.Model.ShowHideFilterSettingsLayer(InventoryBehavior.InventoryScreen, EquipmentIndex.None);
        }

        public override void OnFinalize()
        {
            _inventory = null;
            _model.OnFinalize();
            _model = null;
            base.OnFinalize();
        }
    }
}