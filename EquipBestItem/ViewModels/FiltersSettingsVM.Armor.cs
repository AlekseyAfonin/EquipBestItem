using System;
using System.Globalization;
using EquipBestItem.Settings;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EquipBestItem.ViewModels
{
    partial class FiltersSettingsVM
    {
        #region Slider properties
        
        private float _headArmorValue;
        
        [DataSourceProperty]
        public float HeadArmorValue
        {
            get => _headArmorValue;
            set
            {
                if (!(Math.Abs(_headArmorValue - value) > Tolerance)) return;
                _headArmorValue = value;
                OnPropertyChangedWithValue(value);
                UpdateArmorProperties();
                OnPropertyChanged("HeadArmorValueText");
            }
        }
        
        [DataSourceProperty] 
        public string HeadArmorValueText => GetArmorValuePercentText(HeadArmorValue);
        
        
        private float _bodyArmorValue;
        
        [DataSourceProperty]
        public float BodyArmorValue
        {
            get => _bodyArmorValue;
            set
            {
                if (!(Math.Abs(_bodyArmorValue - value) > Tolerance)) return;
                _bodyArmorValue = value;
                OnPropertyChangedWithValue(value);
                UpdateArmorProperties();
                UpdateHorseHarnessProperties();
                OnPropertyChanged("BodyArmorValueText");
            }
        }
        
        [DataSourceProperty] 
        public string BodyArmorValueText
        {
            get
            {
                if (_currentSlot >= EquipmentIndex.ArmorItemBeginSlot && _currentSlot < EquipmentIndex.ArmorItemEndSlot)
                {
                    return GetArmorValuePercentText(BodyArmorValue);
                }
                
                if (_currentSlot == EquipmentIndex.HorseHarness)
                {
                    return GetHorseHarnessValuePercentText(BodyArmorValue);
                }
                
                return "err";
            }
        }

        private float _legArmorValue;
        
        [DataSourceProperty]
        public float LegArmorValue
        {
            get => _legArmorValue;
            set
            {
                if (!(Math.Abs(_legArmorValue - value) > Tolerance)) return;
                _legArmorValue = value;
                OnPropertyChangedWithValue(value);
                UpdateArmorProperties();
                OnPropertyChanged("LegArmorValueText");
            }
        }

        [DataSourceProperty] 
        public string LegArmorValueText => GetArmorValuePercentText(LegArmorValue);

        private float _armArmorValue;
        
        [DataSourceProperty]
        public float ArmArmorValue
        {
            get => _armArmorValue;
            set
            {
                if (!(Math.Abs(_armArmorValue - value) > Tolerance)) return;
                _armArmorValue = value;
                OnPropertyChangedWithValue(value);
                UpdateArmorProperties();
                OnPropertyChanged("ArmArmorValueText");
            }
        }
        
        [DataSourceProperty] 
        public string ArmArmorValueText => GetArmorValuePercentText(ArmArmorValue);
        
        #endregion

        #region CheckBox properties

        private bool _isHeadArmorValueIsDefault;
        [DataSourceProperty]
        public bool IsHeadArmorValueIsDefault
        {
            get => _isHeadArmorValueIsDefault;
            set
            {
                if (_isHeadArmorValueIsDefault == value) return;
                _isHeadArmorValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isBodyArmorValueIsDefault;
        [DataSourceProperty]
        public bool IsBodyArmorValueIsDefault
        {
            get => _isBodyArmorValueIsDefault;
            set
            {
                if (_isBodyArmorValueIsDefault == value) return;
                _isBodyArmorValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isLegArmorValueIsDefault;
        [DataSourceProperty]
        public bool IsLegArmorValueIsDefault
        {
            get => _isLegArmorValueIsDefault;
            set
            {
                if (_isLegArmorValueIsDefault == value) return;
                _isLegArmorValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _isArmArmorValueIsDefault;
        [DataSourceProperty]
        public bool IsArmArmorValueIsDefault
        {
            get => _isArmArmorValueIsDefault;
            set
            {
                if (_isArmArmorValueIsDefault == value) return;
                _isArmArmorValueIsDefault = value;
                OnPropertyChanged();
            }
        }
        
        #endregion

        #region Rows visibility properties 

        private bool _isHiddenHeadArmor;
        
        [DataSourceProperty]
        public bool IsHiddenHeadArmor
        {
            get => _isHiddenHeadArmor;
            set
            {
                if (_isHiddenHeadArmor == value) return;
                _isHiddenHeadArmor = value;
                OnPropertyChanged();
            }
        }
        
        private bool _isHiddenBodyArmor;
        
        [DataSourceProperty]
        public bool IsHiddenBodyArmor
        {
            get => _isHiddenBodyArmor;
            set
            {
                if (_isHiddenBodyArmor == value) return;
                _isHiddenBodyArmor = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenLegArmor;
        
        [DataSourceProperty]
        public bool IsHiddenLegArmor
        {
            get => _hiddenLegArmor;
            set
            {
                if (_hiddenLegArmor == value) return;
                _hiddenLegArmor = value;
                OnPropertyChanged();
            }
        }

        private bool _hiddenArmArmor;
        
        [DataSourceProperty]
        public bool IsHiddenArmArmor
        {
            get => _hiddenArmArmor;
            set
            {
                if (_hiddenArmArmor == value) return;
                _hiddenArmArmor = value;
                OnPropertyChanged();
            }
        }

        #endregion
        
        [DataSourceProperty]
        public string HeadArmorText { get; } = GameTexts.FindText("str_head_armor").ToString();
        
        
        private string _bodyArmorText = GameTexts.FindText("str_body_armor").ToString();
        [DataSourceProperty]
        public string BodyArmorText { 
            get => _bodyArmorText;
            set
            {
                if (_bodyArmorText == value) return;
                _bodyArmorText = value;
                OnPropertyChanged();
            }
        }
        
        [DataSourceProperty]
        public string ArmArmorText { get; } = new TextObject("{=cf61cce254c7dca65be9bebac7fb9bf5}Arm Armor: ").ToString();
        
        [DataSourceProperty]
        public string LegArmorText { get; } = GameTexts.FindText("str_leg_armor").ToString();
        
        public void ExecuteHeadArmorValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.HeadArmor), HeadArmorValue);
            _model.DefaultFilter[_currentSlot].HeadArmor = HeadArmorValue;
            RefreshValues();
        }
        
        public void ExecuteBodyArmorValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.ArmorBodyArmor), BodyArmorValue);
            _model.DefaultFilter[_currentSlot].ArmorBodyArmor = BodyArmorValue;
            RefreshValues();
        }
        
        public void ExecuteLegArmorValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.LegArmor), LegArmorValue);
            _model.DefaultFilter[_currentSlot].LegArmor = LegArmorValue;
            RefreshValues();
        }
        
        public void ExecuteArmArmorValueDefault()
        {
            _model.SetEveryCharacterNewDefaultValue(nameof(FilterElement.ArmArmor), ArmArmorValue);
            _model.DefaultFilter[_currentSlot].ArmArmor = ArmArmorValue;
            RefreshValues();
        }
        
        private void UpdateArmorProperties()
        {
            OnPropertyChanged("HeadArmorValueText");
            OnPropertyChanged("BodyArmorValueText");
            OnPropertyChanged("LegArmorValueText");
            OnPropertyChanged("ArmArmorValueText");
            OnPropertyChanged("WeightValueText");
        }
        
        private void UpdateArmorCheckBoxStates()
        {
            IsHeadArmorValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].HeadArmor - HeadArmorValue) < Tolerance;
            IsBodyArmorValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].ArmorBodyArmor - BodyArmorValue) < Tolerance;
            IsLegArmorValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].LegArmor - LegArmorValue) < Tolerance;
            IsArmArmorValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].ArmArmor - ArmArmorValue) < Tolerance;
            IsWeightValueIsDefault =
                Math.Abs(_model.DefaultFilter[_currentSlot].Weight - WeightValue) < Tolerance;
        }
        
        private string GetArmorValuePercentText(float propertyValue)
        {
            float sum = Math.Abs(HeadArmorValue) +
                        Math.Abs(BodyArmorValue) +
                        Math.Abs(LegArmorValue) +
                        Math.Abs(ArmArmorValue) +
                        Math.Abs(WeightValue);
            if (sum == 0) return "0%";
            
            return Math.Round(propertyValue / sum * 100).ToString(CultureInfo.InvariantCulture) + "%";
        }
    }
}