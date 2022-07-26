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
                ? 0
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

            // Фильтр предметов по условиям
            bool IsValidItem(SPItemVM item) //TODO refactor
            {
                if (!_originVM.IsInWarSet && !item.IsCivilianItem) return false;
                if (!item.IsEquipableItem) return false;
                if (item.IsLocked) return false;
                if (item.ItemCount == 0) return false;
                if (!CharacterHelper.CanUseItemBasedOnSkill(character, item.ItemRosterElement.EquipmentElement))
                    return false;

                // Отключаем поиск упряжки, если в слоте отсутствует ездовое животное
                if (equipment[EquipmentIndex.Horse].IsEmpty && item.ItemType == EquipmentIndex.HorseHarness)
                    return false;

                if (item.ItemType == EquipmentIndex.HorseHarness)
                    if (equipment[EquipmentIndex.Horse].Item?.HorseComponent?.Monster.FamilyType !=
                        item.ItemRosterElement.EquipmentElement.Item?.ArmorComponent?.FamilyType) return false;

                // Отделяем поиск оружия от других предметов
                if (item.ItemType != EquipmentIndex.Weapon0 || index > EquipmentIndex.Weapon4)
                    return item.ItemType == index;

                var itemPrimaryWeapon = item.ItemRosterElement.EquipmentElement.Item?.PrimaryWeapon;
                var currentPrimaryWeapon = equipment[index].Item?.PrimaryWeapon;

                // Если выбранный класс оружия не определен, смотрим по классу оружия из слота, иначе по выбранному
                if (coefficients[(int) index].WeaponClass == WeaponClass.Undefined)
                {
                    // Если не совпадают классы - пропускаем
                    if (itemPrimaryWeapon?.WeaponClass != currentPrimaryWeapon?.WeaponClass) return false;

                    // Дополнительный фильтр для коротких и длинных луков
                    if (currentPrimaryWeapon?.WeaponClass == WeaponClass.Bow &&
                        itemPrimaryWeapon?.ItemUsage != currentPrimaryWeapon.ItemUsage) return false;

                    var currentWeaponComponents = item.ItemRosterElement.EquipmentElement.Item?.Weapons;
                    var itemWeaponComponents = equipment[index].Item?.Weapons;
                    
                    // Если отличаются по количеству компонентов - пропускаем
                    if (itemWeaponComponents?.Count != currentWeaponComponents?.Count) 
                        return false;
                    
                    // Если они количество компонентов равно, то перебираем каждый и сравниваем по классу
                    for (var i = 0; i < itemWeaponComponents?.Count; i++)
                        if (currentWeaponComponents?[i].ItemUsage != itemWeaponComponents?[i].ItemUsage)
                            return false;
                }
                else
                {
                    if (coefficients[(int) index].WeaponClass != itemPrimaryWeapon?.WeaponClass) return false;
                }

                // Исключаем из поиска щиты, если щит уже надет
                if (coefficients[(int) index].WeaponClass is not (WeaponClass.SmallShield or WeaponClass.LargeShield))
                    return item.ItemType <= index;

                for (var i = EquipmentIndex.Weapon0; i <= EquipmentIndex.ExtraWeaponSlot; i++)
                    if (equipment[i].Item?.PrimaryWeapon?.WeaponClass is WeaponClass.SmallShield
                        or WeaponClass.LargeShield)
                        return false;

                return item.ItemType <= index;
            }

            // Проверка соответствия класса оружия слота, если выбран класс оружия отличный от WeaponClass.Undefined
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