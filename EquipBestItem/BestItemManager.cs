using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipBestItem.Models;
using EquipBestItem.Models.BestItemCalculator;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.MCMSettings;
using Helpers;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Diamond.HelloWorld;
using TaleWorlds.Library;

namespace EquipBestItem;

public class BestItemManager
{
    private readonly ISearcher _calculator;

    public BestItemManager(CharacterCoefficientsRepository repository)
    {
        _calculator = Helper.GetEnumerableOfType<SearcherBase>(repository).First(s => s.Name == MCMSettings.Instance?.SearchMethod.SelectedValue);
    }
    
    public void EquipBestItem(SearcherContext context, SPItemVM? item)
    {
        if (item is null) return;
        
        UnequipItem(context);
        EquipItem(context, item);
    }

    public SPItemVM? GetBestItem(SearcherContext context, params MBBindingList<SPItemVM>?[] itemsLists)
    {
        SPItemVM? bestItem = null;

        var index = context.EquipmentIndex;
        var character = context.Character;
        var equipment = context.IsInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;

        try
        {
            var bestItemValue = equipment[index].IsEmpty || _calculator.IsSlotItemNotValid(equipment[index], context)
                ? 0f : _calculator.GetItemValue(equipment[index], context);

            var validItems = itemsLists
                .Where(items => items is not null)
                .SelectMany(items => items)
                .Where(item => _calculator.IsValidItem(item, context));
            
            foreach (var item in validItems)
            {
                var itemValue = _calculator.GetItemValue(item.ItemRosterElement.EquipmentElement, context);
                
                if (bestItemValue >= itemValue) continue;
                
                bestItem = item;
                bestItemValue = itemValue;
            }
        }
        catch (Exception e)
        {
            Helper.ShowMessage($"{e.Message}", Colors.Red);
        }

        return bestItem;
    }

    private static void UnequipItem(SearcherContext context)
    {
        var equipmentIndex = context.EquipmentIndex;
        var character = context.Character;
        var isInWarSet = context.IsInWarSet;
        var inventoryLogic = InventoryManager.InventoryLogic;
        
        var equipment = isInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;

        if (equipment[equipmentIndex].IsEmpty) return;

        var unequipCommand = TransferCommand.Transfer(
            1,
            InventoryLogic.InventorySide.Equipment,
            InventoryLogic.InventorySide.PlayerInventory,
            new ItemRosterElement(equipment[equipmentIndex], 1),
            equipmentIndex,
            EquipmentIndex.None,
            character,
            !isInWarSet);

        inventoryLogic.AddTransferCommand(unequipCommand);
    }

    private static void EquipItem(SearcherContext context, SPItemVM? item)
    {
        if (item is null) return;

        var equipmentIndex = context.EquipmentIndex;
        var character = context.Character;
        var isInWarSet = context.IsInWarSet;
        var inventoryLogic = InventoryManager.InventoryLogic;
        
        var equipCommand = TransferCommand.Transfer(
            1,
            item.InventorySide,
            InventoryLogic.InventorySide.Equipment,
            new ItemRosterElement(item.ItemRosterElement.EquipmentElement, 1),
            EquipmentIndex.None,
            equipmentIndex,
            character,
            !isInWarSet);

        inventoryLogic.AddTransferCommand(equipCommand);
    }
}