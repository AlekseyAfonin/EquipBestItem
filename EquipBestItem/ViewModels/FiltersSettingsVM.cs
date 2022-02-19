using System;
using System.ComponentModel;
using EquipBestItem.Layers;
using EquipBestItem.Models;
using EquipBestItem.Settings;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    /// <summary>
    ///     The main part with general methods and fields
    /// </summary>
    public partial class FiltersSettingsVM : ViewModel
    {
        private const float Tolerance = 0.000000001f;


        private readonly EquipmentIndex _currentSlot;
        private EquipBestItemManager _manager;

        private string _headerText;

        private bool _hiddenWeight;
        private FiltersSettingsModel _model;

        private float _weightValue;

        private bool _weightValueIsDefault;

        public FiltersSettingsVM(EquipmentIndex currentSlot)
        {
            _manager = EquipBestItemManager.Instance;
            _currentSlot = currentSlot;
            _model = new FiltersSettingsModel(this, _currentSlot);
        }

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
                OnPropertyChanged(nameof(WeightValueText));
            }
        }

        [DataSourceProperty]
        public string WeightValueText
        {
            get
            {
                if (_currentSlot is >= EquipmentIndex.WeaponItemBeginSlot and < EquipmentIndex.NumAllWeaponSlots)
                    return GetWeaponValuePercentText(WeightValue);

                if (_currentSlot is >= EquipmentIndex.ArmorItemBeginSlot and < EquipmentIndex.ArmorItemEndSlot)
                    return GetArmorValuePercentText(WeightValue);

                if (_currentSlot == EquipmentIndex.HorseHarness) return GetHorseHarnessValuePercentText(WeightValue);

                return "err";
            }
        }

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

        [DataSourceProperty] public string WeightText { get; } = GameTexts.FindText("str_weight_text").ToString();

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
            var filtersSettingsLayer = _manager.FindLayer<FiltersSettingsLayer>();
            _manager.RemoveLayer(filtersSettingsLayer);
        }

        public override void OnFinalize()
        {
            _manager = null;
            _model.OnFinalize();
            _model = null;
            base.OnFinalize();
        }
    }
}