using EquipBestItem.Settings;
using SandBox.GauntletUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static bool IsEquipAllButtonEnabled { get; set; }
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
            //ItemUsageListUpdate();
        }

        //private void ItemUsageListUpdate()
        //{
        //    string itemUsage;

        //    if (_inventory.CharacterWeapon1Slot != null)
        //    {
        //        itemUsage = GetItemUsage(_inventory.CharacterWeapon1Slot);
        //        if (!ItemUsageList.Contains(itemUsage) && itemUsage != "")
        //            ItemUsageList.Add(itemUsage);
        //    }
        //    if (_inventory.CharacterWeapon2Slot != null)
        //    {
        //        itemUsage = GetItemUsage(_inventory.CharacterWeapon2Slot);
        //        if (!ItemUsageList.Contains(itemUsage) && itemUsage != "")
        //            ItemUsageList.Add(itemUsage);
        //    }
        //    if (_inventory.CharacterWeapon3Slot != null)
        //    {
        //        itemUsage = GetItemUsage(_inventory.CharacterWeapon3Slot);
        //        if (!ItemUsageList.Contains(itemUsage) && itemUsage != "")
        //            ItemUsageList.Add(itemUsage);
        //    }
        //    if (_inventory.CharacterWeapon4Slot != null)
        //    {
        //        itemUsage = GetItemUsage(_inventory.CharacterWeapon4Slot);
        //        if (!ItemUsageList.Contains(itemUsage) && itemUsage != "")
        //            ItemUsageList.Add(itemUsage);
        //    }

        //    foreach (SPItemVM item in _inventory.RightItemListVM)
        //    {
        //        itemUsage = GetItemUsage(item);
        //        if (!ItemUsageList.Contains(itemUsage) && itemUsage != "")
        //            ItemUsageList.Add(itemUsage);
        //    }
        //    foreach (SPItemVM item in _inventory.LeftItemListVM)
        //    {
        //        itemUsage = GetItemUsage(item);
        //        if (!ItemUsageList.Contains(itemUsage) && itemUsage != "")
        //            ItemUsageList.Add(itemUsage);
        //    }
        //}

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
                                            ArmorIndexCalculation(item, 0) > ArmorIndexCalculation(BestHelm, 0))
                                        {
                                            BestHelm = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.Cape:
                                    {
                                        if (ArmorIndexCalculation(item, 1) > ArmorIndexCalculation(_inventory.CharacterCloakSlot, 1) &&
                                            ArmorIndexCalculation(item, 1) > ArmorIndexCalculation(BestCloak, 1))
                                        {
                                            BestCloak = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.BodyArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 2) > ArmorIndexCalculation(_inventory.CharacterTorsoSlot, 2) &&
                                            ArmorIndexCalculation(item, 2) > ArmorIndexCalculation(BestArmor, 2))
                                        {
                                            BestArmor = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HandArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 3) > ArmorIndexCalculation(_inventory.CharacterGloveSlot, 3) &&
                                            ArmorIndexCalculation(item, 3) > ArmorIndexCalculation(BestGlove, 3))
                                        {
                                            BestGlove = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.LegArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 4) > ArmorIndexCalculation(_inventory.CharacterBootSlot, 4) &&
                                            ArmorIndexCalculation(item, 4) > ArmorIndexCalculation(BestBoot, 4))
                                        {
                                            BestBoot = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HorseHarness:
                                    {
                                        if (ArmorIndexCalculation(item, 5) > ArmorIndexCalculation(_inventory.CharacterMountArmorSlot, 5) &&
                                            ArmorIndexCalculation(item, 5) > ArmorIndexCalculation(BestHarness, 5))
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
                                        WeaponIndexCalculation(item, 0) > WeaponIndexCalculation(BestWeapon1, 0))
                                        {
                                            BestWeapon1 = item;
                                        }
                            if (_inventory.CharacterWeapon2Slot != null && !_inventory.CharacterWeapon2Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon2Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon2Slot))
                                    if (WeaponIndexCalculation(item, 1) > WeaponIndexCalculation(_inventory.CharacterWeapon2Slot, 1) && 
                                        WeaponIndexCalculation(item, 1) > WeaponIndexCalculation(BestWeapon2, 1))
                                    {
                                        BestWeapon2 = item;
                                    }
                            if (_inventory.CharacterWeapon3Slot != null && !_inventory.CharacterWeapon3Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon3Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon3Slot))
                                    if (WeaponIndexCalculation(item, 2) > WeaponIndexCalculation(_inventory.CharacterWeapon3Slot, 2) &&
                                        WeaponIndexCalculation(item, 2) > WeaponIndexCalculation(BestWeapon3, 2))
                                    {
                                        BestWeapon3 = item;
                                    }
                            if (_inventory.CharacterWeapon4Slot != null && !_inventory.CharacterWeapon4Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon4Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon4Slot))
                                    if (WeaponIndexCalculation(item, 3) > WeaponIndexCalculation(_inventory.CharacterWeapon4Slot, 3) &&
                                        WeaponIndexCalculation(item, 3) > WeaponIndexCalculation(BestWeapon4, 3))
                                    {
                                        BestWeapon4 = item;
                                    }
                        }
                        if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent != null)
                        {

                            if (MountIndexCalculation(item) > MountIndexCalculation(_inventory.CharacterMountSlot) &&
                                MountIndexCalculation(item) > MountIndexCalculation(BestMount))
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
                                            ArmorIndexCalculation(item, 0) > ArmorIndexCalculation(BestHelm, 0))
                                        {
                                            BestHelm = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.Cape:
                                    {
                                        if (ArmorIndexCalculation(item, 1) > ArmorIndexCalculation(_inventory.CharacterCloakSlot, 1) &&
                                            ArmorIndexCalculation(item, 1) > ArmorIndexCalculation(BestCloak, 1))
                                        {
                                            BestCloak = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.BodyArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 2) > ArmorIndexCalculation(_inventory.CharacterTorsoSlot, 2) &&
                                            ArmorIndexCalculation(item, 2) > ArmorIndexCalculation(BestArmor, 2))
                                        {
                                            BestArmor = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HandArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 3) > ArmorIndexCalculation(_inventory.CharacterGloveSlot, 3) &&
                                            ArmorIndexCalculation(item, 3) > ArmorIndexCalculation(BestGlove, 3))
                                        {
                                            BestGlove = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.LegArmor:
                                    {
                                        if (ArmorIndexCalculation(item, 4) > ArmorIndexCalculation(_inventory.CharacterBootSlot, 4) &&
                                            ArmorIndexCalculation(item, 4) > ArmorIndexCalculation(BestBoot, 4))
                                        {
                                            BestBoot = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HorseHarness:
                                    {
                                        if (ArmorIndexCalculation(item, 5) > ArmorIndexCalculation(_inventory.CharacterMountArmorSlot, 5) &&
                                            ArmorIndexCalculation(item, 5) > ArmorIndexCalculation(BestHarness, 5))
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
                                        WeaponIndexCalculation(item, 0) > WeaponIndexCalculation(BestWeapon1, 0))
                                    {
                                        BestWeapon1 = item;
                                    }
                            if (_inventory.CharacterWeapon2Slot != null && !_inventory.CharacterWeapon2Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon2Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon2Slot))
                                    if (WeaponIndexCalculation(item, 1) > WeaponIndexCalculation(_inventory.CharacterWeapon2Slot, 1) &&
                                        WeaponIndexCalculation(item, 1) > WeaponIndexCalculation(BestWeapon2, 1))
                                    {
                                        BestWeapon2 = item;
                                    }
                            if (_inventory.CharacterWeapon3Slot != null && !_inventory.CharacterWeapon3Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon3Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon3Slot))
                                    if (WeaponIndexCalculation(item, 2) > WeaponIndexCalculation(_inventory.CharacterWeapon3Slot, 2) &&
                                        WeaponIndexCalculation(item, 2) > WeaponIndexCalculation(BestWeapon3, 2))
                                    {
                                        BestWeapon3 = item;
                                    }
                            if (_inventory.CharacterWeapon4Slot != null && !_inventory.CharacterWeapon4Slot.ItemRosterElement.IsEmpty)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon4Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                        GetItemUsage(item) == GetItemUsage(_inventory.CharacterWeapon4Slot))
                                    if (WeaponIndexCalculation(item, 3) > WeaponIndexCalculation(_inventory.CharacterWeapon4Slot, 3) &&
                                        WeaponIndexCalculation(item, 3) > WeaponIndexCalculation(BestWeapon4, 3))
                                    {
                                        BestWeapon4 = item;
                                    }
                        }
                        if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent != null)
                        {

                            if (MountIndexCalculation(item) > MountIndexCalculation(_inventory.CharacterMountSlot) &&
                                MountIndexCalculation(item) > MountIndexCalculation(BestMount))
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
            IsEquipAllButtonEnabled = false;

            if (BestHelm != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsHelmButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsHelmButtonEnabled = false;
            }

            if (BestCloak != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsCloakButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsCloakButtonEnabled = false;
            }

            if (BestArmor != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsArmorButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsArmorButtonEnabled = false;
            }

            if (BestGlove != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsGloveButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsGloveButtonEnabled = false;
            }

            if (BestBoot != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsBootButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsBootButtonEnabled = false;
            }

            if (BestMount != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsMountButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
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
                    if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                        IsEquipAllButtonEnabled = true;
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
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsWeapon1ButtonEnabled = false;
            }

            if (BestWeapon2 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsWeapon2ButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsWeapon2ButtonEnabled = false;
            }

            if (BestWeapon3 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsWeapon3ButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsWeapon3ButtonEnabled = false;
            }

            if (BestWeapon4 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsWeapon4ButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
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

        //private static float GetMountEffectiveness(SPItemVM item)
        //{

        //    float value = 0f;

        //    if (item != null && item.ItemRosterElement.EquipmentElement.Item != null)
        //    {
        //        if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent != null)
        //        {
        //            if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Monster.MonsterUsage != "camel" && item.ItemDescription != "Mule")
        //                value = (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Maneuver *
        //                    (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Speed +
        //                    (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.ChargeDamage +
        //                    (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.HitPoints * 0.2f;
        //            else
        //                value = ((float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Maneuver *
        //                    (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Speed +
        //                    (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.ChargeDamage +
        //                    (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.HitPoints) * 0.0001f;
        //        }
        //    }

        //    return value;
        //}

        public static void UpdateCurrentCharacterName()
        {
            if (_inventory.CurrentCharacterName != null)
                CurrentCharacterName = _inventory.CurrentCharacterName;
            else
                CurrentCharacterName = Hero.MainHero.Name.ToString();
        }

        //private static float GetArmorEffectiveness(SPItemVM item)
        //{
        //    int value = 0;

        //    if (item != null && item.ItemRosterElement.EquipmentElement.Item != null)
        //    {
        //        value =
        //            item.ItemRosterElement.EquipmentElement.GetArmArmor() +
        //            item.ItemRosterElement.EquipmentElement.GetLegArmor() +
        //            item.ItemRosterElement.EquipmentElement.GetHeadArmor() +
        //            item.ItemRosterElement.EquipmentElement.GetBodyArmorHuman() +
        //            item.ItemRosterElement.EquipmentElement.GetBodyArmorHorse();
        //    }

        //    return value;
        //}


        //private static float WeaponIndexCalculation(SPItemVM item1, SPItemVM item2, int slotNumber)
        //{
        //    if (item1 == null || 
        //        item2 == null || 
        //        item1.ItemRosterElement.IsEmpty ||
        //        item2.ItemRosterElement.IsEmpty ||
        //        item1.ItemRosterElement.EquipmentElement.IsEmpty ||
        //        item2.ItemRosterElement.EquipmentElement.IsEmpty ||
        //        item1.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon == null ||
        //        item2.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon == null)
        //        return 0;

        //    WeaponComponentData primaryWeaponItem1 = item1.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon;
        //    WeaponComponentData primaryWeaponItem2 = item2.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon;

        //    float Accuracy = ((float)primaryWeaponItem1.Accuracy / (float)primaryWeaponItem2.Accuracy) * 100f * (float)_characterSettings.FilterWeapon[slotNumber].Accuracy;
        //    if (float.IsNaN(Accuracy)) Accuracy = 0f;
        //    float BodyArmor = ((float)primaryWeaponItem1.BodyArmor / (float)primaryWeaponItem2.BodyArmor) * 100f * (float)_characterSettings.FilterWeapon[slotNumber].WeaponBodyArmor;
        //    if (float.IsNaN(BodyArmor)) BodyArmor = 0f;
        //    float Handling = ((float)primaryWeaponItem1.Handling / (float)primaryWeaponItem2.Handling) * 100f * (float)_characterSettings.FilterWeapon[slotNumber].Handling;
        //    if (float.IsNaN(Handling)) Handling = 0f;
        //    float MaxDataValue = ((float)primaryWeaponItem1.MaxDataValue / (float)primaryWeaponItem2.MaxDataValue) * 100f * (float)_characterSettings.FilterWeapon[slotNumber].MaxDataValue;
        //    if (float.IsNaN(MaxDataValue)) MaxDataValue = 0f;
        //    float MissileSpeed = ((float)primaryWeaponItem1.MissileSpeed / (float)primaryWeaponItem2.MissileSpeed) * 100f * (float)_characterSettings.FilterWeapon[slotNumber].MissileSpeed;
        //    if (float.IsNaN(MissileSpeed)) MissileSpeed = 0f;
        //    float SwingDamage = ((float)primaryWeaponItem1.SwingDamage / (float)primaryWeaponItem2.SwingDamage) * 100f * (float)_characterSettings.FilterWeapon[slotNumber].SwingDamage;
        //    if (float.IsNaN(SwingDamage)) SwingDamage = 0f;
        //    float SwingSpeed = ((float)primaryWeaponItem1.SwingSpeed / (float)primaryWeaponItem2.SwingSpeed) * 100f * (float)_characterSettings.FilterWeapon[slotNumber].SwingSpeed;
        //    if (float.IsNaN(SwingSpeed)) SwingSpeed = 0f;
        //    float ThrustDamage = ((float)primaryWeaponItem1.ThrustDamage / (float)primaryWeaponItem2.ThrustDamage) * 100f * (float)_characterSettings.FilterWeapon[slotNumber].ThrustDamage;
        //    if (float.IsNaN(ThrustDamage)) ThrustDamage = 0f;
        //    float ThrustSpeed = ((float)primaryWeaponItem1.ThrustSpeed / (float)primaryWeaponItem2.ThrustSpeed) * 100f * (float)_characterSettings.FilterWeapon[slotNumber].ThrustSpeed;
        //    if (float.IsNaN(ThrustSpeed)) ThrustSpeed = 0f;
        //    float WeaponLength = ((float)primaryWeaponItem1.WeaponLength / (float)primaryWeaponItem2.WeaponLength) * 100f * (float)_characterSettings.FilterWeapon[slotNumber].WeaponLength;
        //    if (float.IsNaN(WeaponLength)) WeaponLength = 0f;
        //    float Weight = (item1.ItemRosterElement.EquipmentElement.Weight / item2.ItemRosterElement.EquipmentElement.Weight) * 100f * _characterSettings.FilterWeapon[slotNumber].WeaponWeight;
        //    if (float.IsNaN(Weight)) Weight = 0f;

        //    float value = Accuracy + BodyArmor + Handling + MaxDataValue + MissileSpeed + SwingDamage + SwingSpeed + ThrustDamage + ThrustSpeed + WeaponLength + Weight;
        //    return value;
        //}

        //private static float CompetitiveArmorIndexCalculation(SPItemVM item1, SPItemVM item2, int slotNumber)
        //{
        //    if (item1 == null ||
        //        item2 == null ||
        //        item1.ItemRosterElement.IsEmpty ||
        //        item2.ItemRosterElement.IsEmpty ||
        //        item1.ItemRosterElement.EquipmentElement.IsEmpty ||
        //        item2.ItemRosterElement.EquipmentElement.IsEmpty ||
        //        item1.ItemRosterElement.EquipmentElement.Item.ArmorComponent == null ||
        //        item2.ItemRosterElement.EquipmentElement.Item.ArmorComponent == null)
        //        return 0;

        //    ArmorComponent armorComponentItem1 = item1.ItemRosterElement.EquipmentElement.Item.ArmorComponent;
        //    ArmorComponent armorComponentItem2 = item2.ItemRosterElement.EquipmentElement.Item.ArmorComponent;
        //    FilterArmorSettings filterArmor = _characterSettings.FilterArmor[slotNumber];

        //    float summ = 
        //        Math.Abs(filterArmor.HeadArmor) + 
        //        Math.Abs(filterArmor.ArmArmor) + 
        //        Math.Abs(filterArmor.ArmorBodyArmor) +
        //        Math.Abs(filterArmor.ArmorWeight) +
        //        Math.Abs(filterArmor.ChargeBonus) +
        //        Math.Abs(filterArmor.LegArmor) +
        //        Math.Abs(filterArmor.ManeuverBonus) +
        //        Math.Abs(filterArmor.SpeedBonus);





        //    float HeadArmor = ((float)armorComponentItem1.HeadArmor / (float)armorComponentItem2.HeadArmor) * 100f * (_characterSettings.FilterArmor[slotNumber].HeadArmor / summ);
        //    if (float.IsNaN(HeadArmor)) HeadArmor = 0f;
        //    float BodyArmor = ((float)armorComponentItem1.BodyArmor / (float)armorComponentItem2.BodyArmor) * 100f * (_characterSettings.FilterArmor[slotNumber].ArmorBodyArmor / summ);
        //    if (float.IsNaN(BodyArmor)) BodyArmor = 0f;
        //    float LegArmor = ((float)armorComponentItem1.LegArmor / (float)armorComponentItem2.LegArmor) * 100f * (_characterSettings.FilterArmor[slotNumber].LegArmor / summ);
        //    if (float.IsNaN(LegArmor)) LegArmor = 0f;
        //    float ArmArmor = ((float)armorComponentItem1.ArmArmor / (float)armorComponentItem2.ArmArmor) * 100f * (_characterSettings.FilterArmor[slotNumber].ArmArmor / summ);
        //    if (float.IsNaN(ArmArmor)) ArmArmor = 0f;
        //    float ManeuverBonus = ((float)armorComponentItem1.ManeuverBonus / (float)armorComponentItem2.ManeuverBonus) * 100f * (_characterSettings.FilterArmor[slotNumber].ManeuverBonus / summ);
        //    if (float.IsNaN(ManeuverBonus)) ManeuverBonus = 0f;
        //    float SpeedBonus = ((float)armorComponentItem1.SpeedBonus / (float)armorComponentItem2.SpeedBonus) * 100f * (_characterSettings.FilterArmor[slotNumber].SpeedBonus / summ);
        //    if (float.IsNaN(SpeedBonus)) SpeedBonus = 0f;
        //    float ChargeBonus = ((float)armorComponentItem1.ChargeBonus / (float)armorComponentItem2.ChargeBonus) * 100f * (_characterSettings.FilterArmor[slotNumber].ChargeBonus / summ);
        //    if (float.IsNaN(ChargeBonus)) ChargeBonus = 0f;
        //    float Weight = (item1.ItemRosterElement.EquipmentElement.Weight / item2.ItemRosterElement.EquipmentElement.Weight) * 100f * (_characterSettings.FilterArmor[slotNumber].ArmorWeight / summ);
        //    if (float.IsNaN(Weight)) Weight = 0f;

        //    float value = HeadArmor + BodyArmor + LegArmor + ArmArmor + ManeuverBonus + SpeedBonus + ChargeBonus + Weight;
        //    return value;
        //}


        //private static float CompetitiveMountIndexCalculation(SPItemVM item1, SPItemVM item2)
        //{
        //    if (item1 == null ||
        //        item2 == null ||
        //        item1.ItemRosterElement.IsEmpty ||
        //        item2.ItemRosterElement.IsEmpty ||
        //        item1.ItemRosterElement.EquipmentElement.IsEmpty ||
        //        item2.ItemRosterElement.EquipmentElement.IsEmpty ||
        //        item1.ItemRosterElement.EquipmentElement.Item.HorseComponent == null ||
        //        item2.ItemRosterElement.EquipmentElement.Item.HorseComponent == null)
        //        return 0;

        //    HorseComponent horseComponentItem1 = item1.ItemRosterElement.EquipmentElement.Item.HorseComponent;
        //    HorseComponent horseComponentItem2 = item2.ItemRosterElement.EquipmentElement.Item.HorseComponent;

        //    float ChargeDamage = ((float)horseComponentItem1.ChargeDamage / (float)horseComponentItem2.ChargeDamage) * 100f * _characterSettings.FilterMount.ChargeDamage;
        //    if (float.IsNaN(ChargeDamage)) ChargeDamage = 0f;
        //    float HitPoints = ((float)horseComponentItem1.HitPoints / (float)horseComponentItem2.HitPoints) * 100f * _characterSettings.FilterMount.HitPoints;
        //    if (float.IsNaN(HitPoints)) HitPoints = 0f;
        //    float Maneuver = ((float)horseComponentItem1.Maneuver / (float)horseComponentItem2.Maneuver) * 100f * _characterSettings.FilterMount.Maneuver;
        //    if (float.IsNaN(Maneuver)) Maneuver = 0f;
        //    float Speed = ((float)horseComponentItem1.Speed / (float)horseComponentItem2.Speed) * 100f * _characterSettings.FilterMount.Speed;
        //    if (float.IsNaN(Speed)) Speed = 0f;

        //    //float Weight = (item1.ItemRosterElement.EquipmentElement.Weight / item2.ItemRosterElement.EquipmentElement.Weight) * 100f * _characterSettings.FilterArmor[slotNumber].ArmorWeight;
        //    //if (float.IsNaN(Weight)) Weight = 0f;

        //    float value = ChargeDamage + HitPoints + Maneuver + Speed;
        //    return value;
        //}

        private static float WeaponIndexCalculation(SPItemVM item1, int slotNumber)
        {
            if (item1 == null ||
                item1.ItemRosterElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon == null)
                return 0;

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
            return value;
        }

        private static float ArmorIndexCalculation(SPItemVM item1, int slotNumber)
        {
            if (item1 == null ||
                item1.ItemRosterElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.Item.ArmorComponent == null)
                return 0;

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
                // It doesn't look like the weight multiplier actually gets applied
                // Weight *= mod.WeightMultiplier;
            }

            float value = (
                HeadArmor * filterArmor.HeadArmor +
                BodyArmor * filterArmor.ArmorBodyArmor +
                LegArmor * filterArmor.LegArmor +
                ArmArmor * filterArmor.ArmArmor +
                Weight * filterArmor.ArmorWeight
            ) / sum;

            return value;
        }

        private static float MountIndexCalculation(SPItemVM item1)
        {
            if (item1 == null ||
                item1.ItemRosterElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.IsEmpty ||
                item1.ItemRosterElement.EquipmentElement.Item.HorseComponent == null)
                return 0;

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

            return value;
        }

        //private static float GetWeaponEffectiveness(SPItemVM item)
        //{
        //    float value = 0f;

        //    if (item != null && item.ItemRosterElement.EquipmentElement.Item != null)
        //    {
        //        value = item.ItemRosterElement.EquipmentElement.Item.Effectiveness;
        //    }

        //    return value;
        //}


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
