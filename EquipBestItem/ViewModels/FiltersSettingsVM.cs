using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
                OnPropertyChanged();
                OnPropertyChanged("WeightValueText");
            }
        }
        
        [DataSourceProperty] 
        public string WeightValueText => WeightValue.ToString(CultureInfo.InvariantCulture);

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
        
        public FiltersSettingsVM(SPInventoryVM inventory, EquipmentIndex currentSlot)
        {
            _currentSlot = currentSlot;
            _inventory = inventory;
        }
        
        public sealed override void RefreshValues()
        {
            base.RefreshValues();
        }
        
        public void ExecuteWeightValueDefault()
        {
            IsWeightValueIsDefault = true;
        }

        public override void OnFinalize()
        {
            _inventory = null;
            base.OnFinalize();
        }
    }
}