using System;
using System.Globalization;
using EquipBestItem.Settings;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EquipBestItem.ViewModels
{
    partial class FiltersSettingsVM
    {
        private void UpdateWeaponProperties()
        {
            OnPropertyChanged(nameof(MaxDataValueText));
            OnPropertyChanged(nameof(ThrustSpeedValueText));
            OnPropertyChanged(nameof(SwingSpeedValueText));
            OnPropertyChanged(nameof(MissileSpeedValueText));
            OnPropertyChanged(nameof(MissileDamageValueText));
            OnPropertyChanged(nameof(WeaponLengthValueText));
            OnPropertyChanged(nameof(ThrustDamageValueText));
            OnPropertyChanged(nameof(SwingDamageValueText));
            OnPropertyChanged(nameof(AccuracyValueText));
            OnPropertyChanged(nameof(HandlingValueText));
            OnPropertyChanged(nameof(WeaponBodyArmorValueText));
            OnPropertyChanged(nameof(WeightValueText));
        }

        private void UpdateWeaponCheckBoxStates()
        {
            IsMaxDataValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].MaxDataValue - MaxDataValue) < Tolerance;
            IsThrustSpeedValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].ThrustSpeed - ThrustSpeedValue) < Tolerance;
            IsSwingSpeedValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].SwingSpeed - SwingSpeedValue) < Tolerance;
            IsMissileSpeedValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].MissileSpeed - MissileSpeedValue) < Tolerance;
            IsMissileDamageValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].MissileDamage - MissileDamageValue) < Tolerance;
            IsWeaponLengthValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].WeaponLength - WeaponLengthValue) < Tolerance;
            IsThrustDamageValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].ThrustDamage - ThrustDamageValue) < Tolerance;
            IsSwingDamageValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].SwingDamage - SwingDamageValue) < Tolerance;
            IsAccuracyValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].Accuracy - AccuracyValue) < Tolerance;
            IsHandlingValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].Handling - HandlingValue) < Tolerance;
            IsWeaponBodyArmorValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].WeaponBodyArmor - WeaponBodyArmorValue) < Tolerance;
            IsWeightValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].Weight - WeightValue) < Tolerance;
        }

        private string GetWeaponValuePercentText(float propertyValue)
        {
            var sum = Math.Abs(AccuracyValue) +
                      Math.Abs(WeaponBodyArmorValue) +
                      Math.Abs(HandlingValue) +
                      Math.Abs(MaxDataValue) +
                      Math.Abs(MissileSpeedValue) +
                      Math.Abs(MissileDamageValue) +
                      Math.Abs(SwingDamageValue) +
                      Math.Abs(SwingSpeedValue) +
                      Math.Abs(ThrustDamageValue) +
                      Math.Abs(ThrustSpeedValue) +
                      Math.Abs(WeaponLengthValue) +
                      Math.Abs(WeightValue);
            if (sum == 0) return "0%";

            return Math.Round(propertyValue / sum * 100).ToString(CultureInfo.InvariantCulture) + "%";
        }

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
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string MaxDataValueText => GetWeaponValuePercentText(MaxDataValue);

        private float _thrustSpeedValue;

        [DataSourceProperty]
        public float ThrustSpeedValue
        {
            get => _thrustSpeedValue;
            set
            {
                if (!(Math.Abs(_thrustSpeedValue - value) > Tolerance)) return;
                _thrustSpeedValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string ThrustSpeedValueText => GetWeaponValuePercentText(ThrustSpeedValue);

        private float _swingSpeedValue;

        [DataSourceProperty]
        public float SwingSpeedValue
        {
            get => _swingSpeedValue;
            set
            {
                if (!(Math.Abs(_swingSpeedValue - value) > Tolerance)) return;
                _swingSpeedValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string SwingSpeedValueText => GetWeaponValuePercentText(SwingSpeedValue);

        private float _missileSpeedValue;

        [DataSourceProperty]
        public float MissileSpeedValue
        {
            get => _missileSpeedValue;
            set
            {
                if (!(Math.Abs(_missileSpeedValue - value) > Tolerance)) return;
                _missileSpeedValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string MissileSpeedValueText => GetWeaponValuePercentText(MissileSpeedValue);

        private float _missileDamageValue;

        [DataSourceProperty]
        public float MissileDamageValue
        {
            get => _missileDamageValue;
            set
            {
                if (!(Math.Abs(_missileDamageValue - value) > Tolerance)) return;
                _missileDamageValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string MissileDamageValueText => GetWeaponValuePercentText(MissileDamageValue);

        private float _weaponLengthValue;

        [DataSourceProperty]
        public float WeaponLengthValue
        {
            get => _weaponLengthValue;
            set
            {
                if (!(Math.Abs(_weaponLengthValue - value) > Tolerance)) return;
                _weaponLengthValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string WeaponLengthValueText => GetWeaponValuePercentText(WeaponLengthValue);

        private float _thrustDamageValue;

        [DataSourceProperty]
        public float ThrustDamageValue
        {
            get => _thrustDamageValue;
            set
            {
                if (!(Math.Abs(_thrustDamageValue - value) > Tolerance)) return;
                _thrustDamageValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string ThrustDamageValueText => GetWeaponValuePercentText(ThrustDamageValue);

        private float _swingDamageValue;

        [DataSourceProperty]
        public float SwingDamageValue
        {
            get => _swingDamageValue;
            set
            {
                if (!(Math.Abs(_swingDamageValue - value) > Tolerance)) return;
                _swingDamageValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string SwingDamageValueText => GetWeaponValuePercentText(SwingDamageValue);

        private float _accuracyValue;

        [DataSourceProperty]
        public float AccuracyValue
        {
            get => _accuracyValue;
            set
            {
                if (!(Math.Abs(_accuracyValue - value) > Tolerance)) return;
                _accuracyValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string AccuracyValueText => GetWeaponValuePercentText(AccuracyValue);

        private float _handlingValue;

        [DataSourceProperty]
        public float HandlingValue
        {
            get => _handlingValue;
            set
            {
                if (!(Math.Abs(_handlingValue - value) > Tolerance)) return;
                _handlingValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string HandlingValueText => GetWeaponValuePercentText(HandlingValue);

        private float _weaponBodyArmorValue;

        [DataSourceProperty]
        public float WeaponBodyArmorValue
        {
            get => _weaponBodyArmorValue;
            set
            {
                if (!(Math.Abs(_weaponBodyArmorValue - value) > Tolerance)) return;
                _weaponBodyArmorValue = value;
                OnPropertyChangedWithValue(value);
                UpdateWeaponProperties();
            }
        }

        [DataSourceProperty] public string WeaponBodyArmorValueText => GetWeaponValuePercentText(WeaponBodyArmorValue);

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

        private bool _isMissileDamageValueIsDefault;

        [DataSourceProperty]
        public bool IsMissileDamageValueIsDefault
        {
            get => _isMissileDamageValueIsDefault;
            set
            {
                if (_isMissileDamageValueIsDefault == value) return;
                _isMissileDamageValueIsDefault = value;
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

        private bool _hiddenMissileDamage;

        [DataSourceProperty]
        public bool IsHiddenMissileDamage
        {
            get => _hiddenMissileDamage;
            set
            {
                if (_hiddenMissileDamage == value) return;
                _hiddenMissileDamage = value;
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

        [DataSourceProperty] public string MaxDataText { get; } = new TextObject("{=aCkzVUCR}Hit Points: ").ToString();

        [DataSourceProperty]
        public string ThrustSpeedText { get; } = new TextObject("{=VPYazFVH}Thrust Speed: ").ToString();

        [DataSourceProperty]
        public string SwingSpeedText { get; } = new TextObject("{=nfQhamAF}Swing Speed: ").ToString();

        [DataSourceProperty]
        public string MissileSpeedText { get; } = new TextObject("{=YukbQgHJ}Missile Speed: ").ToString();

        [DataSourceProperty]
        public string MissileDamageText { get; } =
            new TextObject("{=c9c5dfed2ca6bcb7a73d905004c97b23}Damage: ").ToString();

        [DataSourceProperty] public string WeaponLengthText { get; } = new TextObject("{=XUtiwiYP}Length: ").ToString();

        [DataSourceProperty]
        public string ThrustDamageText { get; } = new TextObject("{=7sUhWG0E}Thrust Damage: ").ToString();

        [DataSourceProperty]
        public string SwingDamageText { get; } = new TextObject("{=fMmlUHyz}Swing Damage: ").ToString();

        [DataSourceProperty] public string AccuracyText { get; } = new TextObject("{=xEWwbGVK}Accuracy: ").ToString();

        [DataSourceProperty] public string HandlingText { get; } = new TextObject("{=YOSEIvyf}Handling: ").ToString();

        [DataSourceProperty]
        public string WeaponBodyArmorText { get; } = new TextObject("{=bLWyjOdS}Body Armor: ").ToString();

        #endregion

        #region Methods

        public void ExecuteMaxDataValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.MaxDataValue), MaxDataValue);
            _model.DefaultFilter[_currentSlot].MaxDataValue = MaxDataValue;
            RefreshValues();
        }

        public void ExecuteThrustSpeedValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.ThrustSpeed), ThrustSpeedValue);
            _model.DefaultFilter[_currentSlot].ThrustSpeed = ThrustSpeedValue;
            RefreshValues();
        }

        public void ExecuteSwingSpeedValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.SwingSpeed), SwingSpeedValue);
            _model.DefaultFilter[_currentSlot].SwingSpeed = SwingSpeedValue;
            RefreshValues();
        }

        public void ExecuteMissileSpeedValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.MissileSpeed), MissileSpeedValue);
            _model.DefaultFilter[_currentSlot].MissileSpeed = MissileSpeedValue;
            RefreshValues();
        }

        public void ExecuteMissileDamageValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.MissileDamage), MissileDamageValue);
            _model.DefaultFilter[_currentSlot].MissileDamage = MissileDamageValue;
            RefreshValues();
        }

        public void ExecuteWeaponLengthValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.WeaponLength), WeaponLengthValue);
            _model.DefaultFilter[_currentSlot].WeaponLength = WeaponLengthValue;
            RefreshValues();
        }

        public void ExecuteThrustDamageValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.ThrustDamage), ThrustDamageValue);
            _model.DefaultFilter[_currentSlot].ThrustDamage = ThrustDamageValue;
            RefreshValues();
        }

        public void ExecuteSwingDamageValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.SwingDamage), SwingDamageValue);
            _model.DefaultFilter[_currentSlot].SwingDamage = SwingDamageValue;
            RefreshValues();
        }

        public void ExecuteAccuracyValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.Accuracy), AccuracyValue);
            _model.DefaultFilter[_currentSlot].Accuracy = AccuracyValue;
            RefreshValues();
        }

        public void ExecuteHandlingValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.Handling), HandlingValue);
            _model.DefaultFilter[_currentSlot].Handling = HandlingValue;
            RefreshValues();
        }

        public void ExecuteWeaponBodyArmorValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.WeaponBodyArmor), WeaponBodyArmorValue);
            _model.DefaultFilter[_currentSlot].WeaponBodyArmor = WeaponBodyArmorValue;
            RefreshValues();
        }

        #endregion
    }
}