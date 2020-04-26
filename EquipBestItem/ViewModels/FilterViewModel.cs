using EquipBestItem.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace EquipBestItem
{
    class FilterViewModel : ViewModel
    {
        public CharacterSettings CharacterSettings;

        public static int CurrentSlot = 0;

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

        private bool _isMountSlotHidden = true;

        [DataSourceProperty]
        public bool IsMountSlotHidden
        {
            get => _isMountSlotHidden;
            set
            {
                if (_isMountSlotHidden != value)
                {
                    _isMountSlotHidden = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isArmorSlotHidden = true;

        [DataSourceProperty]
        public bool IsArmorSlotHidden
        {
            get => _isArmorSlotHidden;
            set
            {
                if (_isArmorSlotHidden != value)
                {
                    _isArmorSlotHidden = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeaponSlotHidden = true;

        [DataSourceProperty]
        public bool IsWeaponSlotHidden
        {
            get => _isWeaponSlotHidden;
            set
            {
                if (_isWeaponSlotHidden != value)
                {
                    _isWeaponSlotHidden = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _title;

        [DataSourceProperty]
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isHiddenFilterLayer = true;

        [DataSourceProperty]
        public bool IsHiddenFilterLayer
        {
            get => _isHiddenFilterLayer;
            set
            {
                if (_isHiddenFilterLayer != value)
                {
                    _isHiddenFilterLayer = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _swingDamage;
        [DataSourceProperty]
        public string SwingDamage
        {
            get => _swingDamage;
            set
            {
                if (_swingDamage != value)
                {
                    _swingDamage = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _swingSpeed;
        [DataSourceProperty]
        public string SwingSpeed
        {
            get => _swingSpeed;
            set
            {
                if (_swingSpeed != value)
                {
                    _swingSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _thrustDamage;
        [DataSourceProperty]
        public string ThrustDamage
        {
            get => _thrustDamage;
            set
            {
                if (_thrustDamage != value)
                {
                    _thrustDamage = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _thrustSpeed;
        [DataSourceProperty]
        public string ThrustSpeed
        {
            get => _thrustSpeed;
            set
            {
                if (_thrustSpeed != value)
                {
                    _thrustSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _weaponLength;
        [DataSourceProperty]
        public string WeaponLength
        {
            get => _weaponLength;
            set
            {
                if (_weaponLength != value)
                {
                    _weaponLength = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _handling;
        [DataSourceProperty]
        public string Handling
        {
            get => _handling;
            set
            {
                if (_handling != value)
                {
                    _handling = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _weaponWeight;
        [DataSourceProperty]
        public string WeaponWeight
        {
            get => _weaponWeight;
            set
            {
                if (_weaponWeight != value)
                {
                    _weaponWeight = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _accuracy;
        [DataSourceProperty]
        public string Accuracy
        {
            get => _accuracy;
            set
            {
                if (_accuracy != value)
                {
                    _accuracy = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private string _missileSpeed;
        [DataSourceProperty]
        public string MissileSpeed
        {
            get => _missileSpeed;
            set
            {
                if (_missileSpeed != value)
                {
                    _missileSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _weaponBodyArmor;
        [DataSourceProperty]
        public string WeaponBodyArmor
        {
            get => _weaponBodyArmor;
            set
            {
                if (_weaponBodyArmor != value)
                {
                    _weaponBodyArmor = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private string _maxDataValue;
        [DataSourceProperty]
        public string MaxDataValue
        {
            get => _maxDataValue;
            set
            {
                if (_maxDataValue != value)
                {
                    _maxDataValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _headArmor;
        [DataSourceProperty]
        public string HeadArmor
        {
            get => _headArmor;
            set
            {
                if (_headArmor != value)
                {
                    _headArmor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _armorBodyArmor;
        [DataSourceProperty]
        public string ArmorBodyArmor
        {
            get => _armorBodyArmor;
            set
            {
                if (_armorBodyArmor != value)
                {
                    _armorBodyArmor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _legArmor;
        [DataSourceProperty]
        public string LegArmor
        {
            get => _legArmor;
            set
            {
                if (_legArmor != value)
                {
                    _legArmor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _armArmor;
        [DataSourceProperty]
        public string ArmArmor
        {
            get => _armArmor;
            set
            {
                if (_armArmor != value)
                {
                    _armArmor = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maneuverBonus;
        [DataSourceProperty]
        public string ManeuverBonus
        {
            get => _maneuverBonus;
            set
            {
                if (_maneuverBonus != value)
                {
                    _maneuverBonus = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _speedBonus;
        [DataSourceProperty]
        public string SpeedBonus
        {
            get => _speedBonus;
            set
            {
                if (_speedBonus != value)
                {
                    _speedBonus = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _chargeBonus;
        [DataSourceProperty]
        public string ChargeBonus
        {
            get => _chargeBonus;
            set
            {
                if (_chargeBonus != value)
                {
                    _chargeBonus = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _armorWeight;
        [DataSourceProperty]
        public string ArmorWeight
        {
            get => _armorWeight;
            set
            {
                if (_armorWeight != value)
                {
                    _armorWeight = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _chargeDamage;
        [DataSourceProperty]
        public string ChargeDamage
        {
            get => _chargeDamage;
            set
            {
                if (_chargeDamage != value)
                {
                    _chargeDamage = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _hitPoints;
        [DataSourceProperty]
        public string HitPoints
        {
            get => _hitPoints;
            set
            {
                if (_hitPoints != value)
                {
                    _hitPoints = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maneuver;
        [DataSourceProperty]
        public string Maneuver
        {
            get => _maneuver;
            set
            {
                if (_maneuver != value)
                {
                    _maneuver = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _speed;
        [DataSourceProperty]
        public string Speed
        {
            get => _speed;
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    OnPropertyChanged();
                }
            }
        }


        //private string _weaponClass = "Choose weapon class";


        //[DataSourceProperty]
        //public string WeaponClass
        //{
        //    get => _weaponClass;
        //    set
        //    {
        //        if (_weaponClass != value)
        //        {
        //            _weaponClass = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}


        //private string _weaponUsage = "Choose weapon type item usage";
        //[DataSourceProperty]
        //public string WeaponUsage
        //{
        //    get => _weaponUsage;
        //    set
        //    {
        //        if (_weaponUsage != value)
        //        {
        //            _weaponUsage = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //private List<string> _ItemUsageList = new List<string>()
        //{
        //    "arrow_right",
        //    "arrow_top",
        //    "bow",
        //    "crossbow",
        //    "hand_shield",
        //    "long_bow",
        //    "shield"
        //};


        //private List<string> _weaponFlagsList;
        //private int _weaponFlagsCurrentIndex = 0;

        public FilterViewModel()
        {
            this.RefreshValues();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            EquipBestItemViewModel.UpdateCurrentCharacterName();
            this.CharacterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(EquipBestItemViewModel.CurrentCharacterName);

            if (!IsWeaponSlotHidden)
                this.Title = "Weapon " + (CurrentSlot+1) + " filter";
            if (!IsArmorSlotHidden)
                switch (CurrentSlot)
                {
                    case 0:
                        this.Title = "Helm filter";
                        break;
                    case 1:
                        this.Title = "Cloak filter";
                        break;
                    case 2:
                        this.Title = "Armor filter";
                        break;
                    case 3:
                        this.Title = "Glove filter";
                        break;
                    case 4:
                        this.Title = "Boot filter";
                        break;
                    case 5:
                        this.Title = "Harness filter";
                        break;
                    default:
                        this.Title = "Default";
                        break;
                } 
            if (!IsMountSlotHidden)
                this.Title = "Mount " + "filter";

            if (!IsWeaponSlotHidden)
            {
                this.Accuracy = this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy.ToString();
                this.WeaponBodyArmor = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor.ToString();
                this.Handling = this.CharacterSettings.FilterWeapon[CurrentSlot].Handling.ToString();
                this.MaxDataValue = this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue.ToString();
                this.MissileSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed.ToString();
                this.SwingDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage.ToString();
                this.SwingSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed.ToString();
                this.ThrustDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage.ToString();
                this.ThrustSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed.ToString();
                this.WeaponLength = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength.ToString();
                this.WeaponWeight = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight.ToString();
            }

            if (!IsArmorSlotHidden)
            {
                this.HeadArmor = this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor.ToString();
                this.ArmorBodyArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor.ToString();
                this.LegArmor = this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor.ToString();
                this.ArmArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor.ToString();
                this.ManeuverBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus.ToString();
                this.SpeedBonus = this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus.ToString();
                this.ChargeBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus.ToString();
                this.ArmorWeight = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight.ToString();
            }

            if (!IsMountSlotHidden)
            {
                this.ChargeDamage = this.CharacterSettings.FilterMount.ChargeDamage.ToString();
                this.HitPoints = this.CharacterSettings.FilterMount.HitPoints.ToString();
                this.Maneuver = this.CharacterSettings.FilterMount.Maneuver.ToString();
                this.Speed = this.CharacterSettings.FilterMount.Speed.ToString();
            }




            if (SettingsLoader.Debug)
                InformationManager.DisplayMessage(new InformationMessage("FilterVMRefreshValue"));
        }

        public void ExecuteSwingDamagePrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage -= 1f;
            SwingDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage.ToString();
        }
        public void ExecuteSwingDamageNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage += 1f;
            SwingDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage.ToString();
        }

        public void ExecuteSwingSpeedPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed -= 1f;
            SwingSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed.ToString();
        }
        public void ExecuteSwingSpeedNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed += 1f;
            SwingSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed.ToString();
        }

        public void ExecuteThrustDamagePrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage -= 1f;
            ThrustDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage.ToString();
        }
        public void ExecuteThrustDamageNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage += 1f;
            ThrustDamage = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage.ToString();
        }

        public void ExecuteThrustSpeedPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed -= 1f;
            ThrustSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed.ToString();
        }
        public void ExecuteThrustSpeedNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed += 1f;
            ThrustSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed.ToString();
        }

        public void ExecuteWeaponLengthPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength -= 1f;
            WeaponLength = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength.ToString();
        }
        public void ExecuteWeaponLengthNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength += 1f;
            WeaponLength = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength.ToString();
        }

        public void ExecuteHandlingPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Handling -= 1f;
            Handling = this.CharacterSettings.FilterWeapon[CurrentSlot].Handling.ToString();
        }
        public void ExecuteHandlingNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Handling += 1f;
            Handling = this.CharacterSettings.FilterWeapon[CurrentSlot].Handling.ToString();
        }

        public void ExecuteWeaponWeightPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight -= 1f;
            WeaponWeight = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight.ToString();
        }
        public void ExecuteWeaponWeightNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight += 1f;
            WeaponWeight = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight.ToString();
        }

        public void ExecuteMissileSpeedPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed -= 1f;
            MissileSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed.ToString();
        }
        public void ExecuteMissileSpeedNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed += 1f;
            MissileSpeed = this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed.ToString();
        }

        public void ExecuteAccuracyPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy -= 1f;
            Accuracy = this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy.ToString();
        }
        public void ExecuteAccuracyNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy += 1f;
            Accuracy = this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy.ToString();
        }
        
        public void ExecuteWeaponBodyArmorPrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor -= 1f;
            WeaponBodyArmor = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor.ToString();
        }
        public void ExecuteWeaponBodyArmorNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor += 1f;
            WeaponBodyArmor = this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor.ToString();
        }

        public void ExecuteMaxDataValuePrev()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue -= 1f;
            MaxDataValue = this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue.ToString();
        }
        public void ExecuteMaxDataValueNext()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue += 1f;
            MaxDataValue = this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue.ToString();
        }


        public void ExecuteHeadArmorPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor -= 1f;
            HeadArmor = this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor.ToString();
        }
        public void ExecuteHeadArmorNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor += 1f;
            HeadArmor = this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor.ToString();
        }

        public void ExecuteArmorBodyArmorPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor -= 1f;
            ArmorBodyArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor.ToString();
        }
        public void ExecuteArmorBodyArmorNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor += 1f;
            ArmorBodyArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor.ToString();
        }

        public void ExecuteLegArmorPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor -= 1f;
            LegArmor = this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor.ToString();
        }
        public void ExecuteLegArmorNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor += 1f;
            LegArmor = this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor.ToString();
        }

        public void ExecuteArmArmorPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor -= 1f;
            ArmArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor.ToString();
        }
        public void ExecuteArmArmorNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor += 1f;
            ArmArmor = this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor.ToString();
        }

        public void ExecuteManeuverBonusPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus -= 1f;
            ManeuverBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus.ToString();
        }
        public void ExecuteManeuverBonusNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus += 1f;
            ManeuverBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus.ToString();
        }

        public void ExecuteSpeedBonusPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus -= 1f;
            SpeedBonus = this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus.ToString();
        }
        public void ExecuteSpeedBonusNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus += 1f;
            SpeedBonus = this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus.ToString();
        }

        public void ExecuteChargeBonusPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus -= 1f;
            ChargeBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus.ToString();
        }
        public void ExecuteChargeBonusNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus += 1f;
            ChargeBonus = this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus.ToString();
        }

        public void ExecuteArmorWeightPrev()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight -= 1f;
            ArmorWeight = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight.ToString();
        }
        public void ExecuteArmorWeightNext()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight += 1f;
            ArmorWeight = this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight.ToString();
        }

        public void ExecuteChargeDamagePrev()
        {
            this.CharacterSettings.FilterMount.ChargeDamage -= 1f;
            ChargeDamage = this.CharacterSettings.FilterMount.ChargeDamage.ToString();
        }
        public void ExecuteChargeDamageNext()
        {
            this.CharacterSettings.FilterMount.ChargeDamage += 1f;
            ChargeDamage = this.CharacterSettings.FilterMount.ChargeDamage.ToString();
        }

        public void ExecuteHitPointsPrev()
        {
            this.CharacterSettings.FilterMount.HitPoints -= 1f;
            HitPoints = this.CharacterSettings.FilterMount.HitPoints.ToString();
        }
        public void ExecuteHitPointsNext()
        {
            this.CharacterSettings.FilterMount.HitPoints += 1f;
            HitPoints = this.CharacterSettings.FilterMount.HitPoints.ToString();
        }

        public void ExecuteManeuverPrev()
        {
            this.CharacterSettings.FilterMount.Maneuver -= 1f;
            Maneuver = this.CharacterSettings.FilterMount.Maneuver.ToString();
        }
        public void ExecuteManeuverNext()
        {
            this.CharacterSettings.FilterMount.Maneuver += 1f;
            Maneuver = this.CharacterSettings.FilterMount.Maneuver.ToString();
        }

        public void ExecuteSpeedPrev()
        {
            this.CharacterSettings.FilterMount.Speed -= 1f;
            Speed = this.CharacterSettings.FilterMount.Speed.ToString();
        }
        public void ExecuteSpeedNext()
        {
            this.CharacterSettings.FilterMount.Speed += 1f;
            Speed = this.CharacterSettings.FilterMount.Speed.ToString();
        }


        public void ExecuteShowHideWeapon1Filter()
        {
            
            if (CurrentSlot != 0 || this.IsWeaponSlotHidden)
            {
                CurrentSlot = 0;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;
            this.IsWeaponSlotHidden = false;
            this.IsArmorSlotHidden = true;
            this.IsMountSlotHidden = true;
            this.RefreshValues();
            

        }
        public void ExecuteShowHideWeapon2Filter()
        {

            if (CurrentSlot != 1 || this.IsWeaponSlotHidden)
            {
                CurrentSlot = 1;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;
            this.IsWeaponSlotHidden = false;
            this.IsArmorSlotHidden = true;
            this.IsMountSlotHidden = true;
            this.RefreshValues();
        }
        public void ExecuteShowHideWeapon3Filter()
        {

            if (CurrentSlot != 2 || this.IsWeaponSlotHidden)
            {
                CurrentSlot = 2;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;
            this.IsWeaponSlotHidden = false;
            this.IsArmorSlotHidden = true;
            this.IsMountSlotHidden = true;
            this.RefreshValues();
        }
        public void ExecuteShowHideWeapon4Filter()
        {

            if (CurrentSlot != 3 || this.IsWeaponSlotHidden)
            {
                CurrentSlot = 3;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;
            this.IsWeaponSlotHidden = false;
            this.IsArmorSlotHidden = true;
            this.IsMountSlotHidden = true;
            this.RefreshValues();
        }

        public void ExecuteShowHideHelmFilter()
        {
            if (CurrentSlot != 0 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 0;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            this.RefreshValues();
        }

        public void ExecuteShowHideCloakFilter()
        {
            if (CurrentSlot != 1 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 1;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            this.RefreshValues();
        }

        public void ExecuteShowHideArmorFilter()
        {
            if (CurrentSlot != 2 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 2;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            this.RefreshValues();

        }

        public void ExecuteShowHideGloveFilter()
        {
            if (CurrentSlot != 3 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 3;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            this.RefreshValues();

        }

        public void ExecuteShowHideBootFilter()
        {
            if (CurrentSlot != 4 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 4;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            this.RefreshValues();

        }

        public void ExecuteShowHideMountFilter()
        {
            if (this.IsMountSlotHidden)
                this.IsHiddenFilterLayer = false;
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = true;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = false;
            this.RefreshValues();

        }

        public void ExecuteShowHideHarnessFilter()
        {
            if (CurrentSlot != 5 || this.IsArmorSlotHidden)
            {
                CurrentSlot = 5;
                this.IsHiddenFilterLayer = false;
            }
            else
                this.IsHiddenFilterLayer = !IsHiddenFilterLayer;

            this.IsArmorSlotHidden = false;
            this.IsWeaponSlotHidden = true;
            this.IsMountSlotHidden = true;
            this.RefreshValues();

        }

        public void ExecuteWeaponClose()
        {
            IsHiddenFilterLayer = true;
            this.RefreshValues();
        }

        public void ExecuteMountClear()
        {
            this.CharacterSettings.FilterMount.ChargeDamage = 1f;
            this.CharacterSettings.FilterMount.HitPoints = 1f;
            this.CharacterSettings.FilterMount.Maneuver = 1f;
            this.CharacterSettings.FilterMount.Speed = 1f;
            this.RefreshValues();
        }

        public void ExecuteArmorClear()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus = 1f;
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight = 0;
            this.RefreshValues();
        }

        public void ExecuteWeaponClear()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].Handling = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength = 1f;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight = 0;
            this.RefreshValues();
        }

        public void ExecuteMountLock()
        {
            this.CharacterSettings.FilterMount.ChargeDamage = 0;
            this.CharacterSettings.FilterMount.HitPoints = 0;
            this.CharacterSettings.FilterMount.Maneuver = 0;
            this.CharacterSettings.FilterMount.Speed = 0;
            this.RefreshValues();
        }

        public void ExecuteArmorLock()
        {
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmArmor = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorBodyArmor = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].ChargeBonus = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].HeadArmor = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].LegArmor = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].ManeuverBonus = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].SpeedBonus = 0;
            this.CharacterSettings.FilterArmor[CurrentSlot].ArmorWeight = 0;
            this.RefreshValues();
        }

        public void ExecuteWeaponLock()
        {
            this.CharacterSettings.FilterWeapon[CurrentSlot].Accuracy = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponBodyArmor = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].Handling = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].MaxDataValue = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].MissileSpeed = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingDamage = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].SwingSpeed = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustDamage = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].ThrustSpeed = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponLength = 0;
            this.CharacterSettings.FilterWeapon[CurrentSlot].WeaponWeight = 0;
            this.RefreshValues();
        }

        //public void ExecuteWeaponTypeSelectNextItem()
        //{
        //    if (this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass == (WeaponClass)28)
        //        this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass = (WeaponClass)0;
        //    else
        //        this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass += 1;

        //    this.WeaponClass = this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponClass.ToString();
        //}

        //public void ExecuteWeaponUsageSelectPreviousItem()
        //{




        //    //////// НЕ УДАЛЯТЬ МОЖЕТ ПРИГОДИТЬСЯ В БУДУЩЕМ
        //    //_weaponFlagsCurrentIndex -= 1;
        //    //if (_weaponFlagsCurrentIndex < 0)
        //    //    _weaponFlagsCurrentIndex = _weaponFlagsList.Count - 1;


        //    //if (_weaponFlagsList[_weaponFlagsCurrentIndex] == "None")
        //    //    this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)0;
        //    //else
        //    //    this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)Enum.Parse(typeof(WeaponFlags), _weaponFlagsList[_weaponFlagsCurrentIndex]);

        //    //this.WeaponUsage = _weaponFlagsList[_weaponFlagsCurrentIndex];
        //}

        //public void ExecuteWeaponUsageSelectNextItem()
        //{




        //    //////// НЕ УДАЛЯТЬ МОЖЕТ ПРИГОДИТЬСЯ В БУДУЩЕМ
        //    ///
        //    //_weaponFlagsCurrentIndex += 1;
        //    //if (_weaponFlagsCurrentIndex > _weaponFlagsList.Count - 1)
        //    //    _weaponFlagsCurrentIndex = 0;

        //    //if (_weaponFlagsList[_weaponFlagsCurrentIndex] == "None")
        //    //    this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)0;
        //    //else
        //    //    this.CharacterSettings.FilterWeapon[CurrentWeaponSlot].WeaponFlags = (WeaponFlags)Enum.Parse(typeof(WeaponFlags), _weaponFlagsList[_weaponFlagsCurrentIndex]);

        //    //this.WeaponUsage = _weaponFlagsList[_weaponFlagsCurrentIndex];
        //}
    }
}
