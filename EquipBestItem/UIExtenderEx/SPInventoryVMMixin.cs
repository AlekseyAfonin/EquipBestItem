using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using EquipBestItem.Models;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.UIExtenderEx;

/// <summary>
///         https://butr.github.io/Bannerlord.UIExtenderEx/articles/v2/ViewModelMixin.html
/// </summary>
[ViewModelMixin("RefreshValues")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed partial class SPInventoryVMMixin : BaseViewModelMixin<SPInventoryVM>
{
    private readonly SPInventoryMixin _model;

    public SPInventoryVMMixin(SPInventoryVM vm) : base(vm)
    {
        _model = new SPInventoryMixin(vm, this);
        RegisterEvents();
        IsLeftPanelLocked = _model.IsLeftPanelLocked;
        IsRightPanelLocked = _model.IsRightPanelLocked;
        IsLeftMenuVisible = _model.IsLeftMenuVisible;
        IsRightMenuVisible = _model.IsRightMenuVisible;
    }

    public override void OnFinalize()
    {
        if (ViewModel is not null)
        {
            ViewModel.PropertyChangedWithValue -= SPInventoryVM_PropertyChangedWithValue;
        }

        Game.Current.EventManager.UnregisterEvent(
            new Action<InventoryEquipmentTypeChangedEvent>(OnInventoryEquipmentTypeChanged));
        
        base.OnFinalize();
    }

    public override void OnRefresh()
    {
        base.OnRefresh();
        Update();
    }
    
    /// <summary>
    ///     Register for events that will trigger a search
    /// </summary>
    private void RegisterEvents()
    {
        if (ViewModel == null) return;

        //ViewModel.PropertyChanged += SPInventoryVM_PropertyChanged;
        ViewModel.PropertyChangedWithValue += SPInventoryVM_PropertyChangedWithValue;

        Game.Current.EventManager.RegisterEvent(
            new Action<InventoryEquipmentTypeChangedEvent>(OnInventoryEquipmentTypeChanged));
    }

    /// <summary>
    ///     Event when changing the type of kit (military, civilian) of the current character
    /// </summary>
    /// <param name="obj"></param>
    private void OnInventoryEquipmentTypeChanged(InventoryEquipmentTypeChangedEvent obj)
    {
        Update();
    }

    private bool _updateLocked;
    
    private void Update()
    {
        if (_updateLocked) return;
        
        CoefficientsSettings.CloseClick();
        Task.Run(async () => await _model.UpdateBestItemsAsync());
    }

    /// <summary>
    ///     SPInventoryVM the event with value of property value change
    /// </summary>
    /// <param name="sender">SPInventoryVM</param>
    /// <param name="e">Property name, value</param>
    private void SPInventoryVM_PropertyChangedWithValue(object sender, PropertyChangedWithValueEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "IsRefreshed" when (bool) e.Value:
                Update();
                break;
            case "CurrentCharacterName":
                _model.UpdateCurrentCharacter(GetCharacterByName(ViewModel?.CurrentCharacterName));
                Update();
                break;
        }

        CharacterObject GetCharacterByName(string? name)
        {
            var character = from rosterElement in InventoryManager.InventoryLogic.RightMemberRoster.GetTroopRoster()
                where rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name
                select rosterElement.Character;

            return character.FirstOrDefault() ?? throw new InvalidOperationException("Character is null");
        }
    }
}