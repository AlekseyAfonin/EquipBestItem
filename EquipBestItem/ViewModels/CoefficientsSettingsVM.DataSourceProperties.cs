using System;
using System.Diagnostics.CodeAnalysis;
using EquipBestItem.Models;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EquipBestItem.ViewModels;

[SuppressMessage("ReSharper", "StringLiteralTypo")]
internal partial class CoefficientsSettingsVM
{
    private float _accuracy;                            // Slider value
    private bool _accuracyIsDefault;                    // Param checkbox state
    private bool _accuracyIsHidden = true;              // Row hidden state
    private string _accuracyPercentText = string.Empty; // Slider percent value of all visible params

    private float _armArmor;
    private bool _armArmorIsDefault;
    private bool _armArmorIsHidden = true;
    private string _armArmorPercentText = string.Empty;

    private float _bodyArmor;
    private bool _bodyArmorIsDefault;
    private bool _bodyArmorIsHidden = true;
    private string _bodyArmorPercentText = string.Empty;

    private float _chargeDamage;
    private bool _chargeDamageIsDefault;
    private bool _chargeDamageIsHidden = true;
    private string _chargeDamagePercentText = string.Empty;

    private float _handling;
    private bool _handlingIsDefault;
    private bool _handlingIsHidden = true;
    private string _handlingPercentText = string.Empty;

    private float _headArmor;
    private bool _headArmorIsDefault;
    private bool _headArmorIsHidden = true;
    private string _headArmorPercentText = string.Empty;

    private float _hitPoints;
    private bool _hitPointsIsDefault;
    private bool _hitPointsIsHidden = true;
    private string _hitPointsPercentText = string.Empty;

    private float _legArmor;
    private bool _legArmorIsDefault;
    private bool _legArmorIsHidden = true;
    private string _legArmorPercentText = string.Empty;

    private float _maneuver;
    private bool _maneuverIsDefault;
    private bool _maneuverIsHidden = true;
    private string _maneuverPercentText = string.Empty;

    private float _maxDataValue;
    private bool _maxDataValueIsDefault;
    private bool _maxDataValueIsHidden = true;
    private string _maxDataValuePercentText = string.Empty;

    private float _missileDamage;
    private bool _missileDamageIsDefault;
    private bool _missileDamageIsHidden = true;
    private string _missileDamagePercentText = string.Empty;

    private float _missileSpeed;
    private bool _missileSpeedIsDefault;
    private bool _missileSpeedIsHidden = true;
    private string _missileSpeedPercentText = string.Empty;

    private float _speed;
    private bool _speedIsDefault;
    private bool _speedIsHidden = true;
    private string _speedPercentText = string.Empty;

    private float _swingDamage;
    private bool _swingDamageIsDefault;
    private bool _swingDamageIsHidden = true;
    private string _swingDamagePercentText = string.Empty;

    private float _swingSpeed;
    private bool _swingSpeedIsDefault;
    private bool _swingSpeedIsHidden = true;
    private string _swingSpeedPercentText = string.Empty;

    private float _thrustDamage;
    private bool _thrustDamageIsDefault;
    private bool _thrustDamageIsHidden = true;
    private string _thrustDamagePercentText = string.Empty;

    private float _thrustSpeed;
    private bool _thrustSpeedIsDefault;
    private bool _thrustSpeedIsHidden = true;
    private string _thrustSpeedPercentText = string.Empty;
    
    private float _weaponLength;
    private bool _weaponLengthIsDefault;
    private bool _weaponLengthIsHidden = true;
    private string _weaponLengthPercentText = string.Empty;

    private float _weight; 
    private bool _weightIsDefault; 
    private bool _weightIsHidden = true; 
    private string _weightPercentText = string.Empty; 
    
    private WeaponClass _weaponClass;
    private bool _weaponClassIsDefault;
    private bool _weaponClassIsHidden = true;
    
    private string _maxDataValueText = ModTexts.HitPoints;
    private string _thrustSpeedText = ModTexts.ThrustSpeed;

