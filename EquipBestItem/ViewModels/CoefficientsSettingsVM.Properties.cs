using System;
using EquipBestItem.Models.Enums;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM
{
    [DataSourceProperty] public string HeadArmorText { get; } = GameTexts.FindText("str_head_armor").ToString();
    [DataSourceProperty] public string BodyArmorText { get; } = GameTexts.FindText("str_body_armor").ToString();
    [DataSourceProperty] public string LegArmorText { get; } = GameTexts.FindText("str_leg_armor").ToString();
    [DataSourceProperty] public string ArmArmorText { get; } = new TextObject("{=cf61cce254c7dca65be9bebac7fb9bf5}Arm Armor: ").ToString();
    [DataSourceProperty] public string WeightText { get; } = GameTexts.FindText("str_weight_text").ToString();
    [DataSourceProperty] public string HitPointsText { get; } = new TextObject("{=aCkzVUCR}Hit Points: ").ToString();
    [DataSourceProperty] public string ChargeDamageText { get; } = new TextObject("{=c7638a0869219ae845de0f660fd57a9d}Charge Damage: ").ToString();
    [DataSourceProperty] public string ManeuverText { get; } = new TextObject("{=3025020b83b218707499f0de3135ed0a}Maneuver: ").ToString();
    [DataSourceProperty] public string SpeedText { get; } = new TextObject("{=74dc1908cb0b990e80fb977b5a0ef10d}Speed: ").ToString();
    [DataSourceProperty] public string MaxDataValueText { get; } = new TextObject("{=aCkzVUCR}Hit Points: ").ToString();
    [DataSourceProperty] public string ThrustSpeedText { get; } = new TextObject("{=VPYazFVH}Thrust Speed: ").ToString();
    [DataSourceProperty] public string SwingSpeedText { get; } = new TextObject("{=nfQhamAF}Swing Speed: ").ToString();
    [DataSourceProperty] public string MissileSpeedText { get; } = new TextObject("{=YukbQgHJ}Missile Speed: ").ToString();
    [DataSourceProperty] public string MissileDamageText { get; } = new TextObject("{=c9c5dfed2ca6bcb7a73d905004c97b23}Damage: ").ToString();
    [DataSourceProperty] public string WeaponLengthText { get; } = new TextObject("{=XUtiwiYP}Length: ").ToString();
    [DataSourceProperty] public string ThrustDamageText { get; } = new TextObject("{=7sUhWG0E}Thrust Damage: ").ToString();
    [DataSourceProperty] public string SwingDamageText { get; } = new TextObject("{=fMmlUHyz}Swing Damage: ").ToString();
    [DataSourceProperty] public string AccuracyText { get; } = new TextObject("{=xEWwbGVK}Accuracy: ").ToString();
    [DataSourceProperty] public string HandlingText { get; } = new TextObject("{=YOSEIvyf}Handling: ").ToString();
    [DataSourceProperty] public string WeaponBodyArmorText { get; } = new TextObject("{=bLWyjOdS}Body Armor: ").ToString();

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