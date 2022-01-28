using EquipBestItem.Models;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    public class FiltersVM : ViewModel
    {
        private FiltersModel _model;

        public FiltersVM()
        {
            _model = new FiltersModel(this);
        }

        public sealed override void RefreshValues()
        {
            base.RefreshValues();
            _model.RefreshValues();
        }

        public void ExecuteShowHideWeapon1Filter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.Weapon0);
        }

        public void ExecuteShowHideWeapon2Filter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.Weapon1);
        }

        public void ExecuteShowHideWeapon3Filter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.Weapon2);
        }

        public void ExecuteShowHideWeapon4Filter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.Weapon3);
        }

        public void ExecuteShowHideHelmFilter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.Head);
        }

        public void ExecuteShowHideCloakFilter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.Cape);
        }

        public void ExecuteShowHideArmorFilter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.Body);
        }

        public void ExecuteShowHideGloveFilter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.Gloves);
        }

        public void ExecuteShowHideBootFilter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.Leg);
        }

        public void ExecuteShowHideMountFilter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.Horse);
        }

        public void ExecuteShowHideHarnessFilter()
        {
            _model.ShowHideFilterSettingsLayer(EquipmentIndex.HorseHarness);
        }

        public override void OnFinalize()
        {
            _model.OnFinalize();
            _model = null;
            base.OnFinalize();
        }

        #region DataSourcePropertys

        private bool _isHelmFilterSelected;

        [DataSourceProperty]
        public bool IsHelmFilterSelected
        {
            get => _isHelmFilterSelected;
            set
            {
                if (_isHelmFilterSelected == value) return;
                _isHelmFilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isHelmFilterLocked;

        [DataSourceProperty]
        public bool IsHelmFilterLocked
        {
            get => _isHelmFilterLocked;
            set
            {
                if (_isHelmFilterLocked == value) return;
                _isHelmFilterLocked = value;
                OnPropertyChanged();
            }
        }

        private bool _isCloakFilterSelected;

        [DataSourceProperty]
        public bool IsCloakFilterSelected
        {
            get => _isCloakFilterSelected;
            set
            {
                if (_isCloakFilterSelected == value) return;
                _isCloakFilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isCloakFilterLocked;

        [DataSourceProperty]
        public bool IsCloakFilterLocked
        {
            get => _isCloakFilterLocked;
            set
            {
                if (_isCloakFilterLocked == value) return;
                _isCloakFilterLocked = value;
                OnPropertyChanged();
            }
        }


        private bool _isArmorFilterSelected;

        [DataSourceProperty]
        public bool IsArmorFilterSelected
        {
            get => _isArmorFilterSelected;
            set
            {
                if (_isArmorFilterSelected == value) return;
                _isArmorFilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isArmorFilterLocked;

        [DataSourceProperty]
        public bool IsArmorFilterLocked
        {
            get => _isArmorFilterLocked;
            set
            {
                if (_isArmorFilterLocked == value) return;
                _isArmorFilterLocked = value;
                OnPropertyChanged();
            }
        }


        private bool _isGloveFilterSelected;

        [DataSourceProperty]
        public bool IsGloveFilterSelected
        {
            get => _isGloveFilterSelected;
            set
            {
                if (_isGloveFilterSelected == value) return;
                _isGloveFilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isGloveFilterLocked;

        [DataSourceProperty]
        public bool IsGloveFilterLocked
        {
            get => _isGloveFilterLocked;
            set
            {
                if (_isGloveFilterLocked == value) return;
                _isGloveFilterLocked = value;
                OnPropertyChanged();
            }
        }

        private bool _isBootFilterSelected;

        [DataSourceProperty]
        public bool IsBootFilterSelected
        {
            get => _isBootFilterSelected;
            set
            {
                if (_isBootFilterSelected == value) return;
                _isBootFilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isBootFilterLocked;

        [DataSourceProperty]
        public bool IsBootFilterLocked
        {
            get => _isBootFilterLocked;
            set
            {
                if (_isBootFilterLocked == value) return;
                _isBootFilterLocked = value;
                OnPropertyChanged();
            }
        }

        private bool _isMountFilterSelected;

        [DataSourceProperty]
        public bool IsMountFilterSelected
        {
            get => _isMountFilterSelected;
            set
            {
                if (_isMountFilterSelected == value) return;
                _isMountFilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isMountFilterLocked;

        [DataSourceProperty]
        public bool IsMountFilterLocked
        {
            get => _isMountFilterLocked;
            set
            {
                if (_isMountFilterLocked == value) return;
                _isMountFilterLocked = value;
                OnPropertyChanged();
            }
        }

        private bool _isHarnessFilterSelected;

        [DataSourceProperty]
        public bool IsHarnessFilterSelected
        {
            get => _isHarnessFilterSelected;
            set
            {
                if (_isHarnessFilterSelected == value) return;
                _isHarnessFilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isHarnessFilterLocked;

        [DataSourceProperty]
        public bool IsHarnessFilterLocked
        {
            get => _isHarnessFilterLocked;
            set
            {
                if (_isHarnessFilterLocked == value) return;
                _isHarnessFilterLocked = value;
                OnPropertyChanged();
            }
        }

        private bool _isWeapon1FilterSelected;

        [DataSourceProperty]
        public bool IsWeapon1FilterSelected
        {
            get => _isWeapon1FilterSelected;
            set
            {
                if (_isWeapon1FilterSelected == value) return;
                _isWeapon1FilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isWeapon1FilterLocked;

        [DataSourceProperty]
        public bool IsWeapon1FilterLocked
        {
            get => _isWeapon1FilterLocked;
            set
            {
                if (_isWeapon1FilterLocked == value) return;
                _isWeapon1FilterLocked = value;
                OnPropertyChanged();
            }
        }

        private bool _isWeapon2FilterSelected;

        [DataSourceProperty]
        public bool IsWeapon2FilterSelected
        {
            get => _isWeapon2FilterSelected;
            set
            {
                if (_isWeapon2FilterSelected == value) return;
                _isWeapon2FilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isWeapon2FilterLocked;

        [DataSourceProperty]
        public bool IsWeapon2FilterLocked
        {
            get => _isWeapon2FilterLocked;
            set
            {
                if (_isWeapon2FilterLocked == value) return;
                _isWeapon2FilterLocked = value;
                OnPropertyChanged();
            }
        }

        private bool _isWeapon3FilterSelected;

        [DataSourceProperty]
        public bool IsWeapon3FilterSelected
        {
            get => _isWeapon3FilterSelected;
            set
            {
                if (_isWeapon3FilterSelected == value) return;
                _isWeapon3FilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isWeapon3FilterLocked;

        [DataSourceProperty]
        public bool IsWeapon3FilterLocked
        {
            get => _isWeapon3FilterLocked;
            set
            {
                if (_isWeapon3FilterLocked == value) return;
                _isWeapon3FilterLocked = value;
                OnPropertyChanged();
            }
        }

        private bool _isWeapon4FilterSelected;

        [DataSourceProperty]
        public bool IsWeapon4FilterSelected
        {
            get => _isWeapon4FilterSelected;
            set
            {
                if (_isWeapon4FilterSelected == value) return;
                _isWeapon4FilterSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isWeapon4FilterLocked;

        [DataSourceProperty]
        public bool IsWeapon4FilterLocked
        {
            get => _isWeapon4FilterLocked;
            set
            {
                if (_isWeapon4FilterLocked == value) return;
                _isWeapon4FilterLocked = value;
                OnPropertyChanged();
            }
        }

        private bool _isHidden;

        [DataSourceProperty]
        public bool IsHidden
        {
            get => _isHidden;
            set
            {
                if (_isHidden == value) return;
                _isHidden = value;
                OnPropertyChanged();
            }
        }

        private string _title;

        [DataSourceProperty]
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        #endregion DataSourceProperties
    }
}