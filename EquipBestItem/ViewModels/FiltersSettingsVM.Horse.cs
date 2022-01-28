using System;
using System.Globalization;
using EquipBestItem.Settings;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EquipBestItem.ViewModels
{
    partial class FiltersSettingsVM
    {
        [DataSourceProperty]
        public string HitPointsText { get; } = new TextObject("{=aCkzVUCR}Hit Points: ").ToString();

        [DataSourceProperty]
        public string ChargeDamageText { get; } =
            new TextObject("{=c7638a0869219ae845de0f660fd57a9d}Charge Damage: ").ToString();

        [DataSourceProperty]
        public string ManeuverText { get; } =
            new TextObject("{=3025020b83b218707499f0de3135ed0a}Maneuver: ").ToString();

        [DataSourceProperty]
        public string SpeedText { get; } = new TextObject("{=74dc1908cb0b990e80fb977b5a0ef10d}Speed: ").ToString();

        public void ExecuteHitPointsValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.HitPoints), HitPointsValue);
            _model.DefaultFilter[_currentSlot].HitPoints = HitPointsValue;
            RefreshValues();
        }

        public void ExecuteChargeDamageValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.ChargeDamage), ChargeDamageValue);
            _model.DefaultFilter[_currentSlot].ChargeDamage = ChargeDamageValue;
            RefreshValues();
        }

        public void ExecuteManeuverValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.Maneuver), ManeuverValue);
            _model.DefaultFilter[_currentSlot].Maneuver = ManeuverValue;
            RefreshValues();
        }

        public void ExecuteSpeedValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.Speed), SpeedValue);
            _model.DefaultFilter[_currentSlot].Speed = SpeedValue;
            RefreshValues();
        }

        private void UpdateHorseProperties()
        {
            OnPropertyChanged(nameof(HitPointsValueText));
            OnPropertyChanged(nameof(ChargeDamageValueText));
            OnPropertyChanged(nameof(ManeuverValueText));
            OnPropertyChanged(nameof(SpeedValueText));
        }

        private void UpdateHorseCheckBoxStates()
        {
            IsHitPointsValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].HitPoints - HitPointsValue) < Tolerance;
            IsChargeDamageValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].ChargeDamage - ChargeDamageValue) < Tolerance;
            IsManeuverValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].Maneuver - ManeuverValue) < Tolerance;
            IsSpeedValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].Speed - SpeedValue) < Tolerance;
        }

        private string GetHorseValuePercentText(float propertyValue)
        {
            var sum = Math.Abs(HitPointsValue) +
                      Math.Abs(ChargeDamageValue) +
                      Math.Abs(ManeuverValue) +
                      Math.Abs(SpeedValue);
            if (sum == 0) return "0%";

            return Math.Round(propertyValue / sum * 100).ToString(CultureInfo.InvariantCulture) + "%";
        }

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
                OnPropertyChangedWithValue(value);
                UpdateHorseProperties();
                OnPropertyChanged(nameof(ChargeDamageValueText));
            }
        }

        [DataSourceProperty] public string ChargeDamageValueText => GetHorseValuePercentText(ChargeDamageValue);

        private float _hitPointsValue;

        [DataSourceProperty]
        public float HitPointsValue
        {
            get => _hitPointsValue;
            set
            {
                if (!(Math.Abs(_hitPointsValue - value) > Tolerance)) return;
                _hitPointsValue = value;
                OnPropertyChangedWithValue(value);
                UpdateHorseProperties();
                OnPropertyChanged(nameof(HitPointsValueText));
            }
        }

        [DataSourceProperty] public string HitPointsValueText => GetHorseValuePercentText(HitPointsValue);

        private float _maneuverValueValue;

        [DataSourceProperty]
        public float ManeuverValue
        {
            get => _maneuverValueValue;
            set
            {
                if (!(Math.Abs(_maneuverValueValue - value) > Tolerance)) return;
                _maneuverValueValue = value;
                OnPropertyChangedWithValue(value);
                UpdateHorseProperties();
                OnPropertyChanged(nameof(ManeuverValueText));
            }
        }

        [DataSourceProperty] public string ManeuverValueText => GetHorseValuePercentText(ManeuverValue);

        private float _speedValue;

        [DataSourceProperty]
        public float SpeedValue
        {
            get => _speedValue;
            set
            {
                if (!(Math.Abs(_speedValue - value) > Tolerance)) return;
                _speedValue = value;
                OnPropertyChangedWithValue(value);
                UpdateHorseProperties();
                OnPropertyChanged(nameof(SpeedValueText));
            }
        }

        [DataSourceProperty] public string SpeedValueText => GetHorseValuePercentText(SpeedValue);

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
                OnPropertyChangedWithValue(value);
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
    }
}