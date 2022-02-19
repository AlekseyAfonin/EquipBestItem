using EquipBestItem.Models;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    internal class MainVM : ViewModel
    {
        private MainModel _model;

        public MainVM()
        {
            _model = new MainModel();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            _model.RefreshValues();

            IsHelmButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.Head].IsEmpty ||
                                  !_model.BestRightEquipment[EquipmentIndex.Head].IsEmpty;
            IsCloakButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.Cape].IsEmpty ||
                                   !_model.BestRightEquipment[EquipmentIndex.Cape].IsEmpty;
            IsArmorButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.Body].IsEmpty ||
                                   !_model.BestRightEquipment[EquipmentIndex.Body].IsEmpty;
            IsGloveButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.Gloves].IsEmpty ||
                                   !_model.BestRightEquipment[EquipmentIndex.Gloves].IsEmpty;
            IsBootButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.Leg].IsEmpty ||
                                  !_model.BestRightEquipment[EquipmentIndex.Leg].IsEmpty;
            IsMountButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.Horse].IsEmpty ||
                                   !_model.BestRightEquipment[EquipmentIndex.Horse].IsEmpty;
            IsHarnessButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.HorseHarness].IsEmpty ||
                                     !_model.BestRightEquipment[EquipmentIndex.HorseHarness].IsEmpty;
            IsWeapon1ButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.Weapon0].IsEmpty ||
                                     !_model.BestRightEquipment[EquipmentIndex.Weapon0].IsEmpty;
            IsWeapon2ButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.Weapon1].IsEmpty ||
                                     !_model.BestRightEquipment[EquipmentIndex.Weapon1].IsEmpty;
            IsWeapon3ButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.Weapon2].IsEmpty ||
                                     !_model.BestRightEquipment[EquipmentIndex.Weapon2].IsEmpty;
            IsWeapon4ButtonEnabled = !_model.BestLeftEquipment[EquipmentIndex.Weapon3].IsEmpty ||
                                     !_model.BestRightEquipment[EquipmentIndex.Weapon3].IsEmpty;

            for (var equipmentIndex = EquipmentIndex.WeaponItemBeginSlot;
                 equipmentIndex < EquipmentIndex.NumEquipmentSetSlots;
                 equipmentIndex++)
                if (_model.BestLeftEquipment[equipmentIndex].IsEmpty &&
                    _model.BestRightEquipment[equipmentIndex].IsEmpty)
                {
                    IsEquipCurrentCharacterButtonEnabled = false;
                }
                else
                {
                    IsEquipCurrentCharacterButtonEnabled = true;
                    break;
                }
        }

        public void ExecuteHideLayers()
        {
            EquipBestItemManager.Instance.IsLayersHidden = !EquipBestItemManager.Instance.IsLayersHidden;
        }

        public void ExecuteEquipBestHelm()
        {
            _model.EquipBestItem(EquipmentIndex.Head);
        }

        public void ExecuteEquipBestCloak()
        {
            _model.EquipBestItem(EquipmentIndex.Cape);
        }

        public void ExecuteEquipBestArmor()
        {
            _model.EquipBestItem(EquipmentIndex.Body);
        }

        public void ExecuteEquipBestGlove()
        {
            _model.EquipBestItem(EquipmentIndex.Gloves);
        }

        public void ExecuteEquipBestBoot()
        {
            _model.EquipBestItem(EquipmentIndex.Leg);
        }

        public void ExecuteEquipBestMount()
        {
            _model.EquipBestItem(EquipmentIndex.Horse);
        }

        public void ExecuteEquipBestHarness()
        {
            _model.EquipBestItem(EquipmentIndex.HorseHarness);
        }

        public void ExecuteEquipBestWeapon1()
        {
            _model.EquipBestItem(EquipmentIndex.Weapon0);
        }

        public void ExecuteEquipBestWeapon2()
        {
            _model.EquipBestItem(EquipmentIndex.Weapon1);
        }

        public void ExecuteEquipBestWeapon3()
        {
            _model.EquipBestItem(EquipmentIndex.Weapon2);
        }

        public void ExecuteEquipBestWeapon4()
        {
            _model.EquipBestItem(EquipmentIndex.Weapon3);
        }

        public void ExecuteEquipEveryCharacter()
        {
            _model.EquipEveryCharacter();
        }

        public void ExecuteEquipCurrentCharacter()
        {
            _model.EquipCurrentCharacter();
        }

        public override void OnFinalize()
        {
            _model.OnFinalize();
            _model = null;
            base.OnFinalize();
        }

        #region DataSourcePropertys

        private bool _isHelmButtonEnabled;

        [DataSourceProperty]
        public bool IsHelmButtonEnabled
        {
            get => _isHelmButtonEnabled;
            set
            {
                if (_isHelmButtonEnabled != value)
                {
                    _isHelmButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isCloakButtonEnabled;

        [DataSourceProperty]
        public bool IsCloakButtonEnabled
        {
            get => _isCloakButtonEnabled;
            set
            {
                if (_isCloakButtonEnabled != value)
                {
                    _isCloakButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isArmorButtonEnabled;

        [DataSourceProperty]
        public bool IsArmorButtonEnabled
        {
            get => _isArmorButtonEnabled;
            set
            {
                if (_isArmorButtonEnabled != value)
                {
                    _isArmorButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isGloveButtonEnabled;

        [DataSourceProperty]
        public bool IsGloveButtonEnabled
        {
            get => _isGloveButtonEnabled;
            set
            {
                if (_isGloveButtonEnabled != value)
                {
                    _isGloveButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isBootButtonEnabled;

        [DataSourceProperty]
        public bool IsBootButtonEnabled
        {
            get => _isBootButtonEnabled;
            set
            {
                if (_isBootButtonEnabled != value)
                {
                    _isBootButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isMountButtonEnabled;

        [DataSourceProperty]
        public bool IsMountButtonEnabled
        {
            get => _isMountButtonEnabled;
            set
            {
                if (_isMountButtonEnabled != value)
                {
                    _isMountButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isHarnessButtonEnabled;

        [DataSourceProperty]
        public bool IsHarnessButtonEnabled
        {
            get => _isHarnessButtonEnabled;
            set
            {
                if (_isHarnessButtonEnabled != value)
                {
                    _isHarnessButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon1ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon1ButtonEnabled
        {
            get => _isWeapon1ButtonEnabled;
            set
            {
                if (_isWeapon1ButtonEnabled != value)
                {
                    _isWeapon1ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon2ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon2ButtonEnabled
        {
            get => _isWeapon2ButtonEnabled;
            set
            {
                if (_isWeapon2ButtonEnabled != value)
                {
                    _isWeapon2ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon3ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon3ButtonEnabled
        {
            get => _isWeapon3ButtonEnabled;
            set
            {
                if (_isWeapon3ButtonEnabled != value)
                {
                    _isWeapon3ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon4ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon4ButtonEnabled
        {
            get => _isWeapon4ButtonEnabled;
            set
            {
                if (_isWeapon4ButtonEnabled != value)
                {
                    _isWeapon4ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEquipCurrentCharacterButtonEnabled;

        [DataSourceProperty]
        public bool IsEquipCurrentCharacterButtonEnabled
        {
            get => _isEquipCurrentCharacterButtonEnabled;
            set
            {
                if (_isEquipCurrentCharacterButtonEnabled != value)
                {
                    _isEquipCurrentCharacterButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEnabledEquipAllButton;

        private bool _isLayerHidden;

        [DataSourceProperty]
        public bool IsLayerHidden
        {
            get => _isLayerHidden;
            set
            {
                if (_isLayerHidden != value)
                {
                    _isLayerHidden = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataSourceProperty]
        public bool IsEnabledEquipAllButton
        {
            get => _isEnabledEquipAllButton;
            set
            {
                if (_isEnabledEquipAllButton != value)
                {
                    _isEnabledEquipAllButton = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion DataSourcePropertys
    }
}