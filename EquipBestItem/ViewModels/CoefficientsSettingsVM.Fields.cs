namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM
{
    private const float Tolerance = 0.000000001f;
    private string _headerText;
    
    private float _weightValue;
    private bool _weightValueIsDefault;
    private bool _weightIsHidden = true;

    private float _headArmorValue;
    private bool _headArmorValueIsDefault;
    private bool _headArmorIsHidden = true;
    
    private float _bodyArmorValue;
    private bool _bodyArmorValueIsDefault;
    private bool _bodyArmorIsHidden = true;
    
    private float _legArmorValue;
    private bool _legArmorValueIsDefault;
    private bool _legArmorIsHidden = true;
    
    private float _armArmorValue;
    private bool _armArmorValueIsDefault;
    private bool _armArmorIsHidden = true;
    
    
}