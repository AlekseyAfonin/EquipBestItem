using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Library;
using EquipBestItem.ViewModels;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;

namespace EquipBestItem.UIExtenderEx;


/// <summary>
/// Add properties to the target SPInventoryVM class by creating a SPInventoryVMMixin class
/// that inherits from BaseViewModelMixin<T> and marking it with the ViewModelMixin attribute.
/// This class will be swept into the target view model T, making the fields and methods available in the assembly.
/// https://butr.github.io/Bannerlord.UIExtenderEx/articles/v2/ViewModelMixin.html
/// </summary>
[ViewModelMixin("RefreshValues")]
internal sealed class SPInventoryVMMixin : BaseViewModelMixin<SPInventoryVM>
{
    [DataSourceProperty] 
    private ModSPInventoryVM ModSPInventory { get; }


    private CharacterObject? _currentCharacter;
    public CharacterObject CurrentCharacter => GetPrivate<CharacterObject>("_currentCharacter") 
                                               ?? throw new InvalidOperationException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vm">The target viewmodel</param>
    public SPInventoryVMMixin(SPInventoryVM vm) : base(vm)
    {
        ModSPInventory = new ModSPInventoryVM(vm, this);
        RegisterEvents();
        _currentCharacter = GetPrivate<CharacterObject>("_currentCharacter");
    }

    private void RegisterEvents()
    {
        if (ViewModel == null) return;
        
        ViewModel.PropertyChanged += SPInventoryVM_PropertyChanged;
        ViewModel.PropertyChangedWithValue += SPInventoryVM_PropertyChangedWithValue;
        Game.Current.EventManager.RegisterEvent(
            new Action<InventoryEquipmentTypeChangedEvent>(OnInventoryEquipmentTypeChanged));
        ViewModel.CharacterList.SetOnChangeAction(OnCurrentCharacterChanged);
    }

    /// <summary>
    /// The current character changing
    /// </summary>
    /// <param name="obj"></param>
    private void OnCurrentCharacterChanged(SelectorVM<SelectorItemVM> obj)
    {
        ModSPInventory.CurrentCharacter = GetPrivate<CharacterObject>("_currentCharacter");
        ModSPInventory.Update();
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
    
    [DataSourceMethod]
    public void ExecuteEquipBestItem(string equipmentIndexName)
    {
        ModSPInventory.ExecuteEquipBestItem(equipmentIndexName);
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