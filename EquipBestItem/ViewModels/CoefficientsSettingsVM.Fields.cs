using TaleWorlds.Core;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM
{
    private const float Tolerance = 0.00001f;

    private float _accuracy;
    private bool _accuracyIsDefault;
    private bool _accuracyIsHidden = true;
    private string _accuracyPercentText = string.Empty;

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
    private WeaponClass _weaponClass;
    private bool _weaponClassIsDefault;

    private bool _weaponClassIsHidden = true;

    private float _weaponLength;
    private bool _weaponLengthIsDefault;
    private bool _weaponLengthIsHidden = true;
    private string _weaponLengthPercentText = string.Empty;

    private float _weight; // Slider value which used in percent text
    private bool _weightIsDefault; // Param checkbox state
    private bool _weightIsHidden = true; // Row hidden state
    private string _weightPercentText = string.Empty; // Slider percent value of all visible params

    /* Template
    private float _;
    private float _Value;
    private string _PercentText = string.Empty;
    private bool _IsDefault;
    private bool _IsHidden = true;
    */
}