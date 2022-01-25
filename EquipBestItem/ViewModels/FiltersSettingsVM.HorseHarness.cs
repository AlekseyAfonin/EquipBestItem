using System;
using System.Globalization;
using EquipBestItem.Settings;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    partial class FiltersSettingsVM
    {
        #region Slider properties
        
        private float _speedBonusValue;
        
        [DataSourceProperty]
        public float SpeedBonusValue
        {
            get => _speedBonusValue;
            set
            {
                if (!(Math.Abs(_speedBonusValue - value) > Tolerance)) return;
                _speedBonusValue = value;
                OnPropertyChangedWithValue(value);
                UpdateHorseHarnessProperties();
                OnPropertyChanged("SpeedBonusValueText");
            }
        }
        
        [DataSourceProperty] 
        public string SpeedBonusValueText => GetHorseHarnessValuePercentText(SpeedBonusValue);
        
        private float _chargeBonusValue;
        
        [DataSourceProperty]
        public float ChargeBonusValue
        {
            get => _chargeBonusValue;
            set
            {
                if (!(Math.Abs(_chargeBonusValue - value) > Tolerance)) return;
                _chargeBonusValue = value;
                OnPropertyChangedWithValue(value);
                UpdateHorseHarnessProperties();
                OnPropertyChanged("ChargeBonusValueText");
            }
        }
        
        [DataSourceProperty] 
        public string ChargeBonusValueText => GetHorseHarnessValuePercentText(ChargeBonusValue);
        

        private float _maneuverBonusValue;
        
        [DataSourceProperty]
        public float ManeuverBonusValue
        {
            get => _maneuverBonusValue;
            set
            {
                if (!(Math.Abs(_maneuverBonusValue - value) > Tolerance)) return;
                _maneuverBonusValue = value;
                OnPropertyChangedWithValue(value);
                UpdateHorseHarnessProperties();
                OnPropertyChanged("ManeuverBonusValueText");
            }
        }
        
        [DataSourceProperty] 
        public string ManeuverBonusValueText => GetHorseHarnessValuePercentText(ManeuverBonusValue);
        
        #endregion

        #region CheckBox properties

        private bool _isSpeedBonusValueIsDefault;
        
        [DataSourceProperty]
        public bool IsSpeedBonusValueIsDefault
        {
            get => _isSpeedBonusValueIsDefault;
            set
            {
                if (_isSpeedBonusValueIsDefault == value) return;
                _isSpeedBonusValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isChargeBonusValueIsDefault;
        
        [DataSourceProperty]
        public bool IsChargeBonusValueIsDefault
        {
            get => _isChargeBonusValueIsDefault;
            set
            {
                if (_isChargeBonusValueIsDefault == value) return;
                _isChargeBonusValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isManeuverBonusValueIsDefault;
        
        [DataSourceProperty]
        public bool IsManeuverBonusValueIsDefault
        {
            get => _isManeuverBonusValueIsDefault;
            set
            {
                if (_isManeuverBonusValueIsDefault == value) return;
                _isManeuverBonusValueIsDefault = value;
                OnPropertyChanged();
            }
        }
        
        #endregion

        #region Rows visibility properties 

        private bool _hiddenChargeBonus;
        
        [DataSourceProperty]
        public bool IsHiddenChargeBonus
        {
            get => _hiddenChargeBonus;
            set
            {
                if (_hiddenChargeBonus == value) return;
                _hiddenChargeBonus = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenManeuverBonus;
        
        [DataSourceProperty]
        public bool IsHiddenManeuverBonus
        {
            get => _hiddenManeuverBonus;
            set
            {
                if (_hiddenManeuverBonus == value) return;
                _hiddenManeuverBonus = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenSpeedBonus;
        
        [DataSourceProperty]
        public bool IsHiddenSpeedBonus
        {
            get => _hiddenSpeedBonus;
            set
            {
                if (_hiddenSpeedBonus == value) return;
                _hiddenSpeedBonus = value;
                OnPropertyChanged();
            }
        }
        
        #endregion
        
        [DataSourceProperty]
        public string ChargeBonusText { get; } = "Charge bonus";
        
        [DataSourceProperty]
        public string ManeuverBonusText { get; } = "Maneuver bonus";
        
        [DataSourceProperty]
        public string SpeedBonusText { get; } = "Speed bonus";
        
        public void ExecuteChargeBonusValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.ChargeBonus), ChargeBonusValue);
            _model.DefaultFilter[_currentSlot].ChargeBonus = ChargeBonusValue;
            RefreshValues();
        }
        
        public void ExecuteManeuverBonusValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.ManeuverBonus), ManeuverBonusValue);
            _model.DefaultFilter[_currentSlot].ManeuverBonus = ManeuverBonusValue;
            RefreshValues();
        }
        
        public void ExecuteSpeedBonusValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.SpeedBonus), SpeedBonusValue);
            _model.DefaultFilter[_currentSlot].SpeedBonus = SpeedBonusValue;
            RefreshValues();
        }
        
        private void UpdateHorseHarnessProperties()
        {
            OnPropertyChanged("BodyArmorValueText");
            OnPropertyChanged("ChargeBonusValueText");
            OnPropertyChanged("ManeuverBonusValueText");
            OnPropertyChanged("SpeedBonusValueText");
            OnPropertyChanged("WeightValueText");
        }
        
        private void UpdateHorseHarnessCheckBoxStates()
        {
            IsBodyArmorValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].ArmorBodyArmor - BodyArmorValue) < Tolerance;
            IsChargeBonusValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].ChargeBonus - ChargeBonusValue) < Tolerance;
            IsManeuverBonusValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].ManeuverBonus - ManeuverBonusValue) < Tolerance;
            IsSpeedBonusValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].SpeedBonus - SpeedBonusValue) < Tolerance;
            IsWeightValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].Weight - WeightValue) < Tolerance;
        }
        
        private string GetHorseHarnessValuePercentText(float propertyValue)
        {
            float sum = Math.Abs(BodyArmorValue) +
                        Math.Abs(ChargeBonusValue) +
                        Math.Abs(ManeuverBonusValue) +
                        Math.Abs(SpeedBonusValue) +
                        Math.Abs(WeightValue);
            if (sum == 0) return "0%";
            
            return Math.Round(propertyValue / sum * 100).ToString(CultureInfo.InvariantCulture) + "%";
        }
    }
}