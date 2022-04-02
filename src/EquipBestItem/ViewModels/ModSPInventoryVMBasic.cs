using System;
using System.Collections.Generic;
using System.Linq;
using EquipBestItem.Models;
using EquipBestItem.UIExtenderEx;
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
    
    private Equipment BestLeftEquipment { get; } = new Equipment();

    private Equipment BestRightEquipment { get; } = new Equipment();

    public CharacterObject? CurrentCharacter { get; set; }

    public ModSPInventoryVM(SPInventoryVM originVM, SPInventoryVMMixin mixinVM)
    {
        _originVM = originVM;
        _mixinVM = mixinVM;
        _inventoryLogic = InventoryManager.InventoryLogic;
        _settingsRepository = new Repository<Settings>(SettingsLoadOrDefault());
        _charactersCoefficientsRepository = new Repository<CharacterCoefficients>(CharacterCoefficientsLoadOrDefault());
    }
    
    /// <summary>
    /// Deserialize settings or create new with default values
    /// </summary>
    /// <returns>Dictionary with settings by settings name</returns>
    private Dictionary<string, Settings> SettingsLoadOrDefault()
    {
        return Helper.Deserialize<Settings>() ?? new Dictionary<string, Settings>()
        {
            { "IsLeftPanelLocked", new Settings() { Key = "IsLeftPanelLocked", Value = true }},
            { "IsRightPanelLocked", new Settings() { Key = "IsRightPanelLocked", Value = false }}
        };
    }
    
    /// <summary>
    /// Deserialize coefficients or create new with default values
    /// </summary>
    /// <returns>Dictionary with coefficients</returns>
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
        EquipBestItem(equipmentIndex);
    }

    private void EquipBestItem(EquipmentIndex equipmentIndex)
    {
        if (_originVM.GetField("ActiveEquipment") is not Equipment equipment || equipment[equipmentIndex].IsEmpty) return;
        
        //Unequip current equipment element
        var transferCommand = TransferCommand.Transfer(
            1,
            InventoryLogic.InventorySide.Equipment,
            InventoryLogic.InventorySide.PlayerInventory,
            new ItemRosterElement(equipment[equipmentIndex], 1),
            equipmentIndex,
            EquipmentIndex.None,
            CurrentCharacter,
            !_originVM.IsInWarSet
        );
        _inventoryLogic.AddTransferCommand(transferCommand);
        
        //Equip
        if (ItemIndexCalculation(BestLeftEquipment[equipmentIndex], equipmentIndex, CurrentCharacter) >
            ItemIndexCalculation(BestRightEquipment[equipmentIndex], equipmentIndex, CurrentCharacter))
        {
            var equipCommand = TransferCommand.Transfer(
                1,
                InventoryLogic.InventorySide.OtherInventory,
                InventoryLogic.InventorySide.Equipment,
                new ItemRosterElement(BestLeftEquipment[equipmentIndex], 1),
                EquipmentIndex.None,
                equipmentIndex,
                CurrentCharacter,
                !_originVM.IsInWarSet
            );

            _inventoryLogic.AddTransferCommand(equipCommand);
        }
        else
        {
            var equipCommand = TransferCommand.Transfer(
                1,
                InventoryLogic.InventorySide.PlayerInventory,
                InventoryLogic.InventorySide.Equipment,
                new ItemRosterElement(BestRightEquipment[equipmentIndex], 1),
                EquipmentIndex.None,
                equipmentIndex,
                CurrentCharacter,
                !_originVM.IsInWarSet
            );

            _inventoryLogic.AddTransferCommand(equipCommand);
        }

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
        InformationManager.DisplayMessage(new InformationMessage($"Updated {Time.ApplicationTime}"));
    }

    private float ItemIndexCalculation(EquipmentElement item, EquipmentIndex index, BasicCharacterObject? character)
    {
        return 0f;
    }

    public override void OnFinalize()
    {
        _charactersCoefficientsRepository.Save();
        _settingsRepository.Save();
        base.OnFinalize();
    }
    
    public CharacterObject? GetCharacterByName(string name)
    {
        return (
                from rosterElement in _inventoryLogic.RightMemberRoster.GetTroopRoster() 
                where rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name 
                select rosterElement.Character).FirstOrDefault();
    }
}