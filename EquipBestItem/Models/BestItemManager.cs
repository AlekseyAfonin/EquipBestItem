using System;
using System.Linq;
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
    private readonly InventoryLogic _inventoryLogic;
    private readonly SPInventoryVM _originVM;

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
        SPItemVM? bestItem = null;

        try
        {
            var equipment = _originVM.IsInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;
            var bestItemValue = equipment[index].IsEmpty || IsWeaponClassNotUndefinedAndNotEqual(equipment[index])
                ? 0f
                : equipment[index].GetItemValue(coefficients[(int) index]);

            var validItems = itemsLists
                .Where(items => items is not null)
                .SelectMany(items => items)
                .Where(IsValidItem);

            Parallel.ForEach(validItems, item =>
            {
                var itemValue = item.ItemRosterElement.EquipmentElement.GetItemValue(coefficients[(int) index]);

                if (bestItemValue >= itemValue) return;

                bestItem = item;
                bestItemValue = itemValue;
            });

            bool IsValidItem(SPItemVM item)
            {
                if (!_originVM.IsInWarSet && !item.IsCivilianItem) return false;
                
                if (!item.IsEquipableItem) return false;
                
                if (item.IsLocked) return false;
                
                if (item.ItemCount == 0) return false;
                
                if (!CharacterHelper.CanUseItemBasedOnSkill(character, item.ItemRosterElement.EquipmentElement))
                    return false;

                if (IsHorseHarnessNotValid()) return false;
                   
                if (IsItemNotWeapon()) return item.ItemType == index;

                if (IsWeaponNotValid()) return false;

                if (IsShieldNotValid()) return false;
                
                return item.ItemType <= index;

                bool IsShieldNotValid()
                {
                    if (!IsShield(coefficients[(int) index].WeaponClass)) return false;

                    // Exclude shields from the search if the shield is already on
                    for (var i = EquipmentIndex.Weapon0; i <= EquipmentIndex.ExtraWeaponSlot; i++)
                        if (IsShield(equipment[i].Item?.PrimaryWeapon?.WeaponClass)) return true;
                    
                    return false;

                    bool IsShield(WeaponClass? weaponClass)
                    {
                        return weaponClass is WeaponClass.SmallShield or WeaponClass.LargeShield;
                    }
                }
                
                bool IsWeaponNotValid()
                {
                    var itemPrimaryWeapon = item.ItemRosterElement.EquipmentElement.Item?.PrimaryWeapon;
                    var currentPrimaryWeapon = equipment[index].Item?.PrimaryWeapon;

                    // If the selected weapon class is not defined, we look at the weapon class from the slot,
                    // otherwise by the selected
                    if (coefficients[(int) index].WeaponClass == WeaponClass.Undefined)
                    {
                        // If the classes do not match, we skip
                        if (itemPrimaryWeapon?.WeaponClass != currentPrimaryWeapon?.WeaponClass) return true;

                        // Additional filter for short and long bows
                        if (currentPrimaryWeapon?.WeaponClass == WeaponClass.Bow &&
                            itemPrimaryWeapon?.ItemUsage != currentPrimaryWeapon.ItemUsage) return true;

                        var currentWeaponComponents = item.ItemRosterElement.EquipmentElement.Item?.Weapons;
                        var itemWeaponComponents = equipment[index].Item?.Weapons;

                        // If they differ in the number of components skip
                        if (itemWeaponComponents?.Count != currentWeaponComponents?.Count)
                            return true;

                        // If they have the same number of components, then we enumerate each one and compare by class
                        for (var i = 0; i < itemWeaponComponents?.Count; i++)
                            if (currentWeaponComponents?[i].ItemUsage != itemWeaponComponents?[i].ItemUsage)
                                return true;
                    }
                    else
                    {
                        if (coefficients[(int) index].WeaponClass != itemPrimaryWeapon?.WeaponClass) return true;
                    }

                    return false;
                }

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

            // Checking the compliance of the slot's weapon class, if a weapon class other than WeaponClass.Undefined
            bool IsWeaponClassNotUndefinedAndNotEqual(EquipmentElement item)
            {
                return item.Item?.PrimaryWeapon?.WeaponClass != coefficients[(int) index].WeaponClass &&
                       coefficients[(int) index].WeaponClass != WeaponClass.Undefined;
            }
        }
        catch (Exception e)
        {
            Helper.ShowMessage($"{e.Message}", Colors.Red);
        }

        return bestItem;
    }

    private void UnequipItem(EquipmentIndex equipmentIndex, CharacterObject character)
    {
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
    }

    private void EquipItem(EquipmentIndex equipmentIndex, CharacterObject? character, SPItemVM? item)
    {
        if (item is null) return;

        var equipCommand = TransferCommand.Transfer(
            1,
            item.InventorySide,
            InventoryLogic.InventorySide.Equipment,
            item.ItemRosterElement,
            EquipmentIndex.None,
            equipmentIndex,
            character,
            !_originVM.IsInWarSet);

        _inventoryLogic.AddTransferCommand(equipCommand);
    }
}