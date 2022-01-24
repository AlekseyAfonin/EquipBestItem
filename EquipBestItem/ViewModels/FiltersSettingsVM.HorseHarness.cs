using System;
using System.Globalization;
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
                OnPropertyChanged("SpeedBonusValueText");
            }
        }
        
        [DataSourceProperty] 
        public string SpeedBonusValueText => SpeedBonusValue.ToString(CultureInfo.InvariantCulture);
        
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
                OnPropertyChanged("ChargeBonusValueText");
            }
        }
        
        [DataSourceProperty] 
        public string ChargeBonusValueText => ChargeBonusValue.ToString(CultureInfo.InvariantCulture);
        

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
                OnPropertyChanged("ManeuverBonusValueText");
            }
        }
        
        [DataSourceProperty] 
        public string ManeuverBonusValueText => ManeuverBonusValue.ToString(CultureInfo.InvariantCulture);
        
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
            IsChargeBonusValueIsDefault = true;
        }
        
        public void ExecuteManeuverBonusValueDefault()
        {
            IsManeuverBonusValueIsDefault = true;
        }
        
        public void ExecuteSpeedBonusValueDefault()
        {
            IsSpeedBonusValueIsDefault = true;
        }
    }
}