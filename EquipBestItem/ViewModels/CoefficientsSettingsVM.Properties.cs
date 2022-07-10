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

     /* Template percent text
     [DataSourceProperty]
     public string _PercentText
     {
         get => _PercentText;
         set
         {
             if (_PercentText == value) return;
             _PercentText = value;
             OnPropertyChanged();
         }
     }
    */
    

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
    public float HeadArmor
    {
        get => _headArmor;
        set
        {
            if (Math.Abs(_headArmor - value) < Tolerance) return;
            _headArmor = value;
            OnPropertyChangedWithValue(value);
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
    public float HeadArmorValue
    {
        get => _headArmorValue;
        set
        {
            if (Math.Abs(_headArmorValue - value) < Tolerance) return;
            _headArmorValue = value;
            OnPropertyChanged();
            UpdatePercentText();
        }
    }
    
    [DataSourceProperty]
    public float BodyArmorValue
    {
        get => _bodyArmorValue;
        set
        {
            if (Math.Abs(_bodyArmorValue - value) < Tolerance) return;
            _bodyArmorValue = value;
            OnPropertyChanged();
            UpdatePercentText();
        }
    }
    
    [DataSourceProperty]
    public float LegArmorValue
    {
        get => _legArmorValue;
        set
        {
            if (Math.Abs(_legArmorValue - value) < Tolerance) return;
            _legArmorValue = value;
            OnPropertyChanged();
            UpdatePercentText();
        }
    }
    
    [DataSourceProperty]
    public float ArmArmorValue
    {
        get => _armArmorValue;
        set
        {
            if (Math.Abs(_armArmorValue - value) < Tolerance) return;
            _armArmorValue = value;
            OnPropertyChanged();
            UpdatePercentText();
        }
    }
    
    [DataSourceProperty]
    public float WeightValue
    {
        get => _weightValue;
        set
        {
            if (Math.Abs(_weightValue - value) < Tolerance) return;
            _weightValue = value;
            OnPropertyChanged();
            UpdatePercentText();
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