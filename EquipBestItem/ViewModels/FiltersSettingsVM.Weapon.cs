using System;
using System.Globalization;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    partial class FiltersSettingsVM
    {
        #region Slider properties

        private float _maxDataValue;
        
        [DataSourceProperty]
        public float MaxDataValue
        {
            get => _maxDataValue;
            set
            {
                if (!(Math.Abs(_maxDataValue - value) > Tolerance)) return;
                _maxDataValue = value;
                OnPropertyChanged();
                OnPropertyChanged("MaxDataValueText");
            }
        }
        
        [DataSourceProperty] 
        public string MaxDataValueText => MaxDataValue.ToString(CultureInfo.InvariantCulture);
        
        private float _thrustSpeedValue;
        
        [DataSourceProperty]
        public float ThrustSpeedValue
        {
            get => _thrustSpeedValue;
            set
            {
                if (!(Math.Abs(_thrustSpeedValue - value) > Tolerance)) return;
                _thrustSpeedValue = value;
                OnPropertyChanged();
                OnPropertyChanged("ThrustSpeedValueText");
            }
        }
        
        [DataSourceProperty] 
        public string ThrustSpeedValueText => ThrustSpeedValue.ToString(CultureInfo.InvariantCulture);
        
        private float _swingSpeedValue;
        
        [DataSourceProperty]
        public float SwingSpeedValue
        {
            get => _swingSpeedValue;
            set
            {
                if (!(Math.Abs(_swingSpeedValue - value) > Tolerance)) return;
                _swingSpeedValue = value;
                OnPropertyChanged();
                OnPropertyChanged("SwingSpeedValueText");
            }
        }
        
        [DataSourceProperty] 
        public string SwingSpeedValueText => SwingSpeedValue.ToString(CultureInfo.InvariantCulture);
        
        private float _missileSpeedValue;
        
        [DataSourceProperty]
        public float MissileSpeedValue
        {
            get => _missileSpeedValue;
            set
            {
                if (!(Math.Abs(_missileSpeedValue - value) > Tolerance)) return;
                _missileSpeedValue = value;
                OnPropertyChanged();
                OnPropertyChanged("MissileSpeedValueText");
            }
        }
        
        [DataSourceProperty] 
        public string MissileSpeedValueText => MissileSpeedValue.ToString(CultureInfo.InvariantCulture);
        
        private float _weaponLengthValue;
        
        [DataSourceProperty]
        public float WeaponLengthValue
        {
            get => _weaponLengthValue;
            set
            {
                if (!(Math.Abs(_weaponLengthValue - value) > Tolerance)) return;
                _weaponLengthValue = value;
                OnPropertyChanged();
                OnPropertyChanged("WeaponLengthValueText");
            }
        }
        
        [DataSourceProperty] 
        public string WeaponLengthValueText => WeaponLengthValue.ToString(CultureInfo.InvariantCulture);
        
        private float _thrustDamageValue;
        
        [DataSourceProperty]
        public float ThrustDamageValue
        {
            get => _thrustDamageValue;
            set
            {
                if (!(Math.Abs(_thrustDamageValue - value) > Tolerance)) return;
                _thrustDamageValue = value;
                OnPropertyChanged();
                OnPropertyChanged("ThrustDamageValueText");
            }
        }
        
        [DataSourceProperty] 
        public string ThrustDamageValueText => ThrustDamageValue.ToString(CultureInfo.InvariantCulture);
        
        private float _swingDamageValue;
        
        [DataSourceProperty]
        public float SwingDamageValue
        {
            get => _swingDamageValue;
            set
            {
                if (!(Math.Abs(_swingDamageValue - value) > Tolerance)) return;
                _swingDamageValue = value;
                OnPropertyChanged();
                OnPropertyChanged("SwingDamageValueText");
            }
        }
        
        [DataSourceProperty] 
        public string SwingDamageValueText => SwingDamageValue.ToString(CultureInfo.InvariantCulture);
        
        private float _accuracyValue;
        
        [DataSourceProperty]
        public float AccuracyValue
        {
            get => _accuracyValue;
            set
            {
                if (!(Math.Abs(_accuracyValue - value) > Tolerance)) return;
                _accuracyValue = value;
                OnPropertyChanged();
                OnPropertyChanged("AccuracyValueText");
            }
        }
        
        [DataSourceProperty] 
        public string AccuracyValueText => AccuracyValue.ToString(CultureInfo.InvariantCulture);
        
        private float _handlingValue;
        
        [DataSourceProperty]
        public float HandlingValue
        {
            get => _handlingValue;
            set
            {
                if (!(Math.Abs(_handlingValue - value) > Tolerance)) return;
                _handlingValue = value;
                OnPropertyChanged();
                OnPropertyChanged("HandlingValueText");
            }
        }
        [DataSourceProperty] 
        public string HandlingValueText => HandlingValue.ToString(CultureInfo.InvariantCulture);
        
        private float _weaponBodyArmorValue;
        
        [DataSourceProperty]
        public float WeaponBodyArmorValue
        {
            get => _weaponBodyArmorValue;
            set
            {
                if (!(Math.Abs(_weaponBodyArmorValue - value) > Tolerance)) return;
                _weaponBodyArmorValue = value;
                OnPropertyChanged();
                OnPropertyChanged("WeaponBodyArmorValueText");
            }
        }
        [DataSourceProperty] 
        public string WeaponBodyArmorValueText => WeaponBodyArmorValue.ToString(CultureInfo.InvariantCulture);

        #endregion

        #region CheckBox properties

        private bool _isMaxDataValueIsDefault;
        [DataSourceProperty]
        public bool IsMaxDataValueIsDefault
        {
            get => _isMaxDataValueIsDefault;
            set
            {
                if (_isMaxDataValueIsDefault == value) return;
                _isMaxDataValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isThrustSpeedValueIsDefault;
        [DataSourceProperty]
        public bool IsThrustSpeedValueIsDefault
        {
            get => _isThrustSpeedValueIsDefault;
            set
            {
                if (_isThrustSpeedValueIsDefault == value) return;
                _isThrustSpeedValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isSwingSpeedValueIsDefault;
        [DataSourceProperty]
        public bool IsSwingSpeedValueIsDefault
        {
            get => _isSwingSpeedValueIsDefault;
            set
            {
                if (_isSwingSpeedValueIsDefault == value) return;
                _isSwingSpeedValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isMissileSpeedValueIsDefault;
        [DataSourceProperty]
        public bool IsMissileSpeedValueIsDefault
        {
            get => _isMissileSpeedValueIsDefault;
            set
            {
                if (_isMissileSpeedValueIsDefault == value) return;
                _isMissileSpeedValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isWeaponLengthValueIsDefault;
        [DataSourceProperty]
        public bool IsWeaponLengthValueIsDefault
        {
            get => _isWeaponLengthValueIsDefault;
            set
            {
                if (_isWeaponLengthValueIsDefault == value) return;
                _isWeaponLengthValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isThrustDamageValueIsDefault;
        [DataSourceProperty]
        public bool IsThrustDamageValueIsDefault
        {
            get => _isThrustDamageValueIsDefault;
            set
            {
                if (_isThrustDamageValueIsDefault == value) return;
                _isThrustDamageValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isSwingDamageValueIsDefault;
        [DataSourceProperty]
        public bool IsSwingDamageValueIsDefault
        {
            get => _isSwingDamageValueIsDefault;
            set
            {
                if (_isSwingDamageValueIsDefault == value) return;
                _isSwingDamageValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isAccuracyValueIsDefault;
        [DataSourceProperty]
        public bool IsAccuracyValueIsDefault
        {
            get => _isAccuracyValueIsDefault;
            set
            {
                if (_isAccuracyValueIsDefault == value) return;
                _isAccuracyValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isHandlingValueIsDefault;
        [DataSourceProperty]
        public bool IsHandlingValueIsDefault
        {
            get => _isHandlingValueIsDefault;
            set
            {
                if (_isHandlingValueIsDefault == value) return;
                _isHandlingValueIsDefault = value;
                OnPropertyChanged();
            }
        }
        
        private bool _isWeaponBodyArmorValueIsDefault;
        [DataSourceProperty]
        public bool IsWeaponBodyArmorValueIsDefault
        {
            get => _isWeaponBodyArmorValueIsDefault;
            set
            {
                if (_isWeaponBodyArmorValueIsDefault == value) return;
                _isWeaponBodyArmorValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Rows visibility properties

        private bool _hiddenMaxData;
        [DataSourceProperty]
        public bool IsHiddenMaxData
        {
            get => _hiddenMaxData;
            set
            {
                if (_hiddenMaxData == value) return;
                _hiddenMaxData = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenThrustSpeed;
        [DataSourceProperty]
        public bool IsHiddenThrustSpeed
        {
            get => _hiddenThrustSpeed;
            set
            {
                if (_hiddenThrustSpeed == value) return;
                _hiddenThrustSpeed = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenSwingSpeed;
        [DataSourceProperty]
        public bool IsHiddenSwingSpeed
        {
            get => _hiddenSwingSpeed;
            set
            {
                if (_hiddenSwingSpeed == value) return;
                _hiddenSwingSpeed = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenMissileSpeed;
        [DataSourceProperty]
        public bool IsHiddenMissileSpeed
        {
            get => _hiddenMissileSpeed;
            set
            {
                if (_hiddenMissileSpeed == value) return;
                _hiddenMissileSpeed = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenWeaponLength;
        [DataSourceProperty]
        public bool IsHiddenWeaponLength
        {
            get => _hiddenWeaponLength;
            set
            {
                if (_hiddenWeaponLength == value) return;
                _hiddenWeaponLength = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenThrustDamage;
        [DataSourceProperty]
        public bool IsHiddenThrustDamage
        {
            get => _hiddenThrustDamage;
            set
            {
                if (_hiddenThrustDamage == value) return;
                _hiddenThrustDamage = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenSwingDamage;
        [DataSourceProperty]
        public bool IsHiddenSwingDamage
        {
            get => _hiddenSwingDamage;
            set
            {
                if (_hiddenSwingDamage == value) return;
                _hiddenSwingDamage = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenAccuracy;
        [DataSourceProperty]
        public bool IsHiddenAccuracy
        {
            get => _hiddenAccuracy;
            set
            {
                if (_hiddenAccuracy == value) return;
                _hiddenAccuracy = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenHandling;
        [DataSourceProperty]
        public bool IsHiddenHandling
        {
            get => _hiddenHandling;
            set
            {
                if (_hiddenHandling == value) return;
                _hiddenHandling = value;
                OnPropertyChanged();
            }
        }
        
        private bool _hiddenWeaponBodyArmor;
        [DataSourceProperty]
        public bool IsHiddenWeaponBodyArmor
        {
            get => _hiddenWeaponBodyArmor;
            set
            {
                if (_hiddenWeaponBodyArmor == value) return;
                _hiddenWeaponBodyArmor = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Param name

        [DataSourceProperty]
        public string MaxDataText { get; } = "Max Data weapon";
        
        [DataSourceProperty]
        public string ThrustSpeedText { get; } = "Thrust speed";
        
        [DataSourceProperty]
        public string SwingSpeedText { get; } = "Swing speed";
        
        [DataSourceProperty]
        public string MissileSpeedText { get; } = "Missile speed";
        
        [DataSourceProperty]
        public string WeaponLengthText { get; } = "Length";
        
        [DataSourceProperty]
        public string ThrustDamageText { get; } = "Thrust damage";
        
        [DataSourceProperty]
        public string SwingDamageText { get; } = "Swing damage";
        
        [DataSourceProperty]
        public string AccuracyText { get; } = "Accuracy";
        
        [DataSourceProperty]
        public string HandlingText { get; } = "Handling";
        
        [DataSourceProperty]
        public string WeaponBodyArmorText { get; } = "Armor";
        
        #endregion

        #region Methods
        
        public void ExecuteMaxDataValueDefault()
        {
            IsMaxDataValueIsDefault = true;
        }
        
        public void ExecuteThrustSpeedValueDefault()
        {
            IsThrustSpeedValueIsDefault = true;
        }
        
        public void ExecuteSwingSpeedValueDefault()
        {
            IsSwingSpeedValueIsDefault = true;
        }
        
        public void ExecuteMissileSpeedValueDefault()
        {
            IsMissileSpeedValueIsDefault = true;
        }
        
        public void ExecuteWeaponLengthValueDefault()
        {
            IsWeaponLengthValueIsDefault = true;
        }
        
        public void ExecuteThrustDamageValueDefault()
        {
            IsThrustDamageValueIsDefault = true;
        }
        
        public void ExecuteSwingDamageValueDefault()
        {
            IsSwingDamageValueIsDefault = true;
        }
        
        public void ExecuteAccuracyValueDefault()
        {
            IsAccuracyValueIsDefault = true;
        }
        
        public void ExecuteHandlingValueDefault()
        {
            IsHandlingValueIsDefault = true;
        }
        
        public void ExecuteWeaponBodyArmorValueDefault()
        {
            IsWeaponBodyArmorValueIsDefault = true;
        }
        
        #endregion
    }
}