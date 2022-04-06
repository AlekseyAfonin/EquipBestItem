using System;
using System.Collections.Generic;
using System.Linq;
using EquipBestItem.Models;
using EquipBestItem.Models.Entities;
using EquipBestItem.UIExtenderEx;
using Messages.FromLobbyServer.ToClient;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels;

internal sealed partial class ModSPInventoryVM : ViewModel
{
    private readonly SPInventoryVM _originVM;
    private readonly SPInventoryVMMixin _mixinVM;
    private readonly InventoryLogic _inventoryLogic;

    private readonly Repository<Settings> _settingsRepository;
    private readonly Repository<CharacterCoefficients> _charactersCoefficientsRepository;

    public CharacterObject CurrentCharacter { get; set; }
    private readonly SPItemVM?[] _bestItems = new SPItemVM[12];
    public bool[] IsButtonEnabled = new bool[12]; //TODO
    
    public ModSPInventoryVM(SPInventoryVM originVM, SPInventoryVMMixin mixinVM)
    {
        _originVM = originVM;
        _mixinVM = mixinVM;
        _inventoryLogic = InventoryManager.InventoryLogic;
        CurrentCharacter = _inventoryLogic.InitialEquipmentCharacter;
        _settingsRepository = new Repository<Settings>(SettingsLoadOrDefault());
        _charactersCoefficientsRepository = new Repository<CharacterCoefficients>(CharacterCoefficientsLoadOrDefault());
    }
    
    private Dictionary<string, Settings> SettingsLoadOrDefault()
    {
        return Helper.Deserialize<Settings>() ?? new Dictionary<string, Settings>()
        {
            { "IsLeftPanelLocked", new Settings() { Key = "IsLeftPanelLocked", Value = true }},
            { "IsRightPanelLocked", new Settings() { Key = "IsRightPanelLocked", Value = false }}
        };
    }
    
    private Dictionary<string, CharacterCoefficients> CharacterCoefficientsLoadOrDefault()
    {
        return Helper.Deserialize<CharacterCoefficients>() ?? new Dictionary<string, CharacterCoefficients>()
        {
            { 
                "default", new CharacterCoefficients()
                {
                    CivilCoefficients = new Coefficients[(int) EquipmentIndex.NumEquipmentSetSlots - 1],
                    WarCoefficients = new Coefficients[(int) EquipmentIndex.NumEquipmentSetSlots - 1],
                    Key = "default"
                }
            }
        };
    }
    
    public void ExecuteShowFilterSettings(string equipmentIndexName)
    {
        var equipmentIndex = Helper.ParseEnum<EquipmentIndex>(equipmentIndexName);
        
        switch (equipmentIndex)
        {
            case EquipmentIndex.Head :
                InformationManager.DisplayMessage(new InformationMessage($"ShowSettings EquipmentIndex.Head "));
                break;
            case EquipmentIndex.Cape :
                InformationManager.DisplayMessage(new InformationMessage($"ShowSettings EquipmentIndex.Cape"));
                break;
            case EquipmentIndex.None:
                break;
            case EquipmentIndex.WeaponItemBeginSlot:
                break;
            case EquipmentIndex.Weapon1:
                break;
            case EquipmentIndex.Weapon2:
                break;
            case EquipmentIndex.Weapon3:
                break;
            case EquipmentIndex.Weapon4:
                break;
            case EquipmentIndex.Body:
                break;
            case EquipmentIndex.Leg:
                break;
            case EquipmentIndex.Gloves:
                break;
            case EquipmentIndex.ArmorItemEndSlot:
                break;
            case EquipmentIndex.HorseHarness:
                break;
            case EquipmentIndex.NumEquipmentSetSlots:
                break;
            default:
                InformationManager.DisplayMessage(new InformationMessage($"ShowSettings wrong EquipmentIndex"));
                break;
        }
    }
    
    public void ExecuteEquipBestItem(string equipmentIndexName)
    {
        var equipmentIndex = Helper.ParseEnum<EquipmentIndex>(equipmentIndexName);
        EquipBestItem(equipmentIndex, CurrentCharacter);
    }
    
    private void UnequipItem(EquipmentIndex equipmentIndex, CharacterObject character)
    {
        var equipment = _originVM.IsInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;
        
        if (equipment[equipmentIndex].IsEmpty) return;  //TODO
        
        var transferCommand = TransferCommand.Transfer(
            1,
            InventoryLogic.InventorySide.Equipment,
            InventoryLogic.InventorySide.PlayerInventory,
            new ItemRosterElement(equipment[equipmentIndex], 1),
            equipmentIndex,
            EquipmentIndex.None,
            character,
            !_originVM.IsInWarSet
        );
        _inventoryLogic.AddTransferCommand(transferCommand);
    }
    
    private void EquipItem(EquipmentIndex toEquipmentIndex, CharacterObject character)
    {
        var item = _bestItems[(int)toEquipmentIndex];
        
        if (item is null) return;

        var equipCommand = TransferCommand.Transfer(
            1,
            item.InventorySide,
            InventoryLogic.InventorySide.Equipment,
            item.ItemRosterElement, //TODO
            EquipmentIndex.None,
            toEquipmentIndex,
            character,
            !_originVM.IsInWarSet
        );

        _inventoryLogic.AddTransferCommand(equipCommand);
    }
    
    private void EquipBestItem(EquipmentIndex equipmentIndex, CharacterObject character)
    {
        UnequipItem(equipmentIndex, character);
        EquipItem(equipmentIndex, character);

        _originVM.ExecuteRemoveZeroCounts();
        _originVM.RefreshValues();
        _originVM.GetMethod("UpdateCharacterEquipment");
    }

    public override void RefreshValues()
    {
        base.RefreshValues();
        InformationManager.DisplayMessage(new InformationMessage($"ModSP Refresh test"));
    }

    public void Update()
    {
        for (var i = (int)EquipmentIndex.WeaponItemBeginSlot; i <= (int)EquipmentIndex.NumEquipmentSetSlots; i++)
        {
            
            var isBool = true; //TODO
            var equipment = _originVM.IsInWarSet
                ? CurrentCharacter.FirstBattleEquipment
                : CurrentCharacter.FirstCivilianEquipment;

            var coefficients = _originVM.IsInWarSet
                ? _charactersCoefficientsRepository.GetByKey(CurrentCharacter.Name.ToString())
                    .WarCoefficients[i]
                : _charactersCoefficientsRepository.GetByKey(CurrentCharacter.Name.ToString())
                    .CivilCoefficients[i];
            
            _bestItems[i] = BestItem.GetBestItem(coefficients, equipment[i],
                isBool ? _originVM.RightItemListVM : null, 
                isBool ? _originVM.LeftItemListVM : null);
            
            if (_bestItems[i] is not null) IsButtonEnabled[i] = true;
        }
    }

    public override void OnFinalize()
    {
        _charactersCoefficientsRepository.Save();
        _settingsRepository.Save();
        base.OnFinalize();
    }
}