using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using EquipBestItem.Models;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
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
                OnPropertyChanged("WeightValueText");
            }
        }
        
        [DataSourceProperty] 
        public string WeightValueText => GetValuePercentText(WeightValue);

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
        
        public FiltersSettingsVM(SPInventoryVM inventory, EquipmentIndex currentSlot)
        {
            _currentSlot = currentSlot;
            _inventory = inventory;
            _model = new FiltersSettingsModel(this, _inventory, _currentSlot);
        }
        
        public sealed override void RefreshValues()
        {
            base.RefreshValues();
        }
        
        public void ExecuteWeightValueDefault()
        {
            IsWeightValueIsDefault = true;
        }

        private string GetValuePercentText(float propertyValue)
        {
            float sum = Math.Abs(AccuracyValue) +
                        Math.Abs(WeaponBodyArmorValue) +
                        Math.Abs(HandlingValue) +
                        Math.Abs(MaxDataValue) +
                        Math.Abs(MissileSpeedValue) +
                        Math.Abs(SwingDamageValue) +
                        Math.Abs(SwingSpeedValue) +
                        Math.Abs(ThrustDamageValue) +
                        Math.Abs(ThrustSpeedValue) +
                        Math.Abs(WeaponLengthValue) +
                        Math.Abs(WeightValue);
            return Math.Round(propertyValue / sum * 100).ToString(CultureInfo.InvariantCulture);
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