    [DataSourceProperty] public HintViewModel ButtonDefaultHint { get; } = new(ModTexts.ButtonDefaultHint);
    [DataSourceProperty] public HintViewModel ButtonLockHint { get; } = new(ModTexts.ButtonLockHint);
    [DataSourceProperty] public HintViewModel CheckboxHint { get; } = new(ModTexts.CheckboxHint);
    [DataSourceProperty] public HintViewModel PercentHint { get; } = new(ModTexts.PercentHint);

    [DataSourceProperty] public string ButtonDefaultText { get; } = ModTexts.ButtonDefault;
    [DataSourceProperty] public string ButtonLockText { get; } = ModTexts.ButtonLock;
    
    [DataSourceProperty] public string HeadArmorText { get; } = ModTexts.HeadArmor;
    [DataSourceProperty] public string BodyArmorText { get; } = ModTexts.BodyArmor;
    [DataSourceProperty] public string LegArmorText { get; } = ModTexts.LegArmor;
    [DataSourceProperty] public string ArmArmorText { get; } = ModTexts.ArmArmor;
    [DataSourceProperty] public string WeightText { get; } = ModTexts.Weight;
    [DataSourceProperty] public string HitPointsText { get; } = ModTexts.HitPoints;
    [DataSourceProperty] public string ChargeDamageText { get; } = ModTexts.ChargeDamage;
    [DataSourceProperty] public string ManeuverText { get; } = ModTexts.Maneuver;
    [DataSourceProperty] public string SpeedText { get; } = ModTexts.Speed;
    [DataSourceProperty] public string SwingSpeedText { get; } = ModTexts.SwingSpeed;
    [DataSourceProperty] public string MissileSpeedText { get; } = ModTexts.MissileSpeed;
    [DataSourceProperty] public string MissileDamageText { get; } = ModTexts.MissileDamage;
    [DataSourceProperty] public string WeaponLengthText { get; } = ModTexts.WeaponLength;
    [DataSourceProperty] public string ThrustDamageText { get; } = ModTexts.ThrustDamage;
    [DataSourceProperty] public string SwingDamageText { get; } = ModTexts.SwingDamage;
    [DataSourceProperty] public string AccuracyText { get; } = ModTexts.Accuracy;
    [DataSourceProperty] public string HandlingText { get; } = ModTexts.Handling;
    [DataSourceProperty] public string WeaponBodyArmorText { get; } = ModTexts.BodyArmor; //TODO
 
    [DataSourceProperty]
    public string MaxDataValueText
    {
        get => _maxDataValueText;
        set
        {
            if (_maxDataValueText == value) return;
            _maxDataValueText = value;
            OnPropertyChanged();
        }
    } 
    
    [DataSourceProperty]
    public string ThrustSpeedText
    {
        get => _thrustSpeedText;
        set
        {
            if (_thrustSpeedText == value) return;
            _thrustSpeedText = value;
            OnPropertyChanged();
        }
    } 
    
