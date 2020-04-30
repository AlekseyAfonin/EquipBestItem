using EquipBestItem.Settings;
using SandBox.GauntletUI;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using static TaleWorlds.Core.ItemObject;

namespace EquipBestItem
{
    public class EquipBestItemViewModel : ViewModel
    {

        public static InventoryGauntletScreen InventoryScreen { get; set; }

        public static bool IsHelmButtonEnabled { get; set; }
        public static bool IsCloakButtonEnabled { get; set; }
        public static bool IsArmorButtonEnabled { get; set; }
        public static bool IsGloveButtonEnabled { get; set; }
        public static bool IsBootButtonEnabled { get; set; }
        public static bool IsMountButtonEnabled { get; set; }
        public static bool IsHarnessButtonEnabled { get; set; }
        public static bool IsWeapon1ButtonEnabled { get; set; }
        public static bool IsWeapon2ButtonEnabled { get; set; }
        public static bool IsWeapon3ButtonEnabled { get; set; }
        public static bool IsWeapon4ButtonEnabled { get; set; }
        public static bool IsEquipAllActivated { get; set; }

        public static bool IsLeftPanelLocked { get; set; }
        public static bool IsRightPanelLocked { get; set; }
        public static List<string> ItemUsageList { get; private set; }

        public static SPItemVM BestHelm;
        public static SPItemVM BestCloak;
        public static SPItemVM BestArmor;
        public static SPItemVM BestGlove;
        public static SPItemVM BestBoot;
        public static SPItemVM BestMount;
        public static SPItemVM BestHarness;
        public static SPItemVM BestWeapon1;
        public static SPItemVM BestWeapon2;
        public static SPItemVM BestWeapon3;
        public static SPItemVM BestWeapon4;

        private static CharacterSettings _characterSettings { get; set; }
        public static string CurrentCharacterName { get; set; }

        public static SPInventoryVM _inventory;

        public static bool NewGame;

        public EquipBestItemViewModel()
        {
            _inventory = EquipBestItemViewModel.InventoryScreen.GetField("_dataSource") as SPInventoryVM;
            ItemUsageList = new List<string>();
            UpdateCurrentCharacterName();
        }

        public static string GetItemUsage(SPItemVM item)
        {
            if (item == null || item.ItemRosterElement.IsEmpty || item.ItemRosterElement.EquipmentElement.IsEmpty || item.ItemRosterElement.EquipmentElement.Item.WeaponComponent == null)
                return "";
            string value = item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.ItemUsage;
            return value;
        }

