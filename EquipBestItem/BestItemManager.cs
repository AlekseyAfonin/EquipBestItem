using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipBestItem.Models.BestItemCalculator;
using EquipBestItem.Models.Entities;
using Helpers;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem;

public sealed class BestItemManager
{
    private readonly List<IBestItemCalculator> _calculators;
    private int _selectedIndex;

    private BestItemManager()
    {
        _calculators = new List<IBestItemCalculator>();
    }
    
    private static readonly BestItemManager Instance = new ();
    
    private IBestItemCalculator Calculator => _calculators[_selectedIndex];
    
    public static BestItemManager GetInstance()
    {
        return Instance;
    }

    public void AddCalculator(IBestItemCalculator calculator)
    {
        _calculators.Add(calculator);
    }

    public IEnumerable<string> GetCalculatorsNames()
    {
        return _calculators.Select(calculator => calculator.Name);
    }

    public void SelectCalculator(int selectedIndex)
    {
        _selectedIndex = selectedIndex;
    }

    public void EquipBestItem(CalculatorContext context, SPItemVM? item)
    {
        if (item is null) return;
        
        UnequipItem(context);
        EquipItem(context, item);
    }

    public SPItemVM? GetBestItem(CalculatorContext context, params MBBindingList<SPItemVM>?[] itemsLists)
    {
        SPItemVM? bestItem = null;

        var index = context.EquipmentIndex;
        var character = context.Character;
        var equipment = context.IsInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;

        try
        {
            var bestItemValue = equipment[index].IsEmpty || Calculator.IsSlotItemNotValid(equipment[index], context)
                ? 0f : Calculator.GetItemValue(equipment[index], context);

            var validItems = itemsLists
                .Where(items => items is not null)
                .SelectMany(items => items)
                .Where(IsValidItem);

            Parallel.ForEach(validItems, item =>
            {
                var itemValue = Calculator.GetItemValue(item.ItemRosterElement.EquipmentElement, context);

                if (bestItemValue >= itemValue) return;

                bestItem = item;
                bestItemValue = itemValue;
            });

            bool IsValidItem(SPItemVM item)
            {
                if (!context.IsInWarSet && !item.IsCivilianItem) return false;
                
                if (!item.IsEquipableItem) return false;
                
                if (item.IsLocked) return false;
                
                if (item.ItemCount == 0) return false;
                
                if (!CharacterHelper.CanUseItemBasedOnSkill(character, item.ItemRosterElement.EquipmentElement))
                    return false;

                if (IsHorseHarnessNotValid()) return false;
                   
                if (IsItemNotWeapon()) return item.ItemType == index;

                if (_calculators[_selectedIndex].IsItemNotValid(item, context)) return false;

                return item.ItemType <= index;

                bool IsHorseHarnessNotValid()
                {
                    if (item.ItemType != EquipmentIndex.HorseHarness) return false;
                    
                    // Disable the saddles search if there is no mount in the slot
                    if (equipment[EquipmentIndex.Horse].IsEmpty && item.ItemType == EquipmentIndex.HorseHarness)
                        return true;
                    
                    // Exclude from the search saddles that are not suitable for mount
                    if (equipment[EquipmentIndex.Horse].Item?.HorseComponent?.Monster.FamilyType !=
                        item.ItemRosterElement.EquipmentElement.Item?.ArmorComponent?.FamilyType) return true;

                    return false;
                }
                
                // Separating the search for weapons from other items
                bool IsItemNotWeapon()
                {
                    return item.ItemType != EquipmentIndex.Weapon0 || index > EquipmentIndex.Weapon4;
                }
            }
        }
        catch (Exception e)
        {
            Helper.ShowMessage($"{e.Message}", Colors.Red);
        }

        return bestItem;
    }

    private static void UnequipItem(CalculatorContext context)
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

    private static void EquipItem(CalculatorContext context, SPItemVM? item)
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