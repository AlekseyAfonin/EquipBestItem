using System.Diagnostics.CodeAnalysis;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EquipBestItem.UIExtenderEx;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed partial class SPInventoryVMMixin
{
    private bool _isBodyButtonDisabled;
    private bool _isCapeButtonDisabled;
    private bool _isGlovesButtonDisabled;
    private bool _isHeadButtonDisabled;
    private bool _isHorseButtonDisabled;
    private bool _isHorseHarnessButtonDisabled;
    private bool _isLeftMenuVisible;
    private bool _isLeftPanelLocked = true;
    private bool _isLegButtonDisabled;
    private bool _isRightMenuVisible;
    private bool _isRightPanelLocked = true;
    private bool _isWeapon0ButtonDisabled;
    private bool _isWeapon1ButtonDisabled;
    private bool _isWeapon2ButtonDisabled;
    private bool _isWeapon3ButtonDisabled;
    
    [DataSourceProperty] public bool LeftClosedLockIsHidden => IsLeftPanelLocked;
    [DataSourceProperty] public bool LeftOpenedLockIsHidden => !IsLeftPanelLocked;
    [DataSourceProperty] public bool RightClosedLockIsHidden => IsRightPanelLocked;
    [DataSourceProperty] public bool RightOpenedLockIsHidden => !IsRightPanelLocked;

    [DataSourceProperty] public SPItemVM? HeadBestItem => _model.BestItems[(int) EquipmentIndex.Head];
    [DataSourceProperty] public SPItemVM? BodyBestItem => _model.BestItems[(int) EquipmentIndex.Body];
    [DataSourceProperty] public SPItemVM? CapeBestItem => _model.BestItems[(int) EquipmentIndex.Cape];
    [DataSourceProperty] public SPItemVM? GlovesBestItem => _model.BestItems[(int) EquipmentIndex.Gloves];
    [DataSourceProperty] public SPItemVM? HorseBestItem => _model.BestItems[(int) EquipmentIndex.Horse];
    [DataSourceProperty] public SPItemVM? LegBestItem => _model.BestItems[(int) EquipmentIndex.Leg];
    [DataSourceProperty] public SPItemVM? Weapon0BestItem => _model.BestItems[(int) EquipmentIndex.Weapon0];
    [DataSourceProperty] public SPItemVM? Weapon1BestItem => _model.BestItems[(int) EquipmentIndex.Weapon1];
    [DataSourceProperty] public SPItemVM? Weapon2BestItem => _model.BestItems[(int) EquipmentIndex.Weapon2];
    [DataSourceProperty] public SPItemVM? Weapon3BestItem => _model.BestItems[(int) EquipmentIndex.Weapon3];
    [DataSourceProperty] public SPItemVM? HorseHarnessBestItem => _model.BestItems[(int) EquipmentIndex.HorseHarness];
    
    [DataSourceProperty]
    public HintViewModel ButtonEquipCurrentHint { get; } =
        new(new TextObject("{=ebi_hint_equip_current}Equip current character"));

    [DataSourceProperty]
    public HintViewModel ButtonEquipAllHint { get; } =
        new(new TextObject("{=ebi_hint_equip_all}Equip all characters"));

    [DataSourceProperty]
    public HintViewModel ButtonLeftPanelLockHint { get; } =
        new(new TextObject("{=ebi_hint_left_panel_lock}Disable search in left-hand items"));

    [DataSourceProperty]
    public HintViewModel ButtonLeftPanelUnlockHint { get; } =
        new(new TextObject("{=ebi_hint_left_panel_unlock}Enable search in the left-hand items"));

    [DataSourceProperty]
    public HintViewModel ButtonRightPanelLockHint { get; } =
        new(new TextObject("{=ebi_hint_right_panel_lock}Disable search in right-hand items"));

    [DataSourceProperty]
    public HintViewModel ButtonRightPanelUnlockHint { get; } =
        new(new TextObject("{=ebi_hint_right_panel_unlock}Enable search in right-hand items"));

    [DataSourceProperty]
    public HintViewModel ButtonMenuHideHint { get; } = new(new TextObject("{=ebi_hint_menu_hide}Hide buttons panel"));

    [DataSourceProperty]
    public HintViewModel ButtonMenuShowHint { get; } = new(new TextObject("{=ebi_hint_menu_show}Show buttons panel"));
    
    [DataSourceProperty]
    public bool IsHeadButtonDisabled
    {
        get => _isHeadButtonDisabled;
        set
        {
            if (_isHeadButtonDisabled == value) return;
            _isHeadButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsCapeButtonDisabled
    {
        get => _isCapeButtonDisabled;
        set
        {
            if (_isCapeButtonDisabled == value) return;
            _isCapeButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsBodyButtonDisabled
    {
        get => _isBodyButtonDisabled;
        set
        {
            if (_isBodyButtonDisabled == value) return;
            _isBodyButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsLegButtonDisabled
    {
        get => _isLegButtonDisabled;
        set
        {
            if (_isLegButtonDisabled == value) return;
            _isLegButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsGlovesButtonDisabled
    {
        get => _isGlovesButtonDisabled;
        set
        {
            if (_isGlovesButtonDisabled == value) return;
            _isGlovesButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsHorseButtonDisabled
    {
        get => _isHorseButtonDisabled;
        set
        {
            if (_isHorseButtonDisabled == value) return;
            _isHorseButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsHorseHarnessButtonDisabled
    {
        get => _isHorseHarnessButtonDisabled;
        set
        {
            if (_isHorseHarnessButtonDisabled == value) return;
            _isHorseHarnessButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsWeapon0ButtonDisabled
    {
        get => _isWeapon0ButtonDisabled;
        set
        {
            if (_isWeapon0ButtonDisabled == value) return;
            _isWeapon0ButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsWeapon1ButtonDisabled
    {
        get => _isWeapon1ButtonDisabled;
        set
        {
            if (_isWeapon1ButtonDisabled == value) return;
            _isWeapon1ButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsWeapon2ButtonDisabled
    {
        get => _isWeapon2ButtonDisabled;
        set
        {
            if (_isWeapon2ButtonDisabled == value) return;
            _isWeapon2ButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsWeapon3ButtonDisabled
    {
        get => _isWeapon3ButtonDisabled;
        set
        {
            if (_isWeapon3ButtonDisabled == value) return;
            _isWeapon3ButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsLeftMenuVisible
    {
        get => _isLeftMenuVisible;
        set
        {
            if (_isLeftMenuVisible == value) return;
            _isLeftMenuVisible = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsRightMenuVisible
    {
        get => _isRightMenuVisible;
        set
        {
            if (_isRightMenuVisible == value) return;
            _isRightMenuVisible = value;
            ViewModel!.OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public bool IsLeftPanelLocked
    {
        get => _isLeftPanelLocked;
        set
        {
            if (_isLeftPanelLocked == value) return;
            _isLeftPanelLocked = value;
            ViewModel!.OnPropertyChanged();
            
            // Updating the visibility status of the lock buttons
            ViewModel!.OnPropertyChanged(nameof(LeftClosedLockIsHidden));
            ViewModel!.OnPropertyChanged(nameof(LeftOpenedLockIsHidden));
        }
    }
    
    [DataSourceProperty]
    public bool IsRightPanelLocked
    {
        get => _isRightPanelLocked;
        set
        {
            if (_isRightPanelLocked == value) return;
            _isRightPanelLocked = value;
            ViewModel!.OnPropertyChanged();
            
            // Updating the visibility status of the lock buttons
            ViewModel!.OnPropertyChanged(nameof(RightClosedLockIsHidden));
            ViewModel!.OnPropertyChanged(nameof(RightOpenedLockIsHidden));
        }
    }
}