    [DataSourceProperty]
    public bool WeaponClassIsHidden
    {
        get => _weaponClassIsHidden;
        set
        {
            if (_weaponClassIsHidden == value) return;
            _weaponClassIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public SelectorVM<SelectorItemVM> WeaponClassSelector
    {
        get => _weaponClassSelector;
        set
        {
            if (value == _weaponClassSelector) return;
            _weaponClassSelector = value;
            OnPropertyChangedWithValue(value);
        }
    }

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

    [DataSourceProperty]
    public string HeadArmorPercentText
    {
        get => _headArmorPercentText;
        set
        {
            if (_headArmorPercentText == value) return;
            _headArmorPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string BodyArmorPercentText
    {
        get => _bodyArmorPercentText;
        set
        {
            if (_bodyArmorPercentText == value) return;
            _bodyArmorPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string LegArmorPercentText
    {
        get => _legArmorPercentText;
        set
        {
            if (_legArmorPercentText == value) return;
            _legArmorPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string ArmArmorPercentText
    {
        get => _armArmorPercentText;
        set
        {
            if (_armArmorPercentText == value) return;
            _armArmorPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string WeightPercentText
    {
        get => _weightPercentText;
        set
        {
            if (_weightPercentText == value) return;
            _weightPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string HitPointsPercentText
    {
        get => _hitPointsPercentText;
        set
        {
            if (_hitPointsPercentText == value) return;
            _hitPointsPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string ChargeDamagePercentText
    {
        get => _chargeDamagePercentText;
        set
        {
            if (_chargeDamagePercentText == value) return;
            _chargeDamagePercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string ManeuverPercentText
    {
        get => _maneuverPercentText;
        set
        {
            if (_maneuverPercentText == value) return;
            _maneuverPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string SpeedPercentText
    {
        get => _speedPercentText;
        set
        {
            if (_speedPercentText == value) return;
            _speedPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string MaxDataValuePercentText
    {
        get => _maxDataValuePercentText;
        set
        {
            if (_maxDataValuePercentText == value) return;
            _maxDataValuePercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string ThrustSpeedPercentText
    {
        get => _thrustSpeedPercentText;
        set
        {
            if (_thrustSpeedPercentText == value) return;
            _thrustSpeedPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string SwingSpeedPercentText
    {
        get => _swingSpeedPercentText;
        set
        {
            if (_swingSpeedPercentText == value) return;
            _swingSpeedPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string MissileSpeedPercentText
    {
        get => _missileSpeedPercentText;
        set
        {
            if (_missileSpeedPercentText == value) return;
            _missileSpeedPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string MissileDamagePercentText
    {
        get => _missileDamagePercentText;
        set
        {
            if (_missileDamagePercentText == value) return;
            _missileDamagePercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string WeaponLengthPercentText
    {
        get => _weaponLengthPercentText;
        set
        {
            if (_weaponLengthPercentText == value) return;
            _weaponLengthPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string ThrustDamagePercentText
    {
        get => _thrustDamagePercentText;
        set
        {
            if (_thrustDamagePercentText == value) return;
            _thrustDamagePercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string SwingDamagePercentText
    {
        get => _swingDamagePercentText;
        set
        {
            if (_swingDamagePercentText == value) return;
            _swingDamagePercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string AccuracyPercentText
    {
        get => _accuracyPercentText;
        set
        {
            if (_accuracyPercentText == value) return;
            _accuracyPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public string HandlingPercentText
    {
        get => _handlingPercentText;
        set
        {
            if (_handlingPercentText == value) return;
            _handlingPercentText = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public float HeadArmor
    {
        get => _headArmor;
        set
        {
            if (Math.Abs(_headArmor - value) < Tolerance) return;
            _headArmor = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float BodyArmor
    {
        get => _bodyArmor;
        set
        {
            if (Math.Abs(_bodyArmor - value) < Tolerance) return;
            _bodyArmor = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float LegArmor
    {
        get => _legArmor;
        set
        {
            if (Math.Abs(_legArmor - value) < Tolerance) return;
            _legArmor = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float ArmArmor
    {
        get => _armArmor;
        set
        {
            if (Math.Abs(_armArmor - value) < Tolerance) return;
            _armArmor = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float Weight
    {
        get => _weight;
        set
        {
            if (Math.Abs(_weight - value) < Tolerance) return;
            _weight = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float HitPoints
    {
        get => _hitPoints;
        set
        {
            if (Math.Abs(_hitPoints - value) < Tolerance) return;
            _hitPoints = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float ChargeDamage
    {
        get => _chargeDamage;
        set
        {
            if (Math.Abs(_chargeDamage - value) < Tolerance) return;
            _chargeDamage = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float Maneuver
    {
        get => _maneuver;
        set
        {
            if (Math.Abs(_maneuver - value) < Tolerance) return;
            _maneuver = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float Speed
    {
        get => _speed;
        set
        {
            if (Math.Abs(_speed - value) < Tolerance) return;
            _speed = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float MaxDataValue
    {
        get => _maxDataValue;
        set
        {
            if (Math.Abs(_maxDataValue - value) < Tolerance) return;
            _maxDataValue = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float ThrustSpeed
    {
        get => _thrustSpeed;
        set
        {
            if (Math.Abs(_thrustSpeed - value) < Tolerance) return;
            _thrustSpeed = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float SwingSpeed
    {
        get => _swingSpeed;
        set
        {
            if (Math.Abs(_swingSpeed - value) < Tolerance) return;
            _swingSpeed = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float MissileSpeed
    {
        get => _missileSpeed;
        set
        {
            if (Math.Abs(_missileSpeed - value) < Tolerance) return;
            _missileSpeed = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float MissileDamage
    {
        get => _missileDamage;
        set
        {
            if (Math.Abs(_missileDamage - value) < Tolerance) return;
            _missileDamage = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float WeaponLength
    {
        get => _weaponLength;
        set
        {
            if (Math.Abs(_weaponLength - value) < Tolerance) return;
            _weaponLength = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float ThrustDamage
    {
        get => _thrustDamage;
        set
        {
            if (Math.Abs(_thrustDamage - value) < Tolerance) return;
            _thrustDamage = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float SwingDamage
    {
        get => _swingDamage;
        set
        {
            if (Math.Abs(_swingDamage - value) < Tolerance) return;
            _swingDamage = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float Accuracy
    {
        get => _accuracy;
        set
        {
            if (Math.Abs(_accuracy - value) < Tolerance) return;
            _accuracy = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public float Handling
    {
        get => _handling;
        set
        {
            if (Math.Abs(_handling - value) < Tolerance) return;
            _handling = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public WeaponClass WeaponClass
    {
        get => _weaponClass;
        set
        {
            if (_weaponClass == value) return;
            _weaponClass = value;
            OnPropertyChangedWithValue(value);
            _model.UpdateCheckboxState(value);
        }
    }

    [DataSourceProperty]
    public bool HeadArmorIsDefault
    {
        get => _headArmorIsDefault;
        set
        {
            if (_headArmorIsDefault == value) return;
            _headArmorIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool BodyArmorIsDefault
    {
        get => _bodyArmorIsDefault;
        set
        {
            if (_bodyArmorIsDefault == value) return;
            _bodyArmorIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool LegArmorIsDefault
    {
        get => _legArmorIsDefault;
        set
        {
            if (_legArmorIsDefault == value) return;
            _legArmorIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool ArmArmorIsDefault
    {
        get => _armArmorIsDefault;
        set
        {
            if (_armArmorIsDefault == value) return;
            _armArmorIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool WeightIsDefault
    {
        get => _weightIsDefault;
        set
        {
            if (_weightIsDefault == value) return;
            _weightIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool HitPointsIsDefault
    {
        get => _hitPointsIsDefault;
        set
        {
            if (_hitPointsIsDefault == value) return;
            _hitPointsIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool ChargeDamageIsDefault
    {
        get => _chargeDamageIsDefault;
        set
        {
            if (_chargeDamageIsDefault == value) return;
            _chargeDamageIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool ManeuverIsDefault
    {
        get => _maneuverIsDefault;
        set
        {
            if (_maneuverIsDefault == value) return;
            _maneuverIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool SpeedIsDefault
    {
        get => _speedIsDefault;
        set
        {
            if (_speedIsDefault == value) return;
            _speedIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool MaxDataValueIsDefault
    {
        get => _maxDataValueIsDefault;
        set
        {
            if (_maxDataValueIsDefault == value) return;
            _maxDataValueIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool ThrustSpeedIsDefault
    {
        get => _thrustSpeedIsDefault;
        set
        {
            if (_thrustSpeedIsDefault == value) return;
            _thrustSpeedIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool SwingSpeedIsDefault
    {
        get => _swingSpeedIsDefault;
        set
        {
            if (_swingSpeedIsDefault == value) return;
            _swingSpeedIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool MissileSpeedIsDefault
    {
        get => _missileSpeedIsDefault;
        set
        {
            if (_missileSpeedIsDefault == value) return;
            _missileSpeedIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool MissileDamageIsDefault
    {
        get => _missileDamageIsDefault;
        set
        {
            if (_missileDamageIsDefault == value) return;
            _missileDamageIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool WeaponLengthIsDefault
    {
        get => _weaponLengthIsDefault;
        set
        {
            if (_weaponLengthIsDefault == value) return;
            _weaponLengthIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool ThrustDamageIsDefault
    {
        get => _thrustDamageIsDefault;
        set
        {
            if (_thrustDamageIsDefault == value) return;
            _thrustDamageIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool SwingDamageIsDefault
    {
        get => _swingDamageIsDefault;
        set
        {
            if (_swingDamageIsDefault == value) return;
            _swingDamageIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool AccuracyIsDefault
    {
        get => _accuracyIsDefault;
        set
        {
            if (_accuracyIsDefault == value) return;
            _accuracyIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool HandlingIsDefault
    {
        get => _handlingIsDefault;
        set
        {
            if (_handlingIsDefault == value) return;
            _handlingIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool WeaponClassIsDefault
    {
        get => _weaponClassIsDefault;
        set
        {
            if (_weaponClassIsDefault == value) return;
            _weaponClassIsDefault = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool HeadArmorIsHidden
    {
        get => _headArmorIsHidden;
        set
        {
            if (_headArmorIsHidden == value) return;
            _headArmorIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool BodyArmorIsHidden
    {
        get => _bodyArmorIsHidden;
        set
        {
            if (_bodyArmorIsHidden == value) return;
            _bodyArmorIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool LegArmorIsHidden
    {
        get => _legArmorIsHidden;
        set
        {
            if (_legArmorIsHidden == value) return;
            _legArmorIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool ArmArmorIsHidden
    {
        get => _armArmorIsHidden;
        set
        {
            if (_armArmorIsHidden == value) return;
            _armArmorIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool WeightIsHidden
    {
        get => _weightIsHidden;
        set
        {
            if (_weightIsHidden == value) return;
            _weightIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool HitPointsIsHidden
    {
        get => _hitPointsIsHidden;
        set
        {
            if (_hitPointsIsHidden == value) return;
            _hitPointsIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool ChargeDamageIsHidden
    {
        get => _chargeDamageIsHidden;
        set
        {
            if (_chargeDamageIsHidden == value) return;
            _chargeDamageIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool ManeuverIsHidden
    {
        get => _maneuverIsHidden;
        set
        {
            if (_maneuverIsHidden == value) return;
            _maneuverIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool SpeedIsHidden
    {
        get => _speedIsHidden;
        set
        {
            if (_speedIsHidden == value) return;
            _speedIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool MaxDataValueIsHidden
    {
        get => _maxDataValueIsHidden;
        set
        {
            if (_maxDataValueIsHidden == value) return;
            _maxDataValueIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool ThrustSpeedIsHidden
    {
        get => _thrustSpeedIsHidden;
        set
        {
            if (_thrustSpeedIsHidden == value) return;
            _thrustSpeedIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool SwingSpeedIsHidden
    {
        get => _swingSpeedIsHidden;
        set
        {
            if (_swingSpeedIsHidden == value) return;
            _swingSpeedIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool MissileSpeedIsHidden
    {
        get => _missileSpeedIsHidden;
        set
        {
            if (_missileSpeedIsHidden == value) return;
            _missileSpeedIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool MissileDamageIsHidden
    {
        get => _missileDamageIsHidden;
        set
        {
            if (_missileDamageIsHidden == value) return;
            _missileDamageIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool WeaponLengthIsHidden
    {
        get => _weaponLengthIsHidden;
        set
        {
            if (_weaponLengthIsHidden == value) return;
            _weaponLengthIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool ThrustDamageIsHidden
    {
        get => _thrustDamageIsHidden;
        set
        {
            if (_thrustDamageIsHidden == value) return;
            _thrustDamageIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool SwingDamageIsHidden
    {
        get => _swingDamageIsHidden;
        set
        {
            if (_swingDamageIsHidden == value) return;
            _swingDamageIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool AccuracyIsHidden
    {
        get => _accuracyIsHidden;
        set
        {
            if (_accuracyIsHidden == value) return;
            _accuracyIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool HandlingIsHidden
    {
        get => _handlingIsHidden;
        set
        {
            if (_handlingIsHidden == value) return;
            _handlingIsHidden = value;
            OnPropertyChanged();
        }
    }
}