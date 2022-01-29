using System;
using System.Diagnostics;
using EquipBestItem.Settings;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.Models
{
    public class MainModel
    {
        private readonly SPInventoryVM _inventory;
        private readonly InventoryLogic _inventoryLogic;

        private CharacterObject _currentCharacter;
        private Stopwatch _watch;

        public MainModel()
        {
            SettingsLoader.Instance.LoadCharacterSettings();
            _inventoryLogic = InventoryManager.InventoryLogic;
            _inventory = EquipBestItemManager.Instance.Inventory;
            _currentCharacter = GetCharacterByName(_inventory.CurrentCharacterName);
        }

        public Equipment BestLeftEquipment { get; private set; }
        public Equipment BestRightEquipment { get; private set; }

        public void RefreshValues()
        {
            if (SettingsLoader.Instance.Settings.Debug) _watch = Stopwatch.StartNew();

            if (_currentCharacter.Name.ToString() != _inventory.CurrentCharacterName)
                _currentCharacter = GetCharacterByName(_inventory.CurrentCharacterName);

            var equipment = _inventory.IsInWarSet
                ? _currentCharacter.FirstBattleEquipment
                : _currentCharacter.FirstCivilianEquipment;
            BestLeftEquipment = new Equipment();
            BestRightEquipment = new Equipment();

            for (var equipmentIndex = EquipmentIndex.WeaponItemBeginSlot;
                 equipmentIndex < EquipmentIndex.NumEquipmentSetSlots;
                 equipmentIndex++)
            {
                if (equipment[equipmentIndex].IsEmpty && equipmentIndex < EquipmentIndex.NonWeaponItemBeginSlot ||
                    equipment[EquipmentIndex.Horse].IsEmpty && equipmentIndex == EquipmentIndex.HorseHarness)
                    continue;

                EquipmentElement bestLeftEquipmentElement = default;
                EquipmentElement bestRightEquipmentElement = default;

                if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
                    bestLeftEquipmentElement = GetBetterItemFromSide(_inventory.LeftItemListVM,
                        equipment[equipmentIndex], equipmentIndex, !_inventory.IsInWarSet, _currentCharacter);
                if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
                    bestRightEquipmentElement = GetBetterItemFromSide(_inventory.RightItemListVM,
                        equipment[equipmentIndex], equipmentIndex, !_inventory.IsInWarSet, _currentCharacter);

                if (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null)
                    if (ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex, _currentCharacter) >
                        ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex, _currentCharacter))
                        BestLeftEquipment[equipmentIndex] = bestLeftEquipmentElement;
                    else
                        BestRightEquipment[equipmentIndex] = bestRightEquipmentElement;
            }

            if (SettingsLoader.Instance.Settings.Debug) _watch.Stop();
        }


        public void EquipEveryCharacter()
        {
            foreach (var rosterElement in _inventoryLogic.RightMemberRoster.GetTroopRoster())
                if (rosterElement.Character.IsHero)
                    EquipCharacter(rosterElement.Character);
        }

        public void EquipCharacterEquipment(CharacterObject character, Equipment equipment, bool isCivilian)
        {
            for (var equipmentIndex = EquipmentIndex.WeaponItemBeginSlot;
                 equipmentIndex < EquipmentIndex.NumEquipmentSetSlots;
                 equipmentIndex++)
            {
                if (equipment[equipmentIndex].IsEmpty && equipmentIndex < EquipmentIndex.NonWeaponItemBeginSlot ||
                    equipment[EquipmentIndex.Horse].IsEmpty && equipmentIndex == EquipmentIndex.HorseHarness)
                    continue;

                EquipmentElement bestLeftEquipmentElement = default;
                EquipmentElement bestRightEquipmentElement = default;

                if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
                    bestLeftEquipmentElement = GetBetterItemFromSide(_inventory.LeftItemListVM,
                        equipment[equipmentIndex], equipmentIndex, isCivilian, character);
                if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
                    bestRightEquipmentElement = GetBetterItemFromSide(_inventory.RightItemListVM,
                        equipment[equipmentIndex], equipmentIndex, isCivilian, character);

                //Unequip current equipment element
                if (!equipment[equipmentIndex].IsEmpty &&
                    (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null))
                {
                    var transferCommand = TransferCommand.Transfer(
                        1,
                        InventoryLogic.InventorySide.Equipment,
                        InventoryLogic.InventorySide.PlayerInventory,
                        new ItemRosterElement(equipment[equipmentIndex], 1),
                        equipmentIndex,
                        EquipmentIndex.None,
                        character,
                        isCivilian
                    );
                    _inventoryLogic.AddTransferCommand(transferCommand);
                }

                if (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null)
                    if (ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex, character) >
                        ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex, character))
                    {
                        var equipCommand = TransferCommand.Transfer(
                            1,
                            InventoryLogic.InventorySide.OtherInventory,
                            InventoryLogic.InventorySide.Equipment,
                            new ItemRosterElement(bestLeftEquipmentElement, 1),
                            EquipmentIndex.None,
                            equipmentIndex,
                            character,
                            isCivilian
                        );

                        EquipMessage(equipmentIndex, character);
                        _inventoryLogic.AddTransferCommand(equipCommand);
                    }
                    else
                    {
                        var equipCommand = TransferCommand.Transfer(
                            1,
                            InventoryLogic.InventorySide.PlayerInventory,
                            InventoryLogic.InventorySide.Equipment,
                            new ItemRosterElement(bestRightEquipmentElement, 1),
                            EquipmentIndex.None,
                            equipmentIndex,
                            character,
                            isCivilian
                        );

                        EquipMessage(equipmentIndex, character);
                        _inventoryLogic.AddTransferCommand(equipCommand);
                    }

                _inventory.ExecuteRemoveZeroCounts();
            }

            _inventory.RefreshValues();
            _inventory.GetMethod("UpdateCharacterEquipment");
        }

        public void EquipCharacter(CharacterObject character)
        {
            if (_inventory.IsInWarSet)
            {
                var battleEquipment = character.FirstBattleEquipment;
                EquipCharacterEquipment(character, battleEquipment, false);
            }
            else
            {
                var civilEquipment = character.FirstCivilianEquipment;
                EquipCharacterEquipment(character, civilEquipment, true);
            }
        }

        private static void EquipMessage(EquipmentIndex equipmentIndex, CharacterObject character)
        {
            switch (equipmentIndex)
            {
                case EquipmentIndex.Weapon0:
                    InformationManager.DisplayMessage(
                        new InformationMessage(character.Name + " equips weapon in the first slot"));
                    break;
                case EquipmentIndex.Weapon1:
                    InformationManager.DisplayMessage(
                        new InformationMessage(character.Name + " equips weapon in the second slot"));
                    break;
                case EquipmentIndex.Weapon2:
                    InformationManager.DisplayMessage(
                        new InformationMessage(character.Name + " equips weapon in the third slot"));
                    break;
                case EquipmentIndex.Weapon3:
                    InformationManager.DisplayMessage(
                        new InformationMessage(character.Name + " equips weapon in the fourth slot"));
                    break;
                case EquipmentIndex.Head:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips helmet"));
                    break;
                case EquipmentIndex.Body:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips body armor"));
                    break;
                case EquipmentIndex.Leg:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips boots"));
                    break;
                case EquipmentIndex.Gloves:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips gloves"));
                    break;
                case EquipmentIndex.Cape:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips cape"));
                    break;
                case EquipmentIndex.Horse:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips horse"));
                    break;
                case EquipmentIndex.HorseHarness:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips horse harness"));
                    break;
            }
        }

        //TODO async search
        public EquipmentElement GetBetterItemFromSide(MBBindingList<SPItemVM> itemListVM,
            EquipmentElement equipmentElement, EquipmentIndex slot, bool isCivilian, CharacterObject character)
        {
            EquipmentElement bestEquipmentElement = default;

            foreach (var item in itemListVM)
            {
                if (IsCamel(item) || IsCamelHarness(item))
                    continue;
                if (isCivilian)
                {
                    if (slot < EquipmentIndex.NonWeaponItemBeginSlot &&
                        item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon != null &&
                        item.IsEquipableItem &&
                        item.IsCivilianItem &&
                        CharacterHelper.CanUseItemBasedOnSkill(character, item.ItemRosterElement.EquipmentElement) &&
                        !item.IsLocked
                       )
                    {
                        if (equipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass ==
                            item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.WeaponClass &&
                            GetItemUsage(item) == equipmentElement.Item.PrimaryWeapon.ItemUsage && !item.IsLocked)
                            if (bestEquipmentElement.IsEmpty)
                                if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) >
                                    ItemIndexCalculation(equipmentElement, slot, character) &&
                                    ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) !=
                                    0f)
                                    bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                                else
                                    continue;
                            else if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) >
                                     ItemIndexCalculation(bestEquipmentElement, slot, character) &&
                                     ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) !=
                                     0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                    }
                    else if (item.ItemType == slot && item.IsEquipableItem && item.IsCivilianItem &&
                             CharacterHelper.CanUseItemBasedOnSkill(character,
                                 item.ItemRosterElement.EquipmentElement) && !item.IsLocked)
                    {
                        if (bestEquipmentElement.IsEmpty)
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) >
                                ItemIndexCalculation(equipmentElement, slot, character) &&
                                ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                            else
                                continue;
                        else if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) >
                                 ItemIndexCalculation(bestEquipmentElement, slot, character) &&
                                 ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                            bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                    }
                }
                else
                {
                    if (slot < EquipmentIndex.NonWeaponItemBeginSlot &&
                        item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon != null && item.IsEquipableItem &&
                        CharacterHelper.CanUseItemBasedOnSkill(character, item.ItemRosterElement.EquipmentElement) &&
                        !item.IsLocked)
                    {
                        if (equipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass ==
                            item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.WeaponClass &&
                            GetItemUsage(item) == equipmentElement.Item.PrimaryWeapon.ItemUsage && !item.IsLocked)
                            if (bestEquipmentElement.IsEmpty)
                                if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) >
                                    ItemIndexCalculation(equipmentElement, slot, character) &&
                                    ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) !=
                                    0f)
                                    bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                                else
                                    continue;
                            else if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) >
                                     ItemIndexCalculation(bestEquipmentElement, slot, character) &&
                                     ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) !=
                                     0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                    }
                    else if (item.ItemType == slot && item.IsEquipableItem &&
                             CharacterHelper.CanUseItemBasedOnSkill(character,
                                 item.ItemRosterElement.EquipmentElement) && !item.IsLocked)
                    {
                        if (bestEquipmentElement.IsEmpty)
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) >
                                ItemIndexCalculation(equipmentElement, slot, character) &&
                                ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                            else
                                continue;
                        else if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) >
                                 ItemIndexCalculation(bestEquipmentElement, slot, character) &&
                                 ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                            bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                    }
                }
            }

            return bestEquipmentElement;
        }

        private float ItemIndexCalculation(EquipmentElement sourceItem, EquipmentIndex index, CharacterObject character)
        {
            var characterSettings =
                SettingsLoader.Instance.GetCharacterSettingsByName(character.Name.ToString(), _inventory.IsInWarSet);

            if (sourceItem.IsEmpty)
                return -9999f;

            var value = 0f;

            var filterElement = characterSettings.Filters[index];

            if (sourceItem.Item.HasArmorComponent)
            {
                var armorComponentItem = sourceItem.Item.ArmorComponent;

                var sum =
                    Math.Abs(filterElement.HeadArmor) +
                    Math.Abs(filterElement.ArmArmor) +
                    Math.Abs(filterElement.ArmorBodyArmor) +
                    Math.Abs(filterElement.Weight) +
                    Math.Abs(filterElement.LegArmor);
                //Math.Abs(filterElement.ChargeBonus) +
                //Math.Abs(filterElement.ManeuverBonus) +
                //Math.Abs(filterElement.SpeedBonus);

                //Saddle bonuses are present in the game, but all parameters are 0

                var mod =
                    sourceItem.ItemModifier;

                int headArmor = armorComponentItem.HeadArmor,
                    bodyArmor = armorComponentItem.BodyArmor,
                    legArmor = armorComponentItem.LegArmor,
                    armArmor = armorComponentItem.ArmArmor;
                //chargeBonus = armorComponentItem.ChargeBonus,
                //maneuverBonus = armorComponentItem.ManeuverBonus,
                //speedBonus = armorComponentItem.SpeedBonus;
                var weight = sourceItem.Weight;

                if (mod != null)
                {
                    headArmor = mod.ModifyArmor(headArmor);
                    bodyArmor = mod.ModifyArmor(bodyArmor);
                    legArmor = mod.ModifyArmor(legArmor);
                    armArmor = mod.ModifyArmor(armArmor);
                }

                value = (
                    headArmor * filterElement.HeadArmor +
                    bodyArmor * filterElement.ArmorBodyArmor +
                    legArmor * filterElement.LegArmor +
                    armArmor * filterElement.ArmArmor +
                    weight * -filterElement.Weight
                    //chargeBonus * filterElement.ChargeBonus +
                    //maneuverBonus * filterElement.ManeuverBonus +
                    //speedBonus * filterElement.SpeedBonus
                ) / sum;

                return value;
            }

            if (sourceItem.Item.PrimaryWeapon != null)
            {
                var primaryWeaponItem = sourceItem.Item.PrimaryWeapon;

                var sum =
                    Math.Abs(filterElement.Accuracy) +
                    Math.Abs(filterElement.WeaponBodyArmor) +
                    Math.Abs(filterElement.Handling) +
                    Math.Abs(filterElement.MaxDataValue) +
                    Math.Abs(filterElement.MissileSpeed) +
                    Math.Abs(filterElement.MissileDamage) +
                    Math.Abs(filterElement.SwingDamage) +
                    Math.Abs(filterElement.SwingSpeed) +
                    Math.Abs(filterElement.ThrustDamage) +
                    Math.Abs(filterElement.ThrustSpeed) +
                    Math.Abs(filterElement.WeaponLength) +
                    Math.Abs(filterElement.Weight);

                int accuracy = primaryWeaponItem.Accuracy,
                    bodyArmor = primaryWeaponItem.BodyArmor,
                    handling = primaryWeaponItem.Handling,
                    maxDataValue = primaryWeaponItem.MaxDataValue,
                    missileSpeed = primaryWeaponItem.MissileSpeed,
                    missileDamage = primaryWeaponItem.MissileDamage,
                    swingDamage = primaryWeaponItem.SwingDamage,
                    swingSpeed = primaryWeaponItem.SwingSpeed,
                    thrustDamage = primaryWeaponItem.ThrustDamage,
                    thrustSpeed = primaryWeaponItem.ThrustSpeed,
                    weaponLength = primaryWeaponItem.WeaponLength;
                var weaponWeight = sourceItem.Weight;

                var mod = sourceItem.ItemModifier;
                if (mod != null)
                {
                    handling = mod.ModifySpeed(handling);
                    bodyArmor = mod.ModifyArmor(bodyArmor);
                    missileSpeed = mod.ModifyMissileSpeed(missileSpeed);
                    missileDamage = mod.ModifyDamage(missileDamage);
                    swingDamage = mod.ModifyDamage(swingDamage);
                    swingSpeed = mod.ModifySpeed(swingSpeed);
                    thrustDamage = mod.ModifyDamage(thrustDamage);
                    thrustSpeed = mod.ModifySpeed(thrustSpeed);
                    maxDataValue = mod.ModifyHitPoints((short) maxDataValue);
                }

                //TODO add stack count and missile damage
                value = (
                    accuracy * filterElement.Accuracy +
                    bodyArmor * filterElement.WeaponBodyArmor +
                    handling * filterElement.Handling +
                    maxDataValue * filterElement.MaxDataValue +
                    missileSpeed * filterElement.MissileSpeed +
                    missileDamage * filterElement.MissileDamage +
                    swingDamage * filterElement.SwingDamage +
                    swingSpeed * filterElement.SwingSpeed +
                    thrustDamage * filterElement.ThrustDamage +
                    thrustSpeed * filterElement.ThrustSpeed +
                    weaponLength * filterElement.WeaponLength +
                    weaponWeight * -filterElement.Weight
                ) / sum;

                return value;
            }

            if (sourceItem.Item.HasHorseComponent)
            {
                var horseComponentItem = sourceItem.Item.HorseComponent;

                var sum =
                    Math.Abs(filterElement.ChargeDamage) +
                    Math.Abs(filterElement.HitPoints) +
                    Math.Abs(filterElement.Maneuver) +
                    Math.Abs(filterElement.Speed);

                int chargeDamage = horseComponentItem.ChargeDamage,
                    hitPoints = horseComponentItem.HitPoints,
                    maneuver = horseComponentItem.Maneuver,
                    speed = horseComponentItem.Speed;

                var mod = sourceItem.ItemModifier;
                if (mod != null)
                {
                    chargeDamage = mod.ModifyMountCharge(chargeDamage);
                    maneuver = mod.ModifyMountManeuver(maneuver);
                    speed = mod.ModifyMountSpeed(speed);
                }

                value = (
                    chargeDamage * filterElement.ChargeDamage +
                    hitPoints * filterElement.HitPoints +
                    maneuver * filterElement.Maneuver +
                    speed * filterElement.Speed
                ) / sum;

                return value;
            }

            return value;
        }

        private static bool IsCamel(SPItemVM item)
        {
            if (item != null)
                if (!item.ItemRosterElement.IsEmpty)
                    if (!item.ItemRosterElement.EquipmentElement.IsEmpty)
                        if (item.ItemRosterElement.EquipmentElement.Item.HasHorseComponent)
                            if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Monster.MonsterUsage ==
                                "camel")
                                return true;
            return false;
        }

        private static bool IsCamelHarness(SPItemVM item)
        {
            if (item != null && item.StringId.StartsWith("camel_sadd"))
                return true;
            return false;
        }

        public static string GetItemUsage(SPItemVM item)
        {
            if (item == null || item.ItemRosterElement.IsEmpty || item.ItemRosterElement.EquipmentElement.IsEmpty ||
                item.ItemRosterElement.EquipmentElement.Item.WeaponComponent == null)
                return "";
            var value = item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.ItemUsage;
            return value;
        }

        public CharacterObject GetCharacterByName(string name)
        {
            foreach (var rosterElement in _inventoryLogic.RightMemberRoster.GetTroopRoster())
                if (rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name)
                    return rosterElement.Character;
            return null;
        }

        public void ExecuteEquipEveryCharacter()
        {
            if (SettingsLoader.Instance.Settings.Debug) _watch.Start();

            EquipEveryCharacter();

            if (SettingsLoader.Instance.Settings.Debug)
            {
                _watch.Stop();
                InformationManager.DisplayMessage(
                    new InformationMessage($"EquipEveryCharacter {_watch.Elapsed.TotalMilliseconds} ms"));
            }
        }

        public void ExecuteEquipCurrentCharacter()
        {
            if (SettingsLoader.Instance.Settings.Debug) _watch.Start();

            EquipCharacter(GetCharacterByName(_inventory.CurrentCharacterName));

            if (SettingsLoader.Instance.Settings.Debug)
            {
                _watch.Stop();
                InformationManager.DisplayMessage(
                    new InformationMessage($"EquipCharacter {_watch.Elapsed.TotalMilliseconds} ms"));
            }
        }

        public void EquipBestItem(EquipmentIndex equipmentIndex)
        {
            var equipment = _inventory.IsInWarSet
                ? _currentCharacter.FirstBattleEquipment
                : _currentCharacter.FirstCivilianEquipment;

            //Unequip current equipment element
            if (!equipment[equipmentIndex].IsEmpty)
            {
                var transferCommand = TransferCommand.Transfer(
                    1,
                    InventoryLogic.InventorySide.Equipment,
                    InventoryLogic.InventorySide.PlayerInventory,
                    new ItemRosterElement(equipment[equipmentIndex], 1),
                    equipmentIndex,
                    EquipmentIndex.None,
                    _currentCharacter,
                    !_inventory.IsInWarSet
                );
                _inventoryLogic.AddTransferCommand(transferCommand);
            }

            //Equip
            if (ItemIndexCalculation(BestLeftEquipment[equipmentIndex], equipmentIndex, _currentCharacter) >
                ItemIndexCalculation(BestRightEquipment[equipmentIndex], equipmentIndex, _currentCharacter))
            {
                var equipCommand = TransferCommand.Transfer(
                    1,
                    InventoryLogic.InventorySide.OtherInventory,
                    InventoryLogic.InventorySide.Equipment,
                    new ItemRosterElement(BestLeftEquipment[equipmentIndex], 1),
                    EquipmentIndex.None,
                    equipmentIndex,
                    _currentCharacter,
                    !_inventory.IsInWarSet
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
                    _currentCharacter,
                    !_inventory.IsInWarSet
                );

                _inventoryLogic.AddTransferCommand(equipCommand);
            }

            _inventory.ExecuteRemoveZeroCounts();
            _inventory.RefreshValues();
            _inventory.GetMethod("UpdateCharacterEquipment");
        }

        public void EquipCurrentCharacter()
        {
            EquipCharacter(GetCharacterByName(_inventory.CurrentCharacterName));
        }

        public void OnFinalize()
        {
            BestLeftEquipment = null;
            BestRightEquipment = null;
            _watch = null;
            _currentCharacter = null;
        }
    }
}