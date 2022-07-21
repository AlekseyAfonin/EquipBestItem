using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EquipBestItem.Extensions;
using EquipBestItem.Models.Entities;
using Helpers;
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
    }

    internal SPItemVM? GetBestItem(CharacterObject character, EquipmentIndex index, Coefficients[] coefficients,
        params MBBindingList<SPItemVM>?[] itemsLists)
    {
        // Stopwatch stopwatch = new Stopwatch();
        //
        // stopwatch.Start();
        //////////////
        SPItemVM? bestItem = null;
        
        try
        {
            var equipment = _originVM.IsInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;
            var bestItemValue = equipment[index].IsEmpty ? 0 : equipment[index].GetItemValue(coefficients[(int) index]);

            var validItems = itemsLists
                .Where(items => items is not null)
                .SelectMany(items => items)
                .Where(IsValidItem);

            foreach (var item in validItems)
            {
                var itemValue = item.ItemRosterElement.EquipmentElement.GetItemValue(coefficients[(int) index]);

                if (bestItemValue >= itemValue) continue;

                bestItem = item;
                bestItemValue = itemValue;
            }

            bool IsValidItem(SPItemVM item)
            {
                if (!_originVM.IsInWarSet && !item.IsCivilianItem) return false;
                if (!item.IsEquipableItem) return false;
                if (item.IsLocked) return false;
                if (item.ItemCount == 0) return false;
                if (!CharacterHelper.CanUseItemBasedOnSkill(character, item.ItemRosterElement.EquipmentElement))
                    return false;
                if (equipment[EquipmentIndex.Horse].IsEmpty && item.ItemType == EquipmentIndex.HorseHarness)
                    return false;
                if (item.ItemType != EquipmentIndex.Weapon0 || index is > EquipmentIndex.Weapon4)
                    return item.ItemType == index;
                
                var itemPrimaryWeapon = item.ItemRosterElement.EquipmentElement.Item?.PrimaryWeapon;
                var currentPrimaryWeapon = equipment[index].Item?.PrimaryWeapon;
                
                if (itemPrimaryWeapon?.WeaponClass != currentPrimaryWeapon?.WeaponClass) return false;
                
                return item.ItemType <= index;
            }

            
        }
        catch (Exception e)
        {
            Helper.ShowMessage($"{e.Message}", Colors.Red);
        }
        
        /////////////
        // stopwatch.Stop();
        // Helper.ShowMessage($"GetBestItem {stopwatch.ElapsedMilliseconds}ms");
        
        return bestItem;
    }
    

    private void UnequipItem(EquipmentIndex equipmentIndex, CharacterObject character)
    {
        // Stopwatch stopwatch = new Stopwatch();
        //
        // stopwatch.Start();
        //////////////
        var equipment = _originVM.IsInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;
        
        if (equipment[equipmentIndex].IsEmpty) return;
        
        var unequipCommand = TransferCommand.Transfer(
            1,
            InventoryLogic.InventorySide.Equipment,
            InventoryLogic.InventorySide.PlayerInventory,
            new ItemRosterElement(equipment[equipmentIndex], 1),
            equipmentIndex,
            EquipmentIndex.None,
            character,
            !_originVM.IsInWarSet);
        
        _inventoryLogic.AddTransferCommand(unequipCommand);
        /////////////
        // stopwatch.Stop();
        // Helper.ShowMessage($"UnequipItem {stopwatch.ElapsedMilliseconds}ms");
    }

    private void EquipItem(EquipmentIndex equipmentIndex, CharacterObject? character, SPItemVM? item)
    {
        // Stopwatch stopwatch = new Stopwatch();
        //
        // stopwatch.Start();
        //////////////
        if (item is null) return;
        
        var equipCommand = TransferCommand.Transfer(
            1,
            item.InventorySide,
            InventoryLogic.InventorySide.Equipment,
            new ItemRosterElement(item.ItemRosterElement.EquipmentElement, 1),
            EquipmentIndex.None,
            equipmentIndex,
            character,
            !_originVM.IsInWarSet);
        
        _inventoryLogic.AddTransferCommand(equipCommand);
        /////////////
        // stopwatch.Stop();
        // Helper.ShowMessage($"EquipItem {stopwatch.ElapsedMilliseconds}ms");
    }
}