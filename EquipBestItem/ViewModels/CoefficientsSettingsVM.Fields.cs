using System;
using TaleWorlds.Core;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM
{
    private const float Tolerance = 0.00001f;
    
    private bool _weaponClassIsHidden = true;
    private bool _weaponClassIsDefault;
    private WeaponClass _weaponClass;

    private float _weight;                                      // Slider value which used in percent text
    private string _weightPercentText = string.Empty;           // Slider percent value of all visible params
    private bool _weightIsDefault;                              // Param checkbox state
    private bool _weightIsHidden = true;                        // Row hidden state

    private float _headArmor;
    private string _headArmorPercentText = string.Empty; 
    private bool _headArmorIsDefault;
    private bool _headArmorIsHidden = true;
    
    private float _bodyArmor;
    private string _bodyArmorPercentText = string.Empty; 
    private bool _bodyArmorIsDefault;
    private bool _bodyArmorIsHidden = true;
    
    private float _legArmor;
    private string _legArmorPercentText = string.Empty; 
    private bool _legArmorIsDefault;
    private bool _legArmorIsHidden = true;
    
    private float _armArmor;
    private string _armArmorPercentText = string.Empty; 
    private bool _armArmorIsDefault;
    private bool _armArmorIsHidden = true;
    
    private float _hitPoints;
    private string _hitPointsPercentText = string.Empty;
    private bool _hitPointsIsDefault;
    private bool _hitPointsIsHidden = true;
    
    private float _chargeDamage;
    private string _chargeDamagePercentText = string.Empty;
    private bool _chargeDamageIsDefault;
    private bool _chargeDamageIsHidden = true;
    
    private float _maneuver;
    private string _maneuverPercentText = string.Empty;
    private bool _maneuverIsDefault;
    private bool _maneuverIsHidden = true;
    
    private float _speed;
    private string _speedPercentText = string.Empty;
    private bool _speedIsDefault;
    private bool _speedIsHidden = true;
    
    private float _maxDataValue;
    private string _maxDataValuePercentText = string.Empty;
    private bool _maxDataValueIsDefault;
    private bool _maxDataValueIsHidden = true;
    
    private float _thrustSpeed;
    private string _thrustSpeedPercentText = string.Empty;
    private bool _thrustSpeedIsDefault;
    private bool _thrustSpeedIsHidden = true;
    
    private float _swingSpeed;
    private string _swingSpeedPercentText = string.Empty;
    private bool _swingSpeedIsDefault;
    private bool _swingSpeedIsHidden = true;
    
    private float _missileSpeed;
    private string _missileSpeedPercentText = string.Empty;
    private bool _missileSpeedIsDefault;
    private bool _missileSpeedIsHidden = true;
    
    private float _missileDamage;
    private string _missileDamagePercentText = string.Empty;
    private bool _missileDamageIsDefault;
    private bool _missileDamageIsHidden = true;
    
    private float _weaponLength;
    private string _weaponLengthPercentText = string.Empty;
    private bool _weaponLengthIsDefault;
    private bool _weaponLengthIsHidden = true;
    
    private float _thrustDamage;
    private string _thrustDamagePercentText = string.Empty;
    private bool _thrustDamageIsDefault;
    private bool _thrustDamageIsHidden = true;
    
    private float _swingDamage;
    private string _swingDamagePercentText = string.Empty;
    private bool _swingDamageIsDefault;
    private bool _swingDamageIsHidden = true;
    
    private float _accuracy;
    private string _accuracyPercentText = string.Empty;
    private bool _accuracyIsDefault;
    private bool _accuracyIsHidden = true;
    
    private float _handling;
    private string _handlingPercentText = string.Empty;
    private bool _handlingIsDefault;
    private bool _handlingIsHidden = true;

    /* Template
    private float _;
    private float _Value;
    private string _PercentText = string.Empty;
    private bool _IsDefault;
    private bool _IsHidden = true;
    */
}