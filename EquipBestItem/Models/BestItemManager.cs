using EquipBestItem.Extensions;
using EquipBestItem.Models.Entities;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.Models;

internal class BestItemManager
{
    private readonly SPInventoryVM _originVM;
    private readonly InventoryLogic _inventoryLogic;

    internal BestItemManager(SPInventoryVM originVM)
    {
        _originVM = originVM;
        _inventoryLogic = InventoryManager.InventoryLogic;
    }

    internal void EquipBestItem(EquipmentIndex equipmentIndex, CharacterObject character, ref SPItemVM? item)
    {
        if (item is null) return;
        
        UnequipItem(equipmentIndex, character);
        EquipItem(equipmentIndex, character, item);
        item = null;
        _originVM.ExecuteRemoveZeroCounts();
        _originVM.RefreshValues();
        _originVM.GetMethod("UpdateCharacterEquipment");
    }

    private void UnequipItem(EquipmentIndex equipmentIndex, CharacterObject character)
    {
        var equipment = _originVM.IsInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;
        
        if (equipment[equipmentIndex].IsEmpty) return;
        
        var transferCommand = TransferCommand.Transfer(
            1,
            InventoryLogic.InventorySide.Equipment,
            InventoryLogic.InventorySide.PlayerInventory,
            new ItemRosterElement(equipment[equipmentIndex], 1),
            equipmentIndex,
            EquipmentIndex.None,
            character,
            !_originVM.IsInWarSet);
        
        _inventoryLogic.AddTransferCommand(transferCommand);
    }

    private void EquipItem(EquipmentIndex equipmentIndex, CharacterObject? character, SPItemVM? item)
    {
        var equipCommand = TransferCommand.Transfer(
            1,
            item!.InventorySide,
            InventoryLogic.InventorySide.Equipment,
            item.ItemRosterElement,
            EquipmentIndex.None,
            equipmentIndex,
            character,
            !_originVM.IsInWarSet);
        
        _inventoryLogic.AddTransferCommand(equipCommand);
    }

    internal static SPItemVM? GetBestItem(Coefficients coefficients, EquipmentElement currentItem, EquipmentIndex equipmentIndex,
        params MBBindingList<SPItemVM>?[] lists)
    {
        var bestItemValue = currentItem.IsEmpty ? 0 : currentItem.GetItemValue(coefficients);

        SPItemVM? bestItem = null;

        foreach (var list in lists)
        {
            if (list is null) continue;
            
            foreach (var item in list)
            {
                if (!item.IsEquipableItem || item.IsLocked || !item.CanCharacterUseItem || item.ItemType != equipmentIndex) continue;
            
                var itemValue = item.ItemRosterElement.EquipmentElement.GetItemValue(coefficients);

                if (bestItemValue >= itemValue) continue;

                bestItem = item;
                bestItemValue = itemValue;
            }
        }
        
        return bestItem;
    }
}