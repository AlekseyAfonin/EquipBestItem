using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Library;
using EquipBestItem.ViewModels;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.Core;

namespace EquipBestItem.UIExtenderEx;


/// <summary>
/// Add properties to the target SPInventoryVM class by creating a SPInventoryVMMixin class
/// that inherits from BaseViewModelMixin<T> and marking it with the ViewModelMixin attribute.
/// This class will be swept into the target view model T, making the fields and methods available in the assembly.
/// https://butr.github.io/Bannerlord.UIExtenderEx/articles/v2/ViewModelMixin.html
/// </summary>
[ViewModelMixin("RefreshValues")]
public sealed class SPInventoryVMMixin : BaseViewModelMixin<SPInventoryVM>
{
    private CharacterObject? _currentCharacter;
    

    public SPInventoryVMMixin(SPInventoryVM vm) : base(vm)
    {
        ModSPInventory = new ModSPInventoryVM(vm, this);
        RegisterEvents();
        _currentCharacter = InventoryManager.InventoryLogic.InitialEquipmentCharacter;
        IsLeftPanelLocked = ModSPInventory.IsLeftPanelLocked;
        IsRightPanelLocked = ModSPInventory.IsRightPanelLocked;
    }
    
    [DataSourceProperty] 
    private ModSPInventoryVM ModSPInventory { get; }

    private void RegisterEvents()
    {
        if (ViewModel == null) return;
        
        ViewModel.PropertyChanged += SPInventoryVM_PropertyChanged;
        ViewModel.PropertyChangedWithValue += SPInventoryVM_PropertyChangedWithValue;
        
        Game.Current.EventManager.RegisterEvent(
            new Action<InventoryEquipmentTypeChangedEvent>(OnInventoryEquipmentTypeChanged));
    }
    
    /// <summary>
    /// Event when changing the type of kit (military, civilian) of the current character
    /// </summary>
    /// <param name="obj"></param>
    private void OnInventoryEquipmentTypeChanged(InventoryEquipmentTypeChangedEvent obj)
    {
        Task.Run(async () => await ModSPInventory.UpdateBestItemsAsync());
    }

