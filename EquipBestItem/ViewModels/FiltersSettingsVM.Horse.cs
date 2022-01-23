using System;
using System.Globalization;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    partial class FiltersSettingsVM
    {
        #region Slider properties

        private float _chargeDamageValue;
        
        [DataSourceProperty]
        public float ChargeDamageValue
        {
            get => _chargeDamageValue;
            set
            {
                if (!(Math.Abs(_chargeDamageValue - value) > Tolerance)) return;
                _chargeDamageValue = value;
                OnPropertyChanged();
                OnPropertyChanged("ChargeDamageValueText");
            }
        }
        
        [DataSourceProperty] 
        public string ChargeDamageValueText => ChargeDamageValue.ToString(CultureInfo.InvariantCulture);
        
        private float _hitPointsValue;
        
        [DataSourceProperty]
        public float HitPointsValue
        {
            get => _hitPointsValue;
            set
            {
                if (!(Math.Abs(_hitPointsValue - value) > Tolerance)) return;
                _hitPointsValue = value;
                OnPropertyChanged();
                OnPropertyChanged("HitPointsValueText");
            }
        }
        
        [DataSourceProperty] 
        public string HitPointsValueText => HitPointsValue.ToString(CultureInfo.InvariantCulture);
        
        private float _maneuverValueValue;
        
        [DataSourceProperty]
        public float ManeuverValue
        {
            get => _maneuverValueValue;
            set
            {
                if (!(Math.Abs(_maneuverValueValue - value) > Tolerance)) return;
                _maneuverValueValue = value;
                OnPropertyChanged();
                OnPropertyChanged("ManeuverValueText");
            }
        }
        
        [DataSourceProperty] 
        public string ManeuverValueText => ManeuverValue.ToString(CultureInfo.InvariantCulture);
        
        private float _speedValueValue;
        
        [DataSourceProperty]
        public float SpeedValue
        {
            get => _speedValueValue;
            set
            {
                if (!(Math.Abs(_speedValueValue - value) > Tolerance)) return;
                _speedValueValue = value;
                OnPropertyChanged();
                OnPropertyChanged("SpeedValueText");
            }
        }
        
        [DataSourceProperty] 
        public string SpeedValueText => SpeedValue.ToString(CultureInfo.InvariantCulture);
        
        #endregion

        #region CheckBox properties

        private bool _isChargeDamageValueIsDefault;
        [DataSourceProperty]
        public bool IsChargeDamageValueIsDefault
        {
            get => _isChargeDamageValueIsDefault;
            set
            {
                if (_isChargeDamageValueIsDefault == value) return;
                _isChargeDamageValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isHitPointsValueIsDefault;
        [DataSourceProperty]
        public bool IsHitPointsValueIsDefault
        {
            get => _isHitPointsValueIsDefault;
            set
            {
                if (_isHitPointsValueIsDefault == value) return;
                _isHitPointsValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isManeuverValueIsDefault;
        [DataSourceProperty]
        public bool IsManeuverValueIsDefault
        {
            get => _isManeuverValueIsDefault;
            set
            {
                if (_isManeuverValueIsDefault == value) return;
                _isManeuverValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isSpeedValueIsDefault;
        [DataSourceProperty]
        public bool IsSpeedValueIsDefault
        {
            get => _isSpeedValueIsDefault;
            set
            {
                if (_isSpeedValueIsDefault == value) return;
                _isSpeedValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        #endregion
        
        #region Row visibility properties
        
        private bool _hiddenHitPoints;
        
        [DataSourceProperty]
        public bool IsHiddenHitPoints
        {
            get => _hiddenHitPoints;
            set
            {
                if (_hiddenHitPoints == value) return;
                _hiddenHitPoints = value;
                OnPropertyChanged();
            }
        }
        
        private bool _hiddenChargeDamage;
        
        [DataSourceProperty]
        public bool IsHiddenChargeDamage
        {
            get => _hiddenChargeDamage;
            set
            {
                if (_hiddenChargeDamage == value) return;
                _hiddenChargeDamage = value;
                OnPropertyChanged();
            }
        }
        
        private bool _hiddenManeuver;
        
        [DataSourceProperty]
        public bool IsHiddenManeuver
        {
            get => _hiddenManeuver;
            set
            {
                if (_hiddenManeuver == value) return;
                _hiddenManeuver = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenSpeed;
        
        [DataSourceProperty]
        public bool IsHiddenSpeed
        {
            get => _hiddenSpeed;
            set
            {
                if (_hiddenSpeed == value) return;
                _hiddenSpeed = value;
                OnPropertyChanged();
            }
        }

        #endregion
        
        [DataSourceProperty]
        public string HitPointsText { get; } = "Hit Points";
        
        [DataSourceProperty]
        public string ChargeDamageText { get; } = "Charge damage";
        
        [DataSourceProperty]
        public string ManeuverText { get; } = "Maneuver";
        
        [DataSourceProperty]
        public string SpeedText { get; } = "Speed";
        
        public void ExecuteHitPointsValueDefault()
        {
            IsHitPointsValueIsDefault = true;
        }
        
        public void ExecuteChargeDamageValueDefault()
        {
            IsChargeDamageValueIsDefault = true;
        }
        
        public void ExecuteManeuverValueDefault()
        {
            IsManeuverValueIsDefault = true;
        }
        
        public void ExecuteSpeedValueDefault()
        {
            IsSpeedValueIsDefault = true;
        }
    }
}