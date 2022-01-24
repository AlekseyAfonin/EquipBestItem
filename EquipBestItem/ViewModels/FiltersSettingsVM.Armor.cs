using System;
using System.Globalization;
using TaleWorlds.Library;

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
                OnPropertyChanged("HeadArmorValueText");
            }
        }
        
        [DataSourceProperty] 
        public string HeadArmorValueText => HeadArmorValue.ToString(CultureInfo.InvariantCulture);
        
        
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
                OnPropertyChanged("BodyArmorValueText");
            }
        }
        
        [DataSourceProperty] 
        public string BodyArmorValueText => BodyArmorValue.ToString(CultureInfo.InvariantCulture);
        
        private float _cloakArmorValue;
        
        [DataSourceProperty]
        public float CloakArmorValue
        {
            get => _cloakArmorValue;
            set
            {
                if (!(Math.Abs(_cloakArmorValue - value) > Tolerance)) return;
                _cloakArmorValue = value;
                OnPropertyChangedWithValue(value);
                OnPropertyChanged("CloakArmorValueText");
            }
        }
        
        [DataSourceProperty] 
        public string CloakArmorValueText => CloakArmorValue.ToString(CultureInfo.InvariantCulture);

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
                OnPropertyChanged("LegArmorValueText");
            }
        }
        
        [DataSourceProperty] 
        public string LegArmorValueText => LegArmorValue.ToString(CultureInfo.InvariantCulture);

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
                OnPropertyChanged("ArmArmorValueText");
            }
        }
        
        [DataSourceProperty] 
        public string ArmArmorValueText => ArmArmorValue.ToString(CultureInfo.InvariantCulture);
        
        #endregion

        #region CheckBox properties

        private bool _headArmorValueIsDefault;
        [DataSourceProperty]
        public bool HeadArmorValueIsDefault
        {
            get => _headArmorValueIsDefault;
            set
            {
                if (_headArmorValueIsDefault == value) return;
                _headArmorValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _bodyArmorValueIsDefault;
        [DataSourceProperty]
        public bool BodyArmorValueIsDefault
        {
            get => _bodyArmorValueIsDefault;
            set
            {
                if (_bodyArmorValueIsDefault == value) return;
                _bodyArmorValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _legArmorValueIsDefault;
        [DataSourceProperty]
        public bool LegArmorValueIsDefault
        {
            get => _legArmorValueIsDefault;
            set
            {
                if (_legArmorValueIsDefault == value) return;
                _legArmorValueIsDefault = value;
                OnPropertyChanged();
            }
        }

        private bool _armArmorValueIsDefault;
        [DataSourceProperty]
        public bool ArmArmorValueIsDefault
        {
            get => _armArmorValueIsDefault;
            set
            {
                if (_armArmorValueIsDefault == value) return;
                _armArmorValueIsDefault = value;
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
        public string HeadArmorText { get; } = "Head Armor";
        
        [DataSourceProperty]
        public string BodyArmorText { get; } = "Body Armor";
        
        [DataSourceProperty]
        public string ArmArmorText { get; } = "Arm Armor";
        
        [DataSourceProperty]
        public string LegArmorText { get; } = "Leg Armor";
        
        public void ExecuteHeadArmorValueDefault()
        {
            HeadArmorValueIsDefault = true;
        }
        
        public void ExecuteBodyArmorValueDefault()
        {
            BodyArmorValueIsDefault = true;
        }
        
        public void ExecuteLegArmorValueDefault()
        {
            LegArmorValueIsDefault = true;
        }
        
        public void ExecuteArmArmorValueDefault()
        {
            ArmArmorValueIsDefault = true;
        }
    }
}