    /// <summary>
    /// SPInventoryVM the event of property value change
    /// </summary>
    /// <param name="sender">SPInventoryVM</param>
    /// <param name="e">Property name</param>
    private void SPInventoryVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ModSPInventory.OnPropertyChanged(e.PropertyName);
    }
        
    /// <summary>
    /// SPInventoryVM the event with value of property value change 
    /// </summary>
    /// <param name="sender">SPInventoryVM</param>
    /// <param name="e">Property name, value</param>
    private void SPInventoryVM_PropertyChangedWithValue(object sender, PropertyChangedWithValueEventArgs e)
    {
        ModSPInventory.OnPropertyChangedWithValue(e.Value, e.PropertyName);

        switch (e.PropertyName)
        {
            case "IsRefreshed" when (bool) e.Value:
                Task.Run(async () => await ModSPInventory.UpdateBestItemsAsync());
                break;
            case "CurrentCharacterName":
                ModSPInventory.UpdateCurrentCharacter(GetCharacterByName(ViewModel?.CurrentCharacterName));
                Task.Run(async () => await ModSPInventory.UpdateBestItemsAsync());
                break;
        }
        
        CharacterObject GetCharacterByName(string? name)
        {
            var result = from rosterElement in InventoryManager.InventoryLogic.RightMemberRoster.GetTroopRoster() 
                where rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name
                select rosterElement.Character;
        
            return result.FirstOrDefault() ?? throw new InvalidOperationException("Character is null");
        }
    }
    
    public override void OnFinalize()
    {
        if (ViewModel is not null)
        {
            ViewModel.PropertyChanged -= SPInventoryVM_PropertyChanged;
            ViewModel.PropertyChangedWithValue -= SPInventoryVM_PropertyChangedWithValue;
        }
        Game.Current.EventManager.UnregisterEvent(new Action<InventoryEquipmentTypeChangedEvent>(OnInventoryEquipmentTypeChanged));
        ModSPInventory.OnFinalize();
        base.OnFinalize();
    }
    
    public override void OnRefresh()
    {
        base.OnRefresh();
        Task.Run(async () => await ModSPInventory.UpdateBestItemsAsync());
    }

    private bool _isHeadButtonDisabled;

    [DataSourceProperty]
    public bool IsHeadButtonDisabled
    {
        get => _isHeadButtonDisabled;
        set
        {
            if (_isHeadButtonDisabled == value) return;
            
            _isHeadButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isCapeButtonDisabled;

    [DataSourceProperty]
    public bool IsCapeButtonDisabled
    {
        get => _isCapeButtonDisabled;
        set
        {
            if (_isCapeButtonDisabled == value) return;
            
            _isCapeButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isBodyButtonDisabled;

    [DataSourceProperty]
    public bool IsBodyButtonDisabled
    {
        get => _isBodyButtonDisabled;
        set
        {
            if (_isBodyButtonDisabled == value) return;
            
            _isBodyButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isLegButtonDisabled;

    [DataSourceProperty]
    public bool IsLegButtonDisabled
    {
        get => _isLegButtonDisabled;
        set
        {
            if (_isLegButtonDisabled == value) return;
            
            _isLegButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isGlovesButtonDisabled;

    [DataSourceProperty]
    public bool IsGlovesButtonDisabled
    {
        get => _isGlovesButtonDisabled;
        set
        {
            if (_isGlovesButtonDisabled == value) return;
            
            _isGlovesButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isHorseButtonDisabled;

    [DataSourceProperty]
    public bool IsHorseButtonDisabled
    {
        get => _isHorseButtonDisabled;
        set
        {
            if (_isHorseButtonDisabled == value) return;
            
            _isHorseButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isHorseHarnessButtonDisabled;

    [DataSourceProperty]
    public bool IsHorseHarnessButtonDisabled
    {
        get => _isHorseHarnessButtonDisabled;
        set
        {
            if (_isHorseHarnessButtonDisabled == value) return;
            
            _isHorseHarnessButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isWeapon0ButtonDisabled;

    [DataSourceProperty]
    public bool IsWeapon0ButtonDisabled
    {
        get => _isWeapon0ButtonDisabled;
        set
        {
            if (_isWeapon0ButtonDisabled == value) return;
            
            _isWeapon0ButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isWeapon1ButtonDisabled;

    [DataSourceProperty]
    public bool IsWeapon1ButtonDisabled
    {
        get => _isWeapon1ButtonDisabled;
        set
        {
            if (_isWeapon1ButtonDisabled == value) return;
            
            _isWeapon1ButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isWeapon2ButtonDisabled;

    [DataSourceProperty]
    public bool IsWeapon2ButtonDisabled
    {
        get => _isWeapon2ButtonDisabled;
        set
        {
            if (_isWeapon2ButtonDisabled == value) return;
            
            _isWeapon2ButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isWeapon3ButtonDisabled;

    [DataSourceProperty]
    public bool IsWeapon3ButtonDisabled
    {
        get => _isWeapon3ButtonDisabled;
        set
        {
            if (_isWeapon3ButtonDisabled == value) return;
            
            _isWeapon3ButtonDisabled = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isLeftMenuVisible;
    
    [DataSourceProperty]
    public bool IsLeftMenuVisible
    {
        get => _isLeftMenuVisible;
        set
        {
            if (_isLeftMenuVisible == value) return;
            
            _isLeftMenuVisible = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }
    
    private bool _isRightMenuVisible;
    
    [DataSourceProperty]
    public bool IsRightMenuVisible
    {
        get => _isRightMenuVisible;
        set
        {
            if (_isRightMenuVisible == value) return;
            
            _isRightMenuVisible = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
        }
    }

    private bool _isLeftPanelLocked = true;

    [DataSourceProperty]
    public bool IsLeftPanelLocked
    {
        get => _isLeftPanelLocked;
        set
        {
            if (_isLeftPanelLocked == value) return;
            
            _isLeftPanelLocked = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
            ViewModel!.OnPropertyChanged("LeftClosedLockIsHidden");
            ViewModel!.OnPropertyChanged("LeftOpenedLockIsHidden");
        }
    }
    [DataSourceProperty]
    public bool LeftClosedLockIsHidden => IsLeftPanelLocked;

    [DataSourceProperty]
    public bool LeftOpenedLockIsHidden => !IsLeftPanelLocked;

    private bool _isRightPanelLocked = true;

    [DataSourceProperty]
    public bool IsRightPanelLocked
    {
        get => _isRightPanelLocked;
        set
        {
            if (_isRightPanelLocked == value) return;
            
            _isRightPanelLocked = value;
            ViewModel!.OnPropertyChanged();
            ModSPInventory.OnPropertyChanged();
            ViewModel!.OnPropertyChanged("RightClosedLockIsHidden");
            ViewModel!.OnPropertyChanged("RightOpenedLockIsHidden");
        }
    }
    [DataSourceProperty]
    public bool RightClosedLockIsHidden => IsRightPanelLocked;

    [DataSourceProperty]
    public bool RightOpenedLockIsHidden => !IsRightPanelLocked;
    
    [DataSourceProperty] public SPItemVM? HeadBestItem => ModSPInventory.BestItems[(int) EquipmentIndex.Head];
    [DataSourceProperty] public SPItemVM? BodyBestItem => ModSPInventory.BestItems[(int) EquipmentIndex.Body];
    [DataSourceProperty] public SPItemVM? CapeBestItem => ModSPInventory.BestItems[(int) EquipmentIndex.Cape];
    [DataSourceProperty] public SPItemVM? GlovesBestItem => ModSPInventory.BestItems[(int) EquipmentIndex.Gloves];
    [DataSourceProperty] public SPItemVM? HorseBestItem => ModSPInventory.BestItems[(int) EquipmentIndex.Horse];
    [DataSourceProperty] public SPItemVM? LegBestItem => ModSPInventory.BestItems[(int) EquipmentIndex.Leg];
    [DataSourceProperty] public SPItemVM? Weapon0BestItem => ModSPInventory.BestItems[(int) EquipmentIndex.Weapon0];
    [DataSourceProperty] public SPItemVM? Weapon1BestItem => ModSPInventory.BestItems[(int) EquipmentIndex.Weapon1];
    [DataSourceProperty] public SPItemVM? Weapon2BestItem => ModSPInventory.BestItems[(int) EquipmentIndex.Weapon2];
    [DataSourceProperty] public SPItemVM? Weapon3BestItem => ModSPInventory.BestItems[(int) EquipmentIndex.Weapon3];
    [DataSourceProperty] public SPItemVM? HorseHarnessBestItem => ModSPInventory.BestItems[(int) EquipmentIndex.HorseHarness];
    

    [DataSourceMethod]
    public void ExecuteSwitchLeftMenu()
    {
        IsLeftMenuVisible = !IsLeftMenuVisible;
        Helper.ShowMessage($"ExecuteSwitchLeftMenu");
    }
    
    [DataSourceMethod]
    public void ExecuteSwitchRightMenu()
    {
        IsRightMenuVisible = !IsRightMenuVisible;
        Helper.ShowMessage($"ExecuteSwitchRightMenu");
    }
    
    [DataSourceMethod]
    public void ExecuteLeftPanelLock()
    {
        IsLeftPanelLocked = !IsLeftPanelLocked;
        ModSPInventory.SwitchLeftPanelLock();

        Helper.ShowMessage($"ExecuteLeftPanelLock");
    }
    
    [DataSourceMethod]
    public void ExecuteRightPanelLock()
    {
        IsRightPanelLocked = !IsRightPanelLocked;
        ModSPInventory.SwitchRightPanelLock();
        Helper.ShowMessage($"ExecuteRightPanelLock");
    }
    
    [DataSourceMethod]
    public void ExecuteEquipCurrentCharacter()
    {
        Helper.ShowMessage($"ExecuteEquipCurrentCharacter");
        ModSPInventory.EquipCurrentCharacter();
    }
    
    [DataSourceMethod]
    public void ExecuteEquipAllCharacters()
    {
        Helper.ShowMessage($"ExecuteEquipAllCharacters");
        ModSPInventory.EquipAllCharacters();
    }
    

    [DataSourceMethod]
    public void ExecuteEquipBestItem(string equipmentIndexName)
    {
        ModSPInventory.ExecuteEquipBestItem(equipmentIndexName);
        InformationManager.DisplayMessage(new InformationMessage($"ExecuteEquipBestItem: {equipmentIndexName}"));
    }
    
    [DataSourceMethod]
    public void ExecuteShowFilterSettings(string equipmentIndexName)
    {
        ModSPInventory.ExecuteShowFilterSettings(equipmentIndexName);
    }
    
    [DataSourceMethod]
    public void ExecuteResetTranstactions()
    {
        ViewModel?.ExecuteResetTranstactions();
    }
    
    // [DataSourceMethod]
    // private void EquipEquipment(SPItemVM itemVM)
    // {
    //     InformationManager.DisplayMessage(new InformationMessage($"EquipEquipment test {Time.ApplicationTime}"));
    // }
}