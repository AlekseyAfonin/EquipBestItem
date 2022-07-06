using System;
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

    [DataSourceProperty] public string HeadArmorValueText => GetArmorValuePercentText(HeadArmorValue);
    [DataSourceProperty] public string BodyArmorValueText => GetArmorValuePercentText(BodyArmorValue); //TODO HorseFix
    [DataSourceProperty] public string LegArmorValueText => GetArmorValuePercentText(LegArmorValue);
    [DataSourceProperty] public string ArmArmorValueText => GetArmorValuePercentText(ArmArmorValue);
    [DataSourceProperty] public string WeightValueText => GetArmorValuePercentText(WeightValue);
    
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
    public float HeadArmorValue
    {
        get => _headArmorValue;
        set
        {
            if (!(Math.Abs(_headArmorValue - value) > Tolerance)) return;
            _headArmorValue = value;
            OnPropertyChangedWithValue(value);
            UpdateArmorProperties();
            OnPropertyChanged(nameof(HeadArmorValueText));
        }
    }
    
    [DataSourceProperty]
    public float BodyArmorValue
    {
        get => _bodyArmorValue;
        set
        {
            if (!(Math.Abs(_bodyArmorValue - value) > Tolerance)) return;
            _bodyArmorValue = value;
            OnPropertyChangedWithValue(value);
            UpdateArmorProperties();
            OnPropertyChanged(nameof(BodyArmorValueText));
        }
    }
    
    [DataSourceProperty]
    public float LegArmorValue
    {
        get => _legArmorValue;
        set
        {
            if (!(Math.Abs(_legArmorValue - value) > Tolerance)) return;
            _legArmorValue = value;
            OnPropertyChangedWithValue(value);
            UpdateArmorProperties();
            OnPropertyChanged(nameof(LegArmorValueText));
        }
    }
    
    [DataSourceProperty]
    public float ArmArmorValue
    {
        get => _armArmorValue;
        set
        {
            if (!(Math.Abs(_armArmorValue - value) > Tolerance)) return;
            _armArmorValue = value;
            OnPropertyChangedWithValue(value);
            UpdateArmorProperties();
            OnPropertyChanged(nameof(ArmArmorValueText));
        }
    }
    
    [DataSourceProperty]
    public float WeightValue
    {
        get => _weightValue;
        set
        {
            if (!(Math.Abs(_weightValue - value) > Tolerance)) return;
            _weightValue = value;
            OnPropertyChangedWithValue(value);
            UpdateArmorProperties();
            OnPropertyChanged(nameof(WeightValueText));
        }
    }
    
    [DataSourceProperty]
    public bool HeadArmorValueIsDefault
    {
        get => _headArmorValueIsDefault;
        set
        {
            if (_headArmorValueIsDefault == value) return;
            _headArmorValueIsDefault = value;
            OnPropertyChanged();
        }
    }
    
    [DataSourceProperty]
    public bool BodyArmorValueIsDefault
    {
        get => _bodyArmorValueIsDefault;
        set
        {
            if (_bodyArmorValueIsDefault == value) return;
            _bodyArmorValueIsDefault = value;
            OnPropertyChanged();
        }
    }
    
    [DataSourceProperty]
    public bool LegArmorValueIsDefault
    {
        get => _legArmorValueIsDefault;
        set
        {
            if (_legArmorValueIsDefault == value) return;
            _legArmorValueIsDefault = value;
            OnPropertyChanged();
        }
    }
    
    [DataSourceProperty]
    public bool ArmArmorValueIsDefault
    {
        get => _armArmorValueIsDefault;
        set
        {
            if (_armArmorValueIsDefault == value) return;
            _armArmorValueIsDefault = value;
            OnPropertyChanged();
        }
    }
    
    [DataSourceProperty]
    public bool WeightValueIsDefault
    {
        get => _weightValueIsDefault;
        set
        {
            if (_weightValueIsDefault == value) return;
            _weightValueIsDefault = value;
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
}