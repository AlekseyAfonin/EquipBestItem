using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;

using HarmonyLib;

using SandBox.ViewModelCollection.Nameplate;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using JetBrains.Annotations;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;
using TaleWorlds.ObjectSystem;
using EquipBestItem.ViewModels;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;
using TaleWorlds.InputSystem;

namespace EquipBestItem.UIExtenderEx;


/// <summary>
/// Add properties to the target SPInventoryVM class by creating a SPInventoryVMMixin class
/// that inherits from BaseViewModelMixin<T> and marking it with the ViewModelMixin attribute.
/// This class will be swept into the target view model T, making the fields and methods available in the assembly.
/// https://butr.github.io/Bannerlord.UIExtenderEx/articles/v2/ViewModelMixin.html
/// </summary>
[ViewModelMixin(nameof(SPInventoryVM.RefreshValues))]
internal sealed class SPInventoryVMMixin : BaseViewModelMixin<SPInventoryVM>
{
    private readonly ModSPInventoryVM _modSPInventory;
        
    [DataSourceProperty]
    public ModSPInventoryVM ModSPInventory
    {
        get
        {
            return _modSPInventory;
        }
    }
    
    public SPInventoryVMMixin(SPInventoryVM vm) : base(vm)
    {
        _modSPInventory = new ModSPInventoryVM(vm);
        vm.PropertyChanged += SPInventoryVM_PropertyChanged;
    }

    private void SPInventoryVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        _modSPInventory.OnPropertyChanged(e.PropertyName);
    }
    
    public override void OnFinalize()
    {
        if (ViewModel is not null)
            ViewModel.PropertyChanged -= SPInventoryVM_PropertyChanged;
        
        base.OnFinalize();
    }
    
    [DataSourceMethod]
    public void ExecuteEquipBestHelm()
    {
        InformationManager.DisplayMessage(new InformationMessage($"ExecuteEquipBestHelm"));
    }
    
    [DataSourceMethod]
    public void ExecuteEquipBestCloak()
    {
        InformationManager.DisplayMessage(new InformationMessage($"ExecuteEquipBestCloak"));
    }
    
    [DataSourceMethod]
    public void ExecuteShowFilterSettings(string equipmentIndexName)
    {
        ModSPInventory.ExecuteShowFilterSettings(equipmentIndexName);
    }    
}