using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public class FilterArmorVM : ViewModel
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

        private float _headArmorValue;
        [DataSourceProperty]
        public float HeadArmorValue
        {
            get => _headArmorValue;
            set
            {
                if (!(Math.Abs(_headArmorValue - value) > Tolerance)) return;
                _headArmorValue = value;
                OnPropertyChanged();
                OnPropertyChanged("HeadArmorValueText");
            }
        }
        [DataSourceProperty] public string HeadArmorValueText => _headArmorValue.ToString(CultureInfo.InvariantCulture);
        
        private float _bodyArmorValue;
        [DataSourceProperty]
        public float BodyArmorValue
        {
            get => _bodyArmorValue;
            set
            {
                if (!(Math.Abs(_bodyArmorValue - value) > Tolerance)) return;
                _bodyArmorValue = value;
                OnPropertyChanged();
                OnPropertyChanged("BodyArmorValueText");
            }
        }
        [DataSourceProperty] public string BodyArmorValueText => _bodyArmorValue.ToString(CultureInfo.InvariantCulture);
        

        private float _legArmorValue;
        [DataSourceProperty]
        public float LegArmorValue
        {
            get => _legArmorValue;
            set
            {
                if (!(Math.Abs(_legArmorValue - value) > Tolerance)) return;
                _legArmorValue = value;
                OnPropertyChanged();
                OnPropertyChanged("LegArmorValueText");
            }
        }
        [DataSourceProperty] public string LegArmorValueText => _legArmorValue.ToString(CultureInfo.InvariantCulture);

        private float _armArmorValue;
        [DataSourceProperty]
        public float ArmArmorValue
        {
            get => _armArmorValue;
            set
            {
                if (!(Math.Abs(_armArmorValue - value) > Tolerance)) return;
                _armArmorValue = value;
                OnPropertyChanged();
                OnPropertyChanged("ArmArmorValueText");
            }
        }
        [DataSourceProperty] public string ArmArmorValueText => _armArmorValue.ToString(CultureInfo.InvariantCulture);
        
        
        private float _armorWeightValue;
        [DataSourceProperty]
        public float ArmorWeightValue
        {
            get => _armorWeightValue;
            set
            {
                if (!(Math.Abs(_armorWeightValue - value) > Tolerance)) return;
                _armorWeightValue = value;
                OnPropertyChanged();
                OnPropertyChanged(ArmorWeightValueText);
            }
        }
        [DataSourceProperty] public string ArmorWeightValueText => _armorWeightValue.ToString(CultureInfo.InvariantCulture);


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
        
        private EquipmentIndex _currentSlot;
        private SPInventoryVM _inventory;

        public FilterArmorVM(SPInventoryVM inventory, string header, EquipmentIndex currentSlot)
        {
            _currentSlot = currentSlot;
            _inventory = inventory;
            SetHeaderText(header);
        }

        private void SetHeaderText(string header)
        {
            HeaderText = header;
        }
        
        public sealed override void RefreshValues()
        {
            base.RefreshValues();
        }

        public override void OnFinalize()
        {
            _inventory = null;
            base.OnFinalize();
        }
    }
}