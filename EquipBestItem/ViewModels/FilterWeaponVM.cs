using TaleWorlds.Library;

namespace EquipBestItem.ViewModels
{
    public class FilterWeaponVM : ViewModel
    {
        
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
        
        private bool _maxDataValueIsDefault;
        [DataSourceProperty]
        public bool MaxDataValueIsDefault
        {
            get => _maxDataValueIsDefault;
            set
            {
                if (_maxDataValueIsDefault != value)
                {
                    _maxDataValueIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _thrustSpeedIsDefault;
        [DataSourceProperty]
        public bool ThrustSpeedIsDefault
        {
            get => _thrustSpeedIsDefault;
            set
            {
                if (_thrustSpeedIsDefault != value)
                {
                    _thrustSpeedIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _swingSpeedIsDefault;
        [DataSourceProperty]
        public bool SwingSpeedIsDefault
        {
            get => _swingSpeedIsDefault;
            set
            {
                if (_swingSpeedIsDefault != value)
                {
                    _swingSpeedIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _missileSpeedIsDefault;
        [DataSourceProperty]
        public bool MissileSpeedIsDefault
        {
            get => _missileSpeedIsDefault;
            set
            {
                if (_missileSpeedIsDefault != value)
                {
                    _missileSpeedIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _weaponLengthIsDefault;
        [DataSourceProperty]
        public bool WeaponLengthIsDefault
        {
            get => _weaponLengthIsDefault;
            set
            {
                if (_weaponLengthIsDefault != value)
                {
                    _weaponLengthIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _thrustDamageIsDefault;
        [DataSourceProperty]
        public bool ThrustDamageIsDefault
        {
            get => _thrustDamageIsDefault;
            set
            {
                if (_thrustDamageIsDefault != value)
                {
                    _thrustDamageIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _swingDamageIsDefault;
        [DataSourceProperty]
        public bool SwingDamageIsDefault
        {
            get => _swingDamageIsDefault;
            set
            {
                if (_swingDamageIsDefault != value)
                {
                    _swingDamageIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _accuracyIsDefault;
        [DataSourceProperty]
        public bool AccuracyIsDefault
        {
            get => _accuracyIsDefault;
            set
            {
                if (_accuracyIsDefault != value)
                {
                    _accuracyIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _handlingIsDefault;
        [DataSourceProperty]
        public bool HandlingIsDefault
        {
            get => _handlingIsDefault;
            set
            {
                if (_handlingIsDefault != value)
                {
                    _handlingIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _weaponBodyArmorIsDefault;
        [DataSourceProperty]
        public bool WeaponBodyArmorIsDefault
        {
            get => _weaponBodyArmorIsDefault;
            set
            {
                if (_weaponBodyArmorIsDefault != value)
                {
                    _weaponBodyArmorIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _weightIsDefault;
        [DataSourceProperty]
        public bool WeightIsDefault
        {
            get => _weightIsDefault;
            set
            {
                if (_weightIsDefault != value)
                {
                    _weightIsDefault = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}