using System;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM
{
    private const float Tolerance = 1E-05f;
    private string _headerText;

    private float _weight;                                      // Slider released value
    private float _weightValue;                                 // Slider value which used in percent text
    private string _weightPercentText = string.Empty;           // Slider percent value of all visible params
    private bool _weightIsDefault;                              // Param checkbox state
    private bool _weightIsHidden = true;                        // Row hidden state

    private float _headArmor;
    private float _headArmorValue;
    private string _headArmorPercentText = string.Empty; 
    private bool _headArmorIsDefault;
    private bool _headArmorIsHidden = true;
    
    private float _bodyArmor;
    private float _bodyArmorValue;
    private string _bodyArmorPercentText = string.Empty; 
    private bool _bodyArmorIsDefault;
    private bool _bodyArmorIsHidden = true;
    
    private float _legArmor;
    private float _legArmorValue;
    private string _legArmorPercentText = string.Empty; 
    private bool _legArmorIsDefault;
    private bool _legArmorIsHidden = true;
    
    private float _armArmor;
    private float _armArmorValue;
    private string _armArmorPercentText = string.Empty; 
    private bool _armArmorIsDefault;
    private bool _armArmorIsHidden = true;

    /* Template
    private float _;
    private float _Value;
    private string _PercentText;
    private bool _IsDefault;
    private bool _IsHidden = true;
    */
}