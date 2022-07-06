using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Library;
using EquipBestItem.ViewModels;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.SaveSystem.Definition;

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
    }
    
    [DataSourceProperty] 
    private ModSPInventoryVM ModSPInventory { get; }
    
    //public CharacterObject CurrentCharacter => GetPrivate<CharacterObject>("_currentCharacter") ?? throw new InvalidOperationException();

    private void RegisterEvents()
    {
        if (ViewModel == null) return;
        
        ViewModel.PropertyChanged += SPInventoryVM_PropertyChanged;
        ViewModel.PropertyChangedWithValue += SPInventoryVM_PropertyChangedWithValue;
        
        Game.Current.EventManager.RegisterEvent(
            new Action<InventoryEquipmentTypeChangedEvent>(OnInventoryEquipmentTypeChanged));
        //ViewModel.CharacterList.(OnCurrentCharacterChanged);
    }

    private void OnCurrentCharacterChanged()
    {
        ModSPInventory.UpdateCurrentCharacter(GetCharacterByName(ViewModel?.CurrentCharacterName));
        ModSPInventory.Update();
        
        CharacterObject GetCharacterByName(string? name)
        {
            var result = from rosterElement in InventoryManager.InventoryLogic.RightMemberRoster.GetTroopRoster() 
                where rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name
                select rosterElement.Character;
        
            return result.FirstOrDefault() ?? throw new InvalidOperationException("Character can't be null");
        }
    }
    
    /// <summary>
    /// Event when changing the type of kit (military, civilian) of the current character
    /// </summary>
    /// <param name="obj"></param>
    private void OnInventoryEquipmentTypeChanged(InventoryEquipmentTypeChangedEvent obj)
    {
        ModSPInventory.Update();
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
        
        // I couldn't find events that would trigger when the item collections for the left and right panels changed,
        // so I'm looking at changing "IsRefreshed", which works when the item filters change as well.
        if (e.PropertyName == "IsRefreshed" && (bool) e.Value)
        {
            ModSPInventory.Update();
        }

        if (e.PropertyName == "CurrentCharacterName")
        {
            OnCurrentCharacterChanged();
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
        ModSPInventory.Update();
    }

    private bool _isHeadButtonEnabled;

    [DataSourceProperty]
    public bool IsHeadButtonEnabled
    {
        get => _isHeadButtonEnabled;
        set
        {
            if (_isHeadButtonEnabled != value)
            {
                _isHeadButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
    }

    private bool _isCapeButtonEnabled;

    [DataSourceProperty]
    public bool IsCapeButtonEnabled
    {
        get => _isCapeButtonEnabled;
        set
        {
            if (_isCapeButtonEnabled != value)
            {
                _isCapeButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
    }

    private bool _isBodyButtonEnabled;

    [DataSourceProperty]
    public bool IsBodyButtonEnabled
    {
        get => _isBodyButtonEnabled;
        set
        {
            if (_isBodyButtonEnabled != value)
            {
                _isBodyButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
    }

    private bool _isLegButtonEnabled;

    [DataSourceProperty]
    public bool IsLegButtonEnabled
    {
        get => _isLegButtonEnabled;
        set
        {
            if (_isLegButtonEnabled != value)
            {
                _isLegButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
    }

    private bool _isGlovesButtonEnabled;

    [DataSourceProperty]
    public bool IsGlovesButtonEnabled
    {
        get => _isGlovesButtonEnabled;
        set
        {
            if (_isGlovesButtonEnabled != value)
            {
                _isGlovesButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
    }

    private bool _isHorseButtonEnabled;

    [DataSourceProperty]
    public bool IsHorseButtonEnabled
    {
        get => _isHorseButtonEnabled;
        set
        {
            if (_isHorseButtonEnabled != value)
            {
                _isHorseButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
    }

    private bool _isHorseHarnessButtonEnabled;

    [DataSourceProperty]
    public bool IsHorseHarnessButtonEnabled
    {
        get => _isHorseHarnessButtonEnabled;
        set
        {
            if (_isHorseHarnessButtonEnabled != value)
            {
                _isHorseHarnessButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
    }

    private bool _isWeapon0ButtonEnabled;

    [DataSourceProperty]
    public bool IsWeapon0ButtonEnabled
    {
        get => _isWeapon0ButtonEnabled;
        set
        {
            if (_isWeapon0ButtonEnabled != value)
            {
                _isWeapon0ButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
    }

    private bool _isWeapon1ButtonEnabled;

    [DataSourceProperty]
    public bool IsWeapon1ButtonEnabled
    {
        get => _isWeapon1ButtonEnabled;
        set
        {
            if (_isWeapon1ButtonEnabled != value)
            {
                _isWeapon1ButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
    }

    private bool _isWeapon2ButtonEnabled;

    [DataSourceProperty]
    public bool IsWeapon2ButtonEnabled
    {
        get => _isWeapon2ButtonEnabled;
        set
        {
            if (_isWeapon2ButtonEnabled != value)
            {
                _isWeapon2ButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
    }

    private bool _isWeapon3ButtonEnabled;

    [DataSourceProperty]
    public bool IsWeapon3ButtonEnabled
    {
        get => _isWeapon3ButtonEnabled;
        set
        {
            if (_isWeapon3ButtonEnabled != value)
            {
                _isWeapon3ButtonEnabled = value;
                ViewModel!.OnPropertyChanged();
                ModSPInventory.OnPropertyChanged();
            }
        }
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