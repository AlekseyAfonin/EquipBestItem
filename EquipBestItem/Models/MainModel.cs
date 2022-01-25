using System;
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
        private readonly InventoryLogic _inventoryLogic;
        
        private CharacterObject _currentCharacter;
        public Equipment BestLeftEquipment { get; private set; }
        public Equipment BestRightEquipment { get; private set; }
        private readonly SPInventoryVM _inventory;
        private System.Diagnostics.Stopwatch _watch;
        
        public MainModel(SPInventoryVM inventory)
        {
            SettingsLoader.Instance.LoadCharacterSettings();
            _inventoryLogic = InventoryManager.InventoryLogic;
            _inventory = inventory;
            _currentCharacter = GetCharacterByName(_inventory.CurrentCharacterName);
        }

        public async void RefreshValues()
        {
            if (SettingsLoader.Instance.Settings.Debug)
            {
                _watch = System.Diagnostics.Stopwatch.StartNew();
            }

            if (_currentCharacter.Name.ToString() != _inventory.CurrentCharacterName)
            {
                _currentCharacter = GetCharacterByName(_inventory.CurrentCharacterName);
            }
            
            Equipment equipment = _inventory.IsInWarSet ? _currentCharacter.FirstBattleEquipment : _currentCharacter.FirstCivilianEquipment;
            BestLeftEquipment = new Equipment();
            BestRightEquipment = new Equipment();

            for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
            {
                if (equipment[equipmentIndex].IsEmpty && equipmentIndex < EquipmentIndex.NonWeaponItemBeginSlot ||
                    equipment[EquipmentIndex.Horse].IsEmpty && equipmentIndex == EquipmentIndex.HorseHarness)
                    continue;

                EquipmentElement bestLeftEquipmentElement = default;
                EquipmentElement bestRightEquipmentElement = default;

                if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
                {
                    
                    bestLeftEquipmentElement = GetBetterItemFromSide(_inventory.LeftItemListVM, equipment[equipmentIndex], equipmentIndex, !_inventory.IsInWarSet, _currentCharacter);
                }
                if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
                {
                    bestRightEquipmentElement = GetBetterItemFromSide(_inventory.RightItemListVM, equipment[equipmentIndex], equipmentIndex, !_inventory.IsInWarSet, _currentCharacter);
                }

                if (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null)
                    if (ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex, _currentCharacter) > ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex, _currentCharacter))
                    {
                        BestLeftEquipment[equipmentIndex] = bestLeftEquipmentElement;
                    }
                    else
                    {
                        BestRightEquipment[equipmentIndex] = bestRightEquipmentElement;
                    }
            }
            

            if (SettingsLoader.Instance.Settings.Debug)
            {
                _watch.Stop();
                Console.WriteLine("MainVM RefreshValues(): {0}ms", _watch.Elapsed.TotalMilliseconds);
            }
        }
        
        
        public void EquipEveryCharacter()
        {
            foreach (TroopRosterElement rosterElement in _inventoryLogic.RightMemberRoster.GetTroopRoster())
            {
                if (rosterElement.Character.IsHero)
                    EquipCharacter(rosterElement.Character);
            }
        }

        public void EquipCharacterEquipment(CharacterObject character, Equipment equipment, bool isCivilian)
        {
            for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
            {
                if (equipment[equipmentIndex].IsEmpty && equipmentIndex < EquipmentIndex.NonWeaponItemBeginSlot ||
                    equipment[EquipmentIndex.Horse].IsEmpty && equipmentIndex == EquipmentIndex.HorseHarness)
                    continue;

                EquipmentElement bestLeftEquipmentElement = default;
                EquipmentElement bestRightEquipmentElement = default;

                if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
                {
                    bestLeftEquipmentElement = GetBetterItemFromSide(_inventory.LeftItemListVM, equipment[equipmentIndex], equipmentIndex, isCivilian, character);
                }
                if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
                {
                    bestRightEquipmentElement = GetBetterItemFromSide(_inventory.RightItemListVM, equipment[equipmentIndex], equipmentIndex, isCivilian, character);
                }

                //Unequip current equipment element
                if (!equipment[equipmentIndex].IsEmpty && (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null))
                {
                    TransferCommand transferCommand = TransferCommand.Transfer(
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
                    if (ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex, character) > ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex, character))
                    {
                        TransferCommand equipCommand = TransferCommand.Transfer(
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
                        TransferCommand equipCommand = TransferCommand.Transfer(
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
        }

        public void EquipCharacter(CharacterObject character)
        {
            if (_inventory.IsInWarSet)
            {
                Equipment battleEquipment = character.FirstBattleEquipment;
                EquipCharacterEquipment(character, battleEquipment, false);
            }
            else
            {
                Equipment civilEquipment = character.FirstCivilianEquipment;
                EquipCharacterEquipment(character, civilEquipment, true);
            }
        }

        private static void EquipMessage(EquipmentIndex equipmentIndex, CharacterObject character)
        {
            switch (equipmentIndex)
            {
                case EquipmentIndex.Weapon0:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips weapon in the first slot"));
                    break;
                case EquipmentIndex.Weapon1:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips weapon in the second slot"));
                    break;
                case EquipmentIndex.Weapon2:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips weapon in the third slot"));
                    break;
                case EquipmentIndex.Weapon3:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips weapon in the fourth slot"));
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
        public EquipmentElement GetBetterItemFromSide(MBBindingList<SPItemVM> itemListVM, EquipmentElement equipmentElement, EquipmentIndex slot, bool isCivilian, CharacterObject character)
        {
            EquipmentElement bestEquipmentElement = default;

            foreach (SPItemVM item in itemListVM)
            {
                if (IsCamel(item) || IsCamelHarness(item))
                    continue;
                if (isCivilian)
                {
                    if (slot < EquipmentIndex.NonWeaponItemBeginSlot &&
                        item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon != null &&
                        item.IsEquipableItem &&
                        item.IsCivilianItem &&
                        CharacterHelper.CanUseItemBasedOnSkill(character, item.ItemRosterElement.EquipmentElement) && !item.IsLocked
                        )
                    {
                        if (equipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.WeaponClass &&
                            GetItemUsage(item) == equipmentElement.Item.PrimaryWeapon.ItemUsage && !item.IsLocked)
                            if (bestEquipmentElement.IsEmpty)
                                if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) > ItemIndexCalculation(equipmentElement, slot, character) &&
                                    ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                                    bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                                else
                                    continue;
                            else
                                if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) > ItemIndexCalculation(bestEquipmentElement, slot, character) &&
                                    ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                    }
                    else if (item.ItemType == slot && item.IsEquipableItem && item.IsCivilianItem &&
                        CharacterHelper.CanUseItemBasedOnSkill(character, item.ItemRosterElement.EquipmentElement) && !item.IsLocked)
                        if (bestEquipmentElement.IsEmpty)
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) > ItemIndexCalculation(equipmentElement, slot, character) &&
                                ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                            else
                                continue;
                        else
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) > ItemIndexCalculation(bestEquipmentElement, slot, character) &&
                                ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                            bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                }
                else
                {
                    if (slot < EquipmentIndex.NonWeaponItemBeginSlot && item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon != null && item.IsEquipableItem &&
                        CharacterHelper.CanUseItemBasedOnSkill(character, item.ItemRosterElement.EquipmentElement) && !item.IsLocked)
                    {
                        if (equipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.WeaponClass &&
                            GetItemUsage(item) == equipmentElement.Item.PrimaryWeapon.ItemUsage && !item.IsLocked)
                            if (bestEquipmentElement.IsEmpty)
                                if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) > ItemIndexCalculation(equipmentElement, slot, character) &&
                                    ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                                    bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                                else
                                    continue;
                            else
                                if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) > ItemIndexCalculation(bestEquipmentElement, slot, character) &&
                                    ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                    }
                    else if (item.ItemType == slot && item.IsEquipableItem &&
                        CharacterHelper.CanUseItemBasedOnSkill(character, item.ItemRosterElement.EquipmentElement) && !item.IsLocked)
                        if (bestEquipmentElement.IsEmpty)
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) > ItemIndexCalculation(equipmentElement, slot, character) &&
                                ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                            else
                                continue;
                        else
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) > ItemIndexCalculation(bestEquipmentElement, slot, character) &&
                                ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot, character) != 0f)
                            bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                }
            }

            return bestEquipmentElement;
        }

        private float ItemIndexCalculation(EquipmentElement sourceItem, EquipmentIndex index, CharacterObject character)
        {
            var characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(character.Name.ToString(), _inventory.IsInWarSet);

            if (sourceItem.IsEmpty)
                return -9999f;

            float value = 0f;

            FilterElement filterElement = characterSettings.Filters[index];

            if (sourceItem.Item.HasArmorComponent)
            {
                ArmorComponent armorComponentItem = sourceItem.Item.ArmorComponent;

                float sum =
                    Math.Abs(filterElement.HeadArmor) +
                    Math.Abs(filterElement.ArmArmor) +
                    Math.Abs(filterElement.ArmorBodyArmor) +
                    Math.Abs(filterElement.Weight) +
                    Math.Abs(filterElement.LegArmor) +
                    Math.Abs(filterElement.ChargeBonus) +
                    Math.Abs(filterElement.ManeuverBonus) +
                    Math.Abs(filterElement.SpeedBonus);

                Console.WriteLine(String.Format("{0}: HA {1}, BA {2}, LA {3}, AA {4}, W {5} - {6}",
                    sourceItem.Item.Name, filterElement.HeadArmor, filterElement.ArmorBodyArmor, filterElement.LegArmor, filterElement.ArmArmor, filterElement.Weight, "filters"));
                
                ItemModifier mod =
                    sourceItem.ItemModifier;

                int headArmor = armorComponentItem.HeadArmor,
                    bodyArmor = armorComponentItem.BodyArmor,
                    legArmor = armorComponentItem.LegArmor,
                    armArmor = armorComponentItem.ArmArmor,
                    chargeBonus = armorComponentItem.ChargeBonus,
                    maneuverBonus = armorComponentItem.ManeuverBonus,
                    speedBonus = armorComponentItem.SpeedBonus;
                float weight = sourceItem.Weight;
                
                Console.WriteLine(String.Format("{0}: HA {1}, BA {2}, LA {3}, AA {4}, W {5} - {6}",
                    sourceItem.Item.Name, headArmor, bodyArmor, legArmor, armArmor, weight, "source"));
                
                if (mod != null)
                {
                    headArmor = mod.ModifyArmor(headArmor);
                    bodyArmor = mod.ModifyArmor(bodyArmor);
                    legArmor = mod.ModifyArmor(legArmor);
                    armArmor = mod.ModifyArmor(armArmor);
                }
                Console.WriteLine(String.Format("{0}: HA {1}, BA {2}, LA {3}, AA {4}, W {5} - {6}",
                    sourceItem.Item.Name, headArmor, bodyArmor, legArmor, armArmor, weight, "after mods"));
                
                value = (
                    headArmor * filterElement.HeadArmor +
                    bodyArmor * filterElement.ArmorBodyArmor +
                    legArmor * filterElement.LegArmor +
                    armArmor * filterElement.ArmArmor +
                    weight * filterElement.Weight +
                    chargeBonus * filterElement.ChargeBonus +
                    maneuverBonus * filterElement.ManeuverBonus +
                    speedBonus * filterElement.SpeedBonus
                ) / sum;

                Console.WriteLine(String.Format("{0}: HA {1}, BA {2}, LA {3}, AA {4}, W {5} - {6}",
                                sourceItem.Item.Name, headArmor, bodyArmor, legArmor, armArmor, weight, "after filters"));
                Console.WriteLine("Total score: " + value);

                return value;
            }

            if (sourceItem.Item.PrimaryWeapon != null)
            {
                WeaponComponentData primaryWeaponItem = sourceItem.Item.PrimaryWeapon;

                float sum =
                    Math.Abs(filterElement.Accuracy) +
                    Math.Abs(filterElement.WeaponBodyArmor) +
                    Math.Abs(filterElement.Handling) +
                    Math.Abs(filterElement.MaxDataValue) +
                    Math.Abs(filterElement.MissileSpeed) +
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
                    swingDamage = primaryWeaponItem.SwingDamage,
                    swingSpeed = primaryWeaponItem.SwingSpeed,
                    thrustDamage = primaryWeaponItem.ThrustDamage,
                    thrustSpeed = primaryWeaponItem.ThrustSpeed,
                    weaponLength = primaryWeaponItem.WeaponLength;
                    //missileDamage = primaryWeaponItem.MissileDamage;
                float weaponWeight = sourceItem.Weight;

                ItemModifier mod = sourceItem.ItemModifier;
                if (mod != null)
                {
                    handling = mod.ModifySpeed(handling);
                    bodyArmor = mod.ModifyArmor(bodyArmor);
                    missileSpeed = mod.ModifyMissileSpeed(missileSpeed);
                    //missileDamage = mod.ModifyDamage(missileDamage);
                    swingDamage = mod.ModifyDamage(swingDamage);
                    swingSpeed = mod.ModifySpeed(swingSpeed);
                    thrustDamage = mod.ModifyDamage(thrustDamage);
                    thrustSpeed = mod.ModifySpeed(thrustSpeed);
                    maxDataValue = mod.ModifyHitPoints((short)maxDataValue);
                }
                
                //TODO add stack count and missile damage
                value = (
                    accuracy * filterElement.Accuracy +
                    bodyArmor * filterElement.WeaponBodyArmor +
                    handling * filterElement.Handling +
                    maxDataValue * filterElement.MaxDataValue +
                    missileSpeed * filterElement.MissileSpeed +
                    swingDamage * filterElement.SwingDamage +
                    swingSpeed * filterElement.SwingSpeed +
                    thrustDamage * filterElement.ThrustDamage +
                    thrustSpeed * filterElement.ThrustSpeed +
                    weaponLength * filterElement.WeaponLength +
                    weaponWeight * filterElement.Weight
                    //missileDamage * filterElement.MissileDamage;
                ) / sum;

                //Console.WriteLine(String.Format("{0}: Acc {1}, BA {2}, HL {3}, HP {4}, MS {5}, SD {6}, SS {7}, TD {8}, TS {9}, WL {10}, W {11}",
                //                sourceItem.Item.Name, Accuracy, BodyArmor, Handling, MaxDataValue, MissileSpeed, SwingDamage, SwingSpeed, ThrustDamage, ThrustSpeed, WeaponLength, WeaponWeight));
                //Console.WriteLine("Total score: " + value);

                return value;
            }

            if (sourceItem.Item.HasHorseComponent)
            {
                HorseComponent horseComponentItem = sourceItem.Item.HorseComponent;

                float sum =
                    Math.Abs(filterElement.ChargeDamage) +
                    Math.Abs(filterElement.HitPoints) +
                    Math.Abs(filterElement.Maneuver) +
                    Math.Abs(filterElement.Speed);

                int chargeDamage = horseComponentItem.ChargeDamage,
                    hitPoints = horseComponentItem.HitPoints,
                    maneuver = horseComponentItem.Maneuver,
                    speed = horseComponentItem.Speed;

                ItemModifier mod = sourceItem.ItemModifier;
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

                //Console.WriteLine(String.Format("{0}: CD {1}, HP {2}, MR {3}, SD {4}",
                //                sourceItem.Item.Name, ChargeDamage, HitPoints, Maneuver, Speed));
                //Console.WriteLine("Total score: " + value);

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
                            if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Monster.MonsterUsage == "camel")
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
            if (item == null || item.ItemRosterElement.IsEmpty || item.ItemRosterElement.EquipmentElement.IsEmpty || item.ItemRosterElement.EquipmentElement.Item.WeaponComponent == null)
                return "";
            string value = item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.ItemUsage;
            return value;
        }

        public CharacterObject GetCharacterByName(string name)
        {
            foreach (TroopRosterElement rosterElement in _inventoryLogic.RightMemberRoster.GetTroopRoster())
            {
                if (rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name)
                    return rosterElement.Character;
            }
            return null;
        }

        public void ExecuteEquipEveryCharacter()
        {
            if (SettingsLoader.Instance.Settings.Debug)
            {
                _watch.Start();
            }

            EquipEveryCharacter();

            if (SettingsLoader.Instance.Settings.Debug)
            {
                _watch.Stop();
                Console.WriteLine("MainVM EquipEveryCharacter(): {0}ms", _watch.Elapsed.TotalMilliseconds);
            }
        }

        public void ExecuteEquipCurrentCharacter()
        {
            if (SettingsLoader.Instance.Settings.Debug)
            {
                _watch.Start();
            }

            EquipCharacter(GetCharacterByName(_inventory.CurrentCharacterName));

            if (SettingsLoader.Instance.Settings.Debug)
            {
                _watch.Stop();
                Console.WriteLine("MainVM EquipCharacter({0}): {1}ms", GetCharacterByName(_inventory.CurrentCharacterName), _watch.Elapsed.TotalMilliseconds);
            }

            //this.RefreshValues();
        }

        public void EquipBestItem(EquipmentIndex equipmentIndex)
        {
            Equipment equipment = _inventory.IsInWarSet ? _currentCharacter.FirstBattleEquipment : _currentCharacter.FirstCivilianEquipment;
            //Unequip current equipment element
            if (!equipment[equipmentIndex].IsEmpty)
            {
                TransferCommand transferCommand = TransferCommand.Transfer(
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
            if (ItemIndexCalculation(BestLeftEquipment[equipmentIndex], equipmentIndex, _currentCharacter) > ItemIndexCalculation(BestRightEquipment[equipmentIndex], equipmentIndex, _currentCharacter))
            {
                TransferCommand equipCommand = TransferCommand.Transfer(
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
                TransferCommand equipCommand = TransferCommand.Transfer(
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