        private static void FindBestItemsFromSide(MBBindingList<SPItemVM> inventory)
        {
            foreach (SPItemVM item in inventory)
            {
                if (_inventory.IsInWarSet)
                {
                    if (item.IsEquipableItem && item.CanCharacterUseItem)
                    {
                        if (item.ItemRosterElement.EquipmentElement.Item.ArmorComponent != null)
                        {
                            switch (item.ItemRosterElement.EquipmentElement.Item.ItemType)
                            {
                                case ItemTypeEnum.HeadArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 0) > ArmorIndexCalculation(_inventory.CharacterHelmSlot, 0) &&
                                            ArmorIndexCalculation(item, 0) > ArmorIndexCalculation(BestHelm, 0) &&
                                            ArmorIndexCalculation(item, 0) != 0f)
                                        {
                                            BestHelm = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.Cape:
                                    {
                                        if (ArmorIndexCalculation(item, 1) > ArmorIndexCalculation(_inventory.CharacterCloakSlot, 1) &&
                                            ArmorIndexCalculation(item, 1) > ArmorIndexCalculation(BestCloak, 1) &&
                                            ArmorIndexCalculation(item, 1) != 0f)
                                        {
                                            BestCloak = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.BodyArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 2) > ArmorIndexCalculation(_inventory.CharacterTorsoSlot, 2) &&
                                            ArmorIndexCalculation(item, 2) > ArmorIndexCalculation(BestArmor, 2) &&
                                            ArmorIndexCalculation(item, 2) != 0f)
                                        {
                                            BestArmor = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HandArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 3) > ArmorIndexCalculation(_inventory.CharacterGloveSlot, 3) &&
                                            ArmorIndexCalculation(item, 3) > ArmorIndexCalculation(BestGlove, 3) &&
                                            ArmorIndexCalculation(item, 3) != 0f)
                                        {
                                            BestGlove = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.LegArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 4) > ArmorIndexCalculation(_inventory.CharacterBootSlot, 4) &&
                                            ArmorIndexCalculation(item, 4) > ArmorIndexCalculation(BestBoot, 4) &&
                                            ArmorIndexCalculation(item, 4) != 0f)
                                        {
                                            BestBoot = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HorseHarness:
                                    {
                                        if (ArmorIndexCalculation(item, 5) > ArmorIndexCalculation(_inventory.CharacterMountArmorSlot, 5) &&
                                            ArmorIndexCalculation(item, 5) > ArmorIndexCalculation(BestHarness, 5) &&
                                            ArmorIndexCalculation(item, 5) != 0f)
                                        {
                                            BestHarness = item;
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                        if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent != null)
                        {
                            if (_inventory.CharacterWeapon1Slot != null && !_inventory.CharacterWeapon1Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon1Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon1Slot))
                                    if (WeaponIndexCalculation(item, 0) > WeaponIndexCalculation(_inventory.CharacterWeapon1Slot, 0) &&
                                        WeaponIndexCalculation(item, 0) > WeaponIndexCalculation(BestWeapon1, 0) &&
                                            WeaponIndexCalculation(item, 0) != 0f)
                                    {
                                        BestWeapon1 = item;
                                    }
                            if (_inventory.CharacterWeapon2Slot != null && !_inventory.CharacterWeapon2Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon2Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon2Slot))
                                    if (WeaponIndexCalculation(item, 1) > WeaponIndexCalculation(_inventory.CharacterWeapon2Slot, 1) &&
                                        WeaponIndexCalculation(item, 1) > WeaponIndexCalculation(BestWeapon2, 1) &&
                                            WeaponIndexCalculation(item, 1) != 0f)
                                    {
                                        BestWeapon2 = item;
                                    }
                            if (_inventory.CharacterWeapon3Slot != null && !_inventory.CharacterWeapon3Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon3Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon3Slot))
                                    if (WeaponIndexCalculation(item, 2) > WeaponIndexCalculation(_inventory.CharacterWeapon3Slot, 2) &&
                                        WeaponIndexCalculation(item, 2) > WeaponIndexCalculation(BestWeapon3, 2) &&
                                            WeaponIndexCalculation(item, 2) != 0f)
                                    {
                                        BestWeapon3 = item;
                                    }
                            if (_inventory.CharacterWeapon4Slot != null && !_inventory.CharacterWeapon4Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon4Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon4Slot))
                                    if (WeaponIndexCalculation(item, 3) > WeaponIndexCalculation(_inventory.CharacterWeapon4Slot, 3) &&
                                        WeaponIndexCalculation(item, 3) > WeaponIndexCalculation(BestWeapon4, 3) &&
                                            WeaponIndexCalculation(item, 3) != 0f)
                                    {
                                        BestWeapon4 = item;
                                    }
                        }
                        if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent != null)
                        {

                            if (MountIndexCalculation(item) > MountIndexCalculation(_inventory.CharacterMountSlot) &&
                                MountIndexCalculation(item) > MountIndexCalculation(BestMount) &&
                                            MountIndexCalculation(item) != 0f)
                            {
                                BestMount = item;
                            }
                        }
                    }
                }
                else
                {
                    if (item.IsEquipableItem && item.CanCharacterUseItem && item.IsCivilianItem)
                    {
                        if (item.ItemRosterElement.EquipmentElement.Item.ArmorComponent != null)
                        {
                            switch (item.ItemRosterElement.EquipmentElement.Item.ItemType)
                            {
                                case ItemTypeEnum.HeadArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 0) > ArmorIndexCalculation(_inventory.CharacterHelmSlot, 0) &&
                                            ArmorIndexCalculation(item, 0) > ArmorIndexCalculation(BestHelm, 0) &&
                                            ArmorIndexCalculation(item, 0) != 0f)
                                        {
                                            BestHelm = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.Cape:
                                    {
                                        if (ArmorIndexCalculation(item, 1) > ArmorIndexCalculation(_inventory.CharacterCloakSlot, 1) &&
                                            ArmorIndexCalculation(item, 1) > ArmorIndexCalculation(BestCloak, 1) &&
                                            ArmorIndexCalculation(item, 1) != 0f)
                                        {
                                            BestCloak = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.BodyArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 2) > ArmorIndexCalculation(_inventory.CharacterTorsoSlot, 2) &&
                                            ArmorIndexCalculation(item, 2) > ArmorIndexCalculation(BestArmor, 2) &&
                                            ArmorIndexCalculation(item, 2) != 0f)
                                        {
                                            BestArmor = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HandArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 3) > ArmorIndexCalculation(_inventory.CharacterGloveSlot, 3) &&
                                            ArmorIndexCalculation(item, 3) > ArmorIndexCalculation(BestGlove, 3) &&
                                            ArmorIndexCalculation(item, 3) != 0f)
                                        {
                                            BestGlove = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.LegArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 4) > ArmorIndexCalculation(_inventory.CharacterBootSlot, 4) &&
                                            ArmorIndexCalculation(item, 4) > ArmorIndexCalculation(BestBoot, 4) &&
                                            ArmorIndexCalculation(item, 4) != 0f)
                                        {
                                            BestBoot = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HorseHarness:
                                    {
                                        if (ArmorIndexCalculation(item, 5) > ArmorIndexCalculation(_inventory.CharacterMountArmorSlot, 5) &&
                                            ArmorIndexCalculation(item, 5) > ArmorIndexCalculation(BestHarness, 5) &&
                                            ArmorIndexCalculation(item, 5) != 0f)
                                        {
                                            BestHarness = item;
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                        if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent != null)
                        {
                            if (_inventory.CharacterWeapon1Slot != null && !_inventory.CharacterWeapon1Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon1Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon1Slot))
                                    if (WeaponIndexCalculation(item, 0) > WeaponIndexCalculation(_inventory.CharacterWeapon1Slot, 0) &&
                                        WeaponIndexCalculation(item, 0) > WeaponIndexCalculation(BestWeapon1, 0) &&
                                            WeaponIndexCalculation(item, 0) != 0f)
                                    {
                                        BestWeapon1 = item;
                                    }
                            if (_inventory.CharacterWeapon2Slot != null && !_inventory.CharacterWeapon2Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon2Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon2Slot))
                                    if (WeaponIndexCalculation(item, 1) > WeaponIndexCalculation(_inventory.CharacterWeapon2Slot, 1) &&
                                        WeaponIndexCalculation(item, 1) > WeaponIndexCalculation(BestWeapon2, 1) &&
                                            WeaponIndexCalculation(item, 1) != 0f)
                                    {
                                        BestWeapon2 = item;
                                    }
                            if (_inventory.CharacterWeapon3Slot != null && !_inventory.CharacterWeapon3Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon3Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon3Slot))
                                    if (WeaponIndexCalculation(item, 2) > WeaponIndexCalculation(_inventory.CharacterWeapon3Slot, 2) &&
                                        WeaponIndexCalculation(item, 2) > WeaponIndexCalculation(BestWeapon3, 2) &&
                                            WeaponIndexCalculation(item, 2) != 0f)
                                    {
                                        BestWeapon3 = item;
                                    }
                            if (_inventory.CharacterWeapon4Slot != null && !_inventory.CharacterWeapon4Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon4Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon4Slot))
                                    if (WeaponIndexCalculation(item, 3) > WeaponIndexCalculation(_inventory.CharacterWeapon4Slot, 3) &&
                                        WeaponIndexCalculation(item, 3) > WeaponIndexCalculation(BestWeapon4, 3) &&
                                            WeaponIndexCalculation(item, 3) != 0f)
                                    {
                                        BestWeapon4 = item;
                                    }
                        }
                        if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent != null)
                        {

                            if (MountIndexCalculation(item) > MountIndexCalculation(_inventory.CharacterMountSlot) &&
                                MountIndexCalculation(item) > MountIndexCalculation(BestMount) &&
                                            MountIndexCalculation(item) != 0f)
                            {
                                BestMount = item;
                            }
                        }
                    }
                }
            }
        }

        public static void FindBestItems()
        {
            _characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(CurrentCharacterName);

            NullBestItems();

            if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
            {
                FindBestItemsFromSide(_inventory.RightItemListVM);
            }
            if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
            {
                FindBestItemsFromSide(_inventory.LeftItemListVM);
            }

            ButtonStatusUpdate();
        }

        private static bool IsCamel(SPItemVM item)
        {
            if (_inventory.CharacterMountSlot != null)
                if (!_inventory.CharacterMountSlot.ItemRosterElement.IsEmpty)
                    if (_inventory.CharacterMountSlot.ItemRosterElement.EquipmentElement.Item.HorseComponent.Monster.MonsterUsage == "camel")
                        return true;
            return false;
        }

        public static void ButtonStatusUpdate()
        {
            if (BestHelm != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsHelmButtonEnabled = true;
            }
            else
            {
                IsHelmButtonEnabled = false;
            }

            if (BestCloak != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsCloakButtonEnabled = true;
            }
            else
            {
                IsCloakButtonEnabled = false;
            }

            if (BestArmor != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsArmorButtonEnabled = true;
            }
            else
            {
                IsArmorButtonEnabled = false;
            }

            if (BestGlove != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsGloveButtonEnabled = true;
            }
            else
            {
                IsGloveButtonEnabled = false;
            }

            if (BestBoot != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsBootButtonEnabled = true;
            }
            else
            {
                IsBootButtonEnabled = false;
            }

            if (BestMount != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsMountButtonEnabled = true;
            }
            else
            {
                IsMountButtonEnabled = false;
            }

            if (BestHarness != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                if (IsCamel(_inventory.CharacterMountSlot) == false)
                {
                    IsHarnessButtonEnabled = true;
                }
                else
                {
                    IsHarnessButtonEnabled = false;
                }
            }
            else
            {
                IsHarnessButtonEnabled = false;
            }

            if (BestWeapon1 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsWeapon1ButtonEnabled = true;
            }
            else
            {
                IsWeapon1ButtonEnabled = false;
            }

            if (BestWeapon2 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsWeapon2ButtonEnabled = true;
            }
            else
            {
                IsWeapon2ButtonEnabled = false;
            }

            if (BestWeapon3 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsWeapon3ButtonEnabled = true;
            }
            else
            {
                IsWeapon3ButtonEnabled = false;
            }

            if (BestWeapon4 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsWeapon4ButtonEnabled = true;
            }
            else
            {
                IsWeapon4ButtonEnabled = false;
            }

        }

        static void NullBestItems()
        {
            BestArmor = null;
            BestBoot = null;
            BestCloak = null;
            BestGlove = null;
            BestHarness = null;
            BestHelm = null;
            BestMount = null;
            BestWeapon1 = null;
            BestWeapon2 = null;
            BestWeapon3 = null;
            BestWeapon4 = null;
        }

        public static void UpdateCurrentCharacterName()
        {
            if (_inventory.CurrentCharacterName != null)
                CurrentCharacterName = _inventory.CurrentCharacterName;
            else
                CurrentCharacterName = Hero.MainHero.Name.ToString();
        }

        public static void EquipEveryCharacter()
        {
            foreach (TroopRosterElement rosterElement in PartyBase.MainParty.MemberRoster)
            {
                if (rosterElement.Character.IsHero)
                    EquipCharacter(rosterElement.Character);
            }
        }

        public static void EquipCharacter(CharacterObject character)
        {
            Equipment equipment = character.FirstBattleEquipment;

            for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
            {
                if (equipment[equipmentIndex].IsEmpty && equipmentIndex < EquipmentIndex.NonWeaponItemBeginSlot)
                    continue;

                EquipmentElement bestLeftEquipmentElement;
                EquipmentElement bestRightEquipmentElement;

                if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
                {
                    bestLeftEquipmentElement = GetBetterItemFromSide(_inventory.LeftItemListVM, equipment[equipmentIndex], equipmentIndex);
                }
                if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
                {
                    bestRightEquipmentElement = GetBetterItemFromSide(_inventory.RightItemListVM, equipment[equipmentIndex], equipmentIndex);
                }

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
                        false
                    );
                    InventoryManager.MyInventoryLogic.AddTransferCommand(transferCommand);
                }



                if (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null)
                    if (ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex) > ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex))
                    {
                        TransferCommand equipCommand = TransferCommand.Transfer(
                            1,
                            InventoryLogic.InventorySide.OtherInventory,
                            InventoryLogic.InventorySide.Equipment,
                            new ItemRosterElement(bestLeftEquipmentElement, 1),
                            EquipmentIndex.None,
                            equipmentIndex,
                            character,
                            false
                        );

                        EquipMessage(equipmentIndex, character);
                        InventoryManager.MyInventoryLogic.AddTransferCommand(equipCommand); 
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
                            false
                        );

                        EquipMessage(equipmentIndex, character);
                        InventoryManager.MyInventoryLogic.AddTransferCommand(equipCommand);
                    }
                _inventory.Call("ExecuteRemoveZeroCounts");
            }
            _inventory.Call("RefreshInformationValues");

        }

        public static void EquipMessage(EquipmentIndex equipmentIndex, CharacterObject character)
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
                default:
                    break;
            }
        }

        public static EquipmentElement GetBetterItemFromSide(MBBindingList<SPItemVM> itemListVM, EquipmentElement equipmentElement, EquipmentIndex slot)
        {
            EquipmentElement bestEquipmentElement;

            foreach (SPItemVM item in itemListVM)
            {
                if (slot < EquipmentIndex.NonWeaponItemBeginSlot && item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon != null && item.IsEquipableItem)
                {
                    if (equipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.WeaponClass &&
                        GetItemUsage(item) == equipmentElement.Item.PrimaryWeapon.ItemUsage)
                        if (bestEquipmentElement.IsEmpty)
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(equipmentElement, slot))
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                            else
                                continue;
                        else
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(bestEquipmentElement, slot))
                            bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                }
                else if (item.ItemType == slot && item.IsEquipableItem)
                    if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(equipmentElement, slot))
                        if (bestEquipmentElement.IsEmpty)
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(equipmentElement, slot))
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                            else
                                continue;
                        else
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(bestEquipmentElement, slot))
                            bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
            }

            return bestEquipmentElement;
        }

        //public static EquipmentElement GetBetterItem(EquipmentElement equipmentElement, EquipmentIndex slot)
        //{
        //    EquipmentElement bestEquipmentElement;

        //    if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
        //    {
        //        bestEquipmentElement = GetBetterItemFromSide(_inventory.LeftItemListVM, equipmentElement, slot, bestEquipmentElement);
        //    }
        //    if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
        //    {
        //        bestEquipmentElement = GetBetterItemFromSide(_inventory.RightItemListVM, equipmentElement, slot, bestEquipmentElement);
        //    }

        //    return bestEquipmentElement;
        //}

        public static int GetEquipmentSlot(EquipmentIndex slot)
        {
            switch (slot)
            {
                case EquipmentIndex.Weapon0:
                    return 0;
                case EquipmentIndex.Weapon1:
                    return 1;
                case EquipmentIndex.Weapon2:
                    return 2;
                case EquipmentIndex.Weapon3:
                    return 3;
                case EquipmentIndex.Head:
                    return 0;
                case EquipmentIndex.Cape:
                    return 1;
                case EquipmentIndex.Body:
                    return 2;
                case EquipmentIndex.Gloves:
                    return 3;
                case EquipmentIndex.Leg:
                    return 4;
                case EquipmentIndex.Horse:
                    return 0;
                case EquipmentIndex.HorseHarness:
                    return 5;
                default:
                    return 0;
            }
        }

        private static float ItemIndexCalculation(EquipmentElement sourceItem, EquipmentIndex slot)
        {

            if (sourceItem.IsEmpty)
                return -9999f;

            float value = 0f;

            if (sourceItem.Item.HasArmorComponent)
            {
                ArmorComponent armorComponentItem = sourceItem.Item.ArmorComponent;
                FilterArmorSettings filterArmor = _characterSettings.FilterArmor[GetEquipmentSlot(slot)];

                float sum =
                    Math.Abs(filterArmor.HeadArmor) +
                    Math.Abs(filterArmor.ArmArmor) +
                    Math.Abs(filterArmor.ArmorBodyArmor) +
                    Math.Abs(filterArmor.ArmorWeight) +
                    Math.Abs(filterArmor.LegArmor);

                ItemModifier mod =
                    sourceItem.ItemModifier;

                int HeadArmor = armorComponentItem.HeadArmor,
                    BodyArmor = armorComponentItem.BodyArmor,
                    LegArmor = armorComponentItem.LegArmor,
                    ArmArmor = armorComponentItem.ArmArmor;
                float Weight = sourceItem.Weight;

                if (mod != null)
                {
                    HeadArmor = mod.ModifyArmor(HeadArmor);
                    BodyArmor = mod.ModifyArmor(BodyArmor);
                    LegArmor = mod.ModifyArmor(LegArmor);
                    ArmArmor = mod.ModifyArmor(ArmArmor);
                    //Weight *= mod.WeightMultiplier;
                }

                value = (
                    HeadArmor * filterArmor.HeadArmor +
                    BodyArmor * filterArmor.ArmorBodyArmor +
                    LegArmor * filterArmor.LegArmor +
                    ArmArmor * filterArmor.ArmArmor +
                    Weight * filterArmor.ArmorWeight
                ) / sum;

                if (SettingsLoader.Debug)
                {
                    InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: HA {1}, BA {2}, LA {3}, AA {4}, W {5}",
                        sourceItem.Item.Name, HeadArmor, BodyArmor, LegArmor, ArmArmor, Weight)));

                    InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
                }

                return value;
            }

            if (sourceItem.Item.PrimaryWeapon != null)
            {
                WeaponComponentData primaryWeaponItem = sourceItem.Item.PrimaryWeapon;
                FilterWeaponSettings filterWeapon = _characterSettings.FilterWeapon[GetEquipmentSlot(slot)];
                float sum =
                    Math.Abs(filterWeapon.Accuracy) +
                    Math.Abs(filterWeapon.WeaponBodyArmor) +
                    Math.Abs(filterWeapon.Handling) +
                    Math.Abs(filterWeapon.MaxDataValue) +
                    Math.Abs(filterWeapon.MissileSpeed) +
                    Math.Abs(filterWeapon.SwingDamage) +
                    Math.Abs(filterWeapon.SwingSpeed) +
                    Math.Abs(filterWeapon.ThrustDamage) +
                    Math.Abs(filterWeapon.ThrustSpeed) +
                    Math.Abs(filterWeapon.WeaponLength) +
                    Math.Abs(filterWeapon.WeaponWeight);

                int Accuracy = primaryWeaponItem.Accuracy,
                    BodyArmor = primaryWeaponItem.BodyArmor,
                    Handling = primaryWeaponItem.Handling,
                    MaxDataValue = primaryWeaponItem.MaxDataValue,
                    MissileSpeed = primaryWeaponItem.MissileSpeed,
                    SwingDamage = primaryWeaponItem.SwingDamage,
                    SwingSpeed = primaryWeaponItem.SwingSpeed,
                    ThrustDamage = primaryWeaponItem.ThrustDamage,
                    ThrustSpeed = primaryWeaponItem.ThrustSpeed,
                    WeaponLength = primaryWeaponItem.WeaponLength;
                float WeaponWeight = sourceItem.Weight;

                ItemModifier mod = sourceItem.ItemModifier;
                if (mod != null)
                {
                    BodyArmor = mod.ModifyArmor(BodyArmor);
                    MissileSpeed = mod.ModifyMissileSpeed(MissileSpeed);
                    SwingDamage = mod.ModifyDamage(SwingDamage);
                    SwingSpeed = mod.ModifySpeed(SwingSpeed);
                    ThrustDamage = mod.ModifyDamage(ThrustDamage);
                    ThrustSpeed = mod.ModifySpeed(ThrustSpeed);
                    MaxDataValue += mod.HitPoints;
                    //WeaponWeight *= mod.WeightMultiplier;

                }

                var weights = _characterSettings.FilterWeapon[GetEquipmentSlot(slot)];
                value = (
                    Accuracy * weights.Accuracy +
                    BodyArmor * weights.WeaponBodyArmor +
                    Handling * weights.Handling +
                    MaxDataValue * weights.MaxDataValue +
                    MissileSpeed * weights.MissileSpeed +
                    SwingDamage * weights.SwingDamage +
                    SwingSpeed * weights.SwingSpeed +
                    ThrustDamage * weights.ThrustDamage +
                    ThrustSpeed * weights.ThrustSpeed +
                    WeaponLength * weights.WeaponLength +
                    WeaponWeight * weights.WeaponWeight
                ) / sum;

                if (SettingsLoader.Debug)
                {
                    InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: Acc {1}, BA {2}, HL {3}, HP {4}, MS {5}, SD {6}, SS {7}, TD {8}, TS {9}, WL {10}, W {11}",
                        sourceItem.Item.Name, Accuracy, BodyArmor, Handling, MaxDataValue, MissileSpeed, SwingDamage, SwingSpeed, ThrustDamage, ThrustSpeed, WeaponLength, WeaponWeight)));

                    InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
                }

                return value;
            }

            if (sourceItem.Item.HasHorseComponent)
            {
                HorseComponent horseComponentItem = sourceItem.Item.HorseComponent;
                FilterMountSettings filterMount = _characterSettings.FilterMount;

                float sum =
                    Math.Abs(filterMount.ChargeDamage) +
                    Math.Abs(filterMount.HitPoints) +
                    Math.Abs(filterMount.Maneuver) +
                    Math.Abs(filterMount.Speed);

                int ChargeDamage = horseComponentItem.ChargeDamage,
                    HitPoints = horseComponentItem.HitPoints,
                    Maneuver = horseComponentItem.Maneuver,
                    Speed = horseComponentItem.Speed;

                ItemModifier mod =
                    sourceItem.ItemModifier;
                if (mod != null)
                {
                    ChargeDamage = mod.ModifyHorseCharge(ChargeDamage);
                    Maneuver = mod.ModifyHorseManuever(Maneuver);
                    Speed = mod.ModifyHorseSpeed(Speed);
                }

                var weights = _characterSettings.FilterMount;
                value = (
                    ChargeDamage * weights.ChargeDamage +
                    HitPoints * weights.HitPoints +
                    Maneuver * weights.Maneuver +
                    Speed * weights.Speed
                ) / sum;

                if (SettingsLoader.Debug)
                {
                    InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: CD {1}, HP {2}, MR {3}, SD {4}",
                        sourceItem.Item.Name, ChargeDamage, HitPoints, Maneuver, Speed)));

                    InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
                }

                return value;
            }

            return value;
        }



        private static float WeaponIndexCalculation(SPItemVM item1, int slotNumber)
        {
            if (item1 == null ||
                item1.ItemRosterElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon == null)
                return -9999f;

            WeaponComponentData primaryWeaponItem1 = item1.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon;
            FilterWeaponSettings filterWeapon = _characterSettings.FilterWeapon[slotNumber];
            float sum =
                Math.Abs(filterWeapon.Accuracy) +
                Math.Abs(filterWeapon.WeaponBodyArmor) +
                Math.Abs(filterWeapon.Handling) +
                Math.Abs(filterWeapon.MaxDataValue) +
                Math.Abs(filterWeapon.MissileSpeed) +
                Math.Abs(filterWeapon.SwingDamage) +
                Math.Abs(filterWeapon.SwingSpeed) +
                Math.Abs(filterWeapon.ThrustDamage) +
                Math.Abs(filterWeapon.ThrustSpeed) +
                Math.Abs(filterWeapon.WeaponLength) +
                Math.Abs(filterWeapon.WeaponWeight);

            int Accuracy = primaryWeaponItem1.Accuracy,
                BodyArmor = primaryWeaponItem1.BodyArmor,
                Handling = primaryWeaponItem1.Handling,
                MaxDataValue = primaryWeaponItem1.MaxDataValue,
                MissileSpeed = primaryWeaponItem1.MissileSpeed,
                SwingDamage = primaryWeaponItem1.SwingDamage,
                SwingSpeed = primaryWeaponItem1.SwingSpeed,
                ThrustDamage = primaryWeaponItem1.ThrustDamage,
                ThrustSpeed = primaryWeaponItem1.ThrustSpeed,
                WeaponLength = primaryWeaponItem1.WeaponLength;
            float WeaponWeight = item1.ItemRosterElement.EquipmentElement.Weight;

            ItemModifier mod = item1.ItemRosterElement.EquipmentElement.ItemModifier;
            if (mod != null) {
                BodyArmor = mod.ModifyArmor(BodyArmor);
                MissileSpeed = mod.ModifyMissileSpeed(MissileSpeed);
                SwingDamage = mod.ModifyDamage(SwingDamage);
                SwingSpeed = mod.ModifySpeed(SwingSpeed);
                ThrustDamage = mod.ModifyDamage(ThrustDamage);
                ThrustSpeed = mod.ModifySpeed(ThrustSpeed);
                MaxDataValue += mod.HitPoints;
                //WeaponWeight *= mod.WeightMultiplier;

            }

            var weights = _characterSettings.FilterWeapon[slotNumber];
            float value = (
                Accuracy * weights.Accuracy +
                BodyArmor * weights.WeaponBodyArmor +
                Handling * weights.Handling +
                MaxDataValue * weights.MaxDataValue +
                MissileSpeed * weights.MissileSpeed +
                SwingDamage * weights.SwingDamage +
                SwingSpeed * weights.SwingSpeed +
                ThrustDamage * weights.ThrustDamage +
                ThrustSpeed * weights.ThrustSpeed +
                WeaponLength * weights.WeaponLength +
                WeaponWeight * weights.WeaponWeight
            ) / sum;

            if (SettingsLoader.Debug)
            {
                InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: Acc {1}, BA {2}, HL {3}, HP {4}, MS {5}, SD {6}, SS {7}, TD {8}, TS {9}, WL {10}, W {11}",
                    item1.ItemDescription, Accuracy, BodyArmor, Handling, MaxDataValue, MissileSpeed, SwingDamage, SwingSpeed, ThrustDamage, ThrustSpeed, WeaponLength, WeaponWeight)));

                InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
            }

            return value;
        }

        private static float ArmorIndexCalculation(SPItemVM item1, int slotNumber)
        {
            if (item1 == null ||
                item1.ItemRosterElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.Item.ArmorComponent == null)
                return -9999f;

            ArmorComponent armorComponentItem1 = item1.ItemRosterElement.EquipmentElement.Item.ArmorComponent;
            FilterArmorSettings filterArmor = _characterSettings.FilterArmor[slotNumber];

            float sum =
                Math.Abs(filterArmor.HeadArmor) +
                Math.Abs(filterArmor.ArmArmor) +
                Math.Abs(filterArmor.ArmorBodyArmor) +
                Math.Abs(filterArmor.ArmorWeight) +
                Math.Abs(filterArmor.LegArmor);

            ItemModifier mod =
                item1.ItemRosterElement.EquipmentElement.ItemModifier;

            int HeadArmor = armorComponentItem1.HeadArmor,
                BodyArmor = armorComponentItem1.BodyArmor,
                LegArmor = armorComponentItem1.LegArmor,
                ArmArmor = armorComponentItem1.ArmArmor;
            float Weight = item1.ItemRosterElement.EquipmentElement.Weight;

            if (mod != null) {
                HeadArmor = mod.ModifyArmor(HeadArmor);
                BodyArmor = mod.ModifyArmor(BodyArmor);
                LegArmor = mod.ModifyArmor(LegArmor);
                ArmArmor = mod.ModifyArmor(ArmArmor);
                //Weight *= mod.WeightMultiplier;
            }

            float value = (
                HeadArmor * filterArmor.HeadArmor +
                BodyArmor * filterArmor.ArmorBodyArmor +
                LegArmor * filterArmor.LegArmor +
                ArmArmor * filterArmor.ArmArmor +
                Weight * filterArmor.ArmorWeight
            ) / sum;

            if (SettingsLoader.Debug)
            {
                InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: HA {1}, BA {2}, LA {3}, AA {4}, W {5}",
                    item1.ItemDescription, HeadArmor, BodyArmor, LegArmor, ArmArmor, Weight)));

                InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
            }

            return value;
        }

        private static float MountIndexCalculation(SPItemVM item1)
        {
            if (item1 == null ||
                item1.ItemRosterElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.Item.HorseComponent == null)
                return -9999f;

            HorseComponent horseComponentItem1 = item1.ItemRosterElement.EquipmentElement.Item.HorseComponent;
            FilterMountSettings filterMount = _characterSettings.FilterMount;

            float sum =
                Math.Abs(filterMount.ChargeDamage) +
                Math.Abs(filterMount.HitPoints) +
                Math.Abs(filterMount.Maneuver) +
                Math.Abs(filterMount.Speed);

            int ChargeDamage = horseComponentItem1.ChargeDamage,
                HitPoints = horseComponentItem1.HitPoints,
                Maneuver = horseComponentItem1.Maneuver,
                Speed = horseComponentItem1.Speed;

            ItemModifier mod =
                item1.ItemRosterElement.EquipmentElement.ItemModifier;
            if (mod != null) {
                ChargeDamage = mod.ModifyHorseCharge(ChargeDamage);
                Maneuver = mod.ModifyHorseManuever(Maneuver);
                Speed = mod.ModifyHorseSpeed(Speed);
            }

            var weights = _characterSettings.FilterMount;
            float value = (
                ChargeDamage * weights.ChargeDamage +
                HitPoints * weights.HitPoints +
                Maneuver * weights.Maneuver +
                Speed * weights.Speed
            ) / sum;

            if (SettingsLoader.Debug)
            {
                InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: CD {1}, HP {2}, MR {3}, SD {4}",
                    item1.ItemDescription, ChargeDamage, HitPoints, Maneuver, Speed)));

                InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
            }

            return value;
        }

        public static void UpdateValues()
        {
            FindBestItems();
            if (SettingsLoader.Debug)
                InformationManager.DisplayMessage(new InformationMessage("EBIViewModel UpdateValue() " + Game.Current.ApplicationTime.ToString()));
        }

        public static void EquipBestHelm()
        {
            _inventory.Call("ProcessEquipItem", BestHelm);
            BestHelm = null;
            //this.RefreshValues();
        }

        public static void EquipBestCloak()
        {
            _inventory.Call("ProcessEquipItem", BestCloak);
            BestCloak = null;
            //this.RefreshValues();
        }

        public static void EquipBestArmor()
        {
            _inventory.Call("ProcessEquipItem", BestArmor);
            BestArmor = null;
            //this.RefreshValues();
        }

        public static void EquipBestGlove()
        {
            _inventory.Call("ProcessEquipItem", BestGlove);
            BestGlove = null;
            //this.RefreshValues();
        }
        public static void EquipBestBoot()
        {
            _inventory.Call("ProcessEquipItem", BestBoot);
            BestBoot = null;
            //this.RefreshValues();
        }

        public static void EquipBestMount()
        {
            _inventory.Call("ProcessEquipItem", BestMount);
            BestMount = null;
            //this.RefreshValues();
        }
        public static void EquipBestHarness()
        {
            _inventory.Call("ProcessEquipItem", BestHarness);
            BestHarness = null;
            //this.RefreshValues();
        }

        public static void EquipBestWeapon1()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon1Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon1);
            BestWeapon1 = null;
            UpdateValues();
        }
        public static void EquipBestWeapon2()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon2Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon2);
            BestWeapon2 = null;
            UpdateValues();
        }

        public static void EquipBestWeapon3()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon3Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon3);
            BestWeapon3 = null;
            UpdateValues();
        }
        public static void EquipBestWeapon4()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon4Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon4);
            BestWeapon4 = null;
            UpdateValues();
        }

        public static bool IsAllBestItemsNull()
        {
            if (BestHelm != null)
            {
                return false;
            }
            if (BestCloak != null)
            {
                return false;
            }
            if (BestArmor != null)
            {
                return false;
            }
            if (BestGlove != null)
            {
                return false;
            }
            if (BestBoot != null)
            {
                return false;
            }
            if (BestMount != null)
            {
                return false;
            }
            if (BestHarness != null)
            {
                return false;
            }
            if (BestWeapon1 != null)
            {
                return false;
            }
            if (BestWeapon2 != null)
            {
                return false;
            }
            if (BestWeapon3 != null)
            {
                return false;
            }
            if (BestWeapon4 != null)
            {
                return false;
            }
            return true;
        }

        public static void EquipAll()
        {
            if (BestHelm != null)
            {
                EquipBestHelm();
                return;
            }
            if (BestCloak != null)
            {
                EquipBestCloak();
                return;
            }
            if (BestArmor != null)
            {
                EquipBestArmor();
                return;
            }
            if (BestGlove != null)
            {
                EquipBestGlove();
                return;
            }
            if (BestBoot != null)
            {
                EquipBestBoot();
                return;
            }
            if (BestMount != null)
            {
                EquipBestMount();
                return;
            }
            if (BestHarness != null)
            {
                EquipBestHarness();
                return;
            }
            if (BestWeapon1 != null)
            {
                EquipBestWeapon1();
                return;
            }
            if (BestWeapon2 != null)
            {
                EquipBestWeapon2();
                return;
            }
            if (BestWeapon3 != null)
            {
                EquipBestWeapon3();
                return;
            }
            if (BestWeapon4 != null)
            {
                EquipBestWeapon4();
                return;
            }
        }
    